using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExperimentLibrary;
using ExperimentWeb.Models;
using MassTransit;

namespace ExperimentWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IServiceBus Bus { get; set; }

        public static List<MailData> MailSample { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bus = MassTransitInitializer.CreateBus("CustomerPortal_WebApp", x => { });
            MailSample = MailData.GenerateMails(400);
        }

        protected void Application_End()
        {
            Bus.Dispose();
        }
    }
}
