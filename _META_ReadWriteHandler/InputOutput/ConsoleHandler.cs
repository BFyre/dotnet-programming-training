using System;
using System.Collections.Generic;
using ReadWriteHandler.Menu;

namespace ReadWriteHandler.InputOutput
{
    public class ConsoleHandler : IOutputHandler
    {
        public void Print(List<MenuOption> menuOptions)
        {
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i].Label}");
            }
            
            var pressedKey = Console.ReadKey().KeyChar;
            var isInt = int.TryParse(pressedKey.ToString(), out int value);
            var isInRange = isInt && value > 0 && value <= menuOptions.Count;

            if (isInt && isInRange && value != 0)
            {
                Console.Clear();
                menuOptions[value - 1].OnSelected?.Invoke();
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Key '{pressedKey}' is not handled. Press any key to abort...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}