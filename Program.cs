string directoryPath = "PATH";

if (Directory.Exists(directoryPath))
{
    string[] mdFiles = Directory.GetFiles(directoryPath, "*.md");
    foreach (string filePath in mdFiles)
    {
        Console.WriteLine(filePath);
        var lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }
}
else
{
    Console.WriteLine("The specified directory does not exist");
}
