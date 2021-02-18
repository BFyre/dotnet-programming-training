using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProcessing
{
    /// <summary>
    /// Very simple system to simulate game turns that should pass in given intervals, but also should wait if processing the "game logic" is taking more time than the interval.
    /// </summary>
public class GameTurnsController
{
    private int _turn;

    public async Task PassTurns(TimeSpan interval, CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Console.WriteLine("Beginning turn: " + _turn++);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await Task.WhenAll(Task.Delay(interval, cancellationToken), LongRunningOperation(cancellationToken));

            stopwatch.Stop();
            Console.WriteLine($"Turn finished, duration = {stopwatch.ElapsedMilliseconds}ms");
        }
    }

    // expensive method example
    private async Task<long> LongRunningOperation(CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Random r = new Random();
            int n = r.Next(80000, 150001);

            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1; // to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }

                    b++;
                }

                if (prime > 0)
                {
                    count++;
                }

                a++;
            }

            long result = --a;

            stopwatch.Stop();
            Console.WriteLine($"Turn calculation finished, duration = {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }, cancellationToken);
    }
}
}