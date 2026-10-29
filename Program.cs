using OllamaSharp;
using OllamaSharp.Models;

// Configure the environment
string? ANALYSIS_MODEL = Environment.GetEnvironmentVariable("ANALYSIS_MODEL");
string? CARDS_DIRECTORY = Environment.GetEnvironmentVariable("CARDS_DIRECTORY");
string? OLLAMA_URI = Environment.GetEnvironmentVariable("OLLAMA_URI");

if (OLLAMA_URI == null)
{
    OLLAMA_URI = "http://localhost:11434";
}
if (ANALYSIS_MODEL == null)
{
    Console.WriteLine("No ANALYSIS_MODEL provided");
    return;
}
if (CARDS_DIRECTORY == null)
{
    Console.WriteLine("No CARDS_DIRECTORY provided");
    return;
}

//TODO: Do I need to do error handling before interacting with ollama?
var ollama = new OllamaApiClient(new Uri(OLLAMA_URI));
ollama.SelectedModel = ANALYSIS_MODEL;
string? directoryPath = CARDS_DIRECTORY;

if (directoryPath == null)
{
    Console.WriteLine("No CARDS_DIRECTORY provided");
    return;
}

IEnumerable<string> mdFiles = GetDirectoryContents(directoryPath);
foreach (string filePath in mdFiles)
{
    string mdFileContents = GetAllFileContents(filePath);
    var response = await ollama.SystemOneAsync(new SystemOneRequest
    {
        State = mdFileContents,
        Questions = new()
        {
            ["is_fleeting"] = new SystemOneNoulQuestion
            {
                Instructions = "According to Zettelkasaten criteria, is this a fleeting note?"
            },
            ["is_organized"] = new SystemOneNoulQuestion
            {
                Instructions = "According to Zettelkasaten criteria, is this an organized note?"
            },
            ["is_distilled"] = new SystemOneNoulQuestion
            {
                Instructions = "According to Zettelkasaten criteria, is this a distilled note?"
            },
            ["is_permanent"] = new SystemOneNoulQuestion
            {
                Instructions = "According to Zettelkasaten criteria, is this a permanent note?"
            },
            ["zet_note_type"] = new SystemOneChoiceQuestion
            {
                Instructions = "What type of Zettelkasten note is this?",
                Criteria = new()
                {
                    ["fleeting"] = "Fleeting Note",
                    ["organized"] = "Organized Note",
                    ["distilled"] = "Distilled Note",
                    ["permanent"] = "Permanent Note",
                }
            }
        }
    });

    //TODO: organize output
    Console.WriteLine(filePath);
    var isFleeting = (SystemOneNoulAnswer)response.Answers["is_fleeting"];
    Console.WriteLine(isFleeting.Noul);
    var isOrganized = (SystemOneNoulAnswer)response.Answers["is_organized"];
    Console.WriteLine(isOrganized.Noul);
    var isDistilled = (SystemOneNoulAnswer)response.Answers["is_distilled"];
    Console.WriteLine(isDistilled.Noul);
    var isPermanent = (SystemOneNoulAnswer)response.Answers["is_permanent"];
    Console.WriteLine(isPermanent.Noul);
    var zetNoteType = (SystemOneChoiceAnswer)response.Answers["zet_note_type"];
    Console.WriteLine(zetNoteType.Choice);
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
