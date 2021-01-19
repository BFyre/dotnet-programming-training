using ReadWriteHandler.InputOutput;
using ReadWriteHandler.Menu;

namespace MainConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            IOutputHandler outputHandler = new ConsoleHandler();
            IMenu menu = new MainMenu(outputHandler);
            
            outputHandler.Print(menu.GetOptions());
        }
    }
}