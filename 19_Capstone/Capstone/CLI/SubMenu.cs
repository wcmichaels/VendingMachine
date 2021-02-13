using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class SubMenu : MenuFramework.ConsoleMenu
    {
        private VendingMachine vm;
        public SubMenu(VendingMachine vm)
        {
            this.vm = vm;

            this.AddOption("Feed Money", FeedMoney);
            this.AddOption("Select Product", SelectProduct);
            this.AddOption("Finish Transaction", FinishTransaction);

        }

        private MenuOptionResult SelectProduct()
        {
            List<string> itemsToDisplay = vm.GetItemsToDisplay();
            foreach (string item in itemsToDisplay)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Please select the item location that you would like to purchase: ");
            string userInput = Console.ReadLine();
            string messageToUser = vm.PurchaseItem(userInput);
            Console.WriteLine();
            Console.WriteLine(messageToUser);
            Console.Beep(699, 500);
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult FinishTransaction()
        {
            Console.WriteLine(@" _______  __   __  _______  __    _  ___   _  _______  __ ");
            Console.WriteLine(@"|       ||  | |  ||   _   ||  |  | ||   | | ||       ||  |");
            Console.WriteLine(@"|_     _||  |_|  ||  |_|  ||   |_| ||   |_| ||  _____||  |");
            Console.WriteLine(@"  |   |  |       ||       ||       ||      _|| |_____ |  |");
            Console.WriteLine(@"  |   |  |       ||       ||  _    ||     |_ |_____  ||__|");
            Console.WriteLine(@"  |   |  |   _   ||   _   || | |   ||    _  | _____| | __ ");
            Console.WriteLine(@"  |___|  |__| |__||__| |__||_|  |__||___| |_||_______||__|");
            Console.WriteLine();
            Console.WriteLine();

            string messageToUser = vm.FinishTransaction();
            Console.WriteLine(messageToUser);
            Jingle();
            return MenuOptionResult.WaitThenCloseAfterSelection;
        }

        private MenuOptionResult FeedMoney()
        {
            Console.WriteLine(@"  __   __  _______  __    _  _______  __   __          _______  ___      _______  _______  _______  _______  __  ");
            Console.WriteLine(@" |  |_|  ||       ||  |  | ||       ||  | |  |        |       ||   |    |       ||   _   ||       ||       ||  |");
            Console.WriteLine(@" |       ||   _   ||   |_| ||    ___||  |_|  |        |    _  ||   |    |    ___||  |_|  ||  _____||    ___||  |");
            Console.WriteLine(@" |       ||  | |  ||       ||   |___ |       |        |   |_| ||   |    |   |___ |       || |_____ |   |___ |  |");
            Console.WriteLine(@" |       ||  |_|  ||  _    ||    ___||_     _| ___    |    ___||   |___ |    ___||       ||_____  ||    ___||__|");
            Console.WriteLine(@" | ||_|| ||       || | |   ||   |___   |   |  |_  |   |   |    |       ||   |___ |   _   | _____| ||   |___  __ ");
            Console.WriteLine(@" |_|   |_||_______||_|  |__||_______|  |___|    |_|   |___|    |_______||_______||__| |__||_______||_______||__|");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("$1, $2, $5, OR $10 BILLS ONLY");
            Console.Write("Please enter how much money you would like to insert: ");
            string inputString = Console.ReadLine();
            string messageToUser = vm.FeedMoney(inputString);
            Console.WriteLine(messageToUser);
            Console.Beep(1109, 250);
            Console.Beep(880, 250);
            return MenuOptionResult.WaitAfterMenuSelection;

         
        }

        private void Jingle()
        {
            Console.Beep(330, 300);
            Console.Beep(1109, 300);
            Console.Beep(880, 300);
        }
    }

}
