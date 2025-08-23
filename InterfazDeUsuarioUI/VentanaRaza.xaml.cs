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
    /// Lógica de interacción para VentanaRaza.xaml
    /// </summary>
    public partial class VentanaRaza : Window
    {
        RazaBL _razaBL = new RazaBL();
        RazaEN _razaEN = new RazaEN();
  

        public VentanaRaza()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void CargarGrid()
        {

            {
                dgvListarRaza1.ItemsSource = _razaBL.MostrarRaza();
            }

        }







        private void ReiniciarEstadoInicial()
        {
            txtId.Clear();
            txtTipoRaza.Clear();
            dgvListarRaza1.UnselectAll();
        }

       

       

        private void txtTipoRaza_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo letras y espacios
            Regex regex = new Regex("[^a-zA-Z ]+");
            if (regex.IsMatch(e.Text))
            {
                MessageBox.Show("Solo se permiten letras.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoRaza.Text))
            {
                MessageBox.Show("Por favor, ingrese el tipo de raza.", "Campo requerido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _razaEN.TipoRaza = txtTipoRaza.Text.Trim();
            _razaBL.GuardarRaza(_razaEN);

            MessageBox.Show("Raza guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            _razaEN.Id = Convert.ToByte(txtId.Text);
            _razaEN.TipoRaza = txtTipoRaza.Text.Trim();

            _razaBL.ModificarRaza(_razaEN);

            MessageBox.Show("Raza modificada correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);

            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("¿Realmente desea eliminar esta raza?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _razaEN.Id = Convert.ToByte(txtId.Text);
                _razaBL.EliminarRaza(_razaEN);

                MessageBox.Show("Raza eliminada correctamente.", "Eliminación", MessageBoxButton.OK, MessageBoxImage.Information);

                CargarGrid();
                ReiniciarEstadoInicial();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string Nombre = txtBuscar.Text;
            List<RazaEN> raza = RazaBL.BuscarRaza(Nombre);
            dgvListarRaza1.ItemsSource = raza;

        }

        private void dgvListarRaza1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
     
            if (dgvListarRaza1.SelectedItem is RazaEN fila)
            {
                txtId.Text = fila.Id.ToString();
                txtTipoRaza.Text = fila.TipoRaza;

             
            }
        }
    }
    
}
