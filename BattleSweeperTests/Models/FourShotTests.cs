using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class FourShotTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var fourShot = new FourShot();

            // Act
            SquareShotBehaviour result = new SquareShotBehaviour(2);

            // Assert
            Assert.AreEqual(fourShot.shotBeh, result);
        }
    }
}
