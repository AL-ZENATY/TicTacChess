using System.Drawing;

namespace TicTacChess
{
    public class PieceImageManager
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

        public PieceImageManager()
        {
            sqImage = Properties.Resources.spQ;
            srImage = Properties.Resources.spR;
            snImage = Properties.Resources.spK;

            gqImage = Properties.Resources.gpQ;
            grImage = Properties.Resources.gpR;
            gnImage = Properties.Resources.gpK;

            sqGlow = Properties.Resources.spQH;
            srGlow = Properties.Resources.spRH;
            snGlow = Properties.Resources.spKH;

            gqGlow = Properties.Resources.gpQH;
            grGlow = Properties.Resources.gpRH;
            gnGlow = Properties.Resources.gpKH;
        }

        public Image GetPieceImage(string piece)
        {
            if (piece == "SQ") return sqImage;
            if (piece == "SR") return srImage;
            if (piece == "SN") return snImage;

            if (piece == "GQ") return gqImage;
            if (piece == "GR") return grImage;
            if (piece == "GN") return gnImage;

            return null;
        }

        public Image GetGlowImage(string piece)
        {
            if (piece == "SQ") return sqGlow;
            if (piece == "SR") return srGlow;
            if (piece == "SN") return snGlow;

            if (piece == "GQ") return gqGlow;
            if (piece == "GR") return grGlow;
            if (piece == "GN") return gnGlow;

            return null;
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