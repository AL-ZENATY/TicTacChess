using TicTacChess.Game;
using System.Drawing.Text;

namespace TicTacChess
{
    public partial class Form1 : Form
    {
        // =========================
        // Game logic objects
        // =========================

        // Main board data
        private Board board = new Board();

        // Handles game state like turns, setup, selection, and game over
        private GameManager gameManager;

        // Checks if a selected move is legal
        private MoveValidator moveValidator = new MoveValidator();

        // Checks if a player has won
        private WinDetector winDetector = new WinDetector();


        // =========================
        // Board and piece images
        // =========================

        // Turn lamp images
        private Image lampOn;
        private Image lampOff;

        // Normal white piece images
        private Image sqImage;
        private Image srImage;
        private Image snImage;

        // Normal black piece images
        private Image gqImage;
        private Image grImage;
        private Image gnImage;

        // Glow white piece images
        private Image sqGlow;
        private Image srGlow;
        private Image snGlow;

        // Glow black piece images
        private Image gqGlow;
        private Image grGlow;
        private Image gnGlow;

        // Highlight image for legal setup and move squares
        private Image highlightImage;


        // =========================
        // Fonts and setup selection
        // =========================

        // Custom font collection
        PrivateFontCollection customFonts = new PrivateFontCollection();

        // Main game font
        Font gameFont;

        // Stores which setup piece is currently selected
        private string selectedSetupPiece = "";

        private bool silverPiece1Used = false;
        private bool silverPiece2Used = false;
        private bool silverPiece3Used = false;

        private bool goldPiece1Used = false;
        private bool goldPiece2Used = false;
        private bool goldPiece3Used = false;


        // =========================
        // Constructor and form load
        // =========================

        public Form1()
        {
            InitializeComponent();
            gameManager = new GameManager(board);

            lampOn = Properties.Resources.lightOn;
            lampOff = Properties.Resources.lightOff;

            highlightImage = Properties.Resources.highlight;

            sqImage = Properties.Resources.spQ;
            srImage = Properties.Resources.spR;
            snImage = Properties.Resources.spK;

            gqImage = Properties.Resources.gpQ;
            grImage = Properties.Resources.gpR;
            gnImage = Properties.Resources.gpK;

            sqGlow = Properties.Resources.spQH;
            srGlow = Properties.Resources.spRH;
            snGlow = Properties.Resources.spKH;

            gqGlow = Properties.Resources.gpQH;
            grGlow = Properties.Resources.gpRH;
            gnGlow = Properties.Resources.gpKH;

            lblStatusZy.Parent = pbxTagZy;
            lblStatusZy.BackColor = Color.Transparent;
            lblStatusZy.BringToFront();
            lblStatusZy.ForeColor = Color.Silver;
            lblStatusZy.Location = new Point(15, 20);

            UpdateTurnLamps();
            ShowSetupPieces();

            customFonts.AddFontFile("Fonts/SliterD.ttf");
            gameFont = new Font(customFonts.Families[0], 15);
            lblStatusZy.Font = gameFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide the Windows Media Player controls
            videoPlayerZy.uiMode = "none";
        }


        // =========================
        // Main board click logic
        // =========================

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
                    lblStatusZy.Text = "That square is taken.";
                    return;
                }

                // White must place on bottom row, Black on top row
                if (gameManager.PlacingWhite && row != 2)
                {
                    lblStatusZy.Text = "Silver must use bottom row.";
                    return;
                }

                if (!gameManager.PlacingWhite && row != 0)
                {
                    lblStatusZy.Text = "Gold must use top row.";
                    return;
                }

                // Get the next piece, place it, and show it on the button
                if (selectedSetupPiece == "")
                {
                    lblStatusZy.Text = "Select a setup piece.";
                    return;
                }

                string piece = GetSelectedSetupPieceCode();

                board.Squares[row, col] = piece;
                btn.BackgroundImage = GetPieceImage(piece);
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Text = "";

                HideSelectedSetupPicture();
                selectedSetupPiece = "";

                if (piece == "SQ") silverPiece1Used = true;
                if (piece == "SR") silverPiece2Used = true;
                if (piece == "SN") silverPiece3Used = true;

                if (piece == "GQ") goldPiece1Used = true;
                if (piece == "GR") goldPiece2Used = true;
                if (piece == "GN") goldPiece3Used = true;

                gameManager.PlaceSetupPiece(row, col);
                ShowSetupPieces();
                ClearHighlights();

                // Show the correct setup message
                if (gameManager.IsSetupFinished())
                {
                    ClearHighlights();
                    UpdateTurnLamps();
                    lblStatusZy.Text = "Setup complete. Enjoy!";
                }
                else if (gameManager.IsWhiteSetupFinished() && gameManager.BlackIndex == 0)
                {
                    ShowSetupPieces();
                    lblStatusZy.Text = "Silver done. Place gold.";
                }
                else
                {
                    if (gameManager.PlacingWhite)
                    {
                        lblStatusZy.Text = $"{piece} Placed. Place next silver piece.";
                    }
                    else
                    {
                        lblStatusZy.Text = $"{piece} Placed. Place next gold piece.";
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

                // Save the selected piece position and show legal move highlights
                gameManager.SelectPiece(row, col);
                ClearHighlights();
                ShowLegalMoves(row, col);
                lblStatusZy.Text = $"Selected piece at ({row},{col})";
            }
            else
            {
                // Clicking the selected piece again cancels the selection
                if (row == gameManager.SelectedRow && col == gameManager.SelectedCol)
                {
                    lblStatusZy.Text = "Selection canceled.";
                    gameManager.ClearSelection();
                    ClearHighlights();
                    return;
                }

                // Pieces cannot move onto occupied squares
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = "That square is occupied.";
                    return;
                }

                // Read the currently selected piece
                string selectedPiece = board.Squares[gameManager.SelectedRow, gameManager.SelectedCol];

                // Check if the move follows the piece rules
                if (!moveValidator.IsValidMove(board, selectedPiece, gameManager.SelectedRow, gameManager.SelectedCol, row, col))
                {
                    lblStatusZy.Text = moveValidator.GetInvalidMoveMessage(selectedPiece);
                    return;
                }

                // Move the piece in the board data
                gameManager.MovePiece(row, col);
                ClearHighlights();

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

                    if (movedPlayer == "S")
                    {
                        lblStatusZy.Text = "Silver wins!";

                        videoPlayerZy.URL = "Videos/silverWin.mp4";
                        videoPlayerZy.Visible = true;
                        videoPlayerZy.uiMode = "none";
                        videoPlayerZy.Ctlcontrols.play();

                        videoHideTimerZy.Stop();
                        videoHideTimerZy.Interval = 8000;
                        videoHideTimerZy.Start();
                    }
                    else
                    {
                        lblStatusZy.Text = "Gold wins!";

                        videoPlayerZy.URL = "Videos/goldWin.mp4";
                        videoPlayerZy.Visible = true;
                        videoPlayerZy.uiMode = "none";
                        videoPlayerZy.Ctlcontrols.play();

                        videoHideTimerZy.Stop();
                        videoHideTimerZy.Interval = 8000;
                        videoHideTimerZy.Start();
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


        // =========================
        // Board helper methods
        // =========================

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

        private Image GetPieceImage(string piece)
        {
            if (piece == "SQ") return sqImage;
            if (piece == "SR") return srImage;
            if (piece == "SN") return snImage;

            if (piece == "GQ") return gqImage;
            if (piece == "GR") return grImage;
            if (piece == "GN") return gnImage;

            return null;
        }

        private Image GetGlowImage(string piece)
        {
            if (piece == "SQ") return sqGlow;
            if (piece == "SR") return srGlow;
            if (piece == "SN") return snGlow;

            if (piece == "GQ") return gqGlow;
            if (piece == "GR") return grGlow;
            if (piece == "GN") return gnGlow;

            return null;
        }


        // =========================
        // Highlight methods
        // =========================

        private void ClearHighlights()
        {
            // Remove highlight images from empty board squares
            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    string[] parts = btn.Tag.ToString().Split(',');
                    int row = int.Parse(parts[0]);
                    int col = int.Parse(parts[1]);

                    if (board.Squares[row, col] == "")
                    {
                        btn.BackgroundImage = null;
                    }
                }
            }
        }

        private void ShowLegalMoves(int fromRow, int fromCol)
        {
            // Show highlight image on every empty square the selected piece can move to
            string piece = board.Squares[fromRow, fromCol];

            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    string[] parts = btn.Tag.ToString().Split(',');
                    int row = int.Parse(parts[0]);
                    int col = int.Parse(parts[1]);

                    if (board.Squares[row, col] == "")
                    {
                        if (moveValidator.IsValidMove(board, piece, fromRow, fromCol, row, col))
                        {
                            btn.BackgroundImage = highlightImage;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                            btn.Text = "";
                        }
                    }
                }
            }
        }

        private void ShowSetupSquares()
        {
            // Show highlight image on the correct setup row for the current player
            ClearHighlights();

            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    string[] parts = btn.Tag.ToString().Split(',');
                    int row = int.Parse(parts[0]);
                    int col = int.Parse(parts[1]);

                    if (board.Squares[row, col] == "")
                    {
                        if (gameManager.PlacingWhite && row == 2)
                        {
                            btn.BackgroundImage = highlightImage;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        if (!gameManager.PlacingWhite && row == 0)
                        {
                            btn.BackgroundImage = highlightImage;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            }
        }


        // =========================
        // Setup piece methods
        // =========================

        private void picSetup1_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "1";
            ShowSetupSquares();
            lblStatusZy.Text = "Setup piece 'Queen' Selected.";
        }

        private void picSetup2_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "2";
            ShowSetupSquares();
            lblStatusZy.Text = "Setup piece 'Rook' Selected.";
        }

        private void picSetup3_Click(object sender, EventArgs e)
        {
            selectedSetupPiece = "3";
            ShowSetupSquares();
            lblStatusZy.Text = "Setup piece 'Knight' Selected.";
        }

        private void ShowSetupPieces()
        {
            // Show the 3 setup piece images for the current setup side
            picSetup1.Visible = true;
            picSetup2.Visible = true;
            picSetup3.Visible = true;

            if (gameManager.PlacingWhite)
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;

                picSetup1.BackgroundImage = sqImage;
                picSetup2.BackgroundImage = srImage;
                picSetup3.BackgroundImage = snImage;

                picSetup1.Visible = !silverPiece1Used;
                picSetup2.Visible = !silverPiece2Used;
                picSetup3.Visible = !silverPiece3Used;
            }
            else
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOff;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOn;

                picSetup1.BackgroundImage = gqImage;
                picSetup2.BackgroundImage = grImage;
                picSetup3.BackgroundImage = gnImage;

                picSetup1.Visible = !goldPiece1Used;
                picSetup2.Visible = !goldPiece2Used;
                picSetup3.Visible = !goldPiece3Used;
            }

            selectedSetupPiece = "";
        }

        private string GetSelectedSetupPieceCode()
        {
            // Convert the selected setup slot into the correct piece code
            if (gameManager.PlacingWhite)
            {
                if (selectedSetupPiece == "1") return "SQ";
                if (selectedSetupPiece == "2") return "SR";
                if (selectedSetupPiece == "3") return "SN";
            }
            else
            {
                if (selectedSetupPiece == "1") return "GQ";
                if (selectedSetupPiece == "2") return "GR";
                if (selectedSetupPiece == "3") return "GN";
            }

            return "";
        }

        private void HideSelectedSetupPicture()
        {
            // Hide the setup picture after that piece was placed
            if (selectedSetupPiece == "1")
                picSetup1.Visible = false;

            if (selectedSetupPiece == "2")
                picSetup2.Visible = false;

            if (selectedSetupPiece == "3")
                picSetup3.Visible = false;
        }

        private Image GetSetupGlowImage(string setupSlot)
        {
            if (gameManager.PlacingWhite)
            {
                if (setupSlot == "1") return sqGlow;
                if (setupSlot == "2") return srGlow;
                if (setupSlot == "3") return snGlow;
            }
            else
            {
                if (setupSlot == "1") return gqGlow;
                if (setupSlot == "2") return grGlow;
                if (setupSlot == "3") return gnGlow;
            }

            return null;
        }

        private Image GetSetupNormalImage(string setupSlot)
        {
            if (gameManager.PlacingWhite)
            {
                if (setupSlot == "1") return sqImage;
                if (setupSlot == "2") return srImage;
                if (setupSlot == "3") return snImage;
            }
            else
            {
                if (setupSlot == "1") return gqImage;
                if (setupSlot == "2") return grImage;
                if (setupSlot == "3") return gnImage;
            }

            return null;
        }


        // =========================
        // Board hover methods
        // =========================

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


        // =========================
        // Setup hover methods
        // =========================

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


        // =========================
        // Restart button methods
        // =========================

        private void btnRestartZy_Click(object sender, EventArgs e)
        {
            // Reset all game data
            gameManager.ResetGame();

            silverPiece1Used = false;
            silverPiece2Used = false;
            silverPiece3Used = false;

            goldPiece1Used = false;
            goldPiece2Used = false;
            goldPiece3Used = false;

            UpdateTurnLamps();

            // Clear all board button text and images
            foreach (Control control in tblBoardZy.Controls)
            {
                if (control is Button btn)
                {
                    btn.Text = "";
                    btn.BackgroundImage = null;
                }
            }

            ShowSetupPieces();
            lblStatusZy.Text = "Game has been reset.";
            videoPlayerZy.Visible = false;

            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;
            gameManager.PlacingWhite = true;
        }

        private void btnRestartZy_MouseEnter(object sender, EventArgs e)
        {
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtnHover;
        }

        private void btnRestartZy_MouseLeave(object sender, EventArgs e)
        {
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtn;
        }


        // =========================
        // Turn lamp method
        // =========================

        private void UpdateTurnLamps()
        {
            // Keep both lamps off during setup
            if (gameManager.SetupPhase)
            {
                picLampSilverZy.BackgroundImage = lampOff;
                picLampGoldZy.BackgroundImage = lampOff;
                return;
            }

            // Turn on the correct lamp for the active player
            if (gameManager.CurrentPlayer == "S")
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


        // =========================
        // Video timer methods
        // =========================

        private void videoHideTimer_Tick(object sender, EventArgs e)
        {
            videoHideTimerZy.Stop();
            videoPlayerZy.Visible = false;
        }

        private void videoHideTimerZy_Tick(object sender, EventArgs e)
        {
            videoHideTimerZy.Stop();
            videoPlayerZy.Visible = false;
        }

        private void btnGoldSetupZy_Click(object sender, EventArgs e)
        {
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOn;
            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOff;

            gameManager.PlacingWhite = false;
            ShowSetupPieces();
        }

        private void btnSilverSetupZy_Click(object sender, EventArgs e)
        {
            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;

            gameManager.PlacingWhite = true;
            ShowSetupPieces();
        }
    }
}