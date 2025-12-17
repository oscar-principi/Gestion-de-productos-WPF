using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AMBL_WPF.Services;
using AMBL_WPF.Domain.Models;

namespace AMBL_WPF.Views
{
    public partial class ABMLPage : Page
    {
        private readonly ProductoService _productoService = new();
        private ObservableCollection<Producto> _productos = new();
        private Producto? _productoEnEdicion;

        public ABMLPage()
        {
            InitializeComponent();
            dgProductos.ItemsSource = _productos;
            MostrarPanelInicio();
        }

        private void OcultarTodo()
        {
            PanelInicio.Visibility = Visibility.Collapsed;
            PanelListar.Visibility = Visibility.Collapsed;
            PanelAgregar.Visibility = Visibility.Collapsed;
        }

        private void MostrarPanelInicio()
        {
            OcultarTodo();
            PanelInicio.Visibility = Visibility.Visible;
        }

        private void MostrarPanel_Click(object sender, RoutedEventArgs e)
        {
            OcultarTodo();
            PanelInicio.Visibility = Visibility.Visible;
        }

        private async void Listar_Click(object sender, RoutedEventArgs e) => await CargarListado();

        private async Task CargarListado()
        {
            OcultarTodo();
            PanelListar.Visibility = Visibility.Visible;
            _productos.Clear();
            var lista = await _productoService.Listar();
            foreach (var p in lista) _productos.Add(p);
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            _productoEnEdicion = null;
            TituloFormulario.Text = "Nuevo Producto";
            OcultarTodo();
            PanelAgregar.Visibility = Visibility.Visible;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is not Producto producto) return;
            _productoEnEdicion = producto;
            TituloFormulario.Text = "Editar Producto";
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString();
            txtStock.Text = producto.Stock.ToString();
            OcultarTodo();
            PanelAgregar.Visibility = Visibility.Visible;
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || !decimal.TryParse(txtPrecio.Text, out decimal p) || !int.TryParse(txtStock.Text, out int s))
            {
                MessageBox.Show("Datos inválidos."); return;
            }
            if (_productoEnEdicion == null)
                await _productoService.Agregar(new Producto { Nombre = txtNombre.Text, Precio = p, Stock = s });
            else
            {
                _productoEnEdicion.Nombre = txtNombre.Text; _productoEnEdicion.Precio = p; _productoEnEdicion.Stock = s;
                await _productoService.Actualizar(_productoEnEdicion);
            }
            await CargarListado();
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is not Producto p) return;
            if (MessageBox.Show($"¿Eliminar {p.Nombre}?", "Borrar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _productoService.Eliminar(p.Id);
                _productos.Remove(p);
            }
        }

        private async void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(texto)) { if (PanelListar.Visibility == Visibility.Visible) await CargarListado(); return; }
            if (texto.Length >= 2)
            {
                var lista = await _productoService.Buscar(texto);
                _productos.Clear();
                foreach (var item in lista) _productos.Add(item);
            }
        }

        private void LimpiarBusqueda_Click(object sender, RoutedEventArgs e) => txtBuscar.Clear();

        private void Salir_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new LoginPage());
    }
}