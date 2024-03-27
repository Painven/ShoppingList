using System.IO;

namespace ShoppingList.Desktop.BL;

public class WorkingFolderWatcher
{
    private readonly FileSystemWatcher watcher;
    public event Action<string> FileChanged;

    public WorkingFolderWatcher()
    {
        watcher = new FileSystemWatcher();
        watcher.Changed += Watcher_Changed;
    }

    public void Watch(string folder)
    {
        if (Directory.Exists(folder))
        {
            watcher.Path = folder;
            watcher.EnableRaisingEvents = true;
        }
    }

    public void StopForActionAndContinue(Action action)
    {
        watcher.EnableRaisingEvents = false;
        action();
        watcher.EnableRaisingEvents = true;
    }

    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
        StopForActionAndContinue(() => FileChanged?.Invoke(e.FullPath));
    }
}
