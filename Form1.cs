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

        // Tracks which silver setup pieces are already used
        private bool silverPiece1Used = false;
        private bool silverPiece2Used = false;
        private bool silverPiece3Used = false;
        private bool silverPiece4Used = false;

        // Tracks which gold setup pieces are already used
        private bool goldPiece1Used = false;
        private bool goldPiece2Used = false;
        private bool goldPiece3Used = false;
        private bool goldPiece4Used = false;


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

                // Get the next piece, place it, and show it on the button
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

                if (piece == "GQ") goldPiece1Used = true;
                if (piece == "GR") goldPiece2Used = true;
                if (piece == "GKN") goldPiece3Used = true;
                if (piece == "GK") goldPiece4Used = true;

                gameManager.PlaceSetupPiece(row, col);
                ShowSetupPieces();
                ClearHighlights();

                // Show the correct setup message
                if (gameManager.IsSetupFinished())
                {
                    ClearHighlights();
                    UpdateTurnLamps();
                    lblStatusZy.Text = status.SetupCompleteMessage();
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
                // If player clicks another of their own pieces, switch selection
                string clickedPiece = board.Squares[row, col];

                if (clickedPiece != "" && clickedPiece.StartsWith(gameManager.CurrentPlayer))
                {
                    SelectBoardPiece(row, col);
                    return;
                }

                // Pieces cannot move onto occupied squares
                if (board.Squares[row, col] != "")
                {
                    lblStatusZy.Text = status.SquareOccupiedMessage();
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

                if (useRobot)
                {
                    robot.StartMove(gameManager.SelectedRow, gameManager.SelectedCol, row, col);
                }

                ClearHighlights();

                // Show the moved piece on the new button
                btn.BackgroundImage = pieceImages.GetPieceImage(selectedPiece);
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
            // Show the 3 setup piece images for the current setup side
            picSetup1.Visible = true;
            picSetup2.Visible = true;
            picSetup3.Visible = true;
            picSetup4.Visible = true;

            if (gameManager.PlacingWhite)
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;

                picSetup1.BackgroundImage = pieceImages.GetPieceImage("SQ");
                picSetup2.BackgroundImage = pieceImages.GetPieceImage("SR");
                picSetup3.BackgroundImage = pieceImages.GetPieceImage("SKN");
                picSetup4.BackgroundImage = pieceImages.GetPieceImage("SK");

                picSetup1.Visible = !silverPiece1Used;
                picSetup2.Visible = !silverPiece2Used;
                picSetup3.Visible = !silverPiece3Used;
                picSetup4.Visible = !silverPiece4Used;
            }
            else
            {
                btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOff;
                btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOn;

                picSetup1.BackgroundImage = pieceImages.GetPieceImage("GQ");
                picSetup2.BackgroundImage = pieceImages.GetPieceImage("GR");
                picSetup3.BackgroundImage = pieceImages.GetPieceImage("GKN");
                picSetup4.BackgroundImage = pieceImages.GetPieceImage("GK");

                picSetup1.Visible = !goldPiece1Used;
                picSetup2.Visible = !goldPiece2Used;
                picSetup3.Visible = !goldPiece3Used;
                picSetup4.Visible = !goldPiece4Used;
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
            }
            else
            {
                if (selectedSetupPiece == "1") return "GQ";
                if (selectedSetupPiece == "2") return "GR";
                if (selectedSetupPiece == "3") return "GKN";
                if (selectedSetupPiece == "4") return "GK";
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
            // Select the fourth setup piece (King)
            SelectSetupPiece("4", "King");
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

            goldPiece1Used = false;
            goldPiece2Used = false;
            goldPiece3Used = false;
            goldPiece4Used = false;

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
    }
}