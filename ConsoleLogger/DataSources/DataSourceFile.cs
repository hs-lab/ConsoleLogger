using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleLogger.DataSources
{
    public class DataSourceFile:DataSource,IDisposable
    {
        StreamReader reader;
        
        public DataSourceFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"File path does not exist: {filePath}");
            }
            location = filePath;
            reader = new StreamReader(File.Open(location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        public override string Read()
        {
            return reader.ReadToEnd();
        }

        public override void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(location));

            // Watch for changes in LastAccess and LastWrite times, and
            // the renaming of files or directories.
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;

            // Only watch specified file.
            watcher.Filter = Path.GetFileName(location);

            // Add event handlers.
            watcher.Changed += OnChanged;

            // Begin watching.
            watcher.EnableRaisingEvents = true;



        }

        public void Dispose()
        {
            reader.Dispose();
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            Console.WriteLine(((DataSourceFile)source).Read());
        }

    }
}
