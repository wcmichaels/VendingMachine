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
            throw new NotImplementedException();
        }

        private MenuOptionResult FinishTransaction()
        {
            throw new NotImplementedException();
        }

        private MenuOptionResult FeedMoney()
        {
            throw new NotImplementedException();
        }
    }

}
