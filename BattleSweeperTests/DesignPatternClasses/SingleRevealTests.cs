using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class SingleRevealTests
    {
        [TestMethod]
        public void SingleReveal_OnReveal()
        {
            // Arrange
            var singleReveal = new SingleReveal();
            Board board = new Board(10);
            int x = 0;
            int y = 0;

            // Act
            var result = singleReveal.OnReveal(board, x, y);

            ChangePointLeaf expected = new ChangePointLeaf(0, 0);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
