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
    }
}
