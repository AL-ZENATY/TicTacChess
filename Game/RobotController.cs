using TicTacChess.Game;

namespace TicTacChess
{
    public class RobotController
    {
        private ArduinoManager arduino;

        private int fromRow;
        private int fromCol;
        private int toRow;
        private int toCol;

        private int moveArduinoCounter = 0;
        private bool moveBusy = false;
        private string command = "";

        public RobotController(ArduinoManager arduino)
        {
            this.arduino = arduino;
        }

        public void StartMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            this.fromRow = fromRow;
            this.fromCol = fromCol;
            this.toRow = toRow;
            this.toCol = toCol;

            var fromPos = RobotBoardMap.GetPosition(fromRow, fromCol);
            var toPos = RobotBoardMap.GetPosition(toRow, toCol);

            MessageBox.Show(
                "FROM: " + fromRow + "," + fromCol +
                " = RS:" + fromPos.rs + " HS:" + fromPos.hs + " VS:" + fromPos.vs +
                "\nTO: " + toRow + "," + toCol +
                " = RS:" + toPos.rs + " HS:" + toPos.hs + " VS:" + toPos.vs
            );

            moveBusy = false;
            moveArduinoCounter = 0;

            NextStep();
        }

        public void NextStep()
        {
            var fromPos = RobotBoardMap.GetPosition(fromRow, fromCol);
            var toPos = RobotBoardMap.GetPosition(toRow, toCol);

            command = "";

            if (moveArduinoCounter == 0)
            {
                command = "RS:" + fromPos.rs;
            }
            else if (moveArduinoCounter == 1)
            {
                command = "HS:" + fromPos.hs;
            }
            else if (moveArduinoCounter == 2)
            {
                command = "VS:" + fromPos.vs;
            }
            else if (moveArduinoCounter == 3)
            {
                command = "CS:1";
            }
            else if (moveArduinoCounter == 4)
            {
                command = "SS:1";
            }
            else if (moveArduinoCounter == 5)
            {
                command = "VS:0";
            }
            else if (moveArduinoCounter == 6)
            {
                command = "RS:" + toPos.rs;
            }
            else if (moveArduinoCounter == 7)
            {
                command = "HS:" + toPos.hs;
            }
            else if (moveArduinoCounter == 8)
            {
                command = "VS:" + toPos.vs;
            }
            else if (moveArduinoCounter == 9)
            {
                command = "CS:0";
            }
            else if (moveArduinoCounter == 10)
            {
                command = "SS:0";
            }
            else if (moveArduinoCounter == 11)
            {
                command = "VS:0";
            }
            else
            {
                return;
            }

            if (moveBusy == false)
            {
                moveBusy = true;
                arduino.SendCommand(command);
            }
        }

        public void ArduinoStepReady()
        {
            moveBusy = false;
            moveArduinoCounter++;
            NextStep();
        }
    }
}