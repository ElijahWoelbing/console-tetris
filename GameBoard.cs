using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    static class GameBoard
    {

        static string empty = Program.empty;
        public static string[][] gameBoardArray;

        public static void Draw()
        {
            for (int r = 0; r < gameBoardArray.Length; r++)
            {
                for (int c = 0; c < gameBoardArray[r].Length; c++)
                {
                    Program.DrawChar(c, r, Program.cursorStartX, Program.cursorStartY, gameBoardArray[r][c]);
                }
            }
        }


        public static void ClearRows()
        {
            List<int> toDelete = new List<int>();
            int rows = 0;
            for (int i = 0; i < gameBoardArray.Length; i++)
            {
                if (!Array.Exists(gameBoardArray[i], gridCell => gridCell == empty))
                {
                    toDelete.Add(i);
                    rows++;
                }
            }
            if (toDelete.Count > 0)
            {
                string[][] newGrid = new string[gameBoardArray.Length][];
                for (int i = 0; i < toDelete.Count; i++)
                {
                    newGrid[i] = new string[10] { empty, empty, empty, empty, empty, empty, empty, empty, empty, empty };
                }
                int count = 0;
                int newCount = toDelete.Count;
                
                while (count < gameBoardArray.Length)
                {
                    bool remove = false;
                    for (int i = 0; i < toDelete.Count; i++)
                    {
                        if (count == toDelete[i])
                        {
                            remove = true;
                        }
                    }
                    if (!remove)
                    {
                        newGrid[newCount] = gameBoardArray[count];
                        newCount++;
                    }
                    count++;
                }

                gameBoardArray = newGrid;
            }

            Program.lines += rows;
            if (rows == 1)
            {
                if (Program.tetromino.tSpin == true)
                {
                    Program.score += 800 * Program.level;
                }
                else {
                    Program.score += 100 * Program.level;

                }
            }
            else if (rows == 2)
            {
                if (Program.tetromino.tSpin == true)
                {
                    Program.score += 1200 * Program.level;
                }
                else
                {
                    Program.score += 300 * Program.level;

                }
            }
            else if (rows == 3)
            {
                if (Program.tetromino.tSpin == true)
                {
                    Program.score += 1600 * Program.level;
                }
                else
                {
                    Program.score += 500 * Program.level;

                }
            }
            else if (rows == 4)
            {
                Program.score += 800 * Program.level;
            }
        }

        public static void CreateArray(int rows, int columns)
        {
            string[][] grid = new string[rows][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new string[columns];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = empty;
                }
            }
            gameBoardArray = grid;
        }
    }


}
