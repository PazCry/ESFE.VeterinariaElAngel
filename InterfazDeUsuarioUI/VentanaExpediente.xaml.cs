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
    /// Lógica de interacción para VentanaExpediente.xaml
    /// </summary>
    public partial class VentanaExpediente : Window
    {
        private ClienteBL _clienteBL = new ClienteBL();
        private MascotaBL _mascotaBL = new MascotaBL();

        ExpedeinteBL _expedienteBL = new ExpedeinteBL();
        ExpedienteEN _expedienteEN = new ExpedienteEN();

        public VentanaExpediente()
        {
            InitializeComponent();
            CargarGrid();
            CargarCombos();
            ReiniciarEstadoInicial();
            dgvListarExpediente.SelectedIndex = -1;
        }

        public void CargarGrid()
        {
            dgvListarExpediente.ItemsSource = _expedienteBL.MostrarExpe();
        }

        private void CargarCombos()
        {
            // Cliente
            cbxCliente.ItemsSource = _clienteBL.ListarCliente();
            cbxCliente.DisplayMemberPath = "Nombre";   // lo que se muestra
            cbxCliente.SelectedValuePath = "Id";       // lo que se guarda

            // Mascota
            cbxMascota.ItemsSource = _mascotaBL.MostrarMascota();
            cbxMascota.DisplayMemberPath = "Nombre";
            cbxMascota.SelectedValuePath = "Id";

            // Estado de Expediente
            cbxEstado.ItemsSource = _expedienteBL.MostrarExpe();
            cbxEstado.DisplayMemberPath = "Estado";    // o "Descripcion", según tu tabla
            cbxEstado.SelectedValuePath = "Id";
        }
        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrid();
        }




        private void ReiniciarEstadoInicial()
        {


            cbxCliente.SelectedIndex = -1;
            cbxMascota.SelectedIndex = -1;
            cbxEstado.SelectedIndex = -1;
            dtpFechaAtencion.SelectedDate = DateTime.Today;
            txtDescripcionConsulta.Clear();



            dgvListarExpediente.SelectedIndex = -1;


        }



        // Restricciones de entrada en TextBox y ComboBox (similar a KeyPress en WinForms)
        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void SoloLetras(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            if (cbxCliente.SelectedIndex == -1 ||
        cbxMascota.SelectedIndex == -1 ||
        cbxEstado.SelectedIndex == -1 ||
        string.IsNullOrWhiteSpace(txtDescripcionConsulta.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos y seleccione todas las opciones.",
                                "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dtpFechaAtencion.SelectedDate.HasValue && dtpFechaAtencion.SelectedDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha de atención no puede ser anterior a hoy.",
                                "Fecha inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ahora sacamos los IDs directamente de los ComboBox
            _expedienteEN.IdCliente = Convert.ToByte(cbxCliente.SelectedValue);
            _expedienteEN.IdMascota = Convert.ToByte(cbxMascota.SelectedValue);
            _expedienteEN.Estado = cbxEstado.Text; // asegúrate que tu entidad tenga este campo
            _expedienteEN.DescripcionConsulta = txtDescripcionConsulta.Text;
            _expedienteEN.Fecha = dtpFechaAtencion.SelectedDate.Value;

            _expedienteBL.GuardarExpe(_expedienteEN);

            CargarGrid();

            txtId.Clear();
            txtDescripcionConsulta.Clear();
            cbxCliente.SelectedIndex = -1;
            cbxMascota.SelectedIndex = -1;
            cbxEstado.SelectedIndex = -1;

            MessageBox.Show("Expediente guardado correctamente.",
                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(cbxEstado.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionConsulta.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de modificar.",
                                "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea modificar este registro?",
                                "Confirmar modificación", MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _expedienteEN.Id = Convert.ToByte(txtId.Text);
                _expedienteEN.IdCliente = Convert.ToByte(cbxCliente.Text);
                _expedienteEN.IdMascota = Convert.ToByte(cbxMascota.Text);
                _expedienteEN.Estado = cbxEstado.Text;
                _expedienteEN.DescripcionConsulta = txtDescripcionConsulta.Text;

                _expedienteBL.ModificarExpe(_expedienteEN);

                txtDescripcionConsulta.Clear();
                txtId.Clear();
                CargarGrid();

                MessageBox.Show("Expediente modificado correctamente.",
                                "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);
                ReiniciarEstadoInicial();
            }
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Debe seleccionar un registro antes de eliminar.",
                                "Campo requerido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea eliminar este registro?",
                                "Confirmar eliminación", MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _expedienteEN.Id = Convert.ToByte(txtId.Text);
                _expedienteBL.EliminarExpe(_expedienteEN);

                txtDescripcionConsulta.Clear();
                txtId.Clear();
                ReiniciarEstadoInicial();
                CargarGrid();

                MessageBox.Show("Registro eliminado correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBuscar_Click_1(object sender, RoutedEventArgs e)
        {
            string Id = txtBuscar1.Text;
            List<CitaEN> cita = CitaBL.BuscarCita(Id);
            dgvListarExpediente.ItemsSource = cita;
        }

        private void dgvListarExpediente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvListarExpediente.SelectedItem is ExpedienteEN fila)
            {
                cbxCliente.Text = fila.IdCliente.ToString();
                cbxMascota.Text = fila.IdMascota.ToString();
                cbxEstado.Text = fila.Estado;
                dtpFechaAtencion.SelectedDate = fila.Fecha;
                txtDescripcionConsulta.Text = fila.DescripcionConsulta;
                txtId.Text = fila.Id.ToString();


            }
        }
    }
}

