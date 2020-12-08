using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class WideRevealTests
    {
        [TestMethod]
        public void WideReveal_OnReveal_Corner()
        {
            // Arrange
            var wideReveal = new WideReveal();
            Board board = new Board(10);
            int x = 0;
            int y = 0;

            // Act
            var result = wideReveal.OnReveal(board, x, y);

            ChangePointComposite expected = new ChangePointComposite(0, 0);
            expected.Add(new ChangePointLeaf(0, 1));
            expected.Add(new ChangePointLeaf(1, 0));
            expected.Add(new ChangePointLeaf(1, 1));

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WideReveal_OnReveal_Inside()
        {
            // Arrange
            var wideReveal = new WideReveal();
            Board board = new Board(10);
            int x = 2;
            int y = 2;

            // Act
            var result = wideReveal.OnReveal(board, x, y);

            ChangePointComposite expected = new ChangePointComposite(2, 2);
            expected.Add(new ChangePointLeaf(2, 3));
            expected.Add(new ChangePointLeaf(3, 2));
            expected.Add(new ChangePointLeaf(3, 3));

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
