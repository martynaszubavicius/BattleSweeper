using BattleSweeperServer.Models;
using BattleSweeperServer.DesignPatternClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class SquareShotBehaviourTests
    {
        [TestMethod]
        public void Shoot_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var squareShotBehaviour = new SquareShotBehaviour(2);
            Board board = new Board(10);
            int x = 0;
            int y = 0;

            // Act
            var result = squareShotBehaviour.Shoot(
                board,
                x,
                y);

            ChangePointComposite comp = new ChangePointComposite(0, 0);
            comp.Add(new ChangePointLeaf(0, 0));
            comp.Add(new ChangePointLeaf(0, 1));
            comp.Add(new ChangePointLeaf(1, 0));
            comp.Add(new ChangePointLeaf(1, 1));

            // Assert
            Assert.AreEqual(result,comp);
        }
    }
}
