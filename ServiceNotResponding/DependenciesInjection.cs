namespace ServiceNotResponding
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;

    public class DependenciesInjection
    {
        public static void Inject(IServiceCollection services)
        {

            services.AddHostedService<Worker>();

            //Logs
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog("./NLog.config");
            });
        }
    }
}