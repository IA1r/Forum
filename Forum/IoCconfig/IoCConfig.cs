using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Forum.IoCconfig
{
    public class IoCConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(Assembly.Load("BusinessModel"))
                .Where(t => t.Name.EndsWith("Manager") || t.Name.EndsWith("Provider"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();



            builder.RegisterAssemblyTypes(Assembly.Load("DataModel"))
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Work") || t.Name.EndsWith("Provider") || t.Name.EndsWith("Entities"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}