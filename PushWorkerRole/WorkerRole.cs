using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

using PushSharp;
using PushSharp.Android;
using PushSharp.Core;
using PushSharp.WindowsPhone;
using PushSharp.Apple;

using PisDataAccess;
using PISServer.Models;

namespace PushWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        //Create our push services broker
        PushBroker push;
        Logger Log;

        //Windows Phone strings
        public static String WIN_ACCEPTATION_PATH = "/LoggedMainPages/Mapa.xaml";
        //public static String WIN_NEGATION_PATH = "";
        public static String WIN_SOLICITATION_PATH = "/LoggedMainPages/Requests.xaml";

        //Windows Phone strings in english
        public static String WIN_SOLICITATION_STRING1 = "";
        public static String WIN_SOLICITATION_FIRST_STRING2 = "";
        public static String WIN_SOLICITATION_SECOND_STRING2 = " wants to know where you are!";
        
        public static String WIN_ACCEPTATION_STRING1 = "";
        public static String WIN_ACCEPTATION_FIRST_STRING2 = "Now you can see where ";
        public static String WIN_ACCEPTATION_SECOND_STRING2 = " is!";

        public static String WIN_BADGE_STRING1 = "";
        public static String WIN_BADGE_FIRST_STRING2 = "You have ";
        public static String WIN_BADGE_SECOND_STRING2 = " pending notifications!";

        //Windows Phone strings in spanish
        public static String WIN_SOLICITATION_STRING1_ESP = "";
        public static String WIN_SOLICITATION_FIRST_STRING2_ESP = "�";
        public static String WIN_SOLICITATION_SECOND_STRING2_ESP = " quiere saber d�nde est�s!";

        public static String WIN_ACCEPTATION_STRING1_ESP = "";
        public static String WIN_ACCEPTATION_FIRST_STRING2_ESP = "�Ahora pod�s saber d�nde est� ";
        public static String WIN_ACCEPTATION_SECOND_STRING2_ESP = "!";

        public static String WIN_BADGE_STRING1_ESP = "";
        public static String WIN_BADGE_FIRST_STRING2_ESP = "�Tienes ";
        public static String WIN_BADGE_SECOND_STRING2_ESP = " notificaciones pendientes!";


        //iOS strings

        public static String SOLICITATION_ACCEPTATION = " accepted your solicitation";
        public static String SOLICITATION_ACCEPTATION_ESP = " acept� tu solicitud";
        // para windows phone van dos textos
        public static String SOLICITATION_ACCEPTATION_BIS = "";
        public static String SOLICITATION_ACCEPTATION_BIS_ESP = "";
        // uri de path para windows phone


        //public static String SOLICITATION_NEGATION = " couldn't accept your solicitation";
        public static String SOLICITATION_ARRIVAL = " wants to know where you are!";
        public static String SOLICITATION_ARRIVAL_BIS = "Answer his solicitation!";
        public static String SOLICITATION_ARRIVAL_ESP = " quiere saber d�nde est�s!";
        public static String SOLICITATION_ARRIVAL_BIS_ESP = "Contesta su solicitud!";

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            //Trace.TraceInformation("PushWorkerRole entry point called", "Information");

            try
            {
                while (true)
                {
                    try 
                    {
                        PushMiddleware pm = new PushMiddleware();
                        pm.GetAcceptedEvents().ToList().ForEach(n => SendAcceptations(n, pm.GetUserForSolicitation(n.WhereSolicitation)));
                        pm.GetUnsentEvents().ToList().ForEach(n => SendSolicitations(n, pm.GetUserFromSolicitation(n.WhereSolicitation)));
                        pm.Save();
                        Thread.Sleep(10000);
                    }
                    catch (Exception e)
                    {
                        Log.Write("el catch de adentro de run: " + e.StackTrace + "y la excepcion esss " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write("el catch de adentro de run: " + e.Message);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            try
            {
                push = new PushBroker();
                Log = new Logger().ForPushBroker(push);

                // REGISTRO ANDROID
                push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBMjOfPupVM7EEBssc1iMcK0Z7Mnuso67Y"));

                // REGISTRO WINPHONE
                push.RegisterWindowsPhoneService();

                // REGISTRO APPLE
                byte[] certificado = new PushMiddleware().GetPermission("ios");
                push.RegisterAppleService(new ApplePushChannelSettings(certificado,
                    "pis2013"));
            }
            catch (Exception e)
            {
                Log.Write("el catch de onstart: " + e.Message);
            }


            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void SendAcceptations(WhereAcceptationEvent ev, String who)
        {
            List<Session> ls = ev.User.Session.Where(s => s.Active == true)
                                .ToList();
            
            foreach (Session s in ls)
            {
                if (s.DeviceId.CompareTo("no push") != 0)
                {
                    s.Badge++;
                    s.BadgeAccept++;
                    if (s.Platform.Equals("android"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            SendAndroid(s.DeviceId, who + SOLICITATION_ACCEPTATION_ESP, (Int16)s.BadgeAccept, "a");
                        }
                        else
                        {
                            SendAndroid(s.DeviceId, who + SOLICITATION_ACCEPTATION, (Int16)s.BadgeAccept, "s");
                        }
                        Log.Write("Enviando aceptacion android a " + who);
                    }
                    else if (s.Platform.Equals("wp"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            if (s.Badge > 1)
                            {
                                SendWp(s.DeviceId, WIN_BADGE_STRING1_ESP, WIN_BADGE_FIRST_STRING2_ESP + s.Badge + WIN_BADGE_SECOND_STRING2_ESP, WIN_ACCEPTATION_PATH, s.Badge);
                            }
                            else
                            {
                                SendWp(s.DeviceId, WIN_ACCEPTATION_STRING1_ESP, WIN_ACCEPTATION_FIRST_STRING2_ESP + who + WIN_ACCEPTATION_SECOND_STRING2_ESP, WIN_ACCEPTATION_PATH, s.Badge);
                            }
                        }
                        else
                        {
                            if (s.Badge > 1)
                            {
                                SendWp(s.DeviceId, WIN_BADGE_STRING1, WIN_BADGE_FIRST_STRING2 + s.Badge + WIN_BADGE_SECOND_STRING2, WIN_ACCEPTATION_PATH, s.Badge);
                            }
                            else
                            {
                                SendWp(s.DeviceId, WIN_ACCEPTATION_STRING1, WIN_ACCEPTATION_FIRST_STRING2 + who + WIN_ACCEPTATION_SECOND_STRING2, WIN_ACCEPTATION_PATH, s.Badge);
                            }
                        }
                        Log.Write("Enviando aceptacion wp a " + who);
                    }
                    else if (s.Platform.Equals("ios"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            SendIos(s.DeviceId, who + SOLICITATION_ACCEPTATION_ESP, s.Badge);
                        }
                        else
                        {
                            SendIos(s.DeviceId, who + SOLICITATION_ACCEPTATION, s.Badge);
                        }
                        Log.Write("Enviando aceptacion ios a " + who);
                    }
                }
            }
            ev.Sent = true;
        }

        //private void SendRejections(WhereNegationEvent ev, String who)
        //{
        //    List<Session> ls = ev.User.Session.Where(s => s.Active == true)
        //                        .ToList();
        //    foreach (Session s in ls)
        //    {
        //        if (s.Platform.Equals("android"))
        //        {
        //            SendAndroid(s.DeviceId, who + SOLICITATION_NEGATION);
        //            Log.Write("Envando rejection android a " + who);
        //        }
        //        else if (s.Platform.Equals("wp"))
        //        {
        //            SendWp(s.DeviceId,"", "REJECTED", WIN_NEGATION_PATH);
        //            Log.Write("Envando rejection wp a " + who);
        //        }
        //        else if (s.Platform.Equals("ios"))
        //        {
        //            SendIos(s.DeviceId, who + SOLICITATION_ARRIVAL);
        //            Log.Write("Envando rejection ios a " + who);
        //        }
        //    }
        //    ev.Sent = true;
        //}

        private void SendSolicitations(WhereSolicitationEvent ev, String who)
        {
            
            List<Session> ls = ev.User.Session.Where(s => s.Active == true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.DeviceId.CompareTo("no push") != 0)
                {

                    s.Badge++;
                    s.BadgeSolicitation++;
                    if (s.Platform.Equals("android"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            SendAndroid(s.DeviceId, who + SOLICITATION_ARRIVAL_ESP, (Int16)s.BadgeSolicitation, "s");
                        }
                        else
                        {
                            SendAndroid(s.DeviceId, who + SOLICITATION_ARRIVAL, (Int16)s.BadgeSolicitation, "s");
                        }
                        Log.Write("Enviando nueva solicitud Android de " + who);
                    }
                    else if (s.Platform.Equals("wp"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            if (s.Badge > 1)
                            {
                                SendWp(s.DeviceId, WIN_BADGE_STRING1_ESP, WIN_BADGE_FIRST_STRING2_ESP + s.Badge + WIN_BADGE_SECOND_STRING2_ESP, WIN_SOLICITATION_PATH, s.Badge);
                            }
                            else
                            {
                                SendWp(s.DeviceId, WIN_SOLICITATION_STRING1_ESP, WIN_SOLICITATION_FIRST_STRING2_ESP + who + WIN_SOLICITATION_SECOND_STRING2_ESP, WIN_SOLICITATION_PATH, s.Badge);
                            }
                        }
                        else
                        {
                            if (s.Badge > 1)
                            {
                                SendWp(s.DeviceId, WIN_BADGE_STRING1, WIN_BADGE_FIRST_STRING2 + s.Badge + WIN_BADGE_SECOND_STRING2, WIN_SOLICITATION_PATH, s.Badge);
                            }
                            else
                            {
                                SendWp(s.DeviceId, WIN_SOLICITATION_STRING1, WIN_SOLICITATION_FIRST_STRING2 + who + WIN_SOLICITATION_SECOND_STRING2, WIN_SOLICITATION_PATH, s.Badge);
                            }
                        }
                        Log.Write("Enviando nueva solicitud windows de " + who);
                    }
                    else if (s.Platform.Equals("ios"))
                    {
                        if (s.Language.CompareTo("esp") == 0)
                        {
                            SendIos(s.DeviceId, who + SOLICITATION_ARRIVAL_ESP, s.Badge);
                        }
                        else
                        {
                            SendIos(s.DeviceId, who + SOLICITATION_ARRIVAL, s.Badge);
                        }
                        Log.Write("Enviando nueva solicitud ios de " + who);

                    }
                }
            }
            ev.Sent = true;
        }

        private void SendAndroid(string devId, string text, Int16 badge, string type)
        {
            
            push.QueueNotification(new GcmNotification()    //.WithData(new Dictionary())
                                     .ForDeviceRegistrationId(devId)
                                     .WithJson("{\"alert\":\"" + text + "\"," +
                                                "\"badge\": " + (Int16)badge + ", " +
                                                "\"type\": \"" + type + "\"}"));
            
        }

        private void SendWp(string devId, string text1, string text2, String path, int badge)
        {
            Log.Write("Sending to wp to devId " + devId);
            if (!String.IsNullOrEmpty(devId))
            {
                Uri u = new Uri(devId);
                push.QueueNotification(new WindowsPhoneToastNotification()
                        .ForEndpointUri(u)
                        .ForOSVersion(WindowsPhoneDeviceOSVersion.Eight)
                        .WithBatchingInterval(BatchingInterval.Immediate)
                        .WithText1(text1)
                        .WithText2(text2)
                        .WithNavigatePath(path));
            }
        }

        private void SendIos(string devId, string text, Int16 badge)
        {
            Log.Write("Sending to ios to devId " + devId + "con badge " + badge.ToString());
            AppleNotificationAlert ana = new AppleNotificationAlert(){
                Body = text
            };
            AppleNotificationPayload anp = new AppleNotificationPayload(){
                    Badge = badge,
                    Sound = "default",
                    Alert = ana
            };
            push.QueueNotification(new AppleNotification()
                                       .ForDeviceToken(devId)
                //.WithAlert(text)
                //.WithBadge(badge)
                                       .WithPayload(anp));
                                       //.WithSound("default"));
        }

        private class Logger
        {
            
            //private DataRepository<MensajeLog> logger;
            //public Logger()
            //{
            //    this.logger = new DataRepository<MensajeLog>(new DevelopmentPISEntities());
            //}
            public void Write(String mensaje)
            {
                PushMiddleware pm = new PushMiddleware();
                pm.Log(mensaje);
            }

            public Logger ForPushBroker(PushBroker push)
            {
                push.OnNotificationSent += (s, n) => this.Write("Sent: " + s + " -> " + n);
                push.OnNotificationFailed += (s, n, nfe) => this.Write("Failure: " + s + " -> " + nfe.Message + " -> " + n);
                push.OnChannelException += (s, c, e) => this.Write("Channel Exception: " + s + " -> " + e);
                push.OnServiceException += (s, e) => this.Write("Service Exception: " + s + " -> " + e);
                push.OnDeviceSubscriptionExpired += (s, expId, t, not) => this.Write("Device Subscription Expired: " + s + " -> " + expId);
                push.OnChannelDestroyed += s => this.Write("Channel Destroyed for: " + s);
                push.OnChannelCreated += (s, r) => this.Write("Channel Created for: " + s);
                push.OnDeviceSubscriptionChanged += (s, oldId, newId, n) => this.Write("Device Registration Changed:  Old-> " + oldId + "  New-> " + newId + " -> " + n);
                return this;
            }
        }
    }
}
