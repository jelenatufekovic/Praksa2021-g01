using Autofac;
using Autofac.Integration.WebApi;
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

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}