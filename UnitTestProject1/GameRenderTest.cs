using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using GameOfLife.Core; 
using GameOfLife.UI;

namespace UnitTestProject1
{
    [TestClass]
    public class GameRenderTest
    {
        private Universe _universe;
        private int _cellSize;

        [TestInitialize]
        public void Setup()
        {
            _universe = new Universe(width: 10, height: 10);
            _cellSize = 10;
        }

        [TestMethod]
        public void GetScreenX_TopLeftPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.TopLeft);
            float screenX = renderer.GetScreenX(x: 5);
            Assert.AreEqual(5 * _cellSize, screenX);
        }

        [TestMethod]
        public void GetScreenY_TopLeftPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.TopLeft);
            float screenY = renderer.GetScreenY(y: 5);
            Assert.AreEqual(5 * _cellSize, screenY);
        }

        [TestMethod]
        public void GetScreenX_BottomLeftPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.BottomLeft);
            float screenX = renderer.GetScreenX(x: 5);
            Assert.AreEqual(5 * _cellSize, screenX);
        }

        [TestMethod]
        public void GetScreenY_BottomLeftPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.BottomLeft);
            float screenY = renderer.GetScreenY(y: 5);
            Assert.AreEqual((_universe.Height - 5 - 1) * _cellSize, screenY);
        }

        [TestMethod]
        public void GetScreenX_TopRightPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.TopRight);
            float screenX = renderer.GetScreenX(x: 5);
            Assert.AreEqual((_universe.Width - 5 - 1) * _cellSize, screenX);
        }

        [TestMethod]
        public void GetScreenY_TopRightPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.TopRight);
            float screenY = renderer.GetScreenY(y: 5);
            Assert.AreEqual(5 * _cellSize, screenY);
        }

        [TestMethod]
        public void GetScreenX_BottomRightPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.BottomRight);
            float screenX = renderer.GetScreenX(x: 5);
            Assert.AreEqual((_universe.Width - 5 - 1) * _cellSize, screenX);
        }

        [TestMethod]
        public void GetScreenY_BottomRightPerspective_ReturnsCorrectCoordinate()
        {
            var renderer = new GameRenderer(_universe, _cellSize, ViewPerspective.BottomRight);
            float screenY = renderer.GetScreenY(y: 5);
            Assert.AreEqual((_universe.Height - 5 - 1) * _cellSize, screenY);
        }
    }
}