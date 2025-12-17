using System.Windows;
using System.Windows.Controls;
using AMBL_WPF.Services;

namespace AMBL_WPF.Views
{
    public partial class LoginPage : Page
    {
        private readonly UsuarioService _usuarioService = new();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var usuario = await _usuarioService.Login(txtUser.Text, txtPass.Password);

            if (usuario != null)
                NavigationService.Navigate(new ABMLPage());
            else
                MessageBox.Show("Usuario o contraseña incorrectos");
        }

        private void IrRegistro_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
