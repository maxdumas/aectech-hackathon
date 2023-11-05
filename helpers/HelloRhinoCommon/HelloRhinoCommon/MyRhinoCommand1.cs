using System;
using Rhino;
using Rhino.Commands;
using System.Diagnostics; // Added this using to simplify the code below

namespace HelloRhinoCommon
{
    public class MyRhinoCommand1 : Command
    {
        public MyRhinoCommand1()
        {
            Instance = this;
        }

        ///<summary>The only instance of the MyRhinoCommand1 command.</summary>
        public static MyRhinoCommand1 Instance { get; private set; }

        public override string EnglishName => "MyRhinoCommand1";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = @"C:\Users\Pat\aec-hackathon-2023\aectech-hackathon",
                FileName = "cmd.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true, // Added to capture standard error
                UseShellExecute = false,
                Arguments = @"/C ""C:\Program Files (x86)\DVC (Data Version Control)\dvc"" repro -q -f > stepsTest.txt"
            };

            using (var process = new Process { StartInfo = startInfo })
            {
                try
                {
                    process.Start();
                    var output = process.StandardOutput.ReadToEnd(); // Read the output if needed
                    var errors = process.StandardError.ReadToEnd(); // Read the error output if needed
                    process.WaitForExit();

                    // Check if there was an error during the process execution
                    if (process.ExitCode != 0)
                    {
                        // Handle the error case, possibly logging the error message
                        RhinoApp.WriteLine("Process exited with errors: " + errors);
                        return Result.Failure;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that were thrown during the process execution
                    RhinoApp.WriteLine("An error occurred: " + ex.Message);
                    return Result.Failure;
                }
            }

            return Result.Success;
        }
    }
}
