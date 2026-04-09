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

        // Handles all status text messages
        private StatusMessageManager status = new StatusMessageManager();

        // Handles all piece and glow images
        private PieceImageManager pieceImages;

        // Handles communication with the Arduino
        private ArduinoManager arduino;
        private RobotController robot;
        private bool useRobot = true;


        // =========================
        // UI images
        // =========================

        // Turn lamp images
        private Image lampOn;
        private Image lampOff;

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
        private bool showExtraPieces = false; // false = Q,R,KN | true = K,W

        // Tracks which silver setup pieces are already used
        private bool silverPiece1Used = false;
        private bool silverPiece2Used = false;
        private bool silverPiece3Used = false;
        private bool silverPiece4Used = false;
        private bool silverPiece5Used = false;

        // Tracks which gold setup pieces are already used
        private bool goldPiece1Used = false;
        private bool goldPiece2Used = false;
        private bool goldPiece3Used = false;
        private bool goldPiece4Used = false;
        private bool goldPiece5Used = false;


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

            pieceImages = new PieceImageManager();
            arduino = new ArduinoManager();
            arduino.OnArduinoReady = OnArduinoReady;
            robot = new RobotController(arduino);

            if (useRobot)
            {
                arduino.Connect();
            }

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

            btnZeroRobotZy.FlatStyle = FlatStyle.Flat;
            btnZeroRobotZy.FlatAppearance.BorderSize = 0;

            btnZeroRobotZy.BackColor = Color.Transparent;
            btnZeroRobotZy.UseVisualStyleBackColor = false;

            btnZeroRobotZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnZeroRobotZy.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnZeroRobotZy.Parent = this;
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
                lblStatusZy.Text = status.GameOverMessage();
                return;
            }

            // Setup phase: players place pieces on the board
            if (gameManager.SetupPhase)
            {
                // A piece can only be placed on an empty square
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = status.SquareOccupiedMessage();
                    return;
                }

                // White must place on bottom row, Black on top row
                if (gameManager.PlacingWhite && row != 2)
                {
                    lblStatusZy.Text = status.PlaceSilverBottomMessage();
                    return;
                }

                if (!gameManager.PlacingWhite && row != 0)
                {
                    lblStatusZy.Text = status.PlaceGoldTopMessage();
                    return;
                }

                // Player must first choose a setup piece
                if (selectedSetupPiece == "")
                {
                    lblStatusZy.Text = status.ChooseSetupPieceMessage();
                    return;
                }

                string piece = GetSelectedSetupPieceCode();

                board.Squares[row, col] = piece;
                btn.BackgroundImage = pieceImages.GetPieceImage(piece);
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Text = "";

                HideSelectedSetupPicture();
                selectedSetupPiece = "";

                if (piece == "SQ") silverPiece1Used = true;
                if (piece == "SR") silverPiece2Used = true;
                if (piece == "SKN") silverPiece3Used = true;
                if (piece == "SK") silverPiece4Used = true;
                if (piece == "SW") silverPiece5Used = true;

                if (piece == "GQ") goldPiece1Used = true;
                if (piece == "GR") goldPiece2Used = true;
                if (piece == "GKN") goldPiece3Used = true;
                if (piece == "GK") goldPiece4Used = true;
                if (piece == "GW") goldPiece5Used = true;

                gameManager.PlaceSetupPiece(row, col);

                // AUTO SWITCH: if extra pieces done → go back to 3 pieces
                if (showExtraPieces)
                {
                    if (gameManager.PlacingWhite && silverPiece4Used && silverPiece5Used)
                    {
                        showExtraPieces = false;
                    }

                    if (!gameManager.PlacingWhite && goldPiece4Used && goldPiece5Used)
                    {
                        showExtraPieces = false;
                    }
                }

                ShowSetupPieces();
                ClearHighlights();

                // Show the correct setup message
                if (gameManager.IsSetupFinished())
                {
                    ClearHighlights();
                    UpdateTurnLamps();
                    lblStatusZy.Text = status.SetupCompleteMessage();

                    // disable setup UI
                    btnTogglePiecesZy.Enabled = false;
                    btnGoldSetupZy.Enabled = false;
                    btnSilverSetupZy.Enabled = false;

                    picSetup1.Visible = false;
                    picSetup2.Visible = false;
                    picSetup3.Visible = false;
                    picSetup4.Visible = false;
                    picSetup5.Visible = false;
                }

                else if (gameManager.IsWhiteSetupFinished() && gameManager.BlackIndex == 0)
                {
                    ShowSetupPieces();
                    lblStatusZy.Text = status.SilverFinishedMessage();
                }
                else if (gameManager.BlackIndex >= 3 && gameManager.WhiteIndex < 3)
                {
                    ShowSetupPieces();
                    lblStatusZy.Text = status.GoldFinishedMessage();
                }
                else
                {
                    lblStatusZy.Text = status.PiecePlacedMessage(piece);
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
                    lblStatusZy.Text = status.SelectPieceFirstMessage();
                    return;
                }

                // Player can only select their own piece
                if (!piece.StartsWith(gameManager.CurrentPlayer))
                {
                    lblStatusZy.Text = status.NotYourPieceMessage();
                    return;
                }

                // Save the selected piece position and show legal move highlights
                SelectBoardPiece(row, col);
            }
            else
            {
                // Normal selection switching
                string clickedPiece = board.Squares[row, col];
                string selectedPiece = board.Squares[gameManager.SelectedRow, gameManager.SelectedCol];

                // allow wizard to swap instead
                if (clickedPiece != "" && clickedPiece.StartsWith(gameManager.CurrentPlayer))
                {
                    if (!(selectedPiece == "SW" || selectedPiece == "GW"))
                    {
                        SelectBoardPiece(row, col);
                        return;
                    }
                }

                // Normal game rule: occupied squares are not allowed here
                string targetPiece = board.Squares[row, col];

                // Only block enemy pieces (no capturing)
                if (targetPiece != "" && !targetPiece.StartsWith(gameManager.CurrentPlayer))
                {
                    lblStatusZy.Text = status.SquareOccupiedMessage();
                    return;
                }

                // Check if the move follows the piece rules
                if (!moveValidator.IsValidMove(board, selectedPiece, gameManager.SelectedRow, gameManager.SelectedCol, row, col))
                {
                    lblStatusZy.Text = moveValidator.GetInvalidMoveMessage(selectedPiece);
                    return;
                }


                // Move is valid - update the board data
                gameManager.MovePiece(row, col);

                if (useRobot)
                {
                    robot.StartMove(gameManager.SelectedRow, gameManager.SelectedCol, row, col);
                }

                ClearHighlights();

                // Show the moved piece on the new button
                // update BOTH buttons correctly
                Button newButton = GetButtonByPosition(row, col);
                Button oldButton = GetButtonByPosition(gameManager.SelectedRow, gameManager.SelectedCol);

                string newPiece = board.Squares[row, col];
                string oldPiece = board.Squares[gameManager.SelectedRow, gameManager.SelectedCol];

                // new position
                newButton.BackgroundImage = pieceImages.GetPieceImage(newPiece);
                newButton.BackgroundImageLayout = ImageLayout.Zoom;
                newButton.Text = "";

                // old position
                if (oldPiece != "")
                {
                    oldButton.BackgroundImage = pieceImages.GetPieceImage(oldPiece);
                }
                else
                {
                    oldButton.BackgroundImage = null;
                }

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
                        ShowWinVideo(status.SilverWinMessage(), "Videos/silverWin.mp4");
                    }
                    else
                    {
                        ShowWinVideo(status.GoldWinMessage(), "Videos/goldWin.mp4");
                    }
                }
                else
                {
                    lblStatusZy.Text = status.MoveNextPlayerMessage(selectedPiece, row, col);
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

        private void SelectBoardPiece(int row, int col)
        {

            // Select a board piece and show its legal moves
            gameManager.SelectPiece(row, col);
            ClearHighlights();
            ShowLegalMoves(row, col);
            lblStatusZy.Text = status.SelectedPieceMessage(row, col);
        }

        private void ShowWinVideo(string winnerText, string videoPath)
        {
            // Show the winner text and play the correct win video
            lblStatusZy.Text = winnerText;

            videoPlayerZy.URL = videoPath;
            videoPlayerZy.Visible = true;
            videoPlayerZy.uiMode = "none";
            videoPlayerZy.Ctlcontrols.play();

            videoHideTimerZy.Stop();
            videoHideTimerZy.Interval = 8000;
            videoHideTimerZy.Start();
        }

        private void SelectSetupPiece(string pieceNumber, string pieceName)
        {
            // Save which setup piece was chosen and highlight valid setup squares
            selectedSetupPiece = pieceNumber;
            ShowSetupSquares();
            lblStatusZy.Text = status.SetupPieceSelectedMessage(pieceName);
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

        private void ShowSetupPieces()
        {
            // Show the setup piece images for the current setup side
            picSetup1.Visible = true;
            picSetup2.Visible = true;
            picSetup3.Visible = true;
            picSetup4.Visible = true;
            picSetup5.Visible = true;

            if (gameManager.PlacingWhite)
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;

                if (!showExtraPieces)
                {
                    // show 3 pieces
                    picSetup1.BackgroundImage = pieceImages.GetPieceImage("SQ");
                    picSetup2.BackgroundImage = pieceImages.GetPieceImage("SR");
                    picSetup3.BackgroundImage = pieceImages.GetPieceImage("SKN");

                    picSetup1.Visible = !silverPiece1Used;
                    picSetup2.Visible = !silverPiece2Used;
                    picSetup3.Visible = !silverPiece3Used;

                    picSetup4.Visible = false;
                    picSetup5.Visible = false;
                }
                else
                {
                    // show 2 pieces
                    picSetup4.BackgroundImage = pieceImages.GetPieceImage("SK");
                    picSetup5.BackgroundImage = pieceImages.GetPieceImage("SW");

                    picSetup4.Visible = !silverPiece4Used;
                    picSetup5.Visible = !silverPiece5Used;

                    picSetup1.Visible = false;
                    picSetup2.Visible = false;
                    picSetup3.Visible = false;
                }
            }
            else
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOff;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOn;

                if (!showExtraPieces)
                {
                    picSetup1.BackgroundImage = pieceImages.GetPieceImage("GQ");
                    picSetup2.BackgroundImage = pieceImages.GetPieceImage("GR");
                    picSetup3.BackgroundImage = pieceImages.GetPieceImage("GKN");

                    picSetup1.Visible = !goldPiece1Used;
                    picSetup2.Visible = !goldPiece2Used;
                    picSetup3.Visible = !goldPiece3Used;

                    picSetup4.Visible = false;
                    picSetup5.Visible = false;
                }
                else
                {
                    picSetup4.BackgroundImage = pieceImages.GetPieceImage("GK");
                    picSetup5.BackgroundImage = pieceImages.GetPieceImage("GW");

                    picSetup4.Visible = !goldPiece4Used;
                    picSetup5.Visible = !goldPiece5Used;

                    picSetup1.Visible = false;
                    picSetup2.Visible = false;
                    picSetup3.Visible = false;
                }
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
                if (selectedSetupPiece == "3") return "SKN";
                if (selectedSetupPiece == "4") return "SK";
                if (selectedSetupPiece == "5") return "SW";
            }
            else
            {
                if (selectedSetupPiece == "1") return "GQ";
                if (selectedSetupPiece == "2") return "GR";
                if (selectedSetupPiece == "3") return "GKN";
                if (selectedSetupPiece == "4") return "GK";
                if (selectedSetupPiece == "5") return "GW";
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

            if (selectedSetupPiece == "4")
                picSetup4.Visible = false;

            if (selectedSetupPiece == "5")
                picSetup5.Visible = false;
        }

        private void picSetup1_Click(object sender, EventArgs e)
        {
            // Select the first setup piece
            SelectSetupPiece("1", "Queen");
        }

        private void picSetup2_Click(object sender, EventArgs e)
        {
            // Select the second setup piece
            SelectSetupPiece("2", "Rook");
        }

        private void picSetup3_Click(object sender, EventArgs e)
        {
            // Select the third setup piece
            SelectSetupPiece("3", "Knight");
        }

        private void picSetup4_Click(object sender, EventArgs e)
        {
            // Select the fourth setup piece
            SelectSetupPiece("4", "King");
        }

        private void picSetup5_Click(object sender, EventArgs e)
        {
            // Select the fifth setup piece
            SelectSetupPiece("5", "Wizard");
        }


        // =========================
        // Board hover methods
        // =========================

        private void BoardCell_MouseEnter(object sender, EventArgs e)
        {
            // Show glow image when the mouse enters a board piece
            Button btn = (Button)sender;

            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            string piece = board.Squares[row, col];

            if (piece != "")
            {
                btn.BackgroundImage = pieceImages.GetGlowImage(piece);
            }
        }

        private void BoardCell_MouseLeave(object sender, EventArgs e)
        {
            // Restore normal image when the mouse leaves a board piece
            Button btn = (Button)sender;

            string[] parts = btn.Tag.ToString().Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            string piece = board.Squares[row, col];

            if (piece != "")
            {
                btn.BackgroundImage = pieceImages.GetPieceImage(piece);
            }
        }


        // =========================
        // Setup hover methods
        // =========================

        private void SetupPiece_MouseEnter(object sender, EventArgs e)
        {
            // Show glow image when hovering over a setup piece
            PictureBox pic = (PictureBox)sender;

            if (!pic.Visible)
                return;

            if (pic == picSetup1) pic.BackgroundImage = pieceImages.GetSetupGlowImage(gameManager.PlacingWhite, "1");
            if (pic == picSetup2) pic.BackgroundImage = pieceImages.GetSetupGlowImage(gameManager.PlacingWhite, "2");
            if (pic == picSetup3) pic.BackgroundImage = pieceImages.GetSetupGlowImage(gameManager.PlacingWhite, "3");
            if (pic == picSetup4) pic.BackgroundImage = pieceImages.GetSetupGlowImage(gameManager.PlacingWhite, "4");
            if (pic == picSetup5) pic.BackgroundImage = pieceImages.GetSetupGlowImage(gameManager.PlacingWhite, "5");
        }

        private void SetupPiece_MouseLeave(object sender, EventArgs e)
        {
            // Restore normal image when leaving a setup piece
            PictureBox pic = (PictureBox)sender;

            if (!pic.Visible)
                return;

            if (pic == picSetup1) pic.BackgroundImage = pieceImages.GetSetupNormalImage(gameManager.PlacingWhite, "1");
            if (pic == picSetup2) pic.BackgroundImage = pieceImages.GetSetupNormalImage(gameManager.PlacingWhite, "2");
            if (pic == picSetup3) pic.BackgroundImage = pieceImages.GetSetupNormalImage(gameManager.PlacingWhite, "3");
            if (pic == picSetup4) pic.BackgroundImage = pieceImages.GetSetupNormalImage(gameManager.PlacingWhite, "4");
            if (pic == picSetup5) pic.BackgroundImage = pieceImages.GetSetupNormalImage(gameManager.PlacingWhite, "5");
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
            silverPiece4Used = false;
            silverPiece5Used = false;

            goldPiece1Used = false;
            goldPiece2Used = false;
            goldPiece3Used = false;
            goldPiece4Used = false;
            goldPiece5Used = false;

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
            lblStatusZy.Text = status.ResetMessage();
            videoPlayerZy.Visible = false;

            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;
            gameManager.PlacingWhite = true;

            // re-enable setup UI
            btnTogglePiecesZy.Enabled = true;
            btnGoldSetupZy.Enabled = true;
            btnSilverSetupZy.Enabled = true;

            // show setup pieces again
            picSetup1.Visible = true;
            picSetup2.Visible = true;
            picSetup3.Visible = true;
            picSetup4.Visible = true;
            picSetup5.Visible = true;
        }

        private void btnRestartZy_MouseEnter(object sender, EventArgs e)
        {
            // Show hover image for restart button
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtnHover;
        }

        private void btnRestartZy_MouseLeave(object sender, EventArgs e)
        {
            // Restore normal image for restart button
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtn;
        }


        // =========================
        // Setup side switch button methods
        // =========================

        private void btnGoldSetupZy_Click(object sender, EventArgs e)
        {
            // Switch setup side to gold
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOn;
            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOff;

            gameManager.PlacingWhite = false;
            ShowSetupPieces();
        }

        private void btnSilverSetupZy_Click(object sender, EventArgs e)
        {
            // Switch setup side to silver
            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;

            gameManager.PlacingWhite = true;
            ShowSetupPieces();
        }

        private void btnTogglePiecesZy_Click(object sender, EventArgs e)
        {
            showExtraPieces = !showExtraPieces; // switch true/false
            btnTogglePiecesZy.Text = showExtraPieces ? "2 Pieces" : "3 Pieces";
            gameManager.MaxSetupPieces = showExtraPieces ? 2 : 3;

            btnTogglePiecesZy.BackgroundImage = Properties.Resources.extraBtn;
            ShowSetupPieces();

        }

        private void btnTogglePiecesZy_MouseEnter(object sender, EventArgs e)
        {
            btnTogglePiecesZy.BackgroundImage = Properties.Resources.extraBtnH;
        }

        private void btnTogglePiecesZy_MouseLeave(object sender, EventArgs e)
        {
            btnTogglePiecesZy.BackgroundImage = Properties.Resources.extraBtn;
        }


        // =========================
        // Turn lamp methods
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

        private void videoHideTimerZy_Tick(object sender, EventArgs e)
        {
            // Hide the win video after the timer ends
            videoHideTimerZy.Stop();
            videoPlayerZy.Visible = false;
        }

        private void OnArduinoReady()
        {
            if (useRobot)
            {
                robot.ArduinoStepReady();
            }
        }

        private void btnZeroRobotZy_Click(object sender, EventArgs e)
        {
            if (useRobot)
            {
                arduino.SendCommand("ZS:0");
            }
        }

        private void btnZeroRobotZy_MouseEnter(object sender, EventArgs e)
        {
            btnZeroRobotZy.BackgroundImage = Properties.Resources.zeroHover;
        }

        private void btnZeroRobotZy_MouseLeave(object sender, EventArgs e)
        {
            btnZeroRobotZy.BackgroundImage = Properties.Resources.zeroOff;
        }

        private void btnZeroRobotZy_MouseDown(object sender, MouseEventArgs e)
        {
            btnZeroRobotZy.BackgroundImage = Properties.Resources.zeroOn;
        }

        private void btnZeroRobotZy_MouseUp(object sender, MouseEventArgs e)
        {
            btnZeroRobotZy.BackgroundImage = Properties.Resources.zeroOff;
        }
    }
}