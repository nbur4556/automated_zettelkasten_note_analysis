string directoryPath = "PATH";

string[] mdFiles = GetDirectoryContents(directoryPath);
foreach (string filePath in mdFiles)
{
    string mdFilecontents = GetAllFileContents(filePath);
    Console.WriteLine(mdFilecontents);

}

string[] GetDirectoryContents(string directorypath)
{
    Console.WriteLine(directorypath);
    if (Directory.Exists(directorypath) == false)
    {
        Console.WriteLine("The specified directory does not exist");
        return [];
    }

    string[] mdFiles = Directory.GetFiles(directorypath, "*.md");
    string[] subdirectories = Directory.GetDirectories(directorypath);

    foreach (string subdirectory in subdirectories)
    {
        Console.WriteLine(subdirectory);
    }

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
