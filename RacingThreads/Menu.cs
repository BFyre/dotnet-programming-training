using System.Collections.Generic;
using ReadWriteHandler.Menu;

namespace RacingThreads
{
    public class Menu : IMenu
    {
        public List<MenuOption> GetOptions()
        {
            return new List<MenuOption>
            {
                new MenuOption("Run threadracing code", () => RunRacingThreadsExample(RacingThreadsHandling.None)),
                new MenuOption("Run non-threadracing code (lock)", () => RunRacingThreadsExample(RacingThreadsHandling.Lock)),
                new MenuOption("Run non-threadracing code (auto reset events)", () => RunRacingThreadsExample(RacingThreadsHandling.AutoResetEvents))
            };
        }

        private void RunRacingThreadsExample(RacingThreadsHandling racingThreadsHandling)
        {
            new RacingThreadsExample().Run(racingThreadsHandling);
        }
    }
}