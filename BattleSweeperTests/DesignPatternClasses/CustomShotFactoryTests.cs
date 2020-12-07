using BattleSweeperServer.Models;
using BattleSweeperServer.DesignPatternClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class CustomShotFactoryTests
    {
        [TestMethod]
        public void CreateShot_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var factory = new CustomShotFactory();
            string shotType = "LineShot";

            // Act
            var result = factory.CreateShot(
                shotType);

            // Assert
            Assert.IsTrue(result is LineShot);
        }
    }
}
