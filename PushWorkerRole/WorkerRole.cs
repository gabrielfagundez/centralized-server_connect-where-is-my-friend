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

        //Windows Phone strings in spanish
        public static String WIN_SOLICITATION_STRING1_ESP = "";
        public static String WIN_SOLICITATION_FIRST_STRING2_ESP = "";
        public static String WIN_SOLICITATION_SECOND_STRING2_ESP = " quiere saber dónde estás!";

        public static String WIN_ACCEPTATION_STRING1_ESP = "";
        public static String WIN_ACCEPTATION_FIRST_STRING2_ESP = "Ahora podés saber dónde está ";
        public static String WIN_ACCEPTATION_SECOND_STRING2_ESP = "!";


        //iOS strings

        public static String SOLICITATION_ACCEPTATION = " accepted your solicitation";
        public static String SOLICITATION_ACCEPTATION_ESP = " aceptó tu solicitud";
        // para windows phone van dos textos
        public static String SOLICITATION_ACCEPTATION_BIS = "";
        public static String SOLICITATION_ACCEPTATION_BIS_ESP = "";
        // uri de path para windows phone


        //public static String SOLICITATION_NEGATION = " couldn't accept your solicitation";
        public static String SOLICITATION_ARRIVAL = " wants to know where you are";
        public static String SOLICITATION_ARRIVAL_BIS = "Answer his solicitation!";
        public static String SOLICITATION_ARRIVAL_ESP = " quiere saber dónde estás";
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
                        //pm.GetRejectedEvents().ToList().ForEach(n => SendRejections(n, pm.GetUserForSolicitation(n.WhereSolicitation)));
                        pm.GetUnsentEvents().ToList().ForEach(n => SendSolicitations(n, pm.GetUserFromSolicitation(n.WhereSolicitation)));
                        pm.Save();
                        Thread.Sleep(10000);
                        //Trace.TraceInformation("Working", "Information");
                    }
                    catch (Exception e)
                    {

                        Log.Write("el catch de adentro de run: " + e.StackTrace);
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
                push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyCpS_GsfmIatkSYwWVkrtc3CTIw9hpwCKA"));

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
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            
            foreach (Session s in ls)
            {
                s.Badge++;
                if (s.Platform.Equals("android"))
                {
                    if (s.Language.CompareTo("esp") == 0)
                    {
                        SendAndroid(s.DeviceId, who + SOLICITATION_ACCEPTATION_ESP);
                    }
                    else
                    {
                        SendAndroid(s.DeviceId, who + SOLICITATION_ACCEPTATION);
                    }
                    Log.Write("Enviando aceptacion android a " + who);
                }
                else if (s.Platform.Equals("wp"))
                {
                    if (s.Language.CompareTo("esp") == 0)
                    {
                        SendWp(s.DeviceId, WIN_ACCEPTATION_STRING1_ESP, WIN_ACCEPTATION_FIRST_STRING2_ESP + who + WIN_ACCEPTATION_SECOND_STRING2_ESP, WIN_ACCEPTATION_PATH);
                    }
                    else
                    {
                        SendWp(s.DeviceId, WIN_ACCEPTATION_STRING1, WIN_ACCEPTATION_FIRST_STRING2 + who + WIN_ACCEPTATION_SECOND_STRING2, WIN_ACCEPTATION_PATH);
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
            ev.Sent = true;
        }

        //private void SendRejections(WhereNegationEvent ev, String who)
        //{
        //    List<Session> ls = ev.User.Session.Where(s => s.Active = true)
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
            
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                s.Badge++;
                if (s.Platform.Equals("android"))
                {
                    if (s.Language.CompareTo("esp") == 0)
                    {
                        SendAndroid(s.DeviceId, who + SOLICITATION_ARRIVAL_ESP);
                    }
                    else
                    {
                        SendAndroid(s.DeviceId, who + SOLICITATION_ARRIVAL);
                    }
                    Log.Write("Enviando nueva solicitud Android a " + who);
                }
                else if (s.Platform.Equals("wp"))
                {
                    if (s.Language.CompareTo("esp") == 0)
                    {
                        SendWp(s.DeviceId, WIN_SOLICITATION_STRING1_ESP, WIN_SOLICITATION_FIRST_STRING2_ESP + who + WIN_SOLICITATION_SECOND_STRING2_ESP, WIN_SOLICITATION_PATH);
                    }
                    else
                    {
                        SendWp(s.DeviceId, WIN_SOLICITATION_STRING1, WIN_SOLICITATION_FIRST_STRING2 + who + WIN_SOLICITATION_SECOND_STRING2, WIN_SOLICITATION_PATH);
                    }
                    Log.Write("Enviando nueva solicitud windows a " + who);
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
                    Log.Write("Enviando nueva solicitud ios a " + who);
                }
            }
            ev.Sent = true;
        }

        private void SendAndroid(string devId, string text)
        {
            
            push.QueueNotification(new GcmNotification()    //.WithData(new Dictionary())
                                     .ForDeviceRegistrationId(devId)
                                     .WithJson("{\"alert\":\"" + text + "\"," +
                                                "\"badge\":7,"
                                                + "\"sound\":\"sound.caf\"}"));
            
        }

        private void SendWp(string devId, string text1, string text2, String path)
        {
            Log.Write("Sending to wp to devId " + devId);
            if (!String.IsNullOrEmpty(devId))
            {
                push.QueueNotification(new WindowsPhoneToastNotification()
                        .ForEndpointUri(new Uri(devId))
                        .ForOSVersion(WindowsPhoneDeviceOSVersion.Eight)
                        .WithBatchingInterval(BatchingInterval.Immediate)
                        .WithText1(text1)
                        .WithText2(text2)
                        .WithNavigatePath(path));
            }
        }

        private void SendIos(string devId, string text, Int16 badge)
        {
            Log.Write("Sending to ios to devId " + devId);
            push.QueueNotification(new AppleNotification()
                                       .ForDeviceToken(devId)
                                       .WithAlert(text)
                                       .WithBadge(badge);
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
