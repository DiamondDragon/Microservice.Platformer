﻿using System.Collections.Generic;
using Autofac;
using IntelliFlo.AppStartup;
using IntelliFlo.AppStartup.Initializers;
using IntelliFlo.Platform.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Bulk.Modules;
using Monolith.Bulk.v2;

namespace Monolith.Bulk
{
    internal class Startup : MicroserviceStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            foreach (var initializer in base.CreateInitializers())
            {
                if (initializer is MvcInitializer)
                    yield return new BulkMvcInitializer();
                else
                    yield return initializer;
            }

            //yield return new BusInitializer();
            yield return new NHibernateInitializer();
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

        #region Overrides

        public class BulkMvcInitializer : MvcInitializer
        {
            private const string ScopeClaimName = "scope";

            public override void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            {
                base.ConfigureServices(configuration, services);
                services.AddAuthorization(options =>
                {
                    options.AddPolicy(PolicyNames.ClientFinancialData, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ScopeClaimName, Scopes.ClientFinancialData);
                    });
                    options.AddPolicy(PolicyNames.ClientData, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ScopeClaimName, Scopes.ClientData);
                    });
                    options.AddPolicy(PolicyNames.FirmData, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ScopeClaimName, Scopes.FirmData);
                    });
                    options.AddPolicy(PolicyNames.FundData, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ScopeClaimName, Scopes.FundData);
                    });
                    options.AddPolicy(PolicyNames.SystemData, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ScopeClaimName, Scopes.SystemData);
                    });
                });
            }
        }

        #endregion
    }
}
