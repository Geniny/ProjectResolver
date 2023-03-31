using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Lib.Pipeline
{
    internal class CommandLinePipeline
    {
        public CommandLinePipeline() { }

        public string? Execute(string command)
        {
            if (string.IsNullOrEmpty(command))
                return null;

            string output = string.Empty;
            using (Process cmd = new Process())
            {
                ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(
                    "cmd.exe",
                    command
                )
                {
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.UTF8
                };
                cmd.StartInfo = procStartInfo;
                cmd.Start();
                cmd.WaitForExit();

                output = cmd.StandardOutput.ReadToEnd();
            }

            return output;
        }

        public string? Execute(string fileName, string arguments)
        {
            var file = new FileInfo(fileName);

            if(!file.Exists) return null;

            string output = string.Empty;
            using (Process cmd = new Process())
            {
                ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(fileName)
                {
                    RedirectStandardOutput = true,
                    Arguments=arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.UTF8,
                    Verb = "runas"
                };
                cmd.StartInfo = procStartInfo;
                cmd.Start();
                cmd.WaitForExit();

                output = cmd.StandardOutput.ReadToEnd();
            }

            return output;
        }
    }
}
