using System;
using System.Linq;
using Xunit;

namespace GameLogic
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            GameLog gameLog = new GameLog();
            
            int[] RandomNumber = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int index = 4;
            int[] Board = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int x = 1;
            int[] BlackCells = { 22, 23, 24, 25, 26 };
            Assert.True(!RandomNumber.Contains(index + x) && Board.Contains(index + x) && !BlackCells.Contains(index + x), "test");
            
            
            
        }
    }
}
