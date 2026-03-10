namespace TicTacChess.Game
{
    public class MoveValidator
    {
        // Main method: checks if the move is valid depending on the piece type
        public bool IsValidMove(string piece, int fromRow, int fromCol, int toRow, int toCol)
        {
            if (piece == "WQ" || piece == "BQ")
            {
                return IsValidQueenMove(fromRow, fromCol, toRow, toCol);
            }

            if (piece == "WR" || piece == "BR")
            {
                return IsValidRookMove(fromRow, fromCol, toRow, toCol);
            }

            if (piece == "WN" || piece == "BN")
            {
                return IsValidKnightMove(fromRow, fromCol, toRow, toCol);
            }

            return false;
        }

        // Simple message used in the UI if the move is illegal
        public string GetInvalidMoveMessage(string piece)
        {
            if (piece == "WQ" || piece == "BQ")
                return "Invalid Queen move.";

            if (piece == "WR" || piece == "BR")
                return "Invalid Rook move.";

            if (piece == "WN" || piece == "BN")
                return "Invalid Knight move.";

            return "Invalid move.";
        }

        // Queen: horizontal, vertical, or diagonal
        private bool IsValidQueenMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            return fromRow == toRow || fromCol == toCol || rowDiff == colDiff;
        }

        // Rook: straight lines only
        private bool IsValidRookMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            return fromRow == toRow || fromCol == toCol;
        }

        // Knight: L-shape move
        private bool IsValidKnightMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }
    }
}