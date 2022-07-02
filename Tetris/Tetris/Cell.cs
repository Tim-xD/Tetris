using System;

namespace Tetris
{
    internal class Cell
    {
        /** <summary>
         * The tetromino the cell belongs to
         * </summary>
         */
        public Tetromino baseTetromino;

        /** <summary>
         * The position of the tetromino on the board (x, y)
         * </summary>
         */
        public (int, int) position;

        public Cell(Tetromino tetromino, (int, int) position)
        {
            baseTetromino = tetromino;
            this.position = position;
        }
    }
}
