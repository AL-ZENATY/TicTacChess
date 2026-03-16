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
        private string[] whiteToPlace = { "SQ", "SR", "SN" };
        private string[] blackToPlace = { "GQ", "GR", "GN" };

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
            // place the next setup piece on the board
            string piece = GetNextSetupPiece();

            if (PlacingWhite)
            {
                WhiteIndex++;
            }
            else
            {
                BlackIndex++;
            }

            // only end setup when BOTH sides are finished
            if (WhiteIndex >= whiteToPlace.Length && BlackIndex >= blackToPlace.Length)
            {
                SetupPhase = false;
            }
            else if (WhiteIndex >= whiteToPlace.Length)
            {
                PlacingWhite = false;
            }
            else if (BlackIndex >= blackToPlace.Length)
            {
                PlacingWhite = true;
            }
        }

        public bool IsSetupFinished()
        {
            return !SetupPhase;
        }

        public bool IsWhiteSetupFinished()
        {
            return !PlacingWhite && WhiteIndex >= whiteToPlace.Length;
        }

        public void MovePiece(int toRow, int toCol)
        {
            // move the selected piece to the new square
            string selectedPiece = Board.Squares[SelectedRow, SelectedCol];

            Board.Squares[toRow, toCol] = selectedPiece;
            Board.Squares[SelectedRow, SelectedCol] = "";

            // after the first move, win checking becomes active
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