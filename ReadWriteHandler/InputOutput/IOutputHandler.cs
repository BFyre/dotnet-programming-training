using System.Collections.Generic;
using ReadWriteHandler.Menu;

namespace ReadWriteHandler.InputOutput
{
    public interface IOutputHandler
    {
        void Print(List<MenuOption> menuOptions);
        void Print(string text);
    }
}