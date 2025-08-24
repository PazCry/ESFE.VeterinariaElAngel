using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using LogicaDeNegocioBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InterfazDeUsuarioUI
{
    /// <summary>
    /// Lógica de interacción para VentanaMascota.xaml
    /// </summary>
    public partial class VentanaMascota : Window
    {
        EspecieBL _especieBL = new EspecieBL();
        RazaBL _razaBL = new RazaBL();
        GeneroBL _generoBL = new GeneroBL();

        private RazaDAL _razaDAL = new RazaDAL();
        private MascotaBL _mascotaBL = new MascotaBL();
        private MascotaEN _mascotaEN = new MascotaEN();
        private bool _modoModificacion = false;
        private int _idMascotaSeleccionada = 0;

        public VentanaMascota()
        {
            InitializeComponent();
            CargarGrid();
            CargarCombos();
            ReiniciarEstadoInicial();
        }

        private void CargarCombos()
        {
            // 🔹 Especie
            cbxEspecie.ItemsSource = _especieBL.MostrarEspecie();
            cbxEspecie.DisplayMemberPath = "TipoEspecie";     // ajusta si tu propiedad cambia
            cbxEspecie.SelectedValuePath = "IdEspecie";

            cmbRaza.ItemsSource = _razaBL.MostrarRaza();
            cmbRaza.DisplayMemberPath = "TipoRaza";     // ajusta si tu propiedad cambia
            cmbRaza.SelectedValuePath = "IdRaza";



            // 🔹 Género (fijo)
            cbxGenero.ItemsSource = _generoBL.MostrarGenero();
            cbxGenero.DisplayMemberPath = "TipoGenero";  // ajusta si tu propiedad cambia
            cbxGenero.SelectedValuePath = "IdGenero";
        }


        private void CargarGrid()
        {
            dgvListarMascota.ItemsSource = _mascotaBL.MostrarMascota();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text;
            List<MascotaEN> mascotas = MascotaBL.BuscarMascota(nombre);
            dgvListarMascota.ItemsSource = mascotas;
        }

        private void ReiniciarEstadoInicial()
        {
            _idMascotaSeleccionada = 0;

            txtNombre.Clear();
            txtColor.Clear();
            cmbRaza.SelectedIndex = -1;
            dtpFechaNacimiento.SelectedDate = DateTime.Today;
            cbxGenero.SelectedIndex = -1;
            cbxEspecie.SelectedIndex = -1;

            _modoModificacion = false;
            dgvListarMascota.SelectedIndex = -1;
        }

        // Validación: Solo letras
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
               cmbRaza.SelectedIndex == -1 ||
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
            nuevaMascota.IdGenero = Convert.ToByte(cbxGenero.SelectedValue);
            nuevaMascota.IdRaza = Convert.ToByte(cmbRaza.SelectedValue);
            nuevaMascota.IdEspecie = Convert.ToByte(cbxEspecie.SelectedValue);

            _mascotaBL.GuardarMascota(nuevaMascota);
            MessageBox.Show("Mascota guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            CargarGrid();
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (_idMascotaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una mascota para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Realmente desea eliminar esta mascota?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _mascotaEN.Id = (byte)_idMascotaSeleccionada;
                _mascotaBL.EliminarMascota(_mascotaEN);
                ReiniciarEstadoInicial();
                CargarGrid();
            }
        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {
            if (_idMascotaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una mascota para modificar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _mascotaEN.Id = (byte)_idMascotaSeleccionada;
            _mascotaEN.Nombre = txtNombre.Text;
            _mascotaEN.Color = txtColor.Text;
            _mascotaEN.FechaNacimiento = dtpFechaNacimiento.SelectedDate ?? DateTime.Today;
            _mascotaEN.IdGenero = Convert.ToByte(cbxGenero.SelectedValue);
            _mascotaEN.IdRaza = Convert.ToByte(cmbRaza.SelectedValue);
            _mascotaEN.IdEspecie = Convert.ToByte(cbxEspecie.SelectedValue);

            _mascotaBL.ModificarMascota(_mascotaEN);

            CargarGrid();
            MessageBox.Show("Registro modificado correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);
            ReiniciarEstadoInicial();
        }

        private void dgvListarMascota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgvListarMascota.SelectedItem is MascotaEN mascotaSeleccionada)
            {
                _idMascotaSeleccionada = mascotaSeleccionada.Id;

                txtNombre.Text = mascotaSeleccionada.Nombre;
                txtColor.Text = mascotaSeleccionada.Color;
                dtpFechaNacimiento.SelectedDate = mascotaSeleccionada.FechaNacimiento;

                cbxGenero.SelectedValue = mascotaSeleccionada.IdGenero;
                cmbRaza.SelectedValue = mascotaSeleccionada.IdRaza;
                cbxEspecie.SelectedValue = mascotaSeleccionada.IdEspecie;

            }
        }
    }

}
