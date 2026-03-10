using TicTacChess.Game;

namespace TicTacChess
{
    public partial class Form1 : Form
    {
        Board board = new Board();

        private bool setupPhase = true;
        private string[] whiteToPlace = { "WQ", "WR", "WN" };
        private string[] blackToPlace = { "BQ", "BR", "BN" };

        private int whiteIndex = 0;
        private int blackIndex = 0;

        private int selectedRow = -1;
        private int selectedCol = -1;

        private string currentPlayer = "W";

        private bool placingWhite = true; // true = white placing, false = black placing
        public Form1()
        {
            InitializeComponent();
        }

        private void BoardCell_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            if (setupPhase)
            {
                // Rule: must click empty square
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = "That square is already taken.";
                    return;
                }

                // Rule: white bottom row only (row 2), black top row only (row 0)
                if (placingWhite && row != 2)
                {
                    lblStatusZy.Text = "White must place in the bottom row.";
                    return;
                }
                if (!placingWhite && row != 0)
                {
                    lblStatusZy.Text = "Black must place in the top row.";
                    return;
                }

                // Place next piece
                if (placingWhite)
                {
                    string piece = whiteToPlace[whiteIndex];
                    board.Squares[row, col] = piece;
                    btn.Text = piece;

                    whiteIndex++;
                    if (whiteIndex >= whiteToPlace.Length)
                    {
                        placingWhite = false;
                        lblStatusZy.Text = "White done. Now place Black pieces (top row).";
                    }
                    else
                    {
                        lblStatusZy.Text = $"Placed {piece}. Place next White piece.";
                    }
                }
                else
                {
                    string piece = blackToPlace[blackIndex];
                    board.Squares[row, col] = piece;
                    btn.Text = piece;

                    blackIndex++;
                    if (blackIndex >= blackToPlace.Length)
                    {
                        setupPhase = false;
                        lblStatusZy.Text = "Setup complete. Game phase starts!";
                    }
                    else
                    {
                        lblStatusZy.Text = $"Placed {piece}. Place next Black piece.";
                    }
                }

                return;
            }

            // If nothing selected yet
            if (selectedRow == -1)
            {
                string piece = board.Squares[row, col];

                if (piece == "")
                {
                    lblStatusZy.Text = "Select a piece first.";
                    return;
                }

                if (!piece.StartsWith(currentPlayer))
                {
                    lblStatusZy.Text = "That is not your piece.";
                    return;
                }

                selectedRow = row;
                selectedCol = col;

                lblStatusZy.Text = $"Selected piece at ({row},{col})";
            }
            else
            {
                // same square = cancel selection
                if (row == selectedRow && col == selectedCol)
                {
                    lblStatusZy.Text = "Selection canceled.";
                    selectedRow = -1;
                    selectedCol = -1;
                    return;
                }

                // cannot move onto occupied square
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = "That square is already occupied.";
                    return;
                }

                // move piece
                string selectedPiece = board.Squares[selectedRow, selectedCol];

                // Validate Queen movement
                if (selectedPiece == "WQ" || selectedPiece == "BQ")
                {
                    int rowDiff = Math.Abs(row - selectedRow);
                    int colDiff = Math.Abs(col - selectedCol);

                    bool validMove =
                        row == selectedRow ||      // horizontal
                        col == selectedCol ||      // vertical
                        rowDiff == colDiff;        // diagonal

                    if (!validMove)
                    {
                        lblStatusZy.Text = "Invalid Queen move.";
                        return;
                    }
                }

                // Validate Rook movement
                if (selectedPiece == "WR" || selectedPiece == "BR")
                {
                    bool validMove =
                        row == selectedRow ||   // same row
                        col == selectedCol;     // same column

                    if (!validMove)
                    {
                        lblStatusZy.Text = "Invalid Rook move.";
                        return;
                    }
                }

                board.Squares[row, col] = selectedPiece;
                board.Squares[selectedRow, selectedCol] = "";

                // update button text
                btn.Text = selectedPiece;

                Button oldButton = GetButtonByPosition(selectedRow, selectedCol);
                oldButton.Text = "";

                // switch turns
                if (currentPlayer == "W")
                {
                    currentPlayer = "B";
                }
                else
                {
                    currentPlayer = "W";
                }

                lblStatusZy.Text = $"Moved {selectedPiece} to ({row},{col}). {currentPlayer}'s turn.";

                selectedRow = -1;
                selectedCol = -1;
            }
        }

        private Button GetButtonByPosition(int row, int col)
        {
            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    if (btn.Tag.ToString() == $"{row},{col}")
                    {
                        return btn;
                    }
                }
            }

            return null;
        }

    }
}
