using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Core;
using System;

namespace GameOfLife.Tests
{
    [TestClass]
    public sealed class UniverseTests
    {
        [TestMethod]
        public void IsWithinBounds_ReturnsTrueForValidCoordinates()
        {
            var universe = new Universe(10, 10);
            Assert.IsTrue(universe.IsWithinBounds(0, 0));
            Assert.IsTrue(universe.IsWithinBounds(5, 5));
            Assert.IsTrue(universe.IsWithinBounds(9, 9));
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsFalseForInvalidCoordinates()
        {
            var universe = new Universe(10, 10);
            Assert.IsFalse(universe.IsWithinBounds(-1, 0));
            Assert.IsFalse(universe.IsWithinBounds(0, -1));
            Assert.IsFalse(universe.IsWithinBounds(10, 0));
            Assert.IsFalse(universe.IsWithinBounds(0, 10));
        }

        [TestMethod]
        public void CountAliveNeighbors_ReturnsZeroForEmptyUniverse()
        {
            var universe = new Universe(3, 3);
            Assert.AreEqual(0, universe.CountAliveNeighbors(1, 1));
        }

        [TestMethod]
        public void CountAliveNeighbors_ReturnsCorrectCountForCenterCell()
        {
            var universe = new Universe(3, 3);
            var cells = universe.GetCells();

            cells[0, 0].IsAlive = true; 
            cells[1, 0].IsAlive = true; 
            cells[2, 1].IsAlive = true; 

            Assert.AreEqual(3, universe.CountAliveNeighbors(1, 1));
        }

        [TestMethod]
        public void NextGeneration_StablePatternRemainsUnchanged()
        {
            var universe = new Universe(4, 4);
            var cells = universe.GetCells();

            cells[1, 1].IsAlive = true;
            cells[1, 2].IsAlive = true;
            cells[2, 1].IsAlive = true;
            cells[2, 2].IsAlive = true;

            universe.NextGeneration();

            Assert.IsTrue(cells[1, 1].IsAlive);
            Assert.IsTrue(cells[1, 2].IsAlive);
            Assert.IsTrue(cells[2, 1].IsAlive);
            Assert.IsTrue(cells[2, 2].IsAlive);
        }

        [TestMethod]
        public void IsExtinct_ReturnsTrueForEmptyUniverse()
        {
            var universe = new Universe(3, 3);
            Assert.IsTrue(universe.IsExtinct());
        }

        [TestMethod]
        public void IsExtinct_ReturnsFalseForNonEmptyUniverse()
        {
            var universe = new Universe(3, 3);
            universe.GetCells()[1, 1].IsAlive = true;
            Assert.IsFalse(universe.IsExtinct());
        }

        [TestMethod]
        public void IsStable_ReturnsTrueForStablePattern()
        {
            var universe = new Universe(4, 4);
            var cells = universe.GetCells();

            universe.NextGeneration();
            var state1 = universe.GetCurrentState();

            universe.NextGeneration();
            var state2 = universe.GetCurrentState();

            bool manuallyCompared = universe.AreStatesEqual(state1, state2);
            Assert.IsTrue(manuallyCompared, "States should be equal");

            Assert.IsTrue(universe.IsStable());
        }

        [TestMethod]
        public void Clear_ResetsAllCellsToDead()
        {
            var universe = new Universe(3, 3);
            var cells = universe.GetCells();

            cells[0, 0].IsAlive = true;
            cells[1, 1].IsAlive = true;

            universe.Clear();

            Assert.AreEqual(0, universe.CountAliveCells());
        }

        [TestMethod]
        public void Randomize_SetsRandomCellStates()
        {
            var universe = new Universe(10, 10);
            universe.Randomize(new Random(42)); 

            int aliveCount = universe.CountAliveCells();
            Assert.IsTrue(aliveCount > 0 && aliveCount < 100);
        }

        [TestMethod]
        public void SaveStateAndLoadState_PreservesCellStates()
        {
            var universe = new Universe(3, 3);
            var cells = universe.GetCells();

            cells[0, 0].IsAlive = true;
            cells[1, 1].IsAlive = true;

            var savedState = universe.SaveState(1, 0);
            var newUniverse = new Universe(3, 3);
            newUniverse.LoadState(savedState);

            var newCells = newUniverse.GetCells();
            Assert.IsTrue(newCells[0, 0].IsAlive);
            Assert.IsTrue(newCells[1, 1].IsAlive);
        }

        [TestMethod]
        public void CountDeaths_ReturnsCorrectNumberOfDeaths()
        {
            var universe = new Universe(3, 3);
            var cells = universe.GetCells();

            cells[0, 0].IsAlive = true;
            cells[1, 1].IsAlive = true;

            var previousState = universe.GetCurrentState();
            cells[0, 0].IsAlive = false; 

            Assert.AreEqual(1, universe.CountDeaths(previousState));
        }
    }
}