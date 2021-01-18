using System.Collections.Generic;

namespace ReadWriteHandler.Menu
{
    public interface IMenu
    {
        List<MenuOption> GetOptions();
    }
}