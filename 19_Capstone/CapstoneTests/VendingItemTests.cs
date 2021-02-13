using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class VendingItemTests
    {
        [TestMethod]
        public void RemoveOneItemFromInventoryTest_HappyPath()
        {
            // Arrange
            VendingItem vi = new VendingItem("Potato Crisps", ItemType.Chip, 3.05M, "A1");
            int expectedResult = 4;

            // Act
            int actualResult = vi.RemoveOneItemFromInventory();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveOneItemFromInventoryTest_One_To_Zero()
        {
            // Arrange
            VendingItem vi = new VendingItem("Potato Crisps", ItemType.Chip, 3.05M, "A1");
            int expectedResult = 0;

            // Act
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            int actualResult = vi.RemoveOneItemFromInventory();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveOneItemFromInventoryTest_Zero_To_Zero()
        {
            // Arrange
            VendingItem vi = new VendingItem("Potato Crisps", ItemType.Chip, 3.05M, "A1");
            int expectedResult = 0;

            // Act
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            vi.RemoveOneItemFromInventory();
            int actualResult = vi.RemoveOneItemFromInventory();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetItemTypeMessage_Candy()
        {
            // Arrange
            VendingItem vi = new VendingItem("Snickers", ItemType.Candy, 3.05M, "A1");
            string expectedOutput = "Munch Munch, Yum!";

            // Act
            string actualOutput = vi.GetItemTypeMessage();

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void GetItemTypeMessage_Chip()
        {
            // Arrange
            VendingItem vi = new VendingItem("Lays", ItemType.Chip, 1.85M, "A4");
            string expectedOutput = "Crunch Crunch, Yum!";

            // Act
            string actualOutput = vi.GetItemTypeMessage();

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void GetItemTypeMessage_Drink()
        {
            // Arrange
            VendingItem vi = new VendingItem("Coca Cola", ItemType.Drink, 1.50M, "B1");
            string expectedOutput = "Glug Glug, Yum!";

            // Act
            string actualOutput = vi.GetItemTypeMessage();

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void GetItemTypeMessage_Gum()
        {
            // Arrange
            VendingItem vi = new VendingItem("Trident Mint", ItemType.Gum, 1.1M, "D1");
            string expectedOutput = "Chew Chew, Yum!";

            // Act
            string actualOutput = vi.GetItemTypeMessage();

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
