using EntidadDeNegociosEN;
using LogicaDeNegocioBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfazDeUsuarioUI
{
    /// <summary>
    /// Lógica de interacción para VentanaCliente.xaml
    /// </summary>
    public partial class VentanaCliente : Window
    {
        private bool _suspendTextChanged = false;
        private readonly ClienteBL _clienteBL = new ClienteBL();
        private readonly ClienteEN _clienteEN = new ClienteEN();
        private int _idMascotaSeleccionada = 0;
        private bool _modoModificacion = false;

        public VentanaCliente()
        {
            InitializeComponent();
            ReiniciarEstadoInicial();
            CargarGrid();
        }
        private void CargarGrid()
        {
            dgClientes.ItemsSource = _clienteBL.ListarCliente();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!long.TryParse(txtTelefono.Text, out long telefono))
            {
                MessageBox.Show("El número de teléfono ingresado no es válido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClienteEN cliente = new ClienteEN
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = telefono,
                FechaCreacion = DateTime.Now
            };

            int resultado = _clienteBL.GuardarCliente(cliente);

            if (resultado == -1)
            {
                MessageBox.Show("El número de teléfono ya existe, use otro por favor!!.", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (resultado == 1)
            {
                MessageBox.Show("Cliente guardado correctamente!!.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                txtNumCliente.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                ReiniciarEstadoInicial();
                CargarGrid();
            }
            else
            {
                MessageBox.Show("¡¡Error al guardar el cliente!!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            _clienteEN.Id = Convert.ToInt32(txtNumCliente.Text);
            _clienteEN.Nombre = txtNombre.Text;
            _clienteEN.Apellido = txtApellido.Text;
            _clienteEN.Telefono = int.Parse(txtTelefono.Text);
            _clienteBL.ModificarCliente(_clienteEN);

            txtNumCliente.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            CargarGrid();

            MessageBox.Show("Registro modificado correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumCliente.Text))
            {
                MessageBox.Show("Selecciona un cliente para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                _clienteEN.Id = Convert.ToInt32(txtNumCliente.Text);
                _clienteBL.EliminarCliente(_clienteEN);

                MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarGrid();
                ReiniciarEstadoInicial();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscarCliente.Text;
            List<ClienteEN> clientes = ClienteBL.BuscarCliente(nombre);
            dgClientes.ItemsSource = clientes;
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            dgClientes.ItemsSource = _clienteBL.ListarCliente();
            txtBuscarCliente.Clear();
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z\s]+$");
            if (e.Handled)
                MessageBox.Show("Solo se permiten letras!!.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void txtApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z\s]+$");
            if (e.Handled)
                MessageBox.Show("Solo se permiten letras!!.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void txtBuscar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z\s]+$");
            if (e.Handled)
                MessageBox.Show("Solo se permiten letras!!.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Campos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_modoModificacion)
            {
                btnGuardar.IsEnabled = false;
            }
            else
            {
                btnGuardar.IsEnabled = !string.IsNullOrWhiteSpace(txtNombre.Text);
            }
        }

        private void ReiniciarEstadoInicial()
        {
            _idMascotaSeleccionada = 0;

            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            _modoModificacion = false;

            txtNumCliente.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();

            dgClientes.UnselectAll();
        }

        private void dgvListarCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientes.SelectedItem is ClienteEN fila)
            {
                txtNumCliente.Text = fila.Id.ToString();
                txtNombre.Text = fila.Nombre;
                txtApellido.Text = fila.Apellido;
                txtTelefono.Text = fila.Telefono.ToString();

                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
                _modoModificacion = true;
            }
        }
    }
}
