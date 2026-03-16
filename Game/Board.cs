namespace TicTacChess.Game
{
    public class Board
    {
        // 3x3 board that stores the pieces (SQ, SR, SN, etc.)
        public string[,] Squares = new string[3, 3];

        public Board()
        {
            // start with an empty board
            Clear();
        }

        public void Clear()
        {
            // reset every square to empty
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Squares[r, c] = "";
                }
            }
        }
    }
}