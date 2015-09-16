using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlphaAndNumbers
{
    sealed class FlashCardDictionary : Dictionary<string, Bitmap>
    {
        public FlashCardDictionary()
        {
            Add("A", Properties.Resources.A);
            Add("B", Properties.Resources.B);
            Add("C", Properties.Resources.C);
            Add("D", Properties.Resources.D);
            Add("E", Properties.Resources.E);
            Add("F", Properties.Resources.F);
            Add("G", Properties.Resources.G);
            Add("H", Properties.Resources.H);
            Add("I", Properties.Resources.I);
            Add("J", Properties.Resources.J);
            Add("K", Properties.Resources.K);
            Add("L", Properties.Resources.L);
            Add("M", Properties.Resources.M);
            Add("N", Properties.Resources.N);
            Add("O", Properties.Resources.O);
            Add("P", Properties.Resources.P);
            Add("Q", Properties.Resources.Q);
            Add("R", Properties.Resources.R);
            Add("S", Properties.Resources.S);
            Add("T", Properties.Resources.T);
            Add("U", Properties.Resources.U);
            Add("V", Properties.Resources.V);
            Add("W", Properties.Resources.W);
            Add("X", Properties.Resources.X);
            Add("Y", Properties.Resources.Y);
            Add("Z", Properties.Resources.Z);
        }
    }

    /// <summary>
    /// Flash Cards Class
    /// Used to display Alphabet images
    /// </summary>
    class FlashCards
    {
        private FlashCardDictionary fcd = new FlashCardDictionary();

        /// <summary>
        /// Gets the ImageSource from Resouce, provided by the resouce name/letter
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public ImageSource GetImageSource(string value)
        {
            Bitmap bmp = fcd[value];

            MemoryStream ms = new MemoryStream();
            ((Bitmap)bmp).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            img.StreamSource = ms;
            img.EndInit();

            return img;
        }
    }
}
