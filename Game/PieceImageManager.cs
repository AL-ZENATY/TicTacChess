using System.Drawing;

namespace TicTacChess
{
    public class PieceImageManager
    {
        private Image sqImage;
        private Image srImage;
        private Image sknImage;
        private Image skImage;

        private Image gqImage;
        private Image grImage;
        private Image gknImage;
        private Image gkImage;

        private Image sqGlow;
        private Image srGlow;
        private Image sknGlow;
        private Image skGlow;

        private Image gqGlow;
        private Image grGlow;
        private Image gknGlow;
        private Image gkGlow;

        public PieceImageManager()
        {
            sqImage = Properties.Resources.spQ;
            srImage = Properties.Resources.spR;
            sknImage = Properties.Resources.spKN;
           //skImage = Properties.Resources.spK;

            gqImage = Properties.Resources.gpQ;
            grImage = Properties.Resources.gpR;
            gknImage = Properties.Resources.gpKN;
         //   gkImage = Properties.Resources.gpK;

            sqGlow = Properties.Resources.spQH;
            srGlow = Properties.Resources.spRH;
            sknGlow = Properties.Resources.spKNH;
         //   skGlow = Properties.Resources.spKH;

            gqGlow = Properties.Resources.gpQH;
            grGlow = Properties.Resources.gpRH;
            gknGlow = Properties.Resources.gpKNH;
          //  gkGlow = Properties.Resources.gpKH;
        }

        public Image GetPieceImage(string piece)
        {
            if (piece == "SQ") return sqImage;
            if (piece == "SR") return srImage;
            if (piece == "SKN") return sknImage;
            if (piece == "SK") return skImage;

            if (piece == "GQ") return gqImage;
            if (piece == "GR") return grImage;
            if (piece == "GKN") return gknImage;
            if (piece == "GK") return gkImage;

            return null;
        }

        public Image GetGlowImage(string piece)
        {
            if (piece == "SQ") return sqGlow;
            if (piece == "SR") return srGlow;
            if (piece == "SKN") return sknGlow;
            if (piece == "SK") return skGlow;

            if (piece == "GQ") return gqGlow;
            if (piece == "GR") return grGlow;
            if (piece == "GKN") return gknGlow;
            if (piece == "GK") return gkGlow;

            return null;
        }

        public Image GetSetupGlowImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return sqGlow;
                if (setupSlot == "2") return srGlow;
                if (setupSlot == "3") return sknGlow;
                if (setupSlot == "4") return skGlow;
            }
            else
            {
                if (setupSlot == "1") return gqGlow;
                if (setupSlot == "2") return grGlow;
                if (setupSlot == "3") return gknGlow;
                if (setupSlot == "4") return gkGlow;
            }

            return null;
        }

        public Image GetSetupNormalImage(bool placingWhite, string setupSlot)
        {
            if (placingWhite)
            {
                if (setupSlot == "1") return sqImage;
                if (setupSlot == "2") return srImage;
                if (setupSlot == "3") return sknImage;
                if (setupSlot == "4") return skImage;
            }
            else
            {
                if (setupSlot == "1") return gqImage;
                if (setupSlot == "2") return grImage;
                if (setupSlot == "3") return gknImage;
                if (setupSlot == "4") return gkImage;
            }

            return null;
        }
    }
}