namespace TicTacChess.Game
{
    public class MoveValidator
    {
        // Main method: checks if the move is valid depending on the piece type
        public bool IsValidMove(Board board, string piece, int fromRow, int fromCol, int toRow, int toCol)
        {
            if (piece == "SQ" || piece == "GQ")
            {
                return IsValidQueenMove(board, fromRow, fromCol, toRow, toCol);
            }

            if (piece == "SR" || piece == "GR")
            {
                return IsValidRookMove(board, fromRow, fromCol, toRow, toCol);
            }

            if (piece == "SKN" || piece == "GKN")
            {
                return IsValidKnightMove(fromRow, fromCol, toRow, toCol);
            }

            if (piece == "SK" || piece == "GK")
            {
                return IsValidKingMove(fromRow, fromCol, toRow, toCol);
            }

            return false;
        }

        // Simple message used in the UI if the move is illegal
        public string GetInvalidMoveMessage(string piece)
        {
            if (piece == "SQ" || piece == "GQ")
                return "Invalid Queen move.";

            if (piece == "SR" || piece == "GR")
                return "Invalid Rook move.";

            if (piece == "SKN" || piece == "GKN")
                return "Invalid Knight move.";
            if (piece == "SK" || piece == "GK")
                return "Invalid King move.";

            return "Invalid move.";
        }

        // Queen: horizontal, vertical, or diagonal
        private bool IsValidQueenMove(Board board, int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            if (fromRow == toRow)
            {
                int startCol = Math.Min(fromCol, toCol) + 1;
                int endCol = Math.Max(fromCol, toCol);

                for (int c = startCol; c < endCol; c++)
                {
                    if (board.Squares[fromRow, c] != "")
                    {
                        return false;
                    }
                }

                return true;
            }

            if (fromCol == toCol)
            {
                int startRow = Math.Min(fromRow, toRow) + 1;
                int endRow = Math.Max(fromRow, toRow);

                for (int r = startRow; r < endRow; r++)
                {
                    if (board.Squares[r, fromCol] != "")
                    {
                        return false;
                    }
                }

                return true;
            }

            if (rowDiff == colDiff)
            {
                int rowStep;
                int colStep;

                if (toRow > fromRow)
                    rowStep = 1;
                else
                    rowStep = -1;

                if (toCol > fromCol)
                    colStep = 1;
                else
                    colStep = -1;

                int currentRow = fromRow + rowStep;
                int currentCol = fromCol + colStep;

                while (currentRow != toRow && currentCol != toCol)
                {
                    if (board.Squares[currentRow, currentCol] != "")
                    {
                        return false;
                    }

                    currentRow += rowStep;
                    currentCol += colStep;
                }

                return true;
            }

            return false;
        }

        // Rook: straight lines only
        private bool IsValidRookMove(Board board, int fromRow, int fromCol, int toRow, int toCol)
        {
            if (fromRow != toRow && fromCol != toCol)
            {
                return false;
            }

            if (fromRow == toRow)
            {
                int startCol = Math.Min(fromCol, toCol) + 1;
                int endCol = Math.Max(fromCol, toCol);

                for (int c = startCol; c < endCol; c++)
                {
                    if (board.Squares[fromRow, c] != "")
                    {
                        return false;
                    }
                }
            }

            if (fromCol == toCol)
            {
                int startRow = Math.Min(fromRow, toRow) + 1;
                int endRow = Math.Max(fromRow, toRow);

                for (int r = startRow; r < endRow; r++)
                {
                    if (board.Squares[r, fromCol] != "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Knight: L-shape move
        private bool IsValidKnightMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        // King: can move 1 square in any direction
        private bool IsValidKingMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            // max 1 step in any direction
            return rowDiff <= 1 && colDiff <= 1;
        }
    }
}