using System;

namespace Tetris
{
    internal class UI // https://en.wikipedia.org/wiki/List_of_Unicode_characters
    {
        /** <summary>
         * Grid
         * </summary>
         */
        string grid =  "╔════════════════════╦════════╗\n" +
                       "║                    ║SCORE   ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ╠════════╣\n" +
                       "║                    ║HOLD    ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ╠════════╣\n" +
                       "║                    ║NEXT    ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║        ║\n" +
                       "║                    ║════════╝\n" +
                       "║                    ║\n" +
                       "║                    ║\n" +
                       "║                    ║\n" +
                       "║                    ║\n" +
                       "║                    ║\n" +
                       "╚════════════════════╝";

        /** <summary>
         * Tetris
         * </summary>
         */
        public Tetris tetris;

        //(int, int) topPos = (22, 2);
        (int, int) scorePos = (22, 2);
        (int, int) holdPos = (22, 5);
        (int, int) nextPos = (22, 11);

        public UI(Tetris tetris)
        {
            this.tetris = tetris;

            PrintGrid();
        }

        /** <summary>
         * Print grid
         * </summary>
         */
        public void PrintGrid()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(grid);

            // Score
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(scorePos.Item1, scorePos.Item2 - 1);
            Console.Write("SCORE");
            Console.SetCursorPosition(scorePos.Item1, scorePos.Item2);
            Console.Write(tetris.score.ToString("00000000"));
            /*Console.SetCursorPosition(topPos.Item1, topPos.Item2 - 1);
            Console.Write("TOP");
            Console.SetCursorPosition(topPos.Item1, topPos.Item2);
            Console.Write(tetris.top.ToString("00000000"));*/

            // HOLD
            Console.SetCursorPosition(holdPos.Item1, holdPos.Item2 - 1);
            Console.Write("HOLD");

            // NEXT
            Console.SetCursorPosition(nextPos.Item1, nextPos.Item2 - 1);
            Console.Write("NEXT");

            // Reset color
            Console.ResetColor();
        }

        /** <summary>
         * Print score
         * </summary>
         */
        public void PrintScore()
        {
            // Score
            Console.SetCursorPosition(scorePos.Item1, scorePos.Item2);
            Console.Write(tetris.score.ToString("000000"));

            // Top
            /*if (tetris.score > tetris.top)
            {
                Console.SetCursorPosition(topPos.Item1, topPos.Item2);
                Console.Write(tetris.score.ToString("000000"));
            }*/
        }

        /** <summary>
         * Print tetromino
         * </summary>
         * <param name="tetromino">Tetromino to print</param>
         * <param name="erase" default="false">Erase tetromino</param>
         */
        public void PrintTetromino(Tetromino tetromino, bool erase = false)
        {
            if (!erase)
            {
                Console.ForegroundColor = TetrominoColor(tetromino.type);
            }

            (int, int) position = (1 + tetromino.position[0] * 2, 1 + tetromino.position[1]);

            for (int i = 0; i < tetris.tetrominoLength; i++)
            {
                for (int j = 0; j < tetris.tetrominoLength; j++)
                {
                    if (tetris.tetrominoShape[(int)tetromino.type, tetromino.rotation, i, j])
                    {
                        if (j == 0 || !tetris.tetrominoShape[(int)tetromino.type, tetromino.rotation, i, j - 1])
                        {
                            Console.SetCursorPosition(position.Item1 + j * 2, position.Item2 + i);
                        }

                        if (!erase)
                        {
                            Console.Write("██");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                }
            }

            if (!erase)
            {
                Console.ResetColor();
            }
        }

        /** <summary>
         * Print holded tetromino
         * </summary>
         */
        public void PrintHold()
        {
            if (tetris.isHoldedPhantom)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = TetrominoColor(tetris.holdedTetromino.type);
            }

            for (int i = 0; i < tetris.tetrominoLength; i++)
            {
                Console.SetCursorPosition(holdPos.Item1, holdPos.Item2 + i);

                for (int j = 0; j < tetris.tetrominoLength; j++)
                {
                    if (tetris.tetrominoShape[(int)tetris.holdedTetromino.type, tetris.holdedTetromino.rotation, i, j])
                    {
                        Console.Write("██");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }
            
            Console.ResetColor();
        }

        /** <summary>
         * Print next tetromino
         * </summary>
         */
        public void PrintNext()
        {
            Console.ForegroundColor = TetrominoColor(tetris.bag.Peek());

            for (int i = 0; i < tetris.tetrominoLength; i++)
            {
                Console.SetCursorPosition(nextPos.Item1, nextPos.Item2 + i);

                for (int j = 0; j < tetris.tetrominoLength; j++)
                {
                    if (tetris.tetrominoShape[(int)tetris.bag.Peek(), 0, i, j])
                    {
                        Console.Write("██");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }

            Console.ResetColor();
        }

        /** <summary>
         * Print board
         * </summary>
         * <param name="board">Board</param>
         */
        public void PrintBoard(Cell[,] board)
        {
            for (int i = 0; i < tetris.boardSize.Item2; i++)
            {
                Console.SetCursorPosition(1, i + 1);

                for (int j = 0; j < tetris.boardSize.Item1; j++)
                {
                    if (board[i, j] != null)
                    {
                        Console.ForegroundColor = TetrominoColor(board[i, j].baseTetromino.type);
                        Console.Write("██");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }

            Console.ResetColor();
        }

        /** <summary>
         * Determine color of tetromino
         * </summary>
         */
        ConsoleColor TetrominoColor(Tetris.TetrominoType type)
        {
            switch (type)
            {
                case Tetris.TetrominoType.I:
                    return ConsoleColor.Cyan;
                case Tetris.TetrominoType.J:
                    return ConsoleColor.Blue;
                case Tetris.TetrominoType.L:
                    return ConsoleColor.DarkYellow;
                case Tetris.TetrominoType.O:
                    return ConsoleColor.Yellow;
                case Tetris.TetrominoType.S:
                    return ConsoleColor.Green;
                case Tetris.TetrominoType.T:
                    return ConsoleColor.Magenta;
                case Tetris.TetrominoType.Z:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
