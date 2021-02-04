using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class MainMenu : MenuFramework.ConsoleMenu
    {
        /*******************************************************************************
         * Private data:
         * Usually, a menu has to hold a reference to some type of "business objects",
         * on which all of the actions requested by the user are performed. A common 
         * technique would be to declare those private fields here, and then pass them
         * in through the constructor of the menu.
         * ****************************************************************************/

        // NOTE: This constructor could be changed to accept arguments needed by the menu

        private VendingMachine vm; //user only needs to access methods we're using in this class
        public MainMenu(VendingMachine vm) //passed new vm created in Program
        {
           this.vm = vm;

            this.AddOption("Display Items", DisplayItems);
            this.AddOption("Make Purchase", PurchaseItems);
            this.AddOption("Exit", Exit);

          
        }

        private MenuOptionResult PurchaseItems()
        {
            SubMenu subMenu = new SubMenu(vm);
            while (true)
            {
                subMenu.Show();
            }
        }

        private MenuOptionResult DisplayItems()
        {
            List<string> ItemsToDisplay = vm.GetItemsToDisplay();
            foreach (string thing in ItemsToDisplay)
            {
                Console.WriteLine(thing);
            }
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        protected override void OnBeforeShow()
        {
            {
                Console.WriteLine(@" __  __       _         __  __                  ");
                Console.WriteLine(@"|  \/  | __ _(_)_ __   |  \/  | ___ _ __  _   _ ");
                Console.WriteLine(@"| |\/| |/ _` | | '_ \  | |\/| |/ _ \ '_ \| | | |");
                Console.WriteLine(@"| |  | | (_| | | | | | | |  | |  __/ | | | |_| |");
                Console.WriteLine(@"|_|  |_|\__,_|_|_| |_| |_|  |_|\___|_| |_|\__,_|");
                Console.WriteLine();
            }
        }

    }
}
