namespace TicTacChess.Game
{
    public class GameManager
    {
        // keeps the board object
        public Board Board { get; private set; }

        // game state flags
        public bool SetupPhase { get; private set; }
        public bool PlacingWhite { get; set; }
        public bool WinCheckActive { get; private set; }
        public bool GameOver { get; private set; }

        // keeps track of setup order
        public int WhiteIndex { get; private set; }
        public int BlackIndex { get; private set; }

        // stores the selected piece position
        public int SelectedRow { get; private set; }
        public int SelectedCol { get; private set; }

        // W = White, B = Black
        public string CurrentPlayer { get; private set; }

        // setup order for each side
        private string[] whiteToPlace = { "SQ", "SR", "SKN", "SK", "SW" };
        private string[] blackToPlace = { "GQ", "GR", "GKN", "GK", "GW" };
        public int MaxSetupPieces = 3;

        public GameManager(Board board)
        {
            Board = board;
            ResetGame();
        }

        public void ResetGame()
        {
            // reset board and all game values
            Board.Clear();

            SetupPhase = true;
            PlacingWhite = true;
            WinCheckActive = false;
            GameOver = false;

            WhiteIndex = 0;
            BlackIndex = 0;

            SelectedRow = -1;
            SelectedCol = -1;

            CurrentPlayer = "S";
        }

        public bool HasSelectedPiece()
        {
            // -1 means nothing is selected
            return SelectedRow != -1;
        }

        public void SelectPiece(int row, int col)
        {
            // save which piece the player clicked
            SelectedRow = row;
            SelectedCol = col;
        }

        public void ClearSelection()
        {
            // clear current selection
            SelectedRow = -1;
            SelectedCol = -1;
        }

        public string GetNextSetupPiece()
        {
            // returns the next piece that should be placed
            if (PlacingWhite)
            {
                return whiteToPlace[WhiteIndex];
            }

            return blackToPlace[BlackIndex];
        }

        public void PlaceSetupPiece(int row, int col)
        {

            if (PlacingWhite)
            {
                WhiteIndex++;
            }
            else
            {
                BlackIndex++;
            }

            // finish only when ALL 5 are placed
            if (WhiteIndex >= 3 && BlackIndex >= 3)
            {
                SetupPhase = false;
            }
            else
            {
                //  AUTO SWITCH AFTER 3 PIECES
                if (PlacingWhite && WhiteIndex >= 3 && BlackIndex < 3)
                {
                    PlacingWhite = false;
                }
                else if (!PlacingWhite && BlackIndex >= 3 && WhiteIndex < 3)
                {
                    PlacingWhite = true;
                }

                //  AFTER BOTH DID 3 → continue remaining pieces normally
                else if (WhiteIndex >= 3)
                {
                    PlacingWhite = false;
                }
                else if (BlackIndex >= 3)
                {
                    PlacingWhite = true;
                }
            }
        }

        public bool IsSetupFinished()
        {
            return WhiteIndex >= 3 && BlackIndex >= 3;
        }

        public bool IsWhiteSetupFinished()
        {
            return WhiteIndex >= 5;
        }

        public void MovePiece(int toRow, int toCol)
        {
            string selectedPiece = Board.Squares[SelectedRow, SelectedCol];
            string targetPiece = Board.Squares[toRow, toCol];

            // Wizard swap logic
            if ((selectedPiece == "SW" || selectedPiece == "GW") &&
                targetPiece != "" &&
                targetPiece.StartsWith(CurrentPlayer))
            {
                // swap positions
                Board.Squares[toRow, toCol] = selectedPiece;
                Board.Squares[SelectedRow, SelectedCol] = targetPiece;
            }
            else
            {
                // normal move
                Board.Squares[toRow, toCol] = selectedPiece;
                Board.Squares[SelectedRow, SelectedCol] = "";
            }

            // activate win checking after first move
            WinCheckActive = true;
        }

        public void SwitchTurn()
        {
            // swap turns after a valid move
            if (CurrentPlayer == "S")
            {
                CurrentPlayer = "G";
            }
            else
            {
                CurrentPlayer = "S";
            }
        }

        public void SetGameOver()
        {
            GameOver = true;
        }
    }
}