using System.Drawing;

namespace TicTacChess
{
    public class SetupPieceManager
    {
        private Image wqImage;
        private Image wrImage;
        private Image wnImage;

        private Image bqImage;
        private Image brImage;
        private Image bnImage;

        private Image wqGlow;
        private Image wrGlow;
        private Image wnGlow;

        private Image bqGlow;
        private Image brGlow;
        private Image bnGlow;

        public SetupPieceManager(
            Image wqImage, Image wrImage, Image wnImage,
            Image bqImage, Image brImage, Image bnImage,
            Image wqGlow, Image wrGlow, Image wnGlow,
            Image bqGlow, Image brGlow, Image bnGlow)
        {
            this.wqImage = wqImage;
            this.wrImage = wrImage;
            this.wnImage = wnImage;

            this.bqImage = bqImage;
            this.brImage = brImage;
            this.bnImage = bnImage;

            this.wqGlow = wqGlow;
            this.wrGlow = wrGlow;
            this.wnGlow = wnGlow;

            this.bqGlow = bqGlow;
            this.brGlow = brGlow;
            this.bnGlow = bnGlow;
        }

        public string GetSelectedSetupPieceCode(bool placingWhite, string selectedSetupPiece)
        {
            if (placingWhite)
            {
                if (selectedSetupPiece == "1") return "WQ";
                if (selectedSetupPiece == "2") return "WR";
                if (selectedSetupPiece == "3") return "WN";
            }
            else
            {
                if (selectedSetupPiece == "1") return "BQ";
                if (selectedSetupPiece == "2") return "BR";
                if (selectedSetupPiece == "3") return "BN";
            }

            return "";
        }

        public Image GetSetupGlowImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return wqGlow;
                if (setupSlot == "2") return wrGlow;
                if (setupSlot == "3") return wnGlow;
            }
            else
            {
                if (setupSlot == "1") return bqGlow;
                if (setupSlot == "2") return brGlow;
                if (setupSlot == "3") return bnGlow;
            }

            return null;
        }

        public Image GetSetupNormalImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return wqImage;
                if (setupSlot == "2") return wrImage;
                if (setupSlot == "3") return wnImage;
            }
            else
            {
                if (setupSlot == "1") return bqImage;
                if (setupSlot == "2") return brImage;
                if (setupSlot == "3") return bnImage;
            }

            return null;
        }
    }
}