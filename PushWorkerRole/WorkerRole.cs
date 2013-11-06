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

        public static String PLATAFORMA_PERMISO_IOS = "ios_test";

        public static String SOLICITATION_ACCEPTATION = " accepted your solicitation";
        // para windows phone van dos textos
        public static String SOLICITATION_ACCEPTATION_BIS = "You can see where he is!";
        // uri de path para windows phone
        public static String WIN_ACCEPTATION_PATH = "path";
        public static String WIN_NEGATION_PATH = "path";
        public static String WIN_ARRIVAL_PATH = "path";

        public static String SOLICITATION_NEGATION = " couldn't accept your solicitation";
        public static String SOLICITATION_ARRIVAL = " wants to know where you are";
        public static String SOLICITATION_ARRIVAL_BIS = "Answer his solicitation!";

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            //Trace.TraceInformation("PushWorkerRole entry point called", "Information");

            try
            {
                while (true)
                {
                    PushMiddleware pm = new PushMiddleware();
                    pm.GetAcceptedEvents().ToList().ForEach(n => SendAcceptations(n, pm.GetUserForSolicitation(n.WhereSolicitation)));
                    pm.GetRejectedEvents().ToList().ForEach(n => SendRejections(n, pm.GetUserForSolicitation(n.WhereSolicitation)));
                    pm.GetUnsentEvents().ToList().ForEach(n => SendSolicitations(n, pm.GetUserFromSolicitation(n.WhereSolicitation)));
                    pm.Save();
                    Thread.Sleep(10000);
                    //Trace.TraceInformation("Working", "Information");
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            push = new PushBroker();
            Log = new Logger().ForPushBroker(push);

            try
            {
                // REGISTRO ANDROID
                push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyCpS_GsfmIatkSYwWVkrtc3CTIw9hpwCKA"));

                // REGISTRO WINPHONE
                push.RegisterWindowsPhoneService();

                // REGISTRO APPLE
                byte[] certificado = new PushMiddleware().GetPermission(PLATAFORMA_PERMISO_IOS);
                push.RegisterAppleService(new ApplePushChannelSettings(certificado,
                    "pis2013"));
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }


            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void SendAcceptations(WhereAcceptationEvent ev, String who)
        {
            Log.Write("Enviando aceptacion a " + who);
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(s.DeviceId, who + SOLICITATION_ACCEPTATION);
                }
                else if (s.Platform.Equals("wp"))
                {
                    //No se si es ese ev.User.Name el nombre del que acepta pero esa era la idea
                    SendWp(s.DeviceId,
                        who + SOLICITATION_ACCEPTATION,
                        SOLICITATION_ACCEPTATION_BIS, WIN_ACCEPTATION_PATH);
                }
                else if (s.Platform.Equals("ios"))
                {
                    SendIosAceptation(s.DeviceId, who + SOLICITATION_ARRIVAL);
                }
            }
            ev.Sent = true;
        }

        private void SendRejections(WhereNegationEvent ev, String who)
        {
            Log.Write("Envando rejection a " + who);
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(s.DeviceId, who + SOLICITATION_NEGATION);
                }
                else if (s.Platform.Equals("wp"))
                {
                    SendWp(s.DeviceId, who + SOLICITATION_NEGATION, "", WIN_NEGATION_PATH);
                }
                else if (s.Platform.Equals("ios"))
                {
                    SendIosAceptation(s.DeviceId, who + SOLICITATION_ARRIVAL);
                }
            }
            ev.Sent = true;
        }

        private void SendSolicitations(WhereSolicitationEvent ev, String who)
        {
            Log.Write("Enviando nueva solicitud a " + who);
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(s.DeviceId, who + SOLICITATION_ARRIVAL);
                }
                else if (s.Platform.Equals("wp"))
                {
                    SendWp(s.DeviceId, who + SOLICITATION_ARRIVAL, SOLICITATION_ARRIVAL_BIS, WIN_ARRIVAL_PATH);
                }
                else if (s.Platform.Equals("ios"))
                {
                    SendIosAceptation(s.DeviceId, who + SOLICITATION_ARRIVAL);
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
            push.QueueNotification(new WindowsPhoneToastNotification()
                    .ForEndpointUri(new Uri(devId))
                    .ForOSVersion(WindowsPhoneDeviceOSVersion.Eight)
                    .WithBatchingInterval(BatchingInterval.Immediate)
                    .WithText1(text1)
                    .WithText2(text2)
                    .WithNavigatePath(path));
        }

        private void SendIosAceptation(string devId, string text)
        {
            push.QueueNotification(new AppleNotification()
                                       .ForDeviceToken(devId)
                                       .WithAlert(text)
                                       .WithBadge(7)
                                       .WithSound("sound.caf"));
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
