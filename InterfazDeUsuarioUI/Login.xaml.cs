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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void txtCorreo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCorreo.Text == "Ingrese Correo @")
            {
                txtCorreo.Text = "";
                txtCorreo.Foreground = Brushes.Maroon;
            }
        }

        private void txtCorreo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                txtCorreo.Text = "Ingrese Correo @";
                txtCorreo.Foreground = Brushes.Gray;
            }
        }

        private void txtContrasena_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPassword.Foreground = Brushes.Black;
            }
        }

        private void txtContrasena_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                txtPassword.Tag = "Contraseña";
                txtPassword.Foreground = Brushes.Gray;
            }
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioEN usuarioEN = new UsuarioEN();
            UsuarioActualEN.Correo = txtCorreo.Text.Trim();

            usuarioEN.Correo = txtCorreo.Text.Trim();
            usuarioEN.Contra = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(usuarioEN.Correo) || string.IsNullOrEmpty(usuarioEN.Contra))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo debe contener una '@'.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UsuarioBL usuarioBL = new UsuarioBL();
            int idRolObtenido = usuarioBL.VerificarUsuarioLogin(usuarioEN);

            if (idRolObtenido == 1)
            {
                new InicioAdministración().Show();
                this.Close();
            }
            else if (idRolObtenido == 2)
            {
                new InicioVeterinaria().Show();
                this.Close();
            }
            else if (idRolObtenido == 3)
            {
                new InicioSecretario().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
