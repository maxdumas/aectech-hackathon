//using System;
//using Rhino;
//using Rhino.Commands;
//using System.Diagnostics;
//using System.IO;

//namespace DvcRhinoPlugin
//{
//    public class WriteStepsToTextWhenFileLoads : Command
//    {
//        public WriteStepsToTextWhenFileLoads()
//        {
//            Instance = this;
//            RhinoApp.EndOpenDocument += OnEndOpenDocument;
//        }

//        ///<summary>The only instance of the WriteStepsToTextWhenFileLoads command.</summary>
//        public static WriteStepsToTextWhenFileLoads Instance { get; private set; }

//        public override string EnglishName => "WriteStepsToTextWhenFileLoads";

//        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
//        {
//            // This command will be empty as the work is done on the EndOpenDocument event
//            return Result.Success;
//        }

//        private void OnEndOpenDocument(object sender, DocumentOpenEventArgs e)
//        {
//            if (e.Merge)
//                return;

//            var startInfo = new ProcessStartInfo
//            {
//                WindowStyle = ProcessWindowStyle.Hidden,
//                WorkingDirectory = @"C:\Users\Pat\aec-hackathon-2023\aectech-hackathon",
//                FileName = "cmd.exe",
//                RedirectStandardOutput = true,
//                RedirectStandardError = true,
//                UseShellExecute = false,
//                Arguments = @"/C echo something > writeTextWhenRhinoLoads.txt"
//            };

//            using (var process = new Process { StartInfo = startInfo })
//            {
//                try
//                {
//                    process.Start();
//                    var output = process.StandardOutput.ReadToEnd();
//                    var errors = process.StandardError.ReadToEnd();
//                    process.WaitForExit();

//                    // Optionally write the process output to a log file
//                    File.WriteAllText(@"C:\Users\Pat\aec-hackathon-2023\aectech-hackathon\processLog.txt", output);
//                    if (process.ExitCode != 0)
//                    {
//                        // Optionally write the error output to a log file
//                        File.WriteAllText(@"C:\Users\Pat\aec-hackathon-2023\aectech-hackathon\processErrors.txt", errors);
//                        RhinoApp.WriteLine("Process exited with errors. See processErrors.txt for details.");
//                        return;
//                    }

//                    RhinoApp.WriteLine("Writing text when Rhino finishes loading completed successfully.");
//                }
//                catch (Exception ex)
//                {
//                    RhinoApp.WriteLine("An error occurred: " + ex.Message);
//                    // Optionally write the exception to a log file
//                    File.WriteAllText(@"C:\Users\Pat\aec-hackathon-2023\aectech-hackathon\processException.txt", ex.ToString());
//                }
//            }
//        }
//    }
//}
