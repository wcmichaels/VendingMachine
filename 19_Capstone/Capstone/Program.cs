using Capstone.CLI;
using System;

namespace Capstone
{
    class Program
    {
        /****************************************************************************************
         * Notes on this Capstone solution:
         *      This solution contains both a project for the Vending Machine program (Capstone)
         *      and a project for tests (CapstoneTests). The Test project already references the
         *      Capstone project, so all you need to do is add Test Classes and Test Methods.
         *      
         *      ConsoleMenuFramework has been added via Nuget, so the project is ready to derive
         *      new menus. There is already a sample menu in the CLI folder. You can rename this 
         *      one, or create a new one to get started.
         * 
         * *************************************************************************************/
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();

            MainMenu mainMenu = new MainMenu(vm);
            Console.WindowWidth = Console.LargestWindowWidth - 20;
            mainMenu.Welcome();
            mainMenu.Show();
        }
    }
}
