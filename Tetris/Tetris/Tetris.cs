using System;

namespace Tetris
{
    internal class Tetris
    {
        /** <summary>
         * UI
         * </summary>
         */
        public UI ui;

        /** <summary>
         * All possible types of Tetrominos
         * </summary>
         */
        public enum TetrominoType
        {
            I,
            J,
            L,
            O,
            S,
            T,
            Z
        }

        /** <summary>
         * All shapes and rotations of Tetrominos (In the same order as the type)
         * </summary>
         */
        public bool[,,,] tetrominoShape = new bool[,,,] 
        #region TetrominoShape
{
    {
        {
            { false, false, false, false },
            { true, true, true, true },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, false, true, false },
            { false, false, true, false },
            { false, false, true, false },
            { false, false, true, false }
        },
        {
            { false, false, false, false },
            { false, false, false, false },
            { true, true, true, true },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { false, true, false, false },
            { false, true, false, false },
            { false, true, false, false }
        }
    },
    {
        {
            { true, false, false, false },
            { true, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, true, false },
            { false, true, false, false },
            { false, true, false, false },
            { false, false, false, false }
        },
        {
            { false, false, false, false },
            { true, true, true, false },
            { false, false, true, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { false, true, false, false },
            { true, true, false, false },
            { false, false, false, false }
        }
    },
    {
        {
            { false, false, true, false },
            { true, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { false, true, false, false },
            { false, true, true, false },
            { false, false, false, false }
        },
        {
            { false, false, false, false },
            { true, true, true, false },
            { true, false, false, false },
            { false, false, false, false }
        },
        {
            { true, true, false, false },
            { false, true, false, false },
            { false, true, false, false },
            { false, false, false, false }
        }
    },
    {
        {
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        }
    },
    {
        {
            { false, true, true, false },
            { true, true, false, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { false, true, true, false },
            { false, false, true, false },
            { false, false, false, false }
        },
        {
            { false, false, false, false },
            { false, true, true, false },
            { true, true, false, false },
            { false, false, false, false }
        },
        {
            { true, false, false, false },
            { true, true, false, false },
            { false, true, false, false },
            { false, false, false, false }
        }
    },
    {
        {
            { false, true, false, false },
            { true, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { false, true, true, false },
            { false, true, false, false },
            { false, false, false, false }
        },
        {
            { false, false, false, false },
            { true, true, true, false },
            { false, true, false, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { true, true, false, false },
            { false, true, false, false },
            { false, false, false, false }
        }
    },
    {
        {
            { true, true, false, false },
            { false, true, true, false },
            { false, false, false, false },
            { false, false, false, false }
        },
        {
            { false, false, true, false },
            { false, true, true, false },
            { false, true, false, false },
            { false, false, false, false }
        },
        {
            { false, false, false, false },
            { true, true, false, false },
            { false, true, true, false },
            { false, false, false, false }
        },
        {
            { false, true, false, false },
            { true, true, false, false },
            { true, false, false, false },
            { false, false, false, false }
        }
    }
};
        #endregion

        /** <summary>
         * Length of tetrominoShape shape arrays
         * </summary>
         */
        public int tetrominoLength;

        /** <summary>
         * Current tetromino the player is moving
         * </summary>
         */
        public Tetromino activeTetromino;

        /** <summary>
         * Holded tetromino
         * </summary>
         */
        public Tetromino holdedTetromino;

        /** <summary>
         * Is holded tetromino a phantom
         * </summary>
         */
        public bool isHoldedPhantom = false;

        /** <summary>
         * Current tetromino the player is moving
         * </summary>
         */
        Cell[,] board = new Cell[20, 10];

        /** <summary>
         * Size of board (x, y)
         * </summary>
         */
        public (int, int) boardSize = (10, 20);

        /** <summary>
         * Bag
         * </summary>
         */
        public Queue<Tetris.TetrominoType> bag = new Queue<Tetris.TetrominoType>();

        /** <summary>
         * Current score
         * </summary>
         */
        public int score = 0;

        /** <summary>
         * Top score
         * </summary>
         */
        //public int top = 0;

        /** <summary>
         * Time used for gravity
         * </summary>
         */
        DateTime gravity;

        public Tetris()
        {
            ui = new UI(this);

            tetrominoLength = tetrominoShape.GetLength(3);
            activeTetromino = SpawnTetromino();

            Update();
        }

        /** <summary>
         * Update
         * </summary>
         */
        public void Update()
        {
            while (true)
            {
                // User input
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.RightArrow:
                            activeTetromino.Move(1, 0);
                            break;
                        case ConsoleKey.DownArrow:
                            activeTetromino.Move(0, 1);
                            break;
                        case ConsoleKey.LeftArrow:
                            activeTetromino.Move(-1, 0);
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.X:
                            activeTetromino.Rotate(1);
                            break;
                        case ConsoleKey.Z:
                            activeTetromino.Rotate(-1);
                            break;
                        case ConsoleKey.Spacebar:
                            HardDrop();
                            break;
                        case ConsoleKey.C:
                            Hold();
                            break;
                        default:
                            break;
                    }
                }

                // Auto fall
                if ((DateTime.Now - gravity).TotalMilliseconds >= 500)
                {
                    activeTetromino.Move(0, 1);

                    gravity = DateTime.Now;
                }
            }
        }

        /** <summary>
         * Check if cell is already on a location of the board
         * </summary>
         * <param name="position">Position</param>
         * <returns>True if position is empty</returns>
         */
        bool Peek((int, int) position)
        {
            return position.Item1 >= 0 && position.Item1 < boardSize.Item1 && position.Item2 >= 0 && position.Item2 < boardSize.Item2 && board[position.Item2, position.Item1] == null;
        }

        /** <summary>
         * Check if cell of other tetromino is already on a location of the board
         * </summary>
         * <param name="position">Position</param>
         * <param name="position">Position</param>
         * <returns>True if position is empty</returns>
         */
        bool Peek((int, int) position, Tetromino tetromino)
        {
            return (position.Item1 >= 0 && position.Item1 < boardSize.Item1 && position.Item2 >= 0 && position.Item2 < boardSize.Item2) && (board[position.Item2, position.Item1] == null || Tetromino.Equals(board[position.Item2, position.Item1].baseTetromino, tetromino));
        }

        /** <summary>
         * Genreate new set of bags
         * </summary>
         */ 
        public void GenerateBag()
        {
            Random rnd = new Random();
            int[] arr = Enumerable.Range(0, 7).OrderBy(c => rnd.Next()).ToArray();

            foreach (int i in arr)
            {
                bag.Enqueue((Tetris.TetrominoType)i);
            }
        }


        /** <summary>
         * Spawn new tetromino on the board 
         * </summary>
         */
        public Tetromino SpawnTetromino()
        {
            if (bag.Count <= 1)
            {
                GenerateBag();
            }

            Tetromino tetromino = new Tetromino(this, bag.Dequeue());
            gravity = DateTime.Now;

            UpdatePosition(tetromino);
            ui.PrintTetromino(tetromino);
            ui.PrintNext();

            return tetromino;
        }

        /** <summary>
         * Spawn new tetromino on the board 
         * </summary>
         * <param name="type">Type of the tetromino</param>
         * <returns>Spawned tetromino</returns>
         */
        public Tetromino SpawnTetromino(TetrominoType type)
        {
            Tetromino tetromino = new Tetromino(this, type);
            gravity = DateTime.Now;

            UpdatePosition(tetromino);
            ui.PrintTetromino(tetromino);

            return tetromino;
        }

        /** <summary>
         * Hard drop
         * </summary>
         */
        void HardDrop()
        {
            ChangeScore(activeTetromino.cells.Count);

            while (activeTetromino.Move(0, 1))
            {

            }
        }

        /** <summary>
         * Hold tetromino
         * </summary>
         */
        void Hold()
        {
            if (!isHoldedPhantom)
            {
                if (holdedTetromino == null)
                {
                    holdedTetromino = activeTetromino;

                    RemoveTetromino(activeTetromino);
                    ui.PrintTetromino(activeTetromino, true);
                    ui.PrintHold();

                    activeTetromino = SpawnTetromino();
                }
                else
                {
                    RemoveTetromino(activeTetromino);
                    ui.PrintTetromino(activeTetromino, true);
                    Tetromino tetromino = SpawnTetromino(holdedTetromino.type);

                    holdedTetromino = activeTetromino;
                    isHoldedPhantom = true;
                    activeTetromino = tetromino;

                    ui.PrintHold();
                }
            }
        }

        /** <summary>
         * Add value to score
         * </summary>
         * <param name="x">Score to add</param>
         */
        public void ChangeScore(int x)
        {
            score += x;

            if (score > 999999)
            {
                score = 999999;
            }

            ui.PrintScore();
        }

        /** <summary>
         * Check position of tetromino
         * </summary>
         * <param name="tetromino">Tetromino to check position</param>
         */
        public bool CheckPosition(Tetromino tetromino)
        {
            for (int i = 0; i < tetrominoLength; i++)
            {
                for (int j = 0; j < tetrominoLength; j++)
                {
                    if (tetrominoShape[(int)tetromino.type, tetromino.rotation, i, j])
                    {
                        (int, int) position = (tetromino.position[0] + j, tetromino.position[1] + i);

                        if (!Peek(position, tetromino))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /** <summary>
         * Update tetromino on the board after moving or rotating it
         * </summary>
         * <param name="tetromino">Tetromino to update</param>
         */
        public void UpdatePosition(Tetromino tetromino)
        {
            // Remove previous cells
            foreach (Cell cell in tetromino.cells)
            {
                board[cell.position.Item2, cell.position.Item1] = null;
            }
            tetromino.cells.Clear();

            // Add tetromino to the board
            for (int i = 0; i < tetrominoLength; i++)
            {
                for (int j = 0; j < tetrominoLength; j++)
                {
                    if (tetrominoShape[(int)tetromino.type, tetromino.rotation, i, j])
                    {
                        (int, int) position = (tetromino.position[0] + j, tetromino.position[1] + i);
                        
                        if (Peek(position))
                        {
                            board[position.Item2, position.Item1] = tetromino.CreateCell(position);
                        }
                        else // Lost
                        {
                            ui.PrintBoard(board);

                            Environment.Exit(0);
                        }
                    }
                }
            }
        }

        /** <summary>
         * Check if active tetromino completed lines
         * </summary>
         * <returns>True if some lines have been completed</returns>
         */
        public bool CheckLines()
        {
            List<int> completedLines = new List<int>();

            // Find completed lines
            for (int i = activeTetromino.position[1]; i < tetrominoLength + activeTetromino.position[1] && i < boardSize.Item2; i++)
            {
                bool completed = true;

                for (int j = 0; j < boardSize.Item1 && completed; j++)
                {
                    completed = board[i, j] != null;
                }

                if (completed)
                {
                    completedLines.Insert(0, i);
                }
            }

            // Remove completed lines
            if (completedLines.Count != 0)
            {
                // Score
                switch (completedLines.Count)
                {
                    case 1:
                        ChangeScore(100);
                        break;
                    case 2:
                        ChangeScore(300);
                        break;
                    case 3:
                        ChangeScore(500);
                        break;
                    case 4:
                        ChangeScore(800);
                        break;
                    default:
                        ChangeScore(1300);
                        break;
                }

                foreach (int line in completedLines)
                {
                    ClearLine(line);
                }

                DropLines();
                ui.PrintBoard(board);
            }

            return completedLines.Count != 0;
        }

        /** <summary>
         * Clear line that has been completed
         * </summary>
         * <param name="line">Completed line</param>
         */
        void ClearLine(int line)
        {
            for (int i = 0; i < boardSize.Item1; i++)
            {
                board[line, i] = null;
            }
        }

        /** <summary>
         * Remove tetromino from board
         * </summary>
         * <param name="teromino">Tetromino to remove</param>
         */
        void RemoveTetromino(Tetromino tetromino)
        {
            foreach (Cell cell in tetromino.cells)
            {
                board[cell.position.Item2, cell.position.Item1] = null;
            }

            tetromino.cells.Clear();
        }

        /** <summary>
         * Remove empty lines after and drop other lines down
         * </summary>
         */
        void DropLines()
        {   
            Queue<int> emptyLines = new Queue<int>();

            for (int i = boardSize.Item2 - 1; i >= 0; i--)
            {
                if (emptyLines.Count != 0)
                {
                    if (!IsLineEmpty(i))
                    {
                        MoveLine(i, emptyLines.Dequeue());
                        emptyLines.Enqueue(i);
                    }
                    else
                    {
                        emptyLines.Enqueue(i);
                    }
                }
                else if (IsLineEmpty(i))
                {
                    emptyLines.Enqueue(i);
                }
            }
        }

        /** <summary>
         * Check if line is emty
         * </summary>
         * <param name="line">Line to extract</param>
         * <returns>True if line is empty</returns>
         */
        bool IsLineEmpty(int line)
        {
            for (int i = 0; i < boardSize.Item1; i++)
            {
                if (board[line, i] != null)
                {
                    return false;
                }
            }

            return true;
        }

        /** <summary>
         * Move line from board to an other line
         * </summary>
         * <param name="sourceLine">Line to move</param>
         * <param name="destinationLine">Line which will be replaced</param>
         */
        void MoveLine(int sourceLine, int destinationLine)
        {
            for (int i = 0; i < boardSize.Item1; i++)
            {
                board[destinationLine, i] = board[sourceLine, i];
                board[sourceLine, i] = null;
            }
        }
    }
}