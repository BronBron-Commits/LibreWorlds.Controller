using System.Windows;
using LibreWorlds.Controller.ViewModels;

namespace LibreWorlds.Controller
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartSession();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StopSession();
        }
    }
}
