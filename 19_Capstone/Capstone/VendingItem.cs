using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingItem
    {
        // TODO - Make a lot of these props readonly.  Since we can assign the prop a value when we create it
        // That could include name, type, price, location.  Inventory can maybe be a private set and access
        // it through a public method.  Same with total sold (possibly)
        public string Name { get;}
        public ItemType Type { get;}
        public decimal Price { get; }
        public string Location { get;}
        public int Inventory { get; set; } = 5;
        public int TotalSold { get; set;}

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
