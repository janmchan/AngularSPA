using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using AgeRanger.ResourceAccess.Data.Contracts;
using AgeRanger.ResourceAccess.Data.Providers;
using Autofac;
using AgeRanger.Core.Contracts;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace AgeRanger
{
    public class IocCompositionRoot
    {
        public static IContainer Container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            RegisterTypesWithDIMarkerInterfaces(builder);
            
            Container = builder.Build();
            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = 
                new AutofacWebApiDependencyResolver(Container);

        }

        private static void RegisterTypesWithDIMarkerInterfaces(ContainerBuilder builder)
        {


            var dependencyAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.StartsWith("AgeRanger"));
            foreach (var assembly in dependencyAssemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AssignableTo<IDependency>()
                    .AsImplementedInterfaces();

                builder.RegisterAssemblyTypes(assembly)
                    .AssignableTo<ISingletonDependency>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
            

        }
    }
}