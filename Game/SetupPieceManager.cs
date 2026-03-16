using System.Drawing;

namespace TicTacChess
{
    public class SetupPieceManager
    {
        private Image sqImage;
        private Image srImage;
        private Image snImage;

        private Image gqImage;
        private Image grImage;
        private Image gnImage;

        private Image sqGlow;
        private Image srGlow;
        private Image snGlow;

        private Image gqGlow;
        private Image grGlow;
        private Image gnGlow;

        public SetupPieceManager(
            Image sqImage, Image srImage, Image snImage,
            Image gqImage, Image grImage, Image gnImage,
            Image sqGlow, Image srGlow, Image snGlow,
            Image gqGlow, Image grGlow, Image gnGlow)
        {
            this.sqImage = sqImage;
            this.srImage = srImage;
            this.snImage = snImage;

            this.gqImage = gqImage;
            this.grImage = grImage;
            this.gnImage = gnImage;

            this.sqGlow = sqGlow;
            this.srGlow = srGlow;
            this.snGlow = snGlow;

            this.gqGlow = gqGlow;
            this.grGlow = grGlow;
            this.gnGlow = gnGlow;
        }

        
        public string GetSelectedSetupPieceCode(bool placingWhite, string selectedSetupPiece)
        {
            if (placingWhite)
            {
                if (selectedSetupPiece == "1") return "SQ";
                if (selectedSetupPiece == "2") return "SR";
                if (selectedSetupPiece == "3") return "SN";
            }
            else
            {
                if (selectedSetupPiece == "1") return "GQ";
                if (selectedSetupPiece == "2") return "GR";
                if (selectedSetupPiece == "3") return "GN";
            }

            return "";
        }

        public Image GetSetupGlowImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return sqGlow;
                if (setupSlot == "2") return srGlow;
                if (setupSlot == "3") return snGlow;
            }
            else
            {
                if (setupSlot == "1") return gqGlow;
                if (setupSlot == "2") return grGlow;
                if (setupSlot == "3") return gnGlow;
            }

            return null;
        }

        public Image GetSetupNormalImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return sqImage;
                if (setupSlot == "2") return srImage;
                if (setupSlot == "3") return snImage;
            }
            else
            {
                if (setupSlot == "1") return gqImage;
                if (setupSlot == "2") return grImage;
                if (setupSlot == "3") return gnImage;
            }

            return null;
        }
    }
}