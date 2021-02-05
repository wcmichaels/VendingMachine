using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class VendingMachine
    {
        public List<VendingItem> Items { get; private set; } = new List<VendingItem>();
        public decimal CurrentBalance { get; private set; } = 0;

        public VendingMachine()
        {
            LoadVendingItems();
        }

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
                // TODO - finish catch (possibly make a log file for caught exceptions to catch and write the error to)

                Console.WriteLine("Sorry we had a problem loading the file.");
            }
        }

        public void CreateSalesReport()
        {
            decimal runningSum = 0;
            List<string> logItems = new List<string>();

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
                        runningSum += vendingItem.Price;
                        vendingItem.AddOneItemToTotalSold();
                    }
                }
            }
            string fileDate = DateTime.Now.ToString("MMddyyyy_HHmmsstt");
            using (StreamWriter sw = new StreamWriter($"..\\..\\..\\..\\{fileDate}SalesReport.txt"))
            {
                foreach (VendingItem item in this.Items)
                {
                    sw.WriteLine($"{item.Name}|{item.TotalSold}");
                }

                sw.WriteLine();
                sw.WriteLine($"TOTAL SALES: {runningSum:c}");
            }
        }

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
                        string itemTypeMessage = GetItemType(item);
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

        private string GetItemType(VendingItem item)
        {
            if (item.Type == ItemType.Candy)
            {
                return "Munch Munch, Yum!";
            }
            else if (item.Type == ItemType.Chip)
            {
                return "Crunch Crunch, Yum!";
            }
            else if (item.Type == ItemType.Drink)
            {
                return "Glug Glug, Yum!";
            }
            else
            {
                return "Chew Chew, Yum!";
            }
        }


        public string FeedMoney(string inputString)
        {
            bool isValidAmount = decimal.TryParse(inputString, out decimal moneyToAdd);

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

        public string FinishTransaction()
        {
            //CurrentBalance must be zero
            //3.10 
            //*100 = 310 cents
            //310 % 25 gives us the cents remaining that aren't quarters (10)
            //10 % 10 = 0(1 dime)
            //310 / 25 gives us the number of quarters (12)
            //return 12 quarters and one dime as a string
            decimal previousBalanceToLog = CurrentBalance;

            int numberOfCents = (int)(CurrentBalance * 100);
            int quarters = numberOfCents / 25;
            int centsRemaining = numberOfCents % 25;

            int dimes = centsRemaining / 10;
            centsRemaining = centsRemaining % 10;

            int nickels = centsRemaining / 5;
            centsRemaining = centsRemaining % 5;

            int pennies = centsRemaining;
            CurrentBalance = 0;

            LogTransaction("GIVE CHANGE", previousBalanceToLog, CurrentBalance);

            //TODO if no pennies or nickels or dimes, update string accordingly (if else returns)
            string output = "Here's your change.";
            string qOutput = GetChangeWording(quarters, $"quarter");
            string dOutput = GetChangeWording(dimes, $"dime");
            string nOutput = GetChangeWording(nickels, $"nickel");
            string change = $"{output}{qOutput}{dOutput}{nOutput}";
            return change;
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
