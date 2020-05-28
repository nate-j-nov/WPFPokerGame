using System.Windows.Controls;

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
        }
        public void Bind(ViewModels.ViewModel cardsViewModel)
        {
            DataContext = cardsViewModel;
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
