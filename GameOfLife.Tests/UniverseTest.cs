using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Core;
using System;

namespace GameOfLife.Tests
{
    [TestClass]
    [DoNotParallelize]
    public sealed class UniverseTest
    {
        [TestMethod]
        public void IsWithinBounds_ReturnsTrueForValidCoordinates()
        {
            // Arrange
            var universe = new Universe(10, 10);

            // Act & Assert
            Assert.IsTrue(universe.IsWithinBounds(0, 0));
            Assert.IsTrue(universe.IsWithinBounds(5, 5));
            Assert.IsTrue(universe.IsWithinBounds(9, 9));
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsFalseForInvalidCoordinates()
        {
            // Arrange
            var universe = new Universe(10, 10);

            // Act & Assert
            Assert.IsFalse(universe.IsWithinBounds(-1, 0));
            Assert.IsFalse(universe.IsWithinBounds(0, -1));
            Assert.IsFalse(universe.IsWithinBounds(10, 0));
            Assert.IsFalse(universe.IsWithinBounds(0, 10));
        }

        [TestMethod]
        public void CountAliveNeighbors_ReturnsZeroForEmptyUniverse()
        {
            // Arrange
            var universe = new Universe(3, 3);

            // Act
            var count = universe.CountAliveNeighbors(1, 1);

            // Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CountAliveNeighbors_ReturnsCorrectCountForCenterCell()
        {
            // Arrange
            var universe = new Universe(3, 3);
            var cells = universe.GetCells();
            cells[0, 0].IsAlive = true; // Top-left
            cells[1, 0].IsAlive = true; // Top-center
            cells[2, 1].IsAlive = true; // Right-center

            // Act
            var count = universe.CountAliveNeighbors(1, 1);

            // Assert
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void NextGeneration_StablePatternRemainsUnchanged()
        {
            // Arrange - create a stable block pattern
            var universe = new Universe(4, 4);
            var cells = universe.GetCells();
            cells[1, 1].IsAlive = true;
            cells[1, 2].IsAlive = true;
            cells[2, 1].IsAlive = true;
            cells[2, 2].IsAlive = true;

            // Act
            universe.NextGeneration();

            // Assert
            Assert.IsTrue(cells[1, 1].IsAlive);
            Assert.IsTrue(cells[1, 2].IsAlive);
            Assert.IsTrue(cells[2, 1].IsAlive);
            Assert.IsTrue(cells[2, 2].IsAlive);
        }

        [TestMethod]
        public void IsExtinct_ReturnsTrueForEmptyUniverse()
        {
            // Arrange
            var universe = new Universe(3, 3);

            // Act & Assert
            Assert.IsTrue(universe.IsExtinct());
        }

        [TestMethod]
        public void IsExtinct_ReturnsFalseForNonEmptyUniverse()
        {
            // Arrange
            var universe = new Universe(3, 3);
            universe.GetCells()[1, 1].IsAlive = true;

            // Act & Assert
            Assert.IsFalse(universe.IsExtinct());
        }

        [TestMethod]
        public void Clear_ResetsAllCellsToDead()
        {
            // Arrange
            var universe = new Universe(3, 3);
            universe.GetCells()[0, 0].IsAlive = true;
            universe.GetCells()[1, 1].IsAlive = true;

            // Act
            universe.Clear();

            // Assert
            Assert.AreEqual(0, universe.CountAliveCells());
        }

        [TestMethod]
        public void Randomize_SetsRandomCellStates()
        {
            // Arrange
            var universe = new Universe(10, 10);
            var random = new Random(42); // Fixed seed for reproducibility

            // Act
            universe.Randomize(random);

            // Assert
            int aliveCount = universe.CountAliveCells();
            Assert.IsTrue(aliveCount > 0 && aliveCount < 100);
        }

        [TestMethod]
        public void CountDeaths_ReturnsCorrectNumberOfDeaths()
        {
            // Arrange
            var universe = new Universe(3, 3);
            universe.GetCells()[0, 0].IsAlive = true;
            universe.GetCells()[1, 1].IsAlive = true;
            var previousState = universe.GetCurrentState();

            // Kill one cell
            universe.GetCells()[0, 0].IsAlive = false;

            // Act
            var deaths = universe.CountDeaths(previousState);

            // Assert
            Assert.AreEqual(1, deaths);
        }

        [TestMethod]
        public void SaveStateAndLoadState_PreservesCellStates()
        {
            // Arrange
            var universe = new Universe(3, 3);
            universe.GetCells()[0, 0].IsAlive = true;
            universe.GetCells()[1, 1].IsAlive = true;

            // Act
            var savedState = universe.SaveState(5, 10);
            var newUniverse = new Universe(3, 3);
            newUniverse.LoadState(savedState);

            // Assert
            Assert.IsTrue(newUniverse.GetCells()[0, 0].IsAlive);
            Assert.IsTrue(newUniverse.GetCells()[1, 1].IsAlive);
        }
    }
}