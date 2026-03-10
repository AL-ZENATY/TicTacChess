using TicTacChess.Game;
using System.Drawing.Text;

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

        private Image lampOn;
        private Image lampOff;
        private Image wqImage;
        private Image wrImage;
        private Image wnImage;
        private Image bqImage;
        private Image brImage;
        private Image bnImage;

        private Image wqGlow;
        private Image wrGlow;
        private Image wnGlow;

        private Image bqGlow;
        private Image brGlow;
        private Image bnGlow;

        PrivateFontCollection customFonts = new PrivateFontCollection();
        Font gameFont;

        private string selectedSetupPiece = "";


        public Form1()
        {
            InitializeComponent();
            gameManager = new GameManager(board);

            lampOn = Properties.Resources.lightOn;
            lampOff = Properties.Resources.lightOff;

            wqImage = Properties.Resources.spQ;
            wrImage = Properties.Resources.spR;
            wnImage = Properties.Resources.spK;

            bqImage = Properties.Resources.gpQ;
            brImage = Properties.Resources.gpR;
            bnImage = Properties.Resources.gpK;

            wqGlow = Properties.Resources.spQH;
            wrGlow = Properties.Resources.spRH;
            wnGlow = Properties.Resources.spKH;

            bqGlow = Properties.Resources.gpQH;
            brGlow = Properties.Resources.gpRH;
            bnGlow = Properties.Resources.gpKH;

            UpdateTurnLamps();
            ShowSetupPieces();

            customFonts.AddFontFile("Fonts/SliterD.ttf");
            gameFont = new Font(customFonts.Families[0], 8);
            lblStatusZy.Font = gameFont;
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
                if (selectedSetupPiece == "")
                {
                    lblStatusZy.Text = "Select a setup piece first.";
                    return;
                }

                string piece = GetSelectedSetupPieceCode();

                board.Squares[row, col] = piece;
                btn.BackgroundImage = GetPieceImage(piece);
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Text = "";

                HideSelectedSetupPicture();
                selectedSetupPiece = "";

                gameManager.PlaceSetupPiece(row, col);

                // Show the correct setup message
                if (gameManager.IsSetupFinished())
                {
                    lblStatusZy.Text = "Setup complete. Game phase starts!";
                }
                else if (gameManager.IsWhiteSetupFinished() && gameManager.BlackIndex == 0)
                {
                    ShowSetupPieces();
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
                btn.BackgroundImage = GetPieceImage(selectedPiece);
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Text = "";

                // Clear the old button text
                Button oldButton = GetButtonByPosition(gameManager.SelectedRow, gameManager.SelectedCol);
                oldButton.BackgroundImage = null;
                oldButton.Text = "";

                // Change turn after a valid move
                gameManager.SwitchTurn();
                UpdateTurnLamps();

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
            UpdateTurnLamps();

            // Clear all board button text
            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    btn.Text = "";
                    btn.BackgroundImage = null;
                }
            }

            ShowSetupPieces();
            lblStatusZy.Text = "Game reset. Place White pieces first.";
        }

        private void UpdateTurnLamps()
        {
            if (gameManager.CurrentPlayer == "W")
            {
                picLampSilverZy.BackgroundImage = lampOn;
                picLampGoldZy.BackgroundImage = lampOff;
            }
            else
            {
                picLampSilverZy.BackgroundImage = lampOff;
                picLampGoldZy.BackgroundImage = lampOn;
            }
        }

        private void picSetup1_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "1";
            lblStatusZy.Text = "Selected setup piece 1.";
        }

        private void picSetup2_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "2";
            lblStatusZy.Text = "Selected setup piece 2.";
        }

        private void picSetup3_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "3";
            lblStatusZy.Text = "Selected setup piece 3.";
        }

        private void ShowSetupPieces()
        {

            picSetup1.Visible = true;
            picSetup2.Visible = true;
            picSetup3.Visible = true;

            if (gameManager.PlacingWhite)
            {
                picSetup1.BackgroundImage = wqImage;
                picSetup2.BackgroundImage = wrImage;
                picSetup3.BackgroundImage = wnImage;
            }
            else
            {
                picSetup1.BackgroundImage = bqImage;
                picSetup2.BackgroundImage = brImage;
                picSetup3.BackgroundImage = bnImage;
            }

            selectedSetupPiece = "";
        }

        private string GetSelectedSetupPieceCode()
        {
            if (gameManager.PlacingWhite)
            {
                if (selectedSetupPiece == "1") return "WQ";
                if (selectedSetupPiece == "2") return "WR";
                if (selectedSetupPiece == "3") return "WN";
            }
            else
            {
                if (selectedSetupPiece == "1") return "BQ";
                if (selectedSetupPiece == "2") return "BR";
                if (selectedSetupPiece == "3") return "BN";
            }

            return "";
        }

        private void HideSelectedSetupPicture()
        {
            if (selectedSetupPiece == "1")
                picSetup1.Visible = false;

            if (selectedSetupPiece == "2")
                picSetup2.Visible = false;

            if (selectedSetupPiece == "3")
                picSetup3.Visible = false;
        }

        private Image GetPieceImage(string piece)
        {
            if (piece == "WQ") return wqImage;
            if (piece == "WR") return wrImage;
            if (piece == "WN") return wnImage;

            if (piece == "BQ") return bqImage;
            if (piece == "BR") return brImage;
            if (piece == "BN") return bnImage;

            return null;
        }

        private void BoardCell_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            string piece = board.Squares[row, col];

            if (piece != "")
            {
                btn.BackgroundImage = GetGlowImage(piece);
            }
        }

        private Image GetGlowImage(string piece)
        {
            if (piece == "WQ") return wqGlow;
            if (piece == "WR") return wrGlow;
            if (piece == "WN") return wnGlow;

            if (piece == "BQ") return bqGlow;
            if (piece == "BR") return brGlow;
            if (piece == "BN") return bnGlow;

            return null;
        }

        private void BoardCell_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            string piece = board.Squares[row, col];

            if (piece != "")
            {
                btn.BackgroundImage = GetPieceImage(piece);
            }
        }

        private Image GetSetupGlowImage(string setupSlot)
        {
            if (gameManager.PlacingWhite)
            {
                if (setupSlot == "1") return wqGlow;
                if (setupSlot == "2") return wrGlow;
                if (setupSlot == "3") return wnGlow;
            }
            else
            {
                if (setupSlot == "1") return bqGlow;
                if (setupSlot == "2") return brGlow;
                if (setupSlot == "3") return bnGlow;
            }

            return null;
        }

        private Image GetSetupNormalImage(string setupSlot)
        {
            if (gameManager.PlacingWhite)
            {
                if (setupSlot == "1") return wqImage;
                if (setupSlot == "2") return wrImage;
                if (setupSlot == "3") return wnImage;
            }
            else
            {
                if (setupSlot == "1") return bqImage;
                if (setupSlot == "2") return brImage;
                if (setupSlot == "3") return bnImage;
            }

            return null;
        }

        private void SetupPiece_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (!pic.Visible)
                return;

            if (pic == picSetup1) pic.BackgroundImage = GetSetupGlowImage("1");
            if (pic == picSetup2) pic.BackgroundImage = GetSetupGlowImage("2");
            if (pic == picSetup3) pic.BackgroundImage = GetSetupGlowImage("3");
        }

        private void SetupPiece_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (!pic.Visible)
                return;

            if (pic == picSetup1) pic.BackgroundImage = GetSetupNormalImage("1");
            if (pic == picSetup2) pic.BackgroundImage = GetSetupNormalImage("2");
            if (pic == picSetup3) pic.BackgroundImage = GetSetupNormalImage("3");
        }

        private void btnRestartZy_MouseEnter(object sender, EventArgs e)
        {
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtnHover;
        }

        private void btnRestartZy_MouseLeave(object sender, EventArgs e)
        {
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtn;
        }
    }
}