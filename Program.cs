namespace MyNamespace
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

            foreach (var folder in folders)
            {
                RemoveFoldersRecursively(currentPath, folder);
            }
        }

        static void RemoveFoldersRecursively(string dir, string target)
        {
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                var folderName = Path.GetFileName(subDir);

                if (folderName.Equals(target, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Removing Folder: {subDir}");

                    try
                    {
                        Directory.Delete(subDir, true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Skipping locked folder: {subDir}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete {subDir}: {ex.Message}");
                    }
                }
                else
                {
                    RemoveFoldersRecursively(subDir, target);
                }
            }
        }
    }
}
