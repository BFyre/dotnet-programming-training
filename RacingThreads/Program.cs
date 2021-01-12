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
                    new RacingThreadsTest().Run(RacingThreadsHandlingType.None);
                    break;
                case ConsoleKey.D2:
                    new RacingThreadsTest().Run(RacingThreadsHandlingType.Lock);
                    break;
                case ConsoleKey.D3:
                    new RacingThreadsTest().Run(RacingThreadsHandlingType.AutoResetEvents);
                    break;
            }
        }
    }
}