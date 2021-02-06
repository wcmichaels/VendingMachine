using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingItem
    {
        public string Name { get;}
        public ItemType Type { get;}
        public decimal Price { get; }
        public string Location { get;}
        public int Inventory { get; private set; } = 5;
        public int TotalSold { get; private set;}

        public VendingItem(string name, ItemType type, decimal price, string location)
        {
            this.Name = name;
            this.Type = type;
            this.Price = price;
            this.Location = location;
        }

        public void RemoveOneItemFromInventory()
        {
            if (Inventory >= 1)
            {
                this.Inventory--;
            }
        }

        public void AddOneItemToTotalSold()
        {
            TotalSold++;
        }
    }
}
