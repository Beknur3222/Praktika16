using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Praktika16
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в приложение для логирования изменений в файлах!");

            Console.Write("Введите путь к отслеживаемой директории: ");
            string directoryPath = Console.ReadLine();

            Console.Write("Введите путь к лог-файлу: ");
            string logFilePath = Console.ReadLine();

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = directoryPath;
                watcher.IncludeSubdirectories = true;

                watcher.Created += OnFileOrDirectoryChanged;
                watcher.Deleted += OnFileOrDirectoryChanged;
                watcher.Renamed += OnFileRenamed;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine($"Отслеживание изменений в директории {directoryPath}. Для выхода нажмите Enter.");
                Console.ReadLine();
            }
        }

        static void OnFileOrDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.FullPath}");
        }

        static void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.OldFullPath} переименован в {e.FullPath}");
        }

        static void LogChange(string logMessage)
        {
            string logFilePath = "log.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
            }
        }
    }

}
