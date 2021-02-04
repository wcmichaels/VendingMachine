﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class VendingMachine
    {
        public List<VendingItem> Items { get; set; } = new List<VendingItem>();
        public decimal CurrentBalance { get; set; } = 0;

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
                        VendingItem vendingItem = new VendingItem();

                        string line = sr.ReadLine();

                        string[] splits = line.Split("|");

                        vendingItem.Location = splits[0];
                        vendingItem.Name = splits[1];
                        vendingItem.Price = decimal.Parse(splits[2]);
                        vendingItem.Type = Enum.Parse<ItemType>(splits[3]);

                        this.Items.Add(vendingItem);

                    }
                }
            }
            catch (Exception ex)
            {
                // TODO - finish catch

                Console.WriteLine("Sorry we had a problem loading the file.");

            }

        }

        public string PurchaseItem(string userInput)
        {

            foreach (VendingItem item in this.Items)
            {

                if (item.Location.ToLower() == userInput.ToLower())
                {
                    if (item.Inventory > 0 && this.CurrentBalance > item.Price)
                    {
                        this.CurrentBalance -= item.Price;
                        string itemTypeMessage = GetItemType(item);

                        return $"Successfully purchased {item.Name}, remaining balance: {this.CurrentBalance}";
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
            else if (item.Type == ItemType.Chip) {
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
                    CurrentBalance += moneyToAdd;
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
                } else
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

            int numberOfCents = (int)CurrentBalance * 100;
            int quarters = numberOfCents / 25;
            int centsRemaining = numberOfCents % 25;
            int dimes = centsRemaining / 10;
            centsRemaining = centsRemaining % 10;
            int nickels = centsRemaining / 5;
            centsRemaining = centsRemaining % 5;
            int pennies = centsRemaining;
            CurrentBalance = 0;

            //TODO if no pennies or nickels or dimes, update string accordingly (if else returns)

            return ($"Here's your change. {quarters} quarters, {dimes} dimes, {nickels} nickels, and {pennies} pennies.");
        }
    }
}
