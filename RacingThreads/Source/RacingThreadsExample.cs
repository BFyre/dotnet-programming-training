using System;
using System.Threading;

namespace RacingThreads
{
    public class RacingThreadsTest
    {
        private const int ITERATIONS = 10;
        private const int SLEEP_DURATION_MS = 200;
        
        private string _sharedString = "";
        private readonly object _lock = new object();

        public void Run(RacingThreadsHandlingType racingThreadsHandlingType)
        {
            Thread thread1, thread2;
            
            switch (racingThreadsHandlingType)
            {
                case RacingThreadsHandlingType.None:
                    thread1 = new Thread(DisplayThread) {Name = "thread1"};
                    thread2 = new Thread(DisplayThread) {Name = "thread2"};
                    thread1.Start();
                    thread2.Start();
                    break;
                case RacingThreadsHandlingType.Lock:
                    thread1 = new Thread(DisplayThreadWithLock) {Name = "thread1"};
                    thread2 = new Thread(DisplayThreadWithLock) {Name = "thread2"};
                    thread1.Start();
                    thread2.Start();
                    break;
                case RacingThreadsHandlingType.AutoResetEvents:
                    // boolean argument indicates whether the initial state of the event should be set as signaled
                    var thread1LockEvent = new AutoResetEvent(false);
                    var thread2LockEvent = new AutoResetEvent(true);
                    thread1 = new Thread(DisplayThreadWithAutoResetEvent) {Name = "thread1"};
                    thread2 = new Thread(DisplayThreadWithAutoResetEvent) {Name = "thread2"};
                    thread1.Start(new Tuple<AutoResetEvent, AutoResetEvent>(thread1LockEvent, thread2LockEvent));
                    thread2.Start(new Tuple<AutoResetEvent, AutoResetEvent>(thread2LockEvent, thread1LockEvent));
                    break;
            }
        }

        private void DisplayThread()
        {
            var i = 0;
            while (i++ < ITERATIONS)
            {
                DisplayInfo(i);
            }
        }

        private void DisplayThreadWithLock()
        {
            var i = 0;
            while (i++ < ITERATIONS)
            {
                lock (_lock)
                {
                    DisplayInfo(i);
                }
            }
        }

        private void DisplayThreadWithAutoResetEvent(object threadLocks)
        {
            // deconstruct the tuple to get access to lock events
            (AutoResetEvent currentThreadLockEvent, AutoResetEvent secondThreadLockEvent) = (Tuple<AutoResetEvent, AutoResetEvent>) threadLocks;
            var i = 0;
            while (i++ < ITERATIONS)
            {
                // lock the chosen thread until it receives a signal
                secondThreadLockEvent.WaitOne();
                
                DisplayInfo(i);
                
                // send a signal, unlocking waiting threads
                currentThreadLockEvent.Set();
            }
        }

        private void DisplayInfo(int iteration)
        {
            Console.WriteLine(Environment.NewLine + $"Display {Thread.CurrentThread.Name}, iteration " + iteration);
            _sharedString = $"Hello {Thread.CurrentThread.Name}";
                
            // delay to cause potential thread racing condition
            Thread.Sleep(SLEEP_DURATION_MS);
                
            Console.WriteLine($"{Thread.CurrentThread.Name} Output --> {{0}}", _sharedString);
        }
    }
}