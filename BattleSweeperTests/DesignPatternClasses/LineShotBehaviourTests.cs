using BattleSweeperServer.Models;
using BattleSweeperServer.DesignPatternClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class LineShotBehaviourTests
    {
        [TestMethod]
        public void Shoot_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var lineShotBehaviour = new LineShotBehaviour(5);
            Board board = new Board(10);
            int x = 0;
            int y = 0;

            //// Act
            var result = lineShotBehaviour.Shoot(
                board,
                x,
                y);

            ChangePointComposite comp = new ChangePointComposite(0, 0);
            comp.Add(new ChangePointLeaf(0, 0));
            comp.Add(new ChangePointLeaf(1, 0));
            comp.Add(new ChangePointLeaf(2, 0));

            // Assert
            Assert.AreEqual(result, comp);
        }

        [TestMethod]
        public void Shoot_StateUnderTest_aaaExpectedBehavior()
        {
            // Arrange
            var lineShotBehaviour = new LineShotBehaviour(5);

            // Assert
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void aaaFail()
        {
            // Arrange
            var lineShotBehaviour = new LineShotBehaviour(5);
            // Assert
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void aaadfsgbsdfgsdFail()
        {
            // Arrange
            var lineShotBehaviour = new LineShotBehaviour(5);
            // Assert
            Assert.AreEqual(2, 2);
        }
    }
}
