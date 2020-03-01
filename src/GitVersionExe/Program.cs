using System.IO;
using System.Text;
using System.Threading.Tasks;
using GitVersion.Extensions;
using GitVersion.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GitVersion
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
            var outFile = Path.Combine(Directory.GetCurrentDirectory(), "gitversion-diag.txt");
            var s = new FileStream(outFile, FileMode.Create);
            var writer = new StreamWriter(s, Encoding.UTF8);
            Stats.Dump(writer);
            //Stats.Dump(Console.Out);
            //Console.Out.Flush();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddModule(new GitVersionCoreModule());
                    services.AddModule(new GitVersionExeModule());

                    services.AddSingleton(sp => Options.Create(sp.GetService<IArgumentParser>().ParseArguments(args)));

                    services.AddHostedService<GitVersionApp>();
                })
                .UseConsoleLifetime();
    }
}
