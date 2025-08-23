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
        public ObservableCollection<RazaEN> ListaRaza { get; set; }
        private RazaDAL _razaDAL = new RazaDAL();
        private MascotaBL _mascotaBL = new MascotaBL();
        private MascotaEN _mascotaEN = new MascotaEN();
        private bool _modoModificacion = false;
        private int _idMascotaSeleccionada = 0;

        public VentanaMascota()
        {
            InitializeComponent();
            CargarGrid();
            CargarRazas();
            ReiniciarEstadoInicial();
        }

        private void CargarRazas()
        {
            //ListaRaza = new ObservableCollection<RazaEN>(RazaDAL.MostrarRazaEN());

            // Si tu XAML tiene ItemsSource="{Binding ListaRaza}", deja el DataContext:
            this.DataContext = this;

            // —O— si prefieres asignar por código en lugar de binding en XAML, usa esto:
            // cbxRaza.ItemsSource = ListaRaza;
            // cbxRaza.DisplayMemberPath = "TipoRaza";  // ajusta al nombre real en RazaEN
            // cbxRaza.SelectedValuePath = "Id";        // ajusta a la clave real en RazaEN
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
            txtRaza.Clear();
            dtpFechaNacimiento.SelectedDate = DateTime.Today;
            txtGenero.Clear();
            txtEspecie.Clear();

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
               string.IsNullOrWhiteSpace(txtColor.Text) ||
               string.IsNullOrWhiteSpace(txtGenero.Text) ||   
               string.IsNullOrWhiteSpace(txtRaza.Text) ||     
               string.IsNullOrWhiteSpace(txtEspecie.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MascotaEN nuevaMascota = new MascotaEN
            {
                Nombre = txtNombre.Text,
                Color = txtColor.Text,
                FechaNacimiento = dtpFechaNacimiento.SelectedDate ?? DateTime.Today,
                IdGenero = Convert.ToByte(txtGenero.Text),
                IdRaza = Convert.ToByte(txtRaza.Text),
                IdEspecie = Convert.ToByte(txtEspecie.Text)
            };

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
            _mascotaEN.IdGenero = Convert.ToByte(txtGenero.Text);
            _mascotaEN.IdRaza = Convert.ToByte(txtRaza.Text);
            _mascotaEN.IdEspecie = Convert.ToByte(txtEspecie.Text);

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

                txtGenero.Text = mascotaSeleccionada.IdGenero.ToString();
                txtRaza.Text = mascotaSeleccionada.IdRaza.ToString();
                txtEspecie.Text = mascotaSeleccionada.IdEspecie.ToString();
            }
        }
    }
    
}
