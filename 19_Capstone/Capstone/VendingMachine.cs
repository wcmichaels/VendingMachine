using System;
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
    }
}
