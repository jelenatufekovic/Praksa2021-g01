﻿using Autofac;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class ModelModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResultsModel>().As<IResultsModel>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
