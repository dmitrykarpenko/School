using Autofac;
using Autofac.Integration.Mvc;
using School.DataLayer.Abstract;
using School.DataLayer.Concrete;
using School.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace School.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofac();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<EFSchoolRepository>().As<ISchoolRepository>();
            builder.RegisterType<StudentsBL>();

            var container = builder.Build();

            //var resolver = new AutofacDependencyResolver(container)
            var resolver = CompositionRoot.DependencyInjector.GetAutofacResolver(Assembly.GetExecutingAssembly());

            DependencyResolver.SetResolver(resolver);
        }
    }
}
