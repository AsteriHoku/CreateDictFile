using System.Text.Json;
using CreateDictFile;

var dict = new Dictionary<string, string>();

string jsonFilePath = "/todo/ExtensionsJSON.json";

if (File.Exists(jsonFilePath)) {
    try {
        string jsonContent = File.ReadAllText(jsonFilePath);

        List<Extensions> extensionsList = JsonSerializer.Deserialize<List<Extensions>>(jsonContent) ??
                                          throw new InvalidOperationException();

        string filePath = "/todo/forJsDict.txt";

        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (Extensions extension in extensionsList) {
                if (extension.extensions?.Length > 0) {
                    foreach (var ext in extension.extensions) {
                        if (!dict.ContainsKey(ext)) {
                            dict.Add(ext, extension.name);
                            if (extension.name?.ToLower() != "markdown") {
                                writer.WriteLine($"'{ext}': '{extension.name}',     // {extension.type}");
                            }
                        } else {
                            Console.WriteLine($"ext {ext} already exists in dict for: {dict[ext]}");
                            Console.WriteLine($"failed to add: name {extension.name} type {extension.type}");
                        }
                    }
                }
            }
        }
    } catch (Exception ex) {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
} else {
    Console.WriteLine("The JSON file does not exist.");
}