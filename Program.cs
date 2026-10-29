string directoryPath = "PATH";

if (Directory.Exists(directoryPath))
{
    string[] mdFiles = Directory.GetFiles(directoryPath, "*.md");
    foreach (string filePath in mdFiles)
    {
        Console.WriteLine(filePath);
        string contents = GetAllFileContents(filePath);
        Console.WriteLine(contents);
    }
}
else
{
    Console.WriteLine("The specified directory does not exist");
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
