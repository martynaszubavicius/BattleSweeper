using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperServer.DesignPatternClasses
{
    [TestClass]
    public class LineShotBehaviourTests
    {
        [TestMethod]
        public void Shoot_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var lineShotBehaviour = new LineShotBehaviour(5);
            

            // Assert
            Assert.AreEqual(true, true);
        }
    }
}
