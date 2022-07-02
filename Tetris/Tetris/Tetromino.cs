using System;

namespace Tetris
{
    internal class Tetromino
    {
        /** <summary>
         * Type of tetromino
         * </summary>
         */
        Tetris tetris;

        /** <summary>
         * Type of tetromino
         * </summary>
         */
        public Tetris.TetrominoType type;

        /** <summary>
         * Cells of the tetromino on the board
         * </summary>
         */
        public List<Cell> cells = new List<Cell>();

        /** <summary>
         * Position of tetromino (x, y)
         * </summary>
         */
        public int[] position = new int[] { 3, 0 }; 

        /** <summary>
         * Rotation of tetromino
         * </summary>
         */
        public int rotation = 0;

        public Tetromino(Tetris tetris, Tetris.TetrominoType type)
        {
            this.tetris = tetris;
            this.type = type;
        }

        /** <summary>
         * Create new cell
         * </summary>
         * <param name="position">Position of the new cell</param>
         */
        public Cell CreateCell((int, int) position)
        {
            Cell cell = new Cell(this, position);
            cells.Add(cell);

            return cell;
        }

        /** <summary>
         * Move tetromino
         * </summary>
         * <param name="x">Delta horizontal position</param>
         * <param name="y">Delta vertical position</param>
         * <returns>True if tetromino hasmoved</returns>
         */
        public bool Move(int x, int y)
        {
            tetris.ui.PrintTetromino(this, true);

            position[0] += x;
            position[1] += y;

            bool linesCompleted = false;
            bool moved = tetris.CheckPosition(this);

            if (moved)
            {
                tetris.UpdatePosition(this);
            }
            else // Set to old position
            {
                position[0] -= x;
                position[1] -= y;

                if (y != 0) // Tetromino blocked
                {
                    if (tetris.isHoldedPhantom) // Unlock holded tetromino
                    {
                        tetris.isHoldedPhantom = false;
                        tetris.ui.PrintHold();
                    }

                    tetris.ChangeScore(cells.Count);

                    linesCompleted = tetris.CheckLines();
                    tetris.activeTetromino = tetris.SpawnTetromino();
                }
            }

            if (!linesCompleted)
            {
                tetris.ui.PrintTetromino(this);
            }

            return moved;
        }

        /** <summary>
         * Rotate tetromino
         * </summary>
         * <param name="x">Direction of rotation</param>
         */
        public void Rotate(int x)
        {
            tetris.ui.PrintTetromino(this, true);

            rotation = (rotation + x) % 4;
            
            if (rotation < 0)
            {
                rotation += tetris.tetrominoLength;
            }

            if (tetris.CheckPosition(this))
            {
                tetris.UpdatePosition(this);
            }
            else // Set to old position
            {
                rotation = (rotation - x) % 4;

                if (rotation < 0)
                {
                    rotation += tetris.tetrominoLength;
                }
            }

            tetris.ui.PrintTetromino(this);
        }
    }
}
