using System.Windows;
using WPFPokerGame.Extensions;
using WPFPokerGame.Services;
using WPFPokerGame.ViewModels;

namespace WPFPokerGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IGameService _gameService;

        private MainWindow _mainWindow;
        private ViewModel _vm;

        void ApplicationStartup(object sender, StartupEventArgs e)
        {
            _gameService = new GameService();

            _vm = new ViewModel(_gameService);

            _mainWindow = new MainWindow();
            _mainWindow.Loaded += (s,a) => _mainWindow.Bind(_vm);
            _mainWindow.Show();
        }
    }
}