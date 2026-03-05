using TicTacChess.Game;

namespace TicTacChess
{
    public partial class Form1 : Form
    {
        Board board = new Board();
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

            // if empty, place an X, else clear it (simple toggle)
            if (board.Squares[row, col] == "")
                board.Squares[row, col] = "X";
            else
                board.Squares[row, col] = "";

            // update the UI square text to match the board state
            btn.Text = board.Squares[row, col];

            lblStatusZy.Text = $"Square ({row},{col}) = '{board.Squares[row, col]}'";
        }

    }
}
