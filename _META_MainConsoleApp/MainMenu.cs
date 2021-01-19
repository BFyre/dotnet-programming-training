using System.Collections.Generic;
using ReadWriteHandler.InputOutput;
using ReadWriteHandler.Menu;

namespace MainConsoleApp
{
    public class MainMenu : IMenu
    {
        private readonly IOutputHandler _outputHandler;
        
        public MainMenu(IOutputHandler outputHandler)
        {
            _outputHandler = outputHandler;
        }
        
        public List<MenuOption> GetOptions()
        {
            return new List<MenuOption>
            {
                new MenuOption("Racing threads", () => _outputHandler.Print(new RacingThreads.Menu().GetOptions())),
                new MenuOption("Covariance and Contravariance", () => _outputHandler.Print(new CovarianceContravariance.Menu().GetOptions()))
            };
        }
    }
}