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
    /// Lógica de interacción para VentanaRol.xaml
    /// </summary>
    public partial class VentanaRol : Window
    {
        RolBL _rolBL = new RolBL();
        RolEN _rolEN = new RolEN();


        public VentanaRol()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void CargarGrid()
        {
            dgvListarRol.ItemsSource = _rolBL.MostrarRol();
        }





        private void txtNombreRol_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) && !char.IsWhiteSpace(e.Text, 0))
            {
                MessageBox.Show("Solo se permiten letras.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void dgvListarRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvListarRol.SelectedItem is RolEN rol)
            {
                txtNumeroRol.Text = rol.Id.ToString();
                txtNombreRol.Text = rol.TipoRol;


            }
        }

        private void ReiniciarEstadoInicial()
        {
            txtNombreRol.Clear();
            txtNumeroRol.Clear();



            dgvListarRol.SelectedIndex = -1;
        }



        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroRol.Text))
            {
                MessageBox.Show("Debe seleccionar un rol para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Realmente desea eliminar este rol?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _rolEN.Id = Convert.ToByte(txtNumeroRol.Text);
                _rolBL.EliminarRol(_rolEN);

                CargarGrid();
                ReiniciarEstadoInicial();
            }
        }



        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombreRol.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del rol.", "Campo requerido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _rolEN.TipoRol = txtNombreRol.Text.Trim();
            _rolBL.GuardarRol(_rolEN);

            MessageBox.Show("Rol guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNumeroRol.Text))
            {
                MessageBox.Show("Debe seleccionar un rol para modificar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _rolEN.Id = Convert.ToByte(txtNumeroRol.Text);
            _rolEN.TipoRol = txtNombreRol.Text.Trim();

            _rolBL.ModificarRol(_rolEN);
            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNumeroRol.Text))
            {
                MessageBox.Show("Debe seleccionar un rol para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Realmente desea eliminar este rol?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _rolEN.Id = Convert.ToByte(txtNumeroRol.Text);
                _rolBL.EliminarRol(_rolEN);

                CargarGrid();
                ReiniciarEstadoInicial();
            }
        }

        private void btnBuscar_Click_1(object sender, RoutedEventArgs e)
        {

            string Id = txtBuscar.Text;
            List<CitaEN> cita = CitaBL.BuscarCita(Id);
            dgvListarRol.ItemsSource = cita;
        }

        private void dgvListarRol_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {


    

            if (dgvListarRol.SelectedItem is RolEN rol)
            {
                txtNumeroRol.Text = rol.Id.ToString();
                txtNombreRol.Text = rol.TipoRol;


            }
        }

    }
}



