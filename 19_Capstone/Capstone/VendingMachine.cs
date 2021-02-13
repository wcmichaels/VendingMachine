using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class VendingMachine
    {
        /// <summary>
        /// List containing all the vending items loaded from text file.
        /// </summary>
        public List<VendingItem> Items { get; private set; } = new List<VendingItem>();

        /// <summary>
        /// Represents the current balance of the vending machine.
        /// </summary>
        public decimal CurrentBalance { get; private set; } = 0;

        public VendingMachine()
        {
            LoadVendingItems();
        }

        /// <summary>
        /// Reads text file and creates a vending item for each line in file.
        /// Adds each vending item to Items property in vending machine.
        /// </summary>
        private void LoadVendingItems()
        {
            string filePath = @"..\..\..\..\vendingmachine.csv";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {

                        string line = sr.ReadLine();

                        string[] splits = line.Split("|");

                        string location = splits[0];
                        string name = splits[1];
                        decimal price = decimal.Parse(splits[2]);
                        ItemType type = Enum.Parse<ItemType>(splits[3]);

                        VendingItem vendingItem = new VendingItem(name, type, price, location);
     
                        this.Items.Add(vendingItem);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry we had a problem loading the file.");
            }
        }

        /// <summary>
        /// Generates report including each vending item with a total
        /// quantity sold of each item spanning history of machine. Includes total sales for machine in dollars. 
        /// </summary>
        public void CreateSalesReport()
        {
            decimal runningSum = 0;
            List<string> logItems = new List<string>();
            Dictionary<VendingItem, int> totalSoldByItem = new Dictionary<VendingItem, int>();

            foreach (VendingItem item in this.Items)
            {
                totalSoldByItem[item] = 0;
            }

            using (StreamReader sr = new StreamReader(@"..\..\..\..\Log.txt"))
            {
                while (!sr.EndOfStream)
                {
                    logItems.Add(sr.ReadLine());
                }
            }

            foreach (string logItem in logItems)
            {
                foreach (VendingItem vendingItem in this.Items)
                {
                    if (logItem.Contains(vendingItem.Name))
                    {
                        totalSoldByItem[vendingItem] += 1;
                        runningSum += vendingItem.Price;
                    }
                }

            }
            string fileDate = DateTime.Now.ToString("MMddyyyy_HHmmsstt");
            using (StreamWriter sw = new StreamWriter($"..\\..\\..\\..\\{fileDate}SalesReport.txt"))
            {
                foreach (VendingItem item in this.Items)
                {

                    sw.WriteLine($"{item.Name}|{totalSoldByItem[item]}");
                }

                sw.WriteLine();
                sw.WriteLine($"TOTAL SALES: {runningSum:c}");
            }
        }

        /// <summary>
        /// Validates if item can be purchased, then executes purchase.
        /// </summary>
        /// <param name="userInput">The alphanumeric location of the vending item to be purchased.</param>
        /// <returns>A string with details of successful purchase, or if not successful a message to the user why couldnt purchase.</returns>
        public string PurchaseItem(string userInput)
        {

            foreach (VendingItem item in this.Items)
            {

                if (item.Location.ToLower() == userInput.ToLower())
                {
                    if (item.Inventory > 0 && this.CurrentBalance >= item.Price)
                    {
                        decimal previousBalanceToLog = CurrentBalance;
                        string logName = $"{item.Name} {item.Location}";
                        this.CurrentBalance -= item.Price;
                        LogTransaction(logName, previousBalanceToLog, CurrentBalance);
                        string itemTypeMessage = item.GetItemTypeMessage();
                        item.RemoveOneItemFromInventory();
                        return $"Successfully purchased {item.Name}, remaining balance: {this.CurrentBalance}\n{itemTypeMessage}";
                    }

                    else if (this.CurrentBalance < item.Price)
                    {
                        return "Please insert more money to purchase this item";
                    }
                    else
                    {
                        return "I'm sorry we do not have this item in stock";
                    }
                }
            }

            return "Please provide a valid location.";
        }

        /// <summary>
        /// Allows money to be added to vending machine balance.
        /// </summary>
        /// <param name="moneyAsString">Money to add to the machine balance as string.</param>
        /// <returns>A message to user indicacating if successfully added to balance.</returns>
        public string FeedMoney(string moneyAsString)
        {
            bool isValidAmount = decimal.TryParse(moneyAsString, out decimal moneyToAdd);

            if (isValidAmount)
            {
                if (moneyToAdd == 1 || moneyToAdd == 2 || moneyToAdd == 5 || moneyToAdd == 10)
                {
                    decimal previousBalanceToLog = CurrentBalance;
                    CurrentBalance += moneyToAdd;
                    LogTransaction("FEED MONEY", previousBalanceToLog, CurrentBalance);
                    return $"Added {moneyToAdd:c} to balance.";
                }
            }

            return "Please enter valid dollar amount.";
        }

        /// <summary>
        /// Retrieves a list of vending items in vending machine.  Indicates if item is sold out.
        /// </summary>
        /// <returns>List of vending items</returns>
        public List<string> GetItemsToDisplay()
        {
            List<string> output = new List<string>();

            foreach (VendingItem item in Items)
            {
                if (item.Inventory == 0)
                {
                    output.Add($"{item.Location} | {item.Price} | {item.Name} | SOLD OUT");
                }
                else
                {
                    output.Add($"{item.Location} | {item.Price} | {item.Name}");
                }

            }
            return output;
        }

        /// <summary>
        /// Clears remaining vending machine balanc to zero and calculates change to give back.
        /// </summary>
        /// <returns>Change as string including number of quarters, dimes, and nickels</returns>
        public string FinishTransaction()
        {
            decimal previousBalanceToLog = CurrentBalance;

            int numberOfCents = (int)(CurrentBalance * 100);
            int quarters = numberOfCents / 25;
            int centsRemaining = numberOfCents % 25;

            int dimes = centsRemaining / 10;
            centsRemaining = centsRemaining % 10;

            int nickels = centsRemaining / 5;

            CurrentBalance = 0;

            LogTransaction("GIVE CHANGE", previousBalanceToLog, CurrentBalance);

            string output = "Here's your change.";
            string qOutput = GetChangeWording(quarters, $"quarter");
            string dOutput = GetChangeWording(dimes, $"dime");
            string nOutput = GetChangeWording(nickels, $"nickel");
            output = $"{output}{qOutput}{dOutput}{nOutput}";
            return output;
        }

        private string GetChangeWording(int numberOfCoins, string coinName)
        {
            if (numberOfCoins == 0)
            {
                return "";
            } else if (numberOfCoins == 1) {
                return $" 1 {coinName}";
            } else
            {
                return $" {numberOfCoins} {coinName}s";
            }
        }
        private void LogTransaction(string logName, decimal previousBalance, decimal currentBalance)
        {
            using (StreamWriter sw = new StreamWriter(@"..\..\..\..\Log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now} {logName} {previousBalance:c} {currentBalance:c}");
            }
        }

    }
}
