using OllamaSharp;


var ollama = new OllamaApiClient(new Uri("http://localhost:11434"));
//TODO: Do I need to do error handling before interacting with ollama?
ollama.SelectedModel = "clef";

string? directoryPath = Environment.GetEnvironmentVariable("CARDS_DIRECTORY");

if (directoryPath == null)
{
    Console.WriteLine("No CARDS_DIRECTORY provided");
    return;
}

IEnumerable<string> mdFiles = GetDirectoryContents(directoryPath);
foreach (string filePath in mdFiles)
{
    string mdFilecontents = GetAllFileContents(filePath);
    //TODO: Get response for every requested file
    var response = await ollama.SystemOneAsync(new OllamaSharp.Models.SystemOneRequest
    {
        State = "Test",
        Questions = new()
        {
            ["q1"] = new OllamaSharp.Models.SystemOneNoulQuestion
            {
                Instructions = "Did I just say test?"
            },
            ["q2"] = new OllamaSharp.Models.SystemOneNoulQuestion
            {
                Instructions = "Did I just say asdf?"
            }
        }
    });
    var q1 = (OllamaSharp.Models.SystemOneNoulAnswer)response.Answers["q1"];
    var q2 = (OllamaSharp.Models.SystemOneNoulAnswer)response.Answers["q2"];
    Console.WriteLine(q1.Noul);
    Console.WriteLine(q2.Noul);
}


//TODO: This should have a configurable max depth?
IEnumerable<string> GetDirectoryContents(string directorypath)
{
    if (Directory.Exists(directorypath) == false)
    {
        Console.WriteLine("The specified directory does not exist");
        return [];
    }

    IEnumerable<string> mdFiles = Directory.EnumerateFiles(directorypath, "*.md", SearchOption.AllDirectories);
    return mdFiles;
}

string GetAllFileContents(string filepath)
{
    if (File.Exists(filepath) == false)
    {
        Console.WriteLine("The specified file does not exist");
        return "";
    }

    string content = "";
    var lines = File.ReadAllLines(filepath);
    foreach (string line in lines)
    {
        content += line;
    }
    return content;
}
