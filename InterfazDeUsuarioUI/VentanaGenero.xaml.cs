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
    /// Lógica de interacción para VentanaGenero.xaml
    /// </summary>
    public partial class VentanaGenero : Window
    {
        private readonly GeneroBL _generoBL = new GeneroBL();
        private readonly GeneroEN _generoEN = new GeneroEN();
        private bool _modoModificacion = false;

        public VentanaGenero()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();
        }
        private void CargarGrid()
        {
            dgvListarGenero.ItemsSource = _generoBL.MostrarGenero();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoGenero.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos requeridos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nuevoGenero = txtTipoGenero.Text.Trim().ToLower();

            // Verificar duplicados en DataGrid
            if (dgvListarGenero.Items.Cast<GeneroEN>().Any(x => x.TipoGen.Trim().ToLower() == nuevoGenero))
            {
                MessageBox.Show("Este género ya está registrado.", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _generoEN.TipoGenero = txtTipoGenero.Text;
            _generoBL.GuardarGenero(_generoEN);

            CargarGrid();
            txtId.Text = string.Empty;
            txtTipoGenero.Text = string.Empty;

            MessageBox.Show("Género guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            _generoEN.Id = Convert.ToInt32(txtId.Text);
            _generoEN.TipoGenero = txtTipoGenero.Text;
            _generoBL.ModificarGenero(_generoEN);

            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            _generoEN.Id = Convert.ToInt32(txtId.Text);
            _generoBL.EliminarGenero(_generoEN);

            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void txtTipoGenero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo letras y espacios
            if (!e.Text.All(char.IsLetter) && !e.Text.All(char.IsWhiteSpace))
            {
                MessageBox.Show("Solo se permiten letras.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void ReiniciarEstadoInicial()
        {
            txtId.Text = string.Empty;
            txtTipoGenero.Text = string.Empty;

            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            _modoModificacion = false;

            dgvListarGenero.SelectedIndex = -1;
        }

        private void txtTipoGenero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_modoModificacion)
                btnGuardar.IsEnabled = false;
            else
                btnGuardar.IsEnabled = !string.IsNullOrWhiteSpace(txtTipoGenero.Text);
        }

        private void dgvListarGenero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvListarGenero.SelectedItem is GeneroEN fila)
            {
                txtId.Text = fila.Id.ToString();
                txtTipoGenero.Text = fila.TipoGenero;

                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
                _modoModificacion = true;
            }
        }
    }
}

