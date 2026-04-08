using System.IO.Ports;
using TicTacChess.Game;

namespace TicTacChess
{
    public class ArduinoManager
    {
        private SerialPort port;
        public Action OnArduinoReady;

        public ArduinoManager()
        {
            port = new SerialPort("COM8", 115200);
            port.DataReceived += Port_DataReceived;
        }

        public void Connect()
        {
            try
            {
                if (!port.IsOpen)
                {
                    port.Open();
                    MessageBox.Show("Arduino connected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        public void SendCommand(string command)
        {
            if (port.IsOpen)
            {
                port.WriteLine(command);
                MessageBox.Show("Sent: " + command);
            }
        }

        public void SendMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (!port.IsOpen)
                return;

            var fromPos = RobotBoardMap.GetPosition(fromRow, fromCol);
            var toPos = RobotBoardMap.GetPosition(toRow, toCol);

            port.WriteLine($"RS:{fromPos.rs}");
            port.WriteLine($"HS:{fromPos.hs}");
            port.WriteLine($"VS:{fromPos.vs}");

            port.WriteLine("CS:1");
            port.WriteLine("SS:1");

            port.WriteLine($"RS:{toPos.rs}");
            port.WriteLine($"HS:{toPos.hs}");
            port.WriteLine($"VS:{toPos.vs}");

            port.WriteLine("CS:0");
            port.WriteLine("SS:0");
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message = port.ReadLine();

            Console.WriteLine("Arduino: " + message);

            if (message.Contains("Ready"))
            {
                OnArduinoReady?.Invoke();
            }
        }
    }
}