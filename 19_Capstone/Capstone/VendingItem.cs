using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingItem
    {

        /// <summary>
        /// Product name of vending item.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Product category, with options being Chip, Candy, Drink, and Gum.
        /// </summary>
        public ItemType Type { get; }

        /// <summary>
        /// Price of vending item.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Alpha numeric code representing slot in vending machine (ex: A3)
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Inventory of a vending item.
        /// </summary>
        public int Inventory { get; private set; } = 5;


        public VendingItem(string name, ItemType type, decimal price, string location)
        {
            this.Name = name;
            this.Type = type;
            this.Price = price;
            this.Location = location;
        }

        /// <summary>
        /// Removes one item from Inventory if inventory above 0.
        /// </summary>
        public int RemoveOneItemFromInventory()
        {
            if (Inventory >= 1)
            {
                this.Inventory--;
            }

            return this.Inventory;
        }
    }
}
