using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests
{
    [TestClass()]
    public class GameLogTests
    {
        [TestMethod()]
        public void CheckSurroundingTest()
        {
            GameLog gameLog = new GameLog();
            int howManyMines = 0;
            int[] RightColumn = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(1, howManyMines);
        }
    }
}