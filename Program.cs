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
        }
    }
}
