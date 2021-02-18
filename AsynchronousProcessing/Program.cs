using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProcessing
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var gameTurnsController = new GameTurnsController();
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                Task.Run(() =>
                {
                    Console.ReadKey(true);
                    cts.Cancel();
                });

                gameTurnsController.PassTurns(TimeSpan.FromSeconds(1), cts.Token).Wait(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(Environment.NewLine + "Cancelled.");
                Environment.ExitCode = 0;
            }
            
            Console.WriteLine("Execution finished.");
        }
    }
}