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
        }
    }
}
