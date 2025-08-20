using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para InicioAdministración.xaml
    /// </summary>
    public partial class InicioAdministración : Window
    {
        private Button currentActiveButton;
        public InicioAdministración()
        {
            InitializeComponent();


            // Establecer el botón de Usuario como activo por defecto
            currentActiveButton = btnUsuario;

            // Cargar la vista inicial
            LoadDashboardView();
        }

        #region Métodos de Navegación

        /// <summary>
        /// Método para cambiar el botón activo visualmente
        /// </summary>
        private void SetActiveButton(Button clickedButton)
        {
            // Quitar el estilo activo del botón anterior
            if (currentActiveButton != null)
            {
                currentActiveButton.Style = (Style)FindResource("SidebarButton");
            }

            // Aplicar el estilo activo al botón clickeado
            clickedButton.Style = (Style)FindResource("ActiveSidebarButton");

            // Actualizar la referencia del botón activo
            currentActiveButton = clickedButton;
        }

        /// <summary>
        /// Método para limpiar el contenido principal
        /// </summary>
        private void ClearMainContent()
        {
            MainContent.Children.Clear();
        }

        /// <summary>
        /// Método para crear un panel de contenido genérico
        /// </summary>
        private StackPanel CreateContentPanel(string title, string description, string emoji = "🌸")
        {
            var panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0.8
            };

            var titleBlock = new TextBlock
            {
                Text = $"{emoji} {title}",
                FontSize = 32,
                FontWeight = FontWeights.Light,
                Foreground = new SolidColorBrush(Color.FromRgb(218, 112, 214)), // #FFDA70D6
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var descBlock = new TextBlock
            {
                Text = description,
                FontSize = 18,
                Foreground = new SolidColorBrush(Color.FromRgb(186, 85, 211)), // #FFBA55D3
                HorizontalAlignment = HorizontalAlignment.Center,
                Opacity = 0.9,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 400
            };

            var subtitleBlock = new TextBlock
            {
                Text = "Próximamente disponible ✨",
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(218, 112, 214)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 15, 0, 0),
                FontStyle = FontStyles.Italic,
                Opacity = 0.7
            };

            panel.Children.Add(titleBlock);
            panel.Children.Add(descBlock);
            panel.Children.Add(subtitleBlock);

            return panel;
        }

        #endregion

        #region Event Handlers de Navegación

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(btnInicio);
            PageTitle.Text = "🏥 Panel Principal - Sistema El Ángel";
            LoadDashboardView();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Usuarios", "👤 Gestión de Usuarios");
            MostrarContenidoUsuarios();
        }
        private void CambiarPagina(string titulo, string tituloCompleto)
        {
            PageTitle.Text = tituloCompleto;
        }


        private void Roles_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Roles", "🏷️ Gestión de Roles");
            MostrarContenidoRoles();
        }


        private void Mascotas_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Mascotas", "🐕 Gestión de Mascotas");
            MostrarContenidoMascotas();
        }

        private void Generos_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Géneros", "⚥ Gestión de Géneros");
            MostrarContenidoGeneros();
        }
        private void Razas_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Razas", "🐕‍🦺 Gestión de Razas");
            MostrarContenidoRazas();
        }

        private void Especies_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(btnEspecie);
            PageTitle.Text = "🦮 Gestión de Especies - El Ángel";
           MostrarContenidoEspecies();
        }

        private void Citas_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Citas", "📅 Gestión de Citas");
            MostrarContenidoCitas();
        }


        private void CitaCalendario_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(btnCitaCalendario);
            PageTitle.Text = "🗓️ Calendario de Citas - El Ángel";
            LoadCalendarioView();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            CambiarPagina("Clientes", "👥 Gestión de Clientes");
            MostrarContenidoClientes();
        }
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmar Cierre de Sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Aquí puedes agregar lógica adicional de cierre de sesión
                // Como limpiar datos, guardar configuraciones, etc.

                MessageBox.Show(
                    "¡Sesión cerrada exitosamente!\n¡Que tengas un día angelical! 🌸",
                    "Hasta Pronto",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Cerrar la aplicación o mostrar ventana de login
                Application.Current.Shutdown();
            }
        }

        #endregion

        #region Métodos para Cargar Vistas

        private void LoadDashboardView()
        {
            ClearMainContent();

            var dashboardPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0.7
            };

            // Título principal
            var titleBlock = new TextBlock
            {
                Text = "🌸 Bienvenido al Sistema Veterinario El Ángel",
                FontSize = 28,
                FontWeight = FontWeights.Light,
                Foreground = new SolidColorBrush(Color.FromRgb(218, 112, 214)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 15)
            };

            // Descripción
            var descBlock = new TextBlock
            {
                Text = "Selecciona una opción del menú lateral para comenzar",
                FontSize = 16,
                Foreground = new SolidColorBrush(Color.FromRgb(186, 85, 211)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Opacity = 0.9
            };

            // Mensaje especial
            var specialBlock = new TextBlock
            {
                Text = "¡Cuidamos a tus mascotas con amor! 💕",
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(218, 112, 214)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0),
                FontStyle = FontStyles.Italic,
                Opacity = 0.8
            };

            dashboardPanel.Children.Add(titleBlock);
            dashboardPanel.Children.Add(descBlock);
            dashboardPanel.Children.Add(specialBlock);

            MainContent.Children.Add(dashboardPanel);
        }

        private void MostrarContenidoUsuarios()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaUsuarios = new VentanaUsuario();
                var contenidoVentana = ventanaUsuarios.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaUsuarios.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaUsuarios.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la vista de usuarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MostrarPlaceholder("Usuarios no disponibles", "No se pudo cargar la vista de usuarios. Inténtalo más tarde.");
            }
        }



        private void MostrarPlaceholder(string titulo, string descripcion)
        {
            var content = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var tituloText = new TextBlock
            {
                Text = titulo,
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E3440")),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var descripcionText = new TextBlock
            {
                Text = descripcion,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Colors.Gray)
            };

            content.Children.Add(tituloText);
            content.Children.Add(descripcionText);
            MainContent.Children.Add(content);
        }
        private void MostrarContenidoRoles()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaRoles = new VentanaRol();
                var contenidoVentana = ventanaRoles.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaRoles.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaRoles.Close();
            }
            catch (Exception ex)
            {
                MostrarPlaceholder("🏷️ Gestión de Roles",
                    "• Crear roles de usuario\n• Asignar permisos\n• Definir niveles de acceso\n• Administrar jerarquías");
            }
        }
        private void MostrarContenidoMascotas()
        {
            MainContent.Children.Clear();

            // 🔥 OPCIÓN 1: Si tienes UserControls creados
            // var controlMascotas = new MascotasUserControl();
            // MainContent.Children.Add(controlMascotas);

            // 🔥 OPCIÓN 2: Cargar el contenido de la ventana existente
            try
            {
                var ventanaMascotas = new VentanaMascota();
                var contenidoVentana = ventanaMascotas.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    // Remover del padre original
                    ventanaMascotas.Content = null;
                    // Agregar al MainContent
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaMascotas.Close(); // Cerrar la ventana vacía
            }
            catch (Exception)
            {
                // Si hay error, mostrar mensaje placeholder
                MostrarPlaceholder("🐕 Gestión de Mascotas",
                    "• Registrar nuevas mascotas\n• Editar información\n• Ver historial médico\n• Buscar por filtros");
            }
        }
        private void MostrarContenidoGeneros()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaGeneros = new VentanaGenero();
                var contenidoVentana = ventanaGeneros.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaGeneros.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaGeneros.Close();
            }
            catch (Exception ex)
            {
                MostrarPlaceholder("⚥ Gestión de Géneros",
                    "• Clasificar por género\n• Macho/Hembra\n• Información reproductiva\n• Datos para historiales");
            }
        }
        private void MostrarContenidoRazas()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaRazas = new VentanaRaza();
                var contenidoVentana = ventanaRazas.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaRazas.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaRazas.Close();
            }
            catch (Exception ex)
            {
                MostrarPlaceholder("🐕‍🦺 Gestión de Razas",
                    "• Registrar razas por especie\n• Características específicas\n• Editar información\n• Organizar por categorías");
            }
        }
        private void MostrarContenidoEspecies()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaEspecies = new VentanaEspecie();
                var contenidoVentana = ventanaEspecies.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaEspecies.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaEspecies.Close();
            }
            catch (Exception ex)
            {
                MostrarPlaceholder("🦎 Gestión de Especies",
                    "• Registrar especies\n• Clasificar animales\n• Editar información\n• Mantener catálogo actualizado");
            }
        }
 private void MostrarContenidoCitas()
 {
     MainContent.Children.Clear();

     try
     {
         var ventanaCitas = new VentanaCita();
         var contenidoVentana = ventanaCitas.Content as FrameworkElement;

         if (contenidoVentana != null)
         {
             ventanaCitas.Content = null;
             MainContent.Children.Add(contenidoVentana);
         }
         ventanaCitas.Close();
     }
     catch (Exception ex)
     {
         MostrarPlaceholder("📅 Gestión de Citas",
             "• Agendar nuevas citas\n• Ver calendario\n• Modificar citas existentes\n• Recordatorios automáticos");
     }
 }

        private void LoadHistorialView()
        {
            ClearMainContent();
            var content = CreateContentPanel(
                "Expedientes Médicos",
                "Consulta y administra los historiales médicos de las mascotas",
                "📋"
            );
            MainContent.Children.Add(content);
        }

        private void LoadCalendarioView()
        {
            ClearMainContent();
            var content = CreateContentPanel(
                "Calendario de Citas",
                "Vista de calendario para una mejor gestión de citas",
                "🗓️"
            );
            MainContent.Children.Add(content);
        }

        private void MostrarContenidoClientes()
        {
            MainContent.Children.Clear();

            try
            {
                var ventanaClientes = new VentanaCliente();
                var contenidoVentana = ventanaClientes.Content as FrameworkElement;

                if (contenidoVentana != null)
                {
                    ventanaClientes.Content = null;
                    MainContent.Children.Add(contenidoVentana);
                }
                ventanaClientes.Close();
            }
            catch (Exception ex)
            {
                MostrarPlaceholder("👥 Gestión de Clientes",
                    "• Registrar nuevos clientes\n• Editar información de contacto\n• Ver mascotas del cliente\n• Historial de servicios");
            }
        }
        #endregion

        #region Eventos Adicionales (Opcional)

        /// <summary>
        /// Evento para manejar el cierre de la ventana
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "¿Está seguro que desea salir del sistema?",
                "Confirmar Salida",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}