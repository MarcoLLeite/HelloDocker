namespace HelloDockerMaster
{
    using System.Diagnostics;
    using Microsoft.Extensions.Logging;

    public class BashRunner
    {
        private readonly ILogger<BashRunner> _logger;

        public BashRunner(ILogger<BashRunner> logger)
        {
            _logger = logger;
        }

        public void RunCommand(string cmd)
        {
            _logger.LogInformation("Bash|Running command: '{cmd}'", cmd);

            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            _logger.LogInformation("Bash|Process exited with result: {result}", result);
        }
    }
}