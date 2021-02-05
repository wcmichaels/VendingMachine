using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [DataTestMethod]
        [DataRow("B3", "5", "Successfully purchased Wonka Bar, remaining balance: 3.50")]
        [DataRow("c1", "1", "Please insert more money to purchase this item")]
        [DataRow("", "1", "Please provide a valid location.")]
        [DataRow("d8", "1", "Please provide a valid location.")]
        [DataRow("C", "1", "Please provide a valid location.")]
        public void PurchaseItemTests(string itemLocation, string moneyToAdd, string expectOutput)
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney(moneyToAdd);

            // Act
            string actualOutput = vm.PurchaseItem(itemLocation);

            // Assert
            Assert.AreEqual(expectOutput, actualOutput);
        }

        [DataTestMethod]
        [DataRow("C4", "1", "Successfully purchased Heavy, remaining balance: 0.00")]
        public void PurchaseItemTests_ExactChange(string itemLocation, string moneyToAdd, string expectOutput)
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney(moneyToAdd);
            vm.FeedMoney(moneyToAdd);
            vm.FeedMoney(moneyToAdd);

            // Act
            string actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);

            // Assert
            Assert.AreEqual(expectOutput, actualOutput);
        }

        [TestMethod]
        public void PurchaseItemTest_SoldOut()
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney("10");
            string itemLocation = "D4";
            string expectedOutput = "I'm sorry we do not have this item in stock";

            // Act
            string actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);
            actualOutput = vm.PurchaseItem(itemLocation);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
