using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;



namespace WPFPokerGame.Models.Cards
{
    //public class CardModel { }
    public sealed class Card : ModelBase
    {
        // Initializes and retrieves card's face and suit

        public bool IsFrontShowing { get; set; }
        public CardFace Face { get; set; }

        public CardSuit Suit { get; set; }

        public string FullCardName
        {
            get
            {
                return Face + " of " + Suit;
            }
        }

        //Creates a card 
        public Card(CardFace face, CardSuit suit)
        {
            Face = face;
            Suit = suit;
            IsFrontShowing = false;
            
            if(cardLibrary == IntPtr.Zero) 
                throw new FileNotFoundException("Couldn't find Cards.dll");
            
        }

        //Prints the card
        public override string ToString()
        {
            return Face + " of " + Suit;
        }

        //Implementation of loading images 

        // Put cards into 2D array: suit, rank (0-12 => 2-A);
        private BitmapSource[,] _bitmapCards;

        public BitmapSource[] _bitMapCardBacks;
        static IntPtr cardLibrary = LoadLibraryEx("C:\\Users\\natej\\Documents\\C#\\DLLs\\PokerGameExperimentation\\Cards.Dll", IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);

        Func<int, BitmapSource> GetBitmapSource = (resource) =>
        {
            // We first load the bitmap as a native resource, and get a ptr to it. 
            var bitmapResource = LoadBitMap(cardLibrary, resource);

            // Now we create a System.Drawing.BitMap from the native bitmap. 
            var bmp = System.Drawing.Bitmap.FromHbitmap(bitmapResource);

            // We can now delete the LoadBitmap.
            DeleteObject(bitmapResource);

            // Now we get a handle to a GDI System.DrawingBitmap
            var hBitmap = bmp.GetHbitmap();

            // We can create a WPF Bitmap source now. 
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                palette: IntPtr.Zero,
                sourceRect: Int32Rect.Empty,
                sizeOptions: BitmapSizeOptions.FromEmptyOptions());

            // We are done with the GDI bitmap
            DeleteObject(hBitmap);
            return bitmapSource;
        };







        public const int LOAD_LIBRARY_AS_DATAFILE = 2;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFileReserved, uint dwFlags);

        [DllImport("User32.dll")]
        public static extern IntPtr LoadBitMap(IntPtr hInstance, int uID);

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);


    }
}
