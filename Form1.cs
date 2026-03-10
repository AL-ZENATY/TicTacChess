using TicTacChess.Game;

namespace TicTacChess
{
    public partial class Form1 : Form
    {
        // Main board data
        private Board board = new Board();

        // Handles game state like turns, setup, selection, and game over
        private GameManager gameManager;

        // Checks if a selected move is legal
        private MoveValidator moveValidator = new MoveValidator();

        // Checks if a player has won
        private WinDetector winDetector = new WinDetector();

        public Form1()
        {
            InitializeComponent();
            gameManager = new GameManager(board);
        }

        private void BoardCell_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // Read the row and column from the button Tag
            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            // Stop all input if the game already ended
            if (gameManager.GameOver)
            {
                lblStatusZy.Text = "The game is over.";
                return;
            }

            // Setup phase: players place pieces on the board
            if (gameManager.SetupPhase)
            {
                // A piece can only be placed on an empty square
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = "That square is already taken.";
                    return;
                }

                // White must place on bottom row, Black on top row
                if (gameManager.PlacingWhite && row != 2)
                {
                    lblStatusZy.Text = "White must place in the bottom row.";
                    return;
                }

                if (!gameManager.PlacingWhite && row != 0)
                {
                    lblStatusZy.Text = "Black must place in the top row.";
                    return;
                }

                // Get the next piece, place it, and show it on the button
                string piece = gameManager.GetNextSetupPiece();
                gameManager.PlaceSetupPiece(row, col);
                btn.Text = piece;

                // Show the correct setup message
                if (gameManager.IsSetupFinished())
                {
                    lblStatusZy.Text = "Setup complete. Game phase starts!";
                }
                else if (gameManager.IsWhiteSetupFinished())
                {
                    lblStatusZy.Text = "White done. Now place Black pieces (top row).";
                }
                else
                {
                    if (gameManager.PlacingWhite)
                    {
                        lblStatusZy.Text = $"Placed {piece}. Place next White piece.";
                    }
                    else
                    {
                        lblStatusZy.Text = $"Placed {piece}. Place next Black piece.";
                    }
                }

                return;
            }

            // Game phase: no piece selected yet
            if (!gameManager.HasSelectedPiece())
            {
                string piece = board.Squares[row, col];

                // Player must click a square that contains a piece
                if (piece == "")
                {
                    lblStatusZy.Text = "Select a piece first.";
                    return;
                }

                // Player can only select their own piece
                if (!piece.StartsWith(gameManager.CurrentPlayer))
                {
                    lblStatusZy.Text = "That is not your piece.";
                    return;
                }

                // Save the selected piece position
                gameManager.SelectPiece(row, col);
                lblStatusZy.Text = $"Selected piece at ({row},{col})";
            }
            else
            {
                // Clicking the selected piece again cancels the selection
                if (row == gameManager.SelectedRow && col == gameManager.SelectedCol)
                {
                    lblStatusZy.Text = "Selection canceled.";
                    gameManager.ClearSelection();
                    return;
                }

                // Pieces cannot move onto occupied squares
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = "That square is already occupied.";
                    return;
                }

                // Read the currently selected piece
                string selectedPiece = board.Squares[gameManager.SelectedRow, gameManager.SelectedCol];

                // Check if the move follows the piece rules
                if (!moveValidator.IsValidMove(selectedPiece, gameManager.SelectedRow, gameManager.SelectedCol, row, col))
                {
                    lblStatusZy.Text = moveValidator.GetInvalidMoveMessage(selectedPiece);
                    return;
                }

                // Move the piece in the board data
                gameManager.MovePiece(row, col);

                // Show the moved piece on the new button
                btn.Text = selectedPiece;

                // Clear the old button text
                Button oldButton = GetButtonByPosition(gameManager.SelectedRow, gameManager.SelectedCol);
                oldButton.Text = "";

                // Change turn after a valid move
                gameManager.SwitchTurn();

                // Check if the player who just moved created a winning line
                string movedPlayer = selectedPiece.Substring(0, 1);
                if (gameManager.WinCheckActive && winDetector.CheckWin(board, movedPlayer))
                {
                    gameManager.SetGameOver();

                    if (movedPlayer == "W")
                    {
                        lblStatusZy.Text = "White wins!";
                    }
                    else
                    {
                        lblStatusZy.Text = "Black wins!";
                    }
                }
                else
                {
                    lblStatusZy.Text = $"Moved {selectedPiece} to ({row},{col}). {gameManager.CurrentPlayer}'s turn.";
                }

                // Clear selection after the move is finished
                gameManager.ClearSelection();
            }
        }

        private Button GetButtonByPosition(int row, int col)
        {
            // Search all board buttons and return the one with matching coordinates
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

        private void btnRestartZy_Click(object sender, EventArgs e)
        {
            // Reset all game data
            gameManager.ResetGame();

            // Clear all board button text
            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    btn.Text = "";
                }
            }

            lblStatusZy.Text = "Game reset. Place White pieces first.";
        }
    }
}