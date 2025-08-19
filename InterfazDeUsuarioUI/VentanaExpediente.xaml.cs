using EntidadDeNegociosEN;
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
        ExpedienteBL _expedienteBL = new ExpedienteBL();
        ExpedienteEN _expedienteEN = new ExpedienteEN();
        private bool _modoModificacion = false;
        public VentanaExpediente()
        {
            InitializeComponent();
            CargarGrid();
            ReiniciarEstadoInicial();

            // Eventos para detectar cambios
            txtCliente.SelectionChanged += Campos_TextChanged;
            cbxMascota.SelectionChanged += Campos_TextChanged;
            txtEstado.SelectionChanged += Campos_TextChanged;
            dpFechaInicio.SelectedDateChanged += Campos_TextChanged;
            txtDescripcion.TextChanged += Campos_TextChanged;

            dgvListarExpediente.SelectedIndex = -1;
        }

        public void CargarGrid()
        {
            ExpedientesDataGrid.ItemsSource = _expedienteBL.MostrarExpe();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCliente.SelectedIndex == -1 ||
                cbxIdMascota.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtEstado.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos y seleccione todas las opciones.",
                                "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dpFechaInicio.SelectedDate.HasValue && dpFechaInicio.SelectedDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha de atención no puede ser anterior a hoy.",
                                "Fecha inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _expedienteEN.IdCliente = Convert.ToByte(txtCliente.Text);
            _expedienteEN.IdMascota = Convert.ToByte(cbxIdMascota.Text);
            _expedienteEN.Estado = txtEstado.Text;
            _expedienteEN.DescripcionConsulta = txtDescripcionConsulta.Text;
            _expedienteEN.FechaAtencion = dtpFechaAtencion.SelectedDate.Value;

            _expedienteBL.GuardarExpe(_expedienteEN);

            CargarGrid();

            txtId.Clear();
            txtDescripcionConsulta.Clear();

            MessageBox.Show("Expediente guardado correctamente.",
                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
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
                _expedienteEN.IdCliente = Convert.ToByte(cbxIdCliente.Text);
                _expedienteEN.IdMascota = Convert.ToByte(cbxIdMascota.Text);
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string Id = SearchTextBox.Text;
            List<ExpedienteEN> registros = ExpedienteBL.BuscarExpe(Id);
            ExpedientesDataGrid.ItemsSource = registros;
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrid();
        }

        private void Campos_TextChanged(object sender, EventArgs e)
        {
            if (!_modoModificacion)
                btnGuardar.IsEnabled = CamposCompletos();
        }

        private bool CamposCompletos()
        {
            return cbxIdCliente.SelectedIndex != -1 &&
                   cbxIdMascota.SelectedIndex != -1 &&
                   !string.IsNullOrWhiteSpace(cbxEstado.Text) &&
                   !string.IsNullOrWhiteSpace(txtDescripcionConsulta.Text);
        }

        private void ReiniciarEstadoInicial()
        {
            cbxIdCliente.SelectionChanged -= Campos_TextChanged;
            cbxIdMascota.SelectionChanged -= Campos_TextChanged;

            cbxIdCliente.SelectedIndex = -1;
            cbxIdMascota.SelectedIndex = -1;
            cbxEstado.SelectedIndex = -1;
            dtpFechaAtencion.SelectedDate = DateTime.Today;
            txtDescripcionConsulta.Clear();

            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            _modoModificacion = false;

            ExpedientesDataGrid.SelectedIndex = -1;

            cbxIdCliente.SelectionChanged += Campos_TextChanged;
            cbxIdMascota.SelectionChanged += Campos_TextChanged;
        }

        private void ExpedientesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExpedientesDataGrid.SelectedItem is ExpedienteEN fila)
            {
                cbxIdCliente.SelectedValue = fila.IdCliente;
                cbxIdMascota.SelectedValue = fila.IdMascota;
                cbxEstado.SelectedItem = fila.Estado;
                dtpFechaAtencion.SelectedDate = fila.FechaAtencion;
                txtDescripcionConsulta.Text = fila.DescripcionConsulta;
                txtId.Text = fila.Id.ToString();

                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
                _modoModificacion = true;
            }
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
    }
}
    }
}
