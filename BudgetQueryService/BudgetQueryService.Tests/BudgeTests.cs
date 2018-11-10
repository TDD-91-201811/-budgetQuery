using System;
using BudgetQueryService.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetQueryService.Tests
{
    [TestClass]
    public class BudgeTests
    {
        private IBudgetRepository _budgetRepository;
        private BudgetService _target;

        [TestMethod]
        public void Invalid_Query()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 2, 1);
            var end = new DateTime(2018,1,31);

            //Act
            var actual = _target.TotalAmount(start, end);

            //Assert
            Assert.AreEqual(0,actual);
        }

        [TestMethod]
        public void DailyQuery()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 1, 1);
            var end = new DateTime(2018, 1, 1);

            //Act
            var actual = _target.TotalAmount(start,end);

            //Assert
            Assert.AreEqual(10,actual);
        }

        [TestMethod]
        public void DailyRangeQuery()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 1, 1);
            var end = new DateTime(2018, 1, 31);

            //Act
            var actual = _target.TotalAmount(start, end);

            //Assert
            Assert.AreEqual(310, actual);
        }

        [TestMethod]
        public void OverOneMonth()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 1, 31);
            var end = new DateTime(2018, 2, 1);

            //Act
            var actual = _target.TotalAmount(start, end);

            //Assert
            Assert.AreEqual(30, actual);
        }

        [TestMethod]
        public void OverManyMonth()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 1, 1);
            var end = new DateTime(2018, 3, 15);

            //Act
            var actual = _target.TotalAmount(start, end);

            //Assert
            Assert.AreEqual(1095, actual);
        }

        [TestMethod]
        public void OverOneYear()
        {
            //Arrange
            _budgetRepository = new StubBudgetRepository();
            _target = new BudgetService(_budgetRepository);
            var start = new DateTime(2018, 1, 1);
            var end = new DateTime(2019, 1, 1);

            //Act
            var actual = _target.TotalAmount(start, end);

            //Assert
            Assert.AreEqual(1335, actual);
        }
    }
}
