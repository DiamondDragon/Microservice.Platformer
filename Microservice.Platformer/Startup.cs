﻿using System.Collections.Generic;
using Autofac;
using IntelliFlo.AppStartup;
using IntelliFlo.AppStartup.Initializers;
using Microservice.Platformer.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microservice.Platformer
{
    internal class Startup : MicroserviceStartup
    {
        public Startup(ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(loggerFactory, configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            foreach (var initializer in base.CreateInitializers())
            {
                 yield return initializer;
            }

            //yield return new NHibernateInitializer(Configuration);
            //yield return new BusInitializer(Configuration);
        }

        protected override IEnumerable<Module> CreateAutofacModules()
        {
            foreach (var module in base.CreateAutofacModules())
            {
                yield return module;
            }

            yield return new BulkApiAutofacModule();
            yield return new BulkAutofacModule();
            yield return new BulkBusAutofacModule();
        }
    }
}
