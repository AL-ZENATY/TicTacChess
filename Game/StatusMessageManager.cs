using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacChess
{
    public class StatusMessageManager
    {
        public string GameOverMessage()
        {
            return "No moves - Game finished";
        }

        public string SquareOccupiedMessage()
        {
            return "Square already occupied";
        }

        public string PlaceSilverBottomMessage()
        {
            return "Place Silver at the bottom";
        }

        public string PlaceGoldTopMessage()
        {
            return "Place Gold at the top";
        }

        public string ChooseSetupPieceMessage()
        {
            return "Choose setup piece first";
        }

        public string SetupCompleteMessage()
        {
            return "Setup complete - BEGIN!";
        }

        public string SilverFinishedMessage()
        {
            return "Silver finished - Place Gold";
        }

        public string GoldFinishedMessage()
        {
            return "Gold finished - Place Silver";
        }

        public string SelectedPieceMessage(int row, int col)
        {
            return $"Selected piece at '{row},{col}'";
        }

        public string NotYourPieceMessage()
        {
            return "That is not your piece";
        }

        public string SelectPieceFirstMessage()
        {
            return "Select a piece first";
        }

        public string MoveNextPlayerMessage(string selectedPiece, int row, int col)
        {
            return $"'{selectedPiece}' to '{row},{col}' - Next Player";
        }

        public string ResetMessage()
        {
            return "Game has been reset";
        }

        public string SilverWinMessage()
        {
            return "Silver Player Wins!";
        }

        public string GoldWinMessage()
        {
            return "Gold Player Wins!";
        }

        public string SetupPieceSelectedMessage(string pieceName)
        {
            return $"Setup piece '{pieceName}' Selected";
        }

        public string PiecePlacedMessage(string piece)
        {
            return $"'{piece}' Placed - Next piece";
        }
    }
}
