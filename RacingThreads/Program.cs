using System;

namespace RacingThreads
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static void ShowMenu()
        {
            Console.Write(
                "Press:" + Environment.NewLine +
                "1. Run threadracing code" + Environment.NewLine +
                "2. Run non-threadracing code with lock" + Environment.NewLine + 
                "3. Run non-threadracing code with auto reset events" + Environment.NewLine
            );

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    new RacingThreadsExample().Run(RacingThreadsHandling.None);
                    break;
                case ConsoleKey.D2:
                    new RacingThreadsExample().Run(RacingThreadsHandling.Lock);
                    break;
                case ConsoleKey.D3:
                    new RacingThreadsExample().Run(RacingThreadsHandling.AutoResetEvents);
                    break;
            }
        }
    }
}