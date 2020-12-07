using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class NineShotTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var nineShot = new NineShot();

            // Act
            SquareShotBehaviour result = new SquareShotBehaviour(3);

            // Assert
            Assert.AreEqual(nineShot.shotBeh, result);
        }
    }
}
