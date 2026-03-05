namespace TicTacChess.Game
{
    public class Board
    {
        public string[,] Squares = new string[3, 3];

        public Board()
        {
            Clear();
        }

        public void Clear()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Squares[r, c] = "";
                }
            }
        }
    }
}