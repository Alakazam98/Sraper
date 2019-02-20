using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class GameLog
    {
        /// <summary>
        /// Pola sąsiadujące z kliknietym polem
        /// </summary>
        public enum SurroundingCells
        {
            TopLeft = -11,
            Top = -10,
            TopRight = -9,
            Left = -1,
            Right = 1,
            BottomLeft = 9,
            Bottom = 10,
            BottomRight = 11
        }
        /// <summary>
        /// Sprawdza otoczenie na występowanie bomb
        /// </summary>
        /// <param name="button"></param>
        public int CheckSurrounding(int index, int[] RandomNumber, int[] LeftColumn, int[] RightColumn)
        {
            int howManyMines = 0;


            if (RandomNumber.Contains(index + (int)SurroundingCells.TopLeft) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.Top))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.TopRight) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.Right) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.BottomRight) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.Bottom))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.BottomLeft) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(index + (int)SurroundingCells.Left) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }

            return howManyMines;

        }
        /// <summary>
        /// Sprawdza czy pole jest puste, nie zawiera bomb i jest na planszy
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="RandomNumber"></param>
        /// <param name="Board"></param>
        /// <param name="BlackCells"></param>
        /// <returns></returns>
        public bool FreeCellsCondition(int index, int x, int[] RandomNumber, int[] Board, List<int> BlackCells)
        {
            if (!RandomNumber.Contains(index + x) && Board.Contains(index + x) && !BlackCells.Contains(index + x))
            {
                return true;
            }
            else
                return false;
        }


    }
}
