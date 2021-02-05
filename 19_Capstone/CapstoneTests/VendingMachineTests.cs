using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [DataTestMethod]
        [DataRow("B3", "5", "Successfully purchased Wonka Bar, remaining balance: 3.50\nMunch Munch, Yum!")]
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
        [DataRow("C4", "1", "Successfully purchased Heavy, remaining balance: 0.00\nGlug Glug, Yum!")]
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

        [DataTestMethod]
        [DataRow("1", "Added $1.00 to balance.")]
        [DataRow("5", "Added $5.00 to balance.")]
        [DataRow("2", "Added $2.00 to balance.")]
        [DataRow("10", "Added $10.00 to balance.")]
        [DataRow("20", "Please enter valid dollar amount.")]
        [DataRow("", "Please enter valid dollar amount.")]
        [DataRow("-2", "Please enter valid dollar amount.")]
        [DataRow("Q", "Please enter valid dollar amount.")]
        [DataRow("0", "Please enter valid dollar amount.")]
        [DataRow("7", "Please enter valid dollar amount.")]
        public void FeedMoneyTest(string inputString, string expectedOutput)
        {
            //VendingItem vi = new VendingItem("Crunchie", ItemType.Candy, 1.75M, "B4");
            VendingMachine vm = new VendingMachine();
          
            //Act
            string actualOutput = vm.FeedMoney(inputString);

            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]

        public void GetItemsToDisplayTestHappyPath()
        {
            VendingMachine vm = new VendingMachine();
            List<string> expectedOutput = new List<string>(){
             "A1 | 3.05 | Potato Crisps",
             "A2 | 1.45 | Stackers",
             "A3 | 2.75 | Grain Waves",
             "A4 | 3.65 | Cloud Popcorn",
             "B1 | 1.80 | Moonpie",
             "B2 | 1.50 | Cowtales",
             "B3 | 1.50 | Wonka Bar",
             "B4 | 1.75 | Crunchie",
             "C1 | 1.25 | Cola",
             "C2 | 1.50 | Dr. Salt",
             "C3 | 1.50 | Mountain Melter",
             "C4 | 1.50 | Heavy",
             "D1 | 0.85 | U-Chews",
             "D2 | 0.95 | Little League Chew",
             "D3 | 0.75 | Chiclets",
             "D4 | 0.75 | Triplemint"
            };

            List<string> actualOutput = vm.GetItemsToDisplay();
            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void GetItemsToDisplayIfSoldOut()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney("10");
            vm.PurchaseItem("D3");
            vm.PurchaseItem("D3");
            vm.PurchaseItem("D3");
            vm.PurchaseItem("D3");
            vm.PurchaseItem("D3");
            
            List<string> expectedOutput = new List<string>(){
             "A1 | 3.05 | Potato Crisps",
             "A2 | 1.45 | Stackers",
             "A3 | 2.75 | Grain Waves",
             "A4 | 3.65 | Cloud Popcorn",
             "B1 | 1.80 | Moonpie",
             "B2 | 1.50 | Cowtales",
             "B3 | 1.50 | Wonka Bar",
             "B4 | 1.75 | Crunchie",
             "C1 | 1.25 | Cola",
             "C2 | 1.50 | Dr. Salt",
             "C3 | 1.50 | Mountain Melter",
             "C4 | 1.50 | Heavy",
             "D1 | 0.85 | U-Chews",
             "D2 | 0.95 | Little League Chew",
             "D3 | 0.75 | Chiclets | SOLD OUT",
             "D4 | 0.75 | Triplemint"
            };

            List<string> actualOutput = vm.GetItemsToDisplay();
            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }
        
        [TestMethod]
        public void FinishTransactionTests()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney("5");
            vm.PurchaseItem("A3");
            //Act
            string expectedOutput = $"Here's your change. 9 quarters";
            string actualOutput = vm.FinishTransaction();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void FinishTransactionTestsDimesNickles()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney("1");
            vm.PurchaseItem("D1");
            //Act
            string expectedOutput = $"Here's your change. 1 dime 1 nickel";
            string actualOutput = vm.FinishTransaction();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void FinishTransactionTestsExactChange()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney("1");
            vm.FeedMoney("1");
            vm.FeedMoney("1");
            vm.PurchaseItem("C4");
            vm.PurchaseItem("C4");

            //Act
            string expectedOutput = $"Here's your change.";
            string actualOutput = vm.FinishTransaction();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }


    }
}
