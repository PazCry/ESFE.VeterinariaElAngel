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
    /// Lógica de interacción para VentanaEspecie.xaml
    /// </summary>
    public partial class VentanaEspecie : Window
    {
        private readonly EspecieBL _especieBL = new EspecieBL();
        private readonly EspecieEN _especieEN = new EspecieEN();

        public VentanaEspecie()
        {
            InitializeComponent();

            CargarGrid();
            txtNombre.PreviewTextInput += txtNombre_PreviewTextInput;
        }
        private void CargarGrid()
        {
            dgEspecies.ItemsSource = _especieBL.MostrarEspecie();
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) && !char.IsWhiteSpace(e.Text, 0))
            {
                MessageBox.Show("Solo se permiten letras.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }





        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos requeridos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _especieEN.TipoEspecie = txtNombre.Text;
            _especieBL.GuardarEspecie(_especieEN);

            CargarGrid();
            txtIdEspecie.Text = "";
            txtNombre.Text = "";

            MessageBox.Show("Especie guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnModificar_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtIdEspecie.Text)) return;

            _especieEN.Id = Convert.ToByte(txtIdEspecie.Text);
            _especieEN.TipoEspecie = txtNombre.Text;
            _especieBL.ModificarEspecie(_especieEN);

            CargarGrid();
            txtIdEspecie.Text = "";
            txtNombre.Text = "";

            MessageBox.Show("Registro modificado correctamente.", "Modificación", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEspecie.Text)) return;

            var confirm = MessageBox.Show("¿Realmente desea eliminar este registro?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                _especieEN.Id = Convert.ToByte(txtIdEspecie.Text);
                _especieEN.TipoEspecie = txtNombre.Text;
                _especieBL.EliminarEspecie(_especieEN);

                txtIdEspecie.Text = "";
                txtNombre.Text = "";
                CargarGrid();

            }
        }

        private void dgEspecies_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            if (dgEspecies.SelectedItem is EspecieEN especie)
            {
                txtIdEspecie.Text = especie.Id.ToString();
                txtNombre.Text = especie.TipoEspecie;


            }
        }
    }
}




