using System;
using System.IO;


namespace ConsoleTetris
{
    class Program
    {
        public static int dropSpeed = 500;
        public static string empty = ".";
        static Tetromino nextTetromino = new Tetromino();
        public static Tetromino tetromino = new Tetromino();
        public static int cursorStartX = 2;
        public static int cursorStartY = 0;
        public static int level;
        public static int score;
        public static int lines;
        public static bool gameStarted;
        static void Main(string[] args)
        {
            showControlls();
        }

        public static void startGame() {
            level = 1;
            score = 0;
            lines = 0;
            dropSpeed = 500;
            gameStarted = true;
            Console.CursorVisible = false;
            GameBoard.CreateArray(21, 10);
            nextTetromino.DrawNext();
            while (true && gameStarted)
            {
                CheckInput();
                tetromino.Move();
                GameBoard.ClearRows();
                endGame();
                SpawnTetromino();
                UpdateScore();
            }
        }

        public static void Update()
        {
            GameBoard.Draw();
            tetromino.Draw();
        }

        public static void DrawChar(int x, int y, int cursorStartX, int cursorStartY, string c)
        {
            Console.SetCursorPosition(cursorStartX + x * 2, cursorStartY + y);
            Console.Write(c);
        }

        static void CheckInput() {
            if (Console.KeyAvailable)
            {
                string key = Console.ReadKey(true).Key.ToString();
                if (key == "A")
                {
                    tetromino.MoveLeft();
                }
                else if (key == "D")
                {
                    tetromino.MoveRight();
                }
                else if (key == "S")
                {
                    tetromino.MoveDown();
                }
                else if (key == "Spacebar")
                {
                    tetromino.Rotate(-1);
                }
                else if (key == "Z")
                {
                    tetromino.Rotate(1);
                }
                else if (key == "W") 
                {
                    tetromino.Drop();
                }
            }
        }

        static void SpawnTetromino()
        {
            if (tetromino.locked)
            {
                tetromino = nextTetromino;
                nextTetromino = new Tetromino();
                nextTetromino.DrawNext();
            }
        }

        static void UpdateScore() 
        {
            if (lines >= level * 5) {
                
                level++;
                
                if (dropSpeed > 50) 
                {
                    dropSpeed -= 10;
                }

            }
            int startX = 25;
            Console.SetCursorPosition(startX, 0);
            Console.WriteLine("Score: {0}", score.ToString());
            Console.SetCursorPosition(startX, 1);
            Console.WriteLine("Level: {0}", level.ToString());
            Console.SetCursorPosition(startX, 2);
            Console.WriteLine("Lines: {0}", lines.ToString());
        }

        static void endGame() 
        {
            if (tetromino.locked && tetromino.y == 0) 
            {
                gameStarted = false;
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("High Score Is {0}", getHighScore());
                Console.WriteLine("made for sarah carrell with love!");
                Console.WriteLine("programming done by Elijah Woelbing");
                Console.WriteLine("press enter to play again");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                while (keyInfo.Key != ConsoleKey.Enter)
                {
                    keyInfo = Console.ReadKey();
                }
                Console.Clear();
                startGame();
            }
        }

        static void showControlls()
        {
            Console.WriteLine("controlls are w, a, s, d, z, space");
            Console.WriteLine("SCORING");
            Console.WriteLine("Single 100 × level");
            Console.WriteLine("Double 300 × level");
            Console.WriteLine("Triple 500 × level");
            Console.WriteLine("Tetris 800 × level");
            Console.WriteLine("T-Spin Single 800 × level; difficult");
            Console.WriteLine("T-Spin Double 1200 × level; difficult");
            Console.WriteLine("press enter to continue");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
            }
            Console.Clear();
            startGame();
        }

        static string getHighScore() 
        {
            string path = Path.Combine(Environment.CurrentDirectory,"scoring.txt");
            string scoreFromFile = File.ReadAllText(path).Trim();
            int x;

            if (int.TryParse(scoreFromFile, out x))
            {
                if (score > x) {
                    File.WriteAllText(path, score.ToString());
                }
                return File.ReadAllText(path);
            }
            return "unable to get High Score";
        }
    }
}
