namespace RemoveFoldersRecursively
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: rr [folder1] [folder2] ...");
                return;
            }

            string currentPath = Directory.GetCurrentDirectory();
            var folders = new List<string>(args);

            Console.WriteLine($"Starting from: {currentPath}");
            foreach (var folder in folders)
            {
                Console.WriteLine($"Looking for folder: {folder}");
            }

            Console.Write("\nAre you sure you want to delete these folders recursively? (y/n): ");
            string? confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "y" && confirmation != "yes")
            {
                Console.WriteLine("Operation cancelled by user.");
                return;
            }

            Console.WriteLine("\nProceeding with deletion...\n");

            foreach (var folder in folders)
            {
                RemoveFoldersRecursively(currentPath, folder);
            }

            Console.WriteLine("\nAll matching folders have been processed.");
        }

        static void RemoveFoldersRecursively(string dir, string target)
        {
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                var folderName = Path.GetFileName(subDir);

                if (folderName.Equals(target, StringComparison.OrdinalIgnoreCase))
                {
                    if (IsInsideCurrentProject(subDir))
                    {
                        Console.WriteLine($"Skipping current project folder: {subDir}");
                        continue;
                    }

                    Console.WriteLine($"Removing Folder: {subDir}");
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
                            Console.WriteLine($"Failed to delete file {file}: {ex.Message}");
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
                Console.WriteLine($"Failed to delete {path}: {ex.Message}");
            }
        }
    }
}
