
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class Program {
    static void Main(string[] args) {
        string filePath = "../dvc.yaml";
        
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }

        string projectPath = "../project";
        string[] yamlFiles = Directory.GetFiles(projectPath, "*.yaml", SearchOption.AllDirectories);
        
        var graphSpec = new Dictionary<string, Dictionary<string, object>>();
        graphSpec["stages"] = new Dictionary<string, object>();

        foreach (string yamlFile in yamlFiles) {
            string yamlFilePath = Path.GetDirectoryName(yamlFile).Replace("/", "-").Replace("..-project-", "");
            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yamlObject = deserializer.Deserialize<Dictionary<string, object>>(new StreamReader(yamlFile));
            string rhinoFilePath = yamlFilePath.Replace(".gh.yaml", ".3dm");
            yamlObject["cmd"] = "../RhinoRunner/bin/Debug/RhinoRunner.exe -File " + rhinoFilePath;
            graphSpec["stages"][yamlFilePath] = yamlObject;
        }

        var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        var serializedYaml = serializer.Serialize(graphSpec);
        Console.WriteLine(serializedYaml);
        using (StreamWriter writer = new StreamWriter(filePath, true)) {
            writer.WriteLine(serializedYaml);
        }
    }
}

