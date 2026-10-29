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
