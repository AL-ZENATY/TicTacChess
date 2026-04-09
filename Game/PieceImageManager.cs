using System.Drawing;

namespace TicTacChess
{
    public class PieceImageManager
    {
        private Image sqImage;
        private Image srImage;
        private Image sknImage;
        private Image skImage;
        private Image swImage;

        private Image gqImage;
        private Image grImage;
        private Image gknImage;
        private Image gkImage;
        private Image gwImage;

        private Image sqGlow;
        private Image srGlow;
        private Image sknGlow;
        private Image skGlow;
        private Image swGlow;

        private Image gqGlow;
        private Image grGlow;
        private Image gknGlow;
        private Image gkGlow;
        private Image gwGlow;


        public PieceImageManager()
        {
            sqImage = Properties.Resources.spQ;
            srImage = Properties.Resources.spR;
            sknImage = Properties.Resources.spKN;
            skImage = Properties.Resources.spK;
            swImage = Properties.Resources.spW;

            gqImage = Properties.Resources.gpQ;
            grImage = Properties.Resources.gpR;
            gknImage = Properties.Resources.gpKN;
            gkImage = Properties.Resources.gpK;
            gwImage = Properties.Resources.gpW;


            sqGlow = Properties.Resources.spQH;
            srGlow = Properties.Resources.spRH;
            sknGlow = Properties.Resources.spKNH;
            skGlow = Properties.Resources.spKH;
            swGlow = Properties.Resources.spWH;

            gqGlow = Properties.Resources.gpQH;
            grGlow = Properties.Resources.gpRH;
            gknGlow = Properties.Resources.gpKNH;
            gkGlow = Properties.Resources.gpKH;
            gwGlow = Properties.Resources.gpWH;
        }

        public Image GetPieceImage(string piece)
        {
            if (piece == "SQ") return sqImage;
            if (piece == "SR") return srImage;
            if (piece == "SKN") return sknImage;
            if (piece == "SK") return skImage;
            if (piece == "SW") return swImage;

            if (piece == "GQ") return gqImage;
            if (piece == "GR") return grImage;
            if (piece == "GKN") return gknImage;
            if (piece == "GK") return gkImage;
            if (piece == "GW") return gwImage;

            return null;
        }

        public Image GetGlowImage(string piece)
        {
            if (piece == "SQ") return sqGlow;
            if (piece == "SR") return srGlow;
            if (piece == "SKN") return sknGlow;
            if (piece == "SK") return skGlow;
            if (piece == "SW") return swGlow;

            if (piece == "GQ") return gqGlow;
            if (piece == "GR") return grGlow;
            if (piece == "GKN") return gknGlow;
            if (piece == "GK") return gkGlow;
            if (piece == "GW") return gwGlow;

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
                if (setupSlot == "5") return swGlow;
            }
            else
            {
                if (setupSlot == "1") return gqGlow;
                if (setupSlot == "2") return grGlow;
                if (setupSlot == "3") return gknGlow;
                if (setupSlot == "4") return gkGlow;
                if (setupSlot == "5") return gwGlow;
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
                if (setupSlot == "5") return swImage;
            }
            else
            {
                if (setupSlot == "1") return gqImage;
                if (setupSlot == "2") return grImage;
                if (setupSlot == "3") return gknImage;
                if (setupSlot == "4") return gkImage;
                if (setupSlot == "5") return gwImage;
            }

            return null;
        }
    }
}