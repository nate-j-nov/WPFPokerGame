using System.Windows;

namespace WPFPokerGame.Extensions
{
    public static class WpfExtensions
    {
        public static void Bind(this FrameworkElement ele, object objectToBindTo)
        {
            ele.DataContext = objectToBindTo;
        }
    }
}