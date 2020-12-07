using BattleSweeperServer.Models;
using BattleSweeperServer.DesignPatternClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.DesignPatternClasses
{
    [TestClass]
    public class ScatterShotBehaviourTests
    {
        [TestMethod]
        public void Shoot_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var scatterShotBehaviour = new ScatterShotBehaviour();
            Board board = new Board(10);
            int x = 2;
            int y = 2;

            // Act
            var result = scatterShotBehaviour.Shoot(
                board,
                x,
                y);

            ChangePointComposite comp = new ChangePointComposite(2, 2);
            comp.Add(new ChangePointLeaf(0, 0));
            comp.Add(new ChangePointLeaf(0, 2));
            comp.Add(new ChangePointLeaf(0, 4));
            comp.Add(new ChangePointLeaf(2, 0));
            comp.Add(new ChangePointLeaf(2, 2));
            comp.Add(new ChangePointLeaf(2, 4));
            comp.Add(new ChangePointLeaf(4, 0));
            comp.Add(new ChangePointLeaf(4, 2));
            comp.Add(new ChangePointLeaf(4, 4));

            // Assert
            Assert.AreEqual(result, comp);
        }
    }
}
