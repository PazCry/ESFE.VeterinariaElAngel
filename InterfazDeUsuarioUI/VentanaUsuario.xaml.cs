using EntidadDeNegociosEN;
using LogicaDeNegocioBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para VentanaUsuario.xaml
    /// </summary>
    public partial class VentanaUsuario : Window
    {
        UsuarioBL _usuarioBL = new UsuarioBL();
        UsuarioEN _usuarioEN = new UsuarioEN();

        public VentanaUsuario()
        {
            InitializeComponent();
            CargarGrid();
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
        }

        private void CargarGrid()
        {
            dgvListarUsuario.ItemsSource = _usuarioBL.MostrarUsuario();
        }







        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                CargarGrid();
            }
            else if (nombre.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                dgvListarUsuario.ItemsSource = _usuarioBL.MostrarUsuarioDE();
            }
            else
            {
                dgvListarUsuario.ItemsSource = UsuarioBL.BuscarUsuario(nombre);
            }
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscar.Clear();
            CargarGrid();
        }

       

        private void txtTelefono_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Solo se permiten números en el teléfono.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
            else if (txtTelefono.Text.Length >= 8)
            {
                MessageBox.Show("El teléfono debe tener solo 8 dígitos.", "Límite alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void txtNombre_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) && !char.IsWhiteSpace(e.Text, 0))
            {
                MessageBox.Show("Solo se permiten letras.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void txtCorreo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo debe contener una '@'.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCorreo.Focus();
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtContra.Clear();
            txtTelefono.Clear();
            cbxIdRol.SelectedIndex = -1;

            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
               string.IsNullOrWhiteSpace(txtApellido.Text) ||
               string.IsNullOrWhiteSpace(txtCorreo.Text) ||
               string.IsNullOrWhiteSpace(txtContra.Password) ||
               string.IsNullOrWhiteSpace(txtTelefono.Text) ||
               cbxIdRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_usuarioBL.CorreoExiste(txtCorreo.Text))
            {
                MessageBox.Show("El correo ya está registrado. Por favor, use otro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _usuarioEN.Nombre = txtNombre.Text;
            _usuarioEN.Apellido = txtApellido.Text;
            _usuarioEN.Correo = txtCorreo.Text;
            _usuarioEN.Contra = txtContra.Password;
            _usuarioEN.Telefono = txtTelefono.Text;
            _usuarioEN.IdRol = Convert.ToByte(cbxIdRol.SelectedIndex + 1);

            _usuarioBL.GuardarUsuario(_usuarioEN);
            CargarGrid();
            LimpiarCampos();
        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
              string.IsNullOrWhiteSpace(txtNombre.Text) ||
              string.IsNullOrWhiteSpace(txtApellido.Text) ||
              string.IsNullOrWhiteSpace(txtCorreo.Text) ||
              string.IsNullOrWhiteSpace(txtContra.Password) ||
              string.IsNullOrWhiteSpace(txtTelefono.Text) ||
              cbxIdRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de modificar.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_usuarioBL.CorreoExiste(txtCorreo.Text, _usuarioEN.Id))
            {
                MessageBox.Show("El correo ya está registrado por otro usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea modificar este usuario?", "Confirmar modificación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _usuarioEN.Id = Convert.ToByte(txtId.Text);
                _usuarioEN.Nombre = txtNombre.Text;
                _usuarioEN.Apellido = txtApellido.Text;
                _usuarioEN.Correo = txtCorreo.Text;
                _usuarioEN.Contra = txtContra.Password;
                _usuarioEN.Telefono = txtTelefono.Text;
                _usuarioEN.IdRol = Convert.ToByte(cbxIdRol.SelectedIndex + 1);

                _usuarioBL.ModificarUsuario(_usuarioEN);
                CargarGrid();
                LimpiarCampos();
                MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Debe seleccionar un usuario antes de eliminar.", "Campo requerido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtCorreo.Text == UsuarioActualEN.Correo.ToString())
            {
                MessageBox.Show("No se puede eliminar el usuario que está actualmente logueado.", "Acción no permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmar eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _usuarioEN.Id = Convert.ToByte(txtId.Text);
                _usuarioBL.EliminarUsuario(_usuarioEN);
                CargarGrid();
                LimpiarCampos();
                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBuscar_Click_1(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                CargarGrid();
            }
            else if (nombre.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                dgvListarUsuario.ItemsSource = _usuarioBL.MostrarUsuarioDE();
            }
            else
            {
                dgvListarUsuario.ItemsSource = UsuarioBL.BuscarUsuario(nombre);
            }

        }

        private void btnReiniciar_Click_1(object sender, RoutedEventArgs e)
        {

            CargarGrid();
        }

        private void dgvListarUsuario_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
            if (dgvListarUsuario.SelectedItem is UsuarioEN usuario)
            {
                txtId.Text = usuario.Id.ToString();
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtCorreo.Text = usuario.Correo;
                txtContra.Password = usuario.Contra;
                txtTelefono.Text = usuario.Telefono;
                cbxIdRol.SelectedIndex = usuario.IdRol - 1;

                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;
            }
        }
    }
    

}
