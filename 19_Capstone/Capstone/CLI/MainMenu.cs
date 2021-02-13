using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class MainMenu : MenuFramework.ConsoleMenu
    {
        private VendingMachine vm;
        public MainMenu(VendingMachine vm) 
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
            List<string> itemsToDisplay = vm.GetItemsToDisplay();
            foreach (string item in itemsToDisplay)
            {
                Console.WriteLine(item);
            }
            return MenuOptionResult.WaitAfterMenuSelection;
        }

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

    }
}
