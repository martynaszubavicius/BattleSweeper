using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void Clone_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var tile = new Tile(0);
            tile.Mine = new SimpleMine();

            // Act
            var result = tile.Clone();

            var expected = new Tile(0);
            expected.Mine = new SimpleMine();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var tile = new Tile(-1);

            // Act
            var result = tile.ToString();

            var expected = "State: -1, Mine: 0";

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
