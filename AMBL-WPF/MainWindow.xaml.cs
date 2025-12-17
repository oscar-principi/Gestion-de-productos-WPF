using System.Windows;
using AMBL_WPF.Views;

namespace AMBL_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        private void MostrarLogin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        private void MostrarRegistro_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegisterPage());
        }
    }
}
