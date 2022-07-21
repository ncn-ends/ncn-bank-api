using System.Diagnostics;
using System.Reflection;

namespace FileReader;

public static class FileReader
{
    public static IEnumerable<File> ReadDirectory(string pathFromSln)
    {
        var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        if (execPath is null) throw new InvalidOperationException("Couldn't find execution path.");
        
        var pathSegments = new List<string>
        {
            execPath,
            @"../../../../",
            pathFromSln
        };
        
        var fullDirPath = Path.GetFullPath(Path.Combine(pathSegments.ToArray()));
        var dirExists = Directory.Exists(fullDirPath);
        
        if (dirExists is false) throw new InvalidOperationException("Directory doesn't exist.");
        DirectoryInfo di = new DirectoryInfo(fullDirPath);

        var files = di.GetFiles("*.sql", SearchOption.AllDirectories);
        var filesToReturn = new List<File>();
        foreach (var file in files)
        {
            var tempPathSegments = pathSegments;
            tempPathSegments.Add(file.Name);
            filesToReturn.Add(new File
            {
                FileName = file.Name,
                FilePath = file.FullName
            });
        }

        return filesToReturn;
    }
}