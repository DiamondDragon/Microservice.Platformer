using System;
using System.Threading;
using IntelliFlo;
using IntelliFlo.AppStartup.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Win32;
using Monolith.Bulk.Properties;

namespace Monolith.Bulk
{
#if NET471
 
    using Topshelf;
    using Topshelf.HostConfigurators;

    public class MonolithBulk
    {
        private IWebHost host;
 
        private readonly string[] args;
 
        public MonolithBulk(string[] args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }
 
        public void Start()
        {
            host = MicroserviceHost
                .Build<Startup>(Settings.Default.BaseAddress, args);
 
            host.Run();
        }
 
        public void Stop()
        {
            host.StopAsync().Wait();
        }
    }

    public static class HostConfiguratorExtensions
    {
        public static HostConfigurator Configure(this HostConfigurator host)
        {
            var profile = string.Empty;
            var portStr = string.Empty;
            var startup = string.Empty;
            var exitAfterStartupStr = string.Empty;

            host.AddCommandLineDefinition(StartupOptions.ProfileFlag, f => { profile = f; });
            host.AddCommandLineDefinition(StartupOptions.PortFlag, f => { portStr = f; });
            host.AddCommandLineDefinition(StartupOptions.StartupFlag, f => { startup = f; });
            host.AddCommandLineDefinition(StartupOptions.ExitAfterStartupFlag, f => { exitAfterStartupStr = f; });
            host.ApplyCommandLine();
            host.UseLog4Net();

            var options = new StartupOptions(profile, portStr, startup, exitAfterStartupStr);

            if (options.HasValue())
            {
                var parameters = options.ToString();
                host.AfterInstall(config =>
                {
                    var key = $@"System\CurrentControlSet\Services\{config.ServiceName}";

                    using (var service = Registry.LocalMachine.OpenSubKey(key, true))
                    {
                        if (service == null)
                            return;

                        const string path = "ImagePath";
                        var imagePath = (string)service.GetValue(path);
                        service.SetValue(path, $"{imagePath} {parameters}");
                    }
                });
            }

            return host;
        }

        public static HostConfigurator ConfigureServiceRecovery(this HostConfigurator host, Action<ServiceRecoveryConfigurator> configureCallback = null)
        {
            if (configureCallback == null)
            {
                host.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(0);
                    rc.RestartService(0);
                    rc.RestartService(0);
                    rc.OnCrashOnly();
                    rc.SetResetPeriod(1);

                });
            }
            else
            {
                host.EnableServiceRecovery(configureCallback);
            }

            return host;
        }
    }


    partial class Program
    {
        private static void RunUsingNetFramework(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Configure();
                x.Service<MonolithBulk>(s =>
                {
                    s.ConstructUsing(name => new MonolithBulk(args));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.ConfigureServiceRecovery();

                x.SetDescription("My service description");
                x.SetDisplayName("Monolith.Bulk");
                x.SetServiceName("Monolith.Bulk");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
 
#endif
}
