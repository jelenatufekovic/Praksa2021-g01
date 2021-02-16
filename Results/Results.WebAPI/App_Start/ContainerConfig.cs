using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Results.Model;
using Results.Repository;
using Results.Service;
using Results.WebAPI.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Results.WebAPI.App_Start
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<ModelModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();

            #region ConfigForAutomapper
            
            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))).SingleInstance();
            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

            #endregion

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}