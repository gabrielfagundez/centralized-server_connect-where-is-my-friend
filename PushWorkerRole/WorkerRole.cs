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

using PisDataAccess;
using PISServer.Models;

namespace PushWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        //Create our push services broker
        PushBroker push;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            //Trace.TraceInformation("PushWorkerRole entry point called", "Information");
            
            while (true)
            {
                PushMiddleware pm = new PushMiddleware();
                pm.GetAcceptedEvents().ToList().ForEach(SendAcceptations);
                pm.GetRejectedEvents().ToList().ForEach(SendRejections);
                pm.GetUnsentEvents().ToList().ForEach(SendSolicitations);
                
                Thread.Sleep(10000);
                //Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            push = new PushBroker();

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void SendAcceptations(WhereAcceptationEvent ev)
        {
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(ev, s.DeviceId, "something");
                }
                else if(s.Platform.Equals("wp"))
                {
                    //No se si es ese ev.User.Name el nombre del que acepta pero esa era la idea
                    SendWp(ev, s.DeviceId, ev.User.Name+" accepted your solicitation", "You can see where he is!", new Uri("uri"));
                }
                else if(s.Platform.Equals("ios"))
                {
                    SendIosAceptation(ev, s.DeviceId);
                }
            }
            ev.Sent = true;
        }

        private void SendRejections(WhereNegationEvent ev)
        {
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(ev, s.DeviceId, "something");
                }
                else if (s.Platform.Equals("wp"))
                {
                    //No se si es ese ev.User.Name el nombre del que acepta pero esa era la idea
                    SendWp(ev, s.DeviceId, ev.User.Name + " couldn't accept your solicitation", "", new Uri("uri"));
                }
                else if (s.Platform.Equals("ios"))
                {
                    SendIosAceptation(ev, s.DeviceId);
                }
            }
            ev.Sent = true;
        }

        private void SendSolicitations(WhereSolicitationEvent ev)
        {
            List<Session> ls = ev.User.Session.Where(s => s.Active = true)
                                .ToList();
            foreach (Session s in ls)
            {
                if (s.Platform.Equals("android"))
                {
                    SendAndroid(ev, s.DeviceId, "something");
                }
                else if (s.Platform.Equals("wp"))
                {
                    //No se si es ese ev.User.Name el nombre del que acepta pero esa era la idea
                    SendWp(ev, s.DeviceId, ev.User.Name + " wants to know where you are", "Answer his solicitation!", new Uri("uri"));
                }
                else if (s.Platform.Equals("ios"))
                {
                    SendIosAceptation(ev, s.DeviceId);
                }
            }
            ev.Sent = true;
        }

        private void SendAndroid(Event wae, string devId, string text)
        {
            push.QueueNotification(new GcmNotification()    //.WithData(new Dictionary())
                                     .ForDeviceRegistrationId(devId)
                                     .WithJson(text));
        }

        private void SendWp(Event wae, string devId, string text1, string text2, Uri uri)
        {
            push.QueueNotification(new WindowsPhoneToastNotification()
                    .ForEndpointUri(new Uri(devId))
                    .ForOSVersion(WindowsPhoneDeviceOSVersion.Eight)
                    .WithBatchingInterval(BatchingInterval.Immediate)
                    .WithText1(text1)
                    .WithText2(text2));
        }

        private void SendIosAceptation(Event wae, string devId)
        {
            //not implemented yet
        }
    }
}
