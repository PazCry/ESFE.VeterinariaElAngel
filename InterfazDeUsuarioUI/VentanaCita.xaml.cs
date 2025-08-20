using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
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
    /// Lógica de interacción para VentanaCita.xaml
    /// </summary>
    public partial class VentanaCita : Window
    {
        public CitaEN citaParaImprimir;
        private readonly CitaBL _citaBL = new CitaBL();
        private readonly CitaEN _citaEN = new CitaEN();
        private bool _modoModificacion = false;
        public VentanaCita()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();
        }

        public void CargarGrid()
        {
            dgvCita.ItemsSource = _citaBL.MostrarCita();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumCita.Text) || dpFechaCita.SelectedDate == null || string.IsNullOrWhiteSpace(txtHora.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fecha = dpFechaCita.SelectedDate.Value;
            if (!TimeSpan.TryParse(txtHora.Text, out TimeSpan horaSeleccionada))
            {
                MessageBox.Show("Ingrese una hora válida (HH:mm).", "Hora inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaHoraCita = fecha.Date + horaSeleccionada;

            if (fechaHoraCita < DateTime.Now)
            {
                MessageBox.Show("La cita no puede ser en el pasado.", "Fecha/Hora inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TimeSpan horaMin = new TimeSpan(8, 0, 0);
            TimeSpan horaMax = new TimeSpan(17, 30, 0);
            if (horaSeleccionada < horaMin || horaSeleccionada > horaMax)
            {
                MessageBox.Show("La hora debe estar entre 08:00 y 17:30.", "Hora fuera de rango", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!byte.TryParse(txtNumCita.Text, out byte idRegistro))
            {
                MessageBox.Show("El ID del expediente debe ser un número válido.", "Dato inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool citaExistente = _citaBL.ExisteCitaEnFechaYHora(fecha, horaSeleccionada);
            if (citaExistente)
            {
                MessageBox.Show("Ya existe una cita en ese horario.", "Cita duplicada", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _citaEN.IdExpediente = idRegistro;
            _citaEN.Hora = horaSeleccionada;
            _citaEN.FechaCita = fecha;

            _citaBL.GuardarCita(_citaEN);

            CargarGrid();
            txtNumCita.Clear();
            txtNumExpediente.Clear();
            txtHora.Clear();

            MessageBox.Show("Cita guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            ReiniciarEstadoInicial();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            _citaEN.Id = Convert.ToByte(txtNumCita.Text);
            _citaEN.IdExpediente = Convert.ToByte(txtNumExpediente.Text);
            _citaEN.FechaCita = dpFechaCita.SelectedDate.Value;
            _citaEN.Hora = TimeSpan.Parse(txtHora.Text);

            _citaBL.ModificarCita(_citaEN);
            CargarGrid();

            txtNumCita.Clear();
            txtNumExpediente.Clear();
            txtHora.Clear();

            MessageBox.Show("Registro modificado correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);
            ReiniciarEstadoInicial();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("¿Realmente desea eliminar este registro?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _citaEN.Id = Convert.ToByte(txtNumCita.Text);
                _citaBL.EliminarCita(_citaEN);
                CargarGrid();
                ReiniciarEstadoInicial();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string Id = txtBuscar.Text;
            List<CitaEN> cita = CitaBL.BuscarCita(Id);
            dgvCita.ItemsSource = cita;
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrid();
        }

        private void dgvCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCita.SelectedItem is CitaEN fila)
            {
                txtNumCita.Text = fila.Id.ToString();
                txtNumExpediente.Text = fila.IdExpediente.ToString();
                dpFechaCita.SelectedDate = fila.FechaCita;
                txtHora.Text = fila.Hora?.ToString(@"hh\:mm");

                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
                _modoModificacion = true;
            }
        }

        private void ReiniciarEstadoInicial()
        {
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            _modoModificacion = false;

            txtNumCita.Clear();
            txtNumExpediente.Clear();
            txtHora.Clear();
            dgvCita.UnselectAll();
        }
    


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lógica para manejar el evento SelectionChanged
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
