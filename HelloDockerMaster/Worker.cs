namespace HelloDockerMaster
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BashRunner _bashRunner;
        private const string ServiceNotRespondingContainer = "servicenotresponding-container";
        public Worker(ILogger<Worker> logger, BashRunner bashRunner)
        {
            _logger = logger;
            _bashRunner = bashRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // install docker.io so the service as access to the docker commands
            _bashRunner.RunCommand("apt-get update ; apt-get install docker.io -y");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("HelloDockerMaster running at: {time}", DateTimeOffset.Now);

                // stop the container
                _bashRunner.RunCommand($"docker stop {ServiceNotRespondingContainer}");
                
                // wait 30 seconds to be sure the service and container have enough time to stop
                Thread.Sleep(3000);

                // start the container (the service "service not responding" should also be started)
                _bashRunner.RunCommand($"docker start {ServiceNotRespondingContainer}");
                
                //this is done every 30 seconds
                await Task.Delay(30000, stoppingToken);
            }
        }

    }
}