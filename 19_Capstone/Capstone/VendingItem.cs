using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingItem
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int Inventory { get; set; } = 5;

    }
}
