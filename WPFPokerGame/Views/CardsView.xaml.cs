using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Diagnostics;
using System;
using System.Windows.Input;
using WPFPokerGame.Commands;


namespace WPFPokerGame.Views
{
    /// <summary>
    /// Interaction logic for CardsView.xaml
    /// </summary>
    public partial class CardsView : UserControl
    {
        public CardsView()
        {
            InitializeComponent();
            //this.DataContext = new WPFPokerGame.ViewModels.CardsViewModel();
        }
    }

    /*public class DebugDummyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }*/
}
