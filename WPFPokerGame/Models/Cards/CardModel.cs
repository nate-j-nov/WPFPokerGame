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
    public sealed class Card : INotifyPropertyChanged
    {
        // Initializes and retrieves card's face and suit

        private bool _isFrontShowing;

        public bool IsFrontShowing 

        public bool IsFrontShowing
        {
            get
            {
                return _isFrontShowing;
            }
            set
            {
                if (_isFrontShowing == value) return;
                _isFrontShowing = value;
                RaisePropertyChanged("IsFrontShowing");
                RaisePropertyChanged("CardDisplay");
            }
        }

        private Image _frontCardImage = new Image();
        public Image FrontCardImage
        {
            get
            {
                return _frontCardImage;
            }
            set
            {
                if (_frontCardImage == value) return;
                _frontCardImage = value;
                RaisePropertyChanged("FrontCardImage");
                RaisePropertyChanged("CardDisplay");
            }
        }

        private Image _backCardImage = new Image();
        public Image BackCardImage
        {
            get
            {
                return _backCardImage;
            }
            set
            {
                if (_backCardImage == value) return;
                _backCardImage = value;
                RaisePropertyChanged("BackCardImage");
                RaisePropertyChanged("CardDisplay");
            }
        }

        
        public Image CardDisplay
        {
            get
            {
                if (IsFrontShowing)
                    return _frontCardImage;
                else
                    return _backCardImage;
            }
        }

        public Image CardDisplay
        {
            get
            {
                if (IsFrontShowing)
                    return _frontCardImage;
                else
                    return _backCardImage;
            }
        }

        private CardFace _face;
        public CardFace Face
        {
            get
            {
                return _face;
            }
            set
            {
                if (_face == value) return;
                _face = value;
                RaisePropertyChanged("Face");
                RaisePropertyChanged("FullCardName");
            }
        }

        private CardSuit _suit;
        public CardSuit Suit
        {
            get
            {
                return _suit;
            }
            set
            {
                if (_suit == value) return;
                _suit = value;
                RaisePropertyChanged("Suit");
                RaisePropertyChanged("FullCardName");
            }
        }

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
            _face = face;
            _suit = suit;
            _isFrontShowing = true;
            _frontCardImage.Source = GetFrontCardImage(this.Face, this.Suit);
            _backCardImage.Source = GetBackCardImage();
        }

        public void SetFrontShowingToFalse()
        {

            IsFrontShowing = false; 

            IsFrontShowing = false;
        }

        //Prints the card
        public override string ToString()
        {
            return Face + " of " + Suit;
        }

        //Implementation of loading images 

        // Put cards into 2D array: suit, rank (0-12 => 2-A);


        Func<int, BitmapSource> GetBitmapSource = (resource) =>
        {
            // Load the Bitmap library.
            IntPtr cardLibrary = LoadLibraryEx("C:\\Users\\natej\\Documents\\C#\\WPFPokerGame\\WPFPokerGame\\WPFPokerGame\\Cards.Dll", IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
            if (cardLibrary == IntPtr.Zero)
                throw new FileNotFoundException("Couldn't find Cards.dll");

            // We first load the bitmap as a native resource, and get a ptr to it. 
            var bitmapResource = LoadBitmap(cardLibrary, resource);

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

        /// <summary>
        /// This gets the Image of the front of teh card by calling GetBitmapSource. 
        /// libraryIndex is a method that does some simple math that maps the suit and face of the card
        /// to the name in the DLL. If I'm not mistaken, this could be put into the "get" function of the CardFrontImage property. 
        /// </summary>
        /// <param name="face"></param>
        /// <param name="suit"></param>
        /// <returns> a BitmapSource</returns>
        public BitmapSource GetFrontCardImage(CardFace face, CardSuit suit)
        {
            int nSuit = (int)suit;
            int nFace = (int)face;
            int libraryIndex = 1 + nFace + (nSuit == 0 ? 0 : nSuit * 13);
            return GetBitmapSource(libraryIndex);
        }

        public BitmapSource GetBackCardImage()
        {
            return GetBitmapSource(60);
        }

        public const int LOAD_LIBRARY_AS_DATAFILE = 2;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFileReserved, uint dwFlags);

        [DllImport("User32.dll")]
        public static extern IntPtr LoadBitmap(IntPtr hInstance, int uID);

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}