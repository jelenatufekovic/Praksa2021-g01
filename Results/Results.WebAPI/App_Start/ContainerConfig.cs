using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Results.Model;
using Results.Repository;
using Results.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Results.WebAPI.App_Start
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ModelModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();

            #region ConfigForAutomapper

            //builder.Register(context => new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<TypeOneClassOrInterface, TypetwoClassOrInterface>();
            //    cfg.CreateMap<TypeOneClassOrInterface, TypetwoClassOrInterface>();
            //    //etc.
            //})).AsSelf().SingleInstance(); 

            //builder.Register(c =>
            //{
            //    var context = c.Resolve<IComponentContext>();
            //    var config = context.Resolve<MapperConfiguration>();
            //    return config.CreateMapper(context.Resolve);
            //}).As<IMapper>().InstancePerLifetimeScope();

            #endregion

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}