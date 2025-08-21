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
    /// Lógica de interacción para VentanaMascota.xaml
    /// </summary>
    public partial class VentanaMascota : Window
    {
        MascotaBL _mascotaBL = new MascotaBL();
        MascotaEN _mascotaEN = new MascotaEN();
        private bool _modoModificacion = false;
        private int _idMascotaSeleccionada = 0;
        public VentanaMascota()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();
        }
        private void CargarGrid()
        {
            MascotasDataGrid.ItemsSource = _mascotaBL.MostrarMascota();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string nombre = txtBuscar.Text;
            List<MascotaEN> mascotas = MascotaBL.BuscarMascota(nombre);
            MascotasDataGrid.ItemsSource = mascotas;
        }






        private void ReiniciarEstadoInicial()
        {
            _idMascotaSeleccionada = 0;

            txtNombre.Clear();
            txtColor.Clear();
            cbxRaza.SelectedIndex = -1;
            dtpFechaNacimiento.SelectedDate = DateTime.Today;
            cbxGenero.SelectedIndex = -1;
            cbxEspecie.SelectedIndex = -1;

            _modoModificacion = false;

            MascotasDataGrid.SelectedIndex = -1;
        }



        // Solo letras
        private void SoloLetras(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
               string.IsNullOrWhiteSpace(txtColor.Text) ||
               cbxGenero.SelectedIndex == -1 ||
               cbxRaza.SelectedIndex == -1 ||
               cbxEspecie.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear una nueva instancia para guardar
            MascotaEN nuevaMascota = new MascotaEN();
            nuevaMascota.Nombre = txtNombre.Text;
            nuevaMascota.Color = txtColor.Text;
            nuevaMascota.FechaNacimiento = dtpFechaNacimiento.SelectedDate ?? DateTime.Today;
            nuevaMascota.IdGenero = Convert.ToByte(cbxGenero.SelectedIndex + 1);
            nuevaMascota.IdRaza = 1; // si tienes catálogo real de razas, cámbialo
            nuevaMascota.IdEspecie = Convert.ToByte(cbxEspecie.SelectedIndex + 1);

            _mascotaBL.GuardarMascota(nuevaMascota);
            MessageBox.Show("Mascota guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            CargarGrid();
            ReiniciarEstadoInicial();
        }


        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("¿Realmente desea eliminar esta mascota?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _mascotaEN.Id = (byte)_idMascotaSeleccionada;
                _mascotaBL.EliminarMascota(_mascotaEN);
                ReiniciarEstadoInicial();
                CargarGrid();
            }
        }

        private void MascotasDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (MascotasDataGrid.SelectedItem is MascotaEN fila)
            {
                _idMascotaSeleccionada = fila.Id;
                txtNombre.Text = fila.Nombre;
                txtColor.Text = fila.Color;
                cbxRaza.Text = fila.IdRaza.ToString();
                dtpFechaNacimiento.SelectedDate = fila.FechaNacimiento;
                cbxGenero.SelectedIndex = fila.IdGenero - 1;
                cbxEspecie.SelectedIndex = fila.IdEspecie - 1;

                _modoModificacion = true;
            }
        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            _mascotaEN.Id = (byte)_idMascotaSeleccionada;
            _mascotaEN.Nombre = txtNombre.Text;
            _mascotaEN.Color = txtColor.Text;
            _mascotaEN.FechaNacimiento = dtpFechaNacimiento.SelectedDate ?? DateTime.Today;
            _mascotaEN.IdGenero = Convert.ToByte(cbxGenero.SelectedIndex + 1);
            _mascotaEN.IdRaza = 1;
            _mascotaEN.IdEspecie = Convert.ToByte(cbxEspecie.SelectedIndex + 1);

            _mascotaBL.ModificarMascota(_mascotaEN);

            CargarGrid();
            MessageBox.Show("Registro modificado correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);
            ReiniciarEstadoInicial();
        }
    }

}




