using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class SingleShotTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var singleShot = new SingleShot();

            // Act
            SquareShotBehaviour result = new SquareShotBehaviour(1);

            // Assert
            Assert.AreEqual(singleShot.shotBeh, result);
        }
    }
}
