using System.Diagnostics;
using System.Reflection;

namespace FileReaderLib;

public static class FileReader
{
    public static IEnumerable<FileData> ReadDirectory(string pathFromSln)
    {
        var (pathSegments, fullPathToDir) = ConstructWorkablePath(pathFromSln);
        var dirExists = Directory.Exists(fullPathToDir);
        
        if (dirExists is false) throw new InvalidOperationException("Directory doesn't exist.");
        DirectoryInfo di = new DirectoryInfo(fullPathToDir);

        var files = di.GetFiles("*.sql", SearchOption.AllDirectories);
        var filesToReturn = new List<FileData>();
        foreach (var file in files)
        {
            var tempPathSegments = pathSegments;
            tempPathSegments.Add(file.Name);
            filesToReturn.Add(new FileData
            {
                FileName = file.Name,
                FilePath = file.FullName
            });
        }

        return filesToReturn;
    }

    public static string ReadFile(string pathFromSln)
    {
        var (_, fullPathToFile) = ConstructWorkablePath(pathFromSln);
        var contents = File.ReadAllText(fullPathToFile);
        return contents;
    }

    private static (List<string> pathSegments, string) ConstructWorkablePath(string pathFromSln)
    {
        
        var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        if (execPath is null) throw new InvalidOperationException("Couldn't find execution path.");
        
        var pathSegments = new List<string>
        {
            execPath,
            @"../../../../",
            pathFromSln
        };
        
        return (pathSegments, Path.GetFullPath(Path.Combine(pathSegments.ToArray())));
    }
}