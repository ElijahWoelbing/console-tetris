using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace ConsoleTetris
{
    class Tetromino
    {
        public string[][][][] tetrominos = new string[][][][]
        {
           new string[][][]
            {
               new string[][] {
                    new string[]{empty, "■", empty},
                    new string[]{"■", "■", "■"},
                    new string[]{empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty, "■", empty},
                    new string[]{empty,"■","■"},
                    new string[]{empty,"■",empty}
                },
                new string[][]{
                    new string[]{empty,empty,empty},
                    new string[]{"■","■","■"},
                    new string[]{empty,"■",empty}

                },
                new string[][]{
                    new string[]{empty,"■",empty},
                    new string[]{"■","■",empty},
                    new string[]{empty,"■",empty}
                }
            },

            new string[][][]
{
               new string[][] {
                    new string[]{empty,empty,"■"},
                    new string[]{"■","■","■"},
                    new string[]{empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■","■"}
                },
                new string[][]{
                    new string[]{empty,empty,empty},
                    new string[]{"■","■","■"},
                    new string[]{"■",empty,empty},
                },
                new string[][]{
                    new string[]{"■","■",empty},
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■",empty}
                }
            },
        new string[][][]
            {
               new string[][] {
                    new string[]{"■",empty,empty},
                    new string[]{"■","■","■"},
                    new string[]{empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,"■","■"},
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■",empty}
                },
                new string[][]{
                    new string[]{empty,empty,empty},
                    new string[]{"■","■","■"},
                    new string[]{empty,empty,"■"},
                },
                new string[][]{
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■",empty},
                    new string[]{"■","■",empty}
                }
            },


       new string[][][]
            {
               new string[][] {
                    new string[]{"■","■",empty},
                    new string[]{empty,"■","■"},
                    new string[]{empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,empty,"■"},
                    new string[]{empty,"■","■"},
                    new string[]{empty,"■",empty}
                },
                new string[][]{
                    new string[]{empty,empty,empty},
                    new string[]{"■","■",empty},
                    new string[]{empty,"■","■"},
                },
                new string[][]{
                    new string[]{empty,"■",empty},
                    new string[]{"■","■",empty},
                    new string[]{"■",empty,empty}
                }
            },

          new string[][][]
            {
               new string[][] {
                    new string[]{empty,"■","■"},
                    new string[]{"■","■",empty},
                    new string[]{empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,"■",empty},
                    new string[]{empty,"■","■"},
                    new string[]{empty,empty,"■"}
                },
                new string[][]{
                    new string[]{empty,empty,empty},
                    new string[]{empty,"■","■"},
                    new string[]{"■","■",empty},
                },
                new string[][]{
                    new string[]{"■",empty,empty},
                    new string[]{"■","■",empty},
                    new string[]{empty,"■",empty}
                }
            },


            new string[][][]
            {
               new string[][] {
                    new string[]{"■","■"},
                    new string[]{"■","■"}
                }
            },

            new string[][][]
            {
                new string[][]{
                    new string[]{empty,empty,empty,empty},
                    new string[]{"■","■","■","■"},
                    new string[]{empty,empty,empty,empty},
                    new string[]{empty,empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,empty,"■",empty},
                    new string[]{empty,empty,"■",empty},
                    new string[]{empty,empty,"■",empty},
                    new string[]{empty,empty,"■",empty}
                },
                new string[][]{
                    new string[]{empty,empty,empty,empty},
                    new string[]{empty,empty,empty,empty},
                    new string[]{"■","■","■","■"},
                    new string[]{empty,empty,empty,empty}
                },
                new string[][]{
                    new string[]{empty,"■",empty,empty},
                    new string[]{empty,"■",empty,empty},
                    new string[]{empty,"■",empty,empty},
                    new string[]{empty,"■",empty,empty}
                }
            }
        };
        public string[][][] activeTetrominoRotations { get; private set; }
        public string[][] activeTetromino;
        public int x { get; private set; }
        public int y { get; private set; } = 0;
        int position = 0;
        static string empty = Program.empty;
        public bool locked { get; private set; } = false;
        Random random = new Random();
        Stopwatch dropStopWatch;
        int dropInterval = Program.dropSpeed;
        public bool tSpin = false;
        
        public Tetromino() {
            activeTetrominoRotations = SetActiveTetromino();
            activeTetromino = activeTetrominoRotations[position];
            x = random.Next(0, 10 - activeTetromino.Length);
            dropStopWatch = new Stopwatch();
            dropStopWatch.Start();
        }

        public void Draw() {
            for (int i = 0; i < activeTetromino.Length; i++) {
                for (int j = 0; j < activeTetromino[i].Length; j++)
                {
                    if (activeTetromino[i][j] == empty) 
                    {
                        continue;
                    }
                    Program.DrawChar(x + j, y + i, Program.cursorStartX, Program.cursorStartY, activeTetromino[i][j]);
                }
            }
        }

        public void Move() {
            if (dropStopWatch.ElapsedMilliseconds > dropInterval) {
                MoveDown();
                Program.Update();
                dropStopWatch.Restart();
            }
        }



        string[][][] SetActiveTetromino()
        {
            return tetrominos[random.Next(0, tetrominos.Length)];
        }

        public void MoveLeft()
        {
            if (!Collision(-1, 0, activeTetromino))
            {
                x -= 1;
                Program.Update();
            }

        }

        public void MoveRight()
        {

            if (!Collision(1, 0, activeTetromino))
            {
                x += 1;
                Program.Update();
            }
            
        }

        public void MoveDown()
        {
            if (!Collision(0, 1, activeTetromino))
            {
                if (dropInterval > 0)
                {
                    Program.score++;
                }
                else
                {
                    Program.score += 2;
                }
                y += 1;
                Program.Update();
            }
            else
            {
                LockTetromino();
            }
        }

        public void Rotate(int dir)
        {
            tSpin = false;
            int kickX = 0;
            if(Collision(kickX, 0, activeTetrominoRotations[((position - dir) + activeTetrominoRotations.Length) % activeTetrominoRotations.Length]))
            {
                if (x < GameBoard.gameBoardArray[0].Length / 2)
                {
                    // L piece needs to move over two spaces to rotate at this position
                    if (x == -2 && activeTetromino == tetrominos[6][1])
                    {
                        kickX += 2;

                    }
                    else {
                        kickX += 1;
                    }
                }
                else if (x > GameBoard.gameBoardArray[0].Length / 2)
                {
                    if (x == 8 && activeTetromino == tetrominos[6][3])
                    {
                        kickX -= 2;

                    }
                    else {
                        kickX -= 1;
                    }
                }
            }


            if (!Collision(kickX, 0, activeTetrominoRotations[((position - dir) + activeTetrominoRotations.Length) % activeTetrominoRotations.Length]))
            {
                if (activeTetrominoRotations == tetrominos[0] && Collision(-1, 0, activeTetromino) && Collision(1, 0, activeTetromino)) {
                    tSpin = true;
                }
                x += kickX;
                position = ((position - dir) + activeTetrominoRotations.Length) % activeTetrominoRotations.Length;
                activeTetromino = activeTetrominoRotations[position];
                Program.Update();
            }
        }

        public bool Collision(int xIncrease, int yIncrease, string[][] piece) {
            for (int i = 0; i < piece.Length; i++) {
                for (int j = 0; j < piece[i].Length; j++)
                {
                    if (piece[i][j] == empty)
                    {
                        continue;
                    }
                    int newX = x + j + xIncrease;
                    int newY = y + i + yIncrease;
                    if (newX < 0 || newX >= 10 || newY >= GameBoard.gameBoardArray.Length) {
                        return true;
                    }
                    if(newY < 0) 
                    {
                        continue;
                    }
                    if (GameBoard.gameBoardArray[newY][newX] != empty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Drop() 
        {
            dropInterval = 0;
            Program.Update();
        }

        void LockTetromino(){
            locked = true;
            for (int i = 0; i < activeTetromino.Length; i++)
            {
                for (int j = 0; j < activeTetromino[i].Length; j++)
                {
                    if (activeTetromino[i][j] == empty)
                    {
                        continue;
                    }
                    GameBoard.gameBoardArray[y + i][x + j] = activeTetromino[i][j];
                }
            }
        }

        public void DrawNext() {
            int startX = 25;
            int startY = 10;
            Console.SetCursorPosition(startX, startY - 1);
            Console.WriteLine("Next");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Program.DrawChar(j, i, startX, startY, empty);
                }
            }
            for (int i = 0; i < activeTetromino.Length; i++)
            {
                for (int j = 0; j < activeTetromino[i].Length; j++)
                {
                    if (activeTetromino[i][j] == empty)
                    {
                        continue;
                    }
                    Program.DrawChar(j, i, startX, startY, activeTetromino[i][j]);
                }
            }
        }
    }
}
