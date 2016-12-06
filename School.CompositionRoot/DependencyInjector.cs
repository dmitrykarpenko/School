using Autofac;
using Autofac.Integration.Mvc;
using School.DataLayer.Abstract;
using School.DataLayer.Concrete;
using School.Logic;
using System.Reflection;
using System.Web.Mvc;

namespace School.CompositionRoot
{
    public static class DependencyInjector
    {
        public static IDependencyResolver GetAutofacResolver(Assembly executingAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(executingAssembly);
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<EFSchoolRepository>().As<ISchoolRepository>();
            builder.RegisterType<StudentsBL>();

            var container = builder.Build();

            return new AutofacDependencyResolver(container);
        }
    }
}
