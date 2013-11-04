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
                List<WhereAcceptationEvent> wae = pm.GetAcceptedEvents();


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

        private void SendNotification(Event ev)
        {
            ////ev.User.Session.
            //if (device.Plataforma.Equals("iphone", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    //falta permisos 
            //}
            //else if (device.Plataforma.Equals("android", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(device.DeviceId)
            //                                      .WithJson("{\"alert\":\"Fuiste pulliado, cabrón!\",\"badge\":7,\"sound\":\"sound.caf\"}"));
            //}
            //else if (device.Plataforma.Equals("winphone", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    push.QueueNotification(new WindowsPhoneToastNotification()
            //    .ForEndpointUri(new Uri(device.DeviceId))
            //    .ForOSVersion(WindowsPhoneDeviceOSVersion.MangoSevenPointFive)
            //    .WithBatchingInterval(BatchingInterval.Immediate)
            //    .WithNavigatePath("/MainPage.xaml")
            //    .WithText1("PISPUSH")
            //    .WithText2("Usted ha sido pispushieado, malacate"));
            //}
        }
    }
}
