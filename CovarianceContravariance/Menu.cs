using System.Collections.Generic;
using ReadWriteHandler.Menu;

namespace CovarianceContravariance
{
    public class Menu : IMenu
    {
        public List<MenuOption> GetOptions()
        {
            return new List<MenuOption>
            {
                new MenuOption("Show simple inheritance behavior", ShowTest1),
                new MenuOption("Show interface with generic type parameter", ShowTest2),
                new MenuOption("Show covariance/contravariance", ShowTest3)
            };
        }

        private void ShowTest1()
        {
            new CovarianceAndContravariance().Test1();
        }

        private void ShowTest2()
        {
            new CovarianceAndContravariance().Test2();
        }
        
        private void ShowTest3()
        {
            new CovarianceAndContravariance().Test3();
        }
    }
}