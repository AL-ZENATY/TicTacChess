namespace TicTacChess.Game
{
    public class WinDetector
    {
        // Checks if a player has 3 pieces in a row
        public bool CheckWin(Board board, string player)
        {
            // check horizontal lines
            for (int r = 0; r < 3; r++)
            {
                if (board.Squares[r, 0].StartsWith(player) &&
                    board.Squares[r, 1].StartsWith(player) &&
                    board.Squares[r, 2].StartsWith(player))
                {
                    return true;
                }
            }

            // check vertical lines
            for (int c = 0; c < 3; c++)
            {
                if (board.Squares[0, c].StartsWith(player) &&
                    board.Squares[1, c].StartsWith(player) &&
                    board.Squares[2, c].StartsWith(player))
                {
                    return true;
                }
            }

            // check diagonal top-left -> bottom-right
            if (board.Squares[0, 0].StartsWith(player) &&
                board.Squares[1, 1].StartsWith(player) &&
                board.Squares[2, 2].StartsWith(player))
            {
                return true;
            }

            // check diagonal top-right -> bottom-left
            if (board.Squares[0, 2].StartsWith(player) &&
                board.Squares[1, 1].StartsWith(player) &&
                board.Squares[2, 0].StartsWith(player))
            {
                return true;
            }

            return false;
        }
    }
}