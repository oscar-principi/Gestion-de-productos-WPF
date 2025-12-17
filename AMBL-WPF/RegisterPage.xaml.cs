using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AMBL_WPF.Services;
using AMBL_WPF.Domain.Models;

namespace AMBL_WPF.Views
{
    public partial class RegisterPage : Page
    {
        private readonly UsuarioService _usuarioService = new();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new LoginPage());
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obtener y limpiar datos
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;
            string confirmPass = txtConfirmPassword.Password;

            // 2. Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Campos Faltantes", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 3. Validar formato de Email (Regex estándar)
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.", "Email Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 4. Validar coincidencia de contraseñas
            if (password != confirmPass)
            {
                MessageBox.Show("Las contraseñas no coinciden. Inténtalo de nuevo.", "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 5. Validar longitud de contraseña (Seguridad básica)
            if (password.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Seguridad", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // 6. Proceso de registro con manejo de errores
            try
            {
                var btn = (Button)sender;
                btn.IsEnabled = false;

                // SIMPLEMENTE LLAMAR AL MÉTODO SIN ASIGNARLO A UNA VARIABLE
                // Si hay un error en la base de datos, saltará al catch automáticamente
                await _usuarioService.RegistrarUsuario(new Usuario
                {
                    Name = username,
                    Password = password,
                    Email = email
                });

                MessageBox.Show("¡Usuario registrado con éxito!", "Registro Completo", MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                // Aquí caerá si el procedimiento almacenado falla (ej. usuario duplicado)
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error de Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                var btn = (Button)sender;
                btn.IsEnabled = true;
            }
        }
    }
}