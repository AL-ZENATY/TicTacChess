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
                lblStatusZy.Text = $"Move from ({selectedRow},{selectedCol}) to ({row},{col})";

                // Reset selection for now
                selectedRow = -1;
                selectedCol = -1;
            }
        }
    }
}
