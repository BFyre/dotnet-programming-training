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
        private const int MIN_PRIME_RANGE = 80000;
        private const int MAX_PRIME_RANGE = 150001;

        private int _turn;

        public async Task PassTurns(TimeSpan interval, CancellationToken cancellationToken)
        {
            Random r = new Random();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                Console.WriteLine("Beginning turn: " + _turn++);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                await Task.WhenAll(Task.Delay(interval, cancellationToken), FindPrimeNumber(r.Next(MIN_PRIME_RANGE, MAX_PRIME_RANGE), cancellationToken));

                stopwatch.Stop();
                Console.WriteLine($"Turn finished, duration = {stopwatch.ElapsedMilliseconds}ms");
            }
        }

        // expensive method example
        private async Task<long> FindPrimeNumber(int n, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

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