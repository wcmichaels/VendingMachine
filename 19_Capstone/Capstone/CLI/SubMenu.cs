using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    class SubMenu : MenuFramework.ConsoleMenu
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

            Console.Write("Please select the item location that you would like to purchase: ");
            string userInput = Console.ReadLine();
            string messageToUser = vm.PurchaseItem(userInput);
            Console.WriteLine(messageToUser);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult FinishTransaction()
        {
            //giving change method, prints message, update balance to 0
            //return to main menu
            string messageToUser = vm.FinishTransaction();
            Console.WriteLine(messageToUser);
            return MenuOptionResult.WaitThenCloseAfterSelection;
        }

        private MenuOptionResult FeedMoney()
        {
            Console.Write("Please enter how much money you would like to insert: ");
            string inputString = Console.ReadLine();
            string messageToUser = vm.FeedMoney(inputString);
            Console.WriteLine(messageToUser);

            return MenuOptionResult.WaitAfterMenuSelection;
        }
    }

}
