namespace RemoveFoldersRecursively.Dashboard
{
    public static class DashboardUI
    {
        public static void Show(string version)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
            _____                      _____          
           /\    \                    /\    \         
          /::\    \                  /::\    \        
         /::::\    \                /::::\    \       
        /::::::\    \              /::::::\    \      
       /:::/\:::\    \            /:::/\:::\    \     
      /:::/__\:::\    \          /:::/__\:::\    \    
     /::::\   \:::\    \        /::::\   \:::\    \   
    /::::::\   \:::\    \      /::::::\   \:::\    \  
   /:::/\:::\   \:::\____\    /:::/\:::\   \:::\____\ 
  /:::/  \:::\   \:::|    |  /:::/  \:::\   \:::|    |
  \::/   |::::\  /:::|____|  \::/   |::::\  /:::|____|
   \/____|:::::\/:::/    /    \/____|:::::\/:::/    / 
         |:::::::::/    /           |:::::::::/    /  
         |::|\::::/    /            |::|\::::/    /   
         |::| \::/____/             |::| \::/____/    
         |::|  ~|                   |::|  ~|          
         |::|   |                   |::|   |          
         \::|   |                   \::|   |          
          \:|   |                    \:|   |          
           \|___|                     \|___|
          ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("RemoveRecursively CLI");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Version: {version}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nUsage:");
            Console.ResetColor();
            Console.WriteLine("  rr [folder1] [folder2] ... [options]");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nExamples:");
            Console.ResetColor();
            Console.WriteLine("  rr bin obj");
            Console.WriteLine("  rr --help");
            Console.WriteLine("  rr --version");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nOptions:");
            Console.ResetColor();
            Console.WriteLine("  --help,  -h       Show this help message");
            Console.WriteLine("  --version, -v     Show version information");

            Console.WriteLine();
        }
    }
}
