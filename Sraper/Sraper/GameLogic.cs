using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sraper
{
    class GameLogic
    {

        readonly char[,] Board = new char[10,10];
         Random rnd = new Random();

        public void  Settingmines()
        {
            int minesCount = 0;

            for (int i = 0; i < 10; i++)
            {
                if(minesCount==10)
                {
                    break;
                }
                for (int j = 0; j < 10; i++)
                {
                    int isThereMine = rnd.Next(1, 2);

                    if (isThereMine == 1)
                    {
                        Board[i, j] = '*';
                        minesCount++;
                    }
                    else
                        continue;


                }

            }

        }
    }
}
