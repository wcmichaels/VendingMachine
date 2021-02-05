using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class MainMenu : MenuFramework.ConsoleMenu
    {

        public void Welcome()
        {
            Console.WriteLine(@"__   __  _______  __    _  ______   _______         __   __  _______  _______  ___   _______     _____   _______  _______  ");
            Console.WriteLine(@"|  | |  ||       ||  |  | ||      | |       |       |  |_|  ||   _   ||       ||   | |       |   |  _  | |  _    ||  _    |");
            Console.WriteLine(@"|  |_|  ||    ___||   |_| ||  _    ||   _   | ____  |       ||  |_|  ||_     _||   | |       |   | |_| | | | |   || | |   |");
            Console.WriteLine(@"|       ||   |___ |       || | |   ||  | |  ||____| |       ||       |  |   |  |   | |       |  |   _   || | |   || | |   |");
            Console.WriteLine(@"|       ||    ___||  _    || |_|   ||  |_|  |       |       ||       |  |   |  |   | |      _|  |  | |  || |_|   || |_|   |");
            Console.WriteLine(@" |     | |   |___ | | |   ||       ||       |       | ||_|| ||   _   |  |   |  |   | |     |_   |  |_|  ||       ||       |");
            Console.WriteLine(@"  |___|  |_______||_|  |__||______| |_______|       |_|   |_||__| |__|  |___|  |___| |_______|  |_______||_______||_______|");
            Console.WriteLine();
            Console.WriteLine("Hit Enter to Continue");
            Console.ReadLine();
        }

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
            this.AddOption("", SalesLog);

            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Blue;
                cfg.SelectedItemForegroundColor = ConsoleColor.Green;
                cfg.MenuSelectionMode = MenuSelectionMode.Arrow;
            });

        }


        private MenuOptionResult SalesLog()
        {
            vm.CreateSalesReport();

            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }

        private MenuOptionResult PurchaseItems()
        {
            SubMenu subMenu = new SubMenu(vm);

            subMenu.Show();

            return MenuOptionResult.WaitAfterMenuSelection;

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

        

    }
}
