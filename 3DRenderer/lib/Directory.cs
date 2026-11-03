namespace _3DRenderer.lib;
using System.Diagnostics;

public static class Directory
{
    public static void Open(string directory)
    {
        string path = Path.GetFullPath("../../../../" + directory, AppContext.BaseDirectory);

        if (OperatingSystem.IsWindows())
        {
            Process.Start("explorer.exe", path);
        }
        else if (OperatingSystem.IsLinux())
        {
            Process.Start("xdg-open", path);
        }
        else if (OperatingSystem.IsMacOS())
        {
            Process.Start("open", path);
        }
    }
    
    public static string GetTestFileContentFromName(string filename)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "TestFiles", filename);
        string content = File.ReadAllText(path);
        return content;
    }
}