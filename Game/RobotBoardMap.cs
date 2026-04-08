using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacChess.Game
{
    public class RobotBoardMap
    {
        public static (int rs, int hs, int vs) GetPosition(int row, int col)
        {
            if (row == 0 && col == 0) return (250, 520, 1300);
            if (row == 0 && col == 1) return (210, 1000, 1300);
            if (row == 0 && col == 2) return (185, 1500, 1300);

            if (row == 1 && col == 0) return (140, 370, 1300);
            if (row == 1 && col == 1) return (130, 850, 1300);
            if (row == 1 && col == 2) return (95, 1350, 1300);

            if (row == 2 && col == 0) return (25, 300, 1300);
            if (row == 2 && col == 1) return (25, 850, 1300);
            if (row == 2 && col == 2) return (25, 1350, 1300);

            return (0, 0, 0);
        }
    }
}
      