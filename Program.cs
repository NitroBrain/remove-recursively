using RemoveFoldersRecursively.Dashboard;
using RemoveRecursively.Core;

namespace RemoveFoldersRecursively
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            const string VERSION = VersionInfo.Version;

            if (args.Length == 0)
            {
                DashboardUI.Show(VERSION);
                return;
            }

            string firstArg = args[0].ToLower();

            switch (firstArg)
            {
                case "--version":
                case "-v":
                    Console.WriteLine(VersionInfo.Version);
                    return;
                case "--help":
                case "--h":
                    DashboardUI.Show(VERSION);
                    return;
            }

            string currentPath = Directory.GetCurrentDirectory();
            var folders = new List<string>(args);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($">>> [STARTING]: {currentPath}");
            Console.ResetColor();

            foreach (var folder in folders)
            {
                bool found = Directory.GetDirectories(currentPath, folder, SearchOption.AllDirectories).Any();

                if (found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(found ? $">>> [FOUND]: {folder}" : $">>> [NOT FOUND]: {folder}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAre you sure you want to delete all FOUND folders recursively? (y/n): ");
            Console.ResetColor();

            string? confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "y")
            {
                Console.WriteLine("Operation cancelled by user.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nProceeding with deletion...\n");
            Console.ResetColor();

            foreach (var folder in folders)
            {
                RemoveFoldersRecursively(currentPath, folder);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAll matching folders have been processed.");
            Console.ResetColor();
        }

        static void RemoveFoldersRecursively(string dir, string target)
        {
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                var folderName = Path.GetFileName(subDir);

                if (folderName.Equals(target, StringComparison.Ordinal))
                {
                    if (IsInsideCurrentProject(subDir))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($">>> [SKIPPING]: {subDir}");
                        Console.ResetColor();
                        Console.WriteLine();

                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($">>> [REMOVING]: {subDir}");
                    Console.ResetColor();
                    Console.WriteLine();

                    ForceDelete(subDir);
                }
                else
                {
                    RemoveFoldersRecursively(subDir, target);
                }
            }
        }

        static bool IsInsideCurrentProject(string path)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(path).StartsWith(Path.GetFullPath(exePath), StringComparison.OrdinalIgnoreCase);
        }

        static void ForceDelete(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        try
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($">>> [FAILED] {file}: {ex.Message}");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }

                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        ForceDelete(dir);
                    }

                    Directory.Delete(path, true);
                }
                else if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($">>> [FAILED] {path}: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}
