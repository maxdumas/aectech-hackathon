using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grasshopper.Kernel;
using YamlDotNet.Serialization;

namespace SpitTextFile
{

    public static class SpitFile
    {

        
        //public void AddEventListner()
        //{
        //}

        public static List<string> GetFilePaths(List<string> interestComponentLst)
        {
            var doc = Grasshopper.Instances.ActiveCanvas.Document;

            //Get the list of all component in this grasshoper document

            var filePaths = new List<string>();
            foreach (IGH_DocumentObject obj in doc.Objects)
            {
                if (obj is IGH_Component liveComp)
                {
                    // If the object is the Grasshopper component, add it to the list


                    if (interestComponentLst.Any(item => item == liveComp.Name))
                    {


                        for (int i = 0; i < liveComp.Params.Input.Count; i++)
                        {
                            if (liveComp.Params.Input[i].Name == "FilePath")
                            {
                                var values = liveComp.Params.Input[i].Sources[0].VolatileData.AllData(true);

                                foreach (var value in values)
                                {
                                    string fileName = "";

                                    GH_Convert.ToString(value, out fileName, GH_Conversion.Both);

                                    filePaths.Add(fileName);
                                }
                              


                            }
                        }
                    }


                }

            }

            return filePaths;
        }


        private static List<string> GetRelativeFilePath(List<string> files)
        {
            var relativeFiles = new List<string>();
            foreach (var file in files)
            {
                string baseDirectory = Path.GetDirectoryName(file);

                Uri fullUri = new Uri(file);
                Uri baseUri = new Uri(baseDirectory);

                Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
                relativeFiles.Add(relativeUri.ToString());
            }
      
            return relativeFiles;
        }

        public static void WriteToFile()
        {
        
            var inputCompName = new List<string>()
            {
                "Reference by BakeName", "Reference by Type", "Reference by Layer", "Reference by Key/Value",
                "Reference Block by Name", "Reference by Name"
            };

            var fullInputFiles = GetFilePaths(inputCompName);
            var relativeInputs = GetRelativeFilePath(fullInputFiles);

           var outputCompN = new List<string>() { "Bake Objects To File" };
            var fullOutputFiles = GetFilePaths(outputCompN);
            var relativeOutputs = GetRelativeFilePath(fullOutputFiles);


            var dict = new Dictionary<string, List<string>>();
            dict.Add("deps", relativeInputs);
            dict.Add("outs", relativeOutputs);

            var doc = Grasshopper.Instances.ActiveCanvas.Document;

            var ghDocName = string.Concat(doc.DisplayName.Split(Path.GetInvalidFileNameChars())); 
            var ghDocDir = Path.GetDirectoryName(doc.FilePath);

            SerializeYaml(ghDocDir, ghDocName, dict);
        }



        private static void SerializeYaml(string dirFolder, string fileName, Dictionary<string, List<string>> data)
        {

            var yamlFile = Path.Combine(dirFolder, fileName + ".deps.yaml");
            var serializer = new SerializerBuilder().Build();

            // Serialize the Dictionary to a YAML string
            string yamlContent = serializer.Serialize(data);

            // Write the YAML content to a file
            System.IO.File.WriteAllText(yamlFile, yamlContent);

        }

    }
}

