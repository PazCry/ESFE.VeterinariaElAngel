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
    /// Lógica de interacción para InicioSecretario.xaml
    /// </summary>
    public partial class InicioSecretario : Window
    {
        private Button currentActiveButton;
        public InicioSecretario()
        {
            InitializeComponent();

            // 🔧 CORRECCIÓN: Establecer el botón de Inicio como activo por defecto
            currentActiveButton = btnInicio;
            SetActiveButton(btnInicio); // Aplicar el estilo activo
            // Cargar la vista inicial
            LoadDashboardView();
        }



            #region Métodos de Navegación

            /// <summary>
            /// Método para cambiar el botón activo visualmente
            /// </summary>y>
            private void SetActiveButton(Button clickedButton)
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cambiar botón activo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            /// <summary>
            /// Método para limpiar el contenido principal
            /// </summary>
            private void ClearMainContent()
            {
                try
                {
                    MainContent.Children.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al limpiar contenido: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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

            #region Event Handlers de Navegación - CORREGIDOS

            private void Dashboard_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnInicio); // 🔧 CORRECCIÓN: Usar btnInicio
                PageTitle.Text = "🏥 Panel Principal - Sistema El Ángel";
                LoadDashboardView();
            }


            private void CambiarPagina(string titulo, string tituloCompleto)
            {
                PageTitle.Text = tituloCompleto;
            }

            private void Roles_Click(object sender, RoutedEventArgs e)
            {
                // 🔧 CORRECCIÓN: Necesitas definir un botón para roles o usar uno existente
                // Como no tienes btnRoles definido, usaré un método alternativo
                CambiarPagina("Roles", "🏷️ Gestión de Roles");
                MostrarContenidoRoles();
            }

            private void Mascotas_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnMascota); // 🔧 CORRECCIÓN: Agregar SetActiveButton
                CambiarPagina("Mascotas", "🐕 Gestión de Mascotas");
                MostrarContenidoMascotas();
            }

            private void Generos_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnGenero); // 🔧 CORRECCIÓN: Agregar SetActiveButton
                CambiarPagina("Géneros", "⚥ Gestión de Géneros");
                MostrarContenidoGeneros();
            }

            private void Razas_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnRaza); // 🔧 CORRECCIÓN: Agregar SetActiveButton
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
                SetActiveButton(btnCita); // 🔧 CORRECCIÓN: Agregar SetActiveButton
                CambiarPagina("Citas", "📅 Gestión de Citas");
                MostrarContenidoCitas();
            }



            private void Historial_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnExpediente); // 🔧 CORRECCIÓN: Agregar SetActiveButton
                CambiarPagina("Expedientes", "📋 Expedientes Médicos");
                MostrarContenidoExpedientes();
            }

            private void Clientes_Click(object sender, RoutedEventArgs e)
            {
                SetActiveButton(btnCliente); // 🔧 CORRECCIÓN: Agregar SetActiveButton
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
                    MessageBox.Show(
                        "¡Sesión cerrada exitosamente!\n¡Que tengas un día angelical! 🌸",
                        "Hasta Pronto",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    Application.Current.Shutdown();
                }
            }

            #endregion

            #region Métodos para Cargar Vistas - MEJORADOS

            private void LoadDashboardView()
            {
                ClearMainContent();

                var dashboardPanel = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Opacity = 0.7
                };

                var titleBlock = new TextBlock
                {
                    Text = "🌸 Bienvenido al Sistema Veterinario El Ángel",
                    FontSize = 28,
                    FontWeight = FontWeights.Light,
                    Foreground = new SolidColorBrush(Color.FromRgb(218, 112, 214)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 15)
                };

                var descBlock = new TextBlock
                {
                    Text = "Selecciona una opción del menú lateral para comenzar",
                    FontSize = 16,
                    Foreground = new SolidColorBrush(Color.FromRgb(186, 85, 211)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Opacity = 0.9
                };

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

            // 🔧 MÉTODO MEJORADO para cargar ventanas con mejor manejo de errores
            private bool CargarVentanaEnContainer<T>(Func<T> crearVentana, string nombreVentana) where T : Window
            {
                try
                {
                    ClearMainContent();

                    // Método 1: Intentar cargar el contenido de la ventana
                    var ventana = crearVentana();

                    if (ventana?.Content is FrameworkElement contenido)
                    {
                        ventana.Content = null; // Remover del padre original
                        MainContent.Children.Add(contenido);
                        ventana.Close(); // Cerrar la ventana vacía
                        return true;
                    }
                    else
                    {
                        ventana?.Close();
                        throw new InvalidOperationException($"La ventana {nombreVentana} no tiene contenido válido");
                    }
                }
                catch (Exception ex)
                {
                    // Mostrar error detallado para debugging
                    MessageBox.Show(
                        $"❌ Error al cargar {nombreVentana}:\n\n{ex.Message}\n\nTipo: {ex.GetType().Name}\n\n" +
                        $"StackTrace:\n{ex.StackTrace}",
                        $"Error - {nombreVentana}",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return false;
                }
            }

            private void MostrarContenidoUsuarios()
            {
                if (!CargarVentanaEnContainer(() => new VentanaUsuario(), "VentanaUsuario"))
                {
                    MostrarPlaceholder("❌ Error en Usuarios",
                        "No se pudo cargar la ventana de usuarios.\nVerifica que la ventana VentanaUsuario existe y es accesible.");
                }
            }

            private void MostrarContenidoRoles()
            {
                if (!CargarVentanaEnContainer(() => new VentanaRol(), "VentanaRol"))
                {
                    MostrarPlaceholder("❌ Error en Roles",
                        "No se pudo cargar la ventana de roles.\nVerifica que la ventana VentanaRol existe y es accesible.");
                }
            }

            private void MostrarContenidoExpedientes()
            {
                if (!CargarVentanaEnContainer(() => new VentanaExpediente(), "VentanaExpediente"))
                {
                    MostrarPlaceholder("📋 Expedientes Médicos",
                        "• Historiales por mascota\n• Consultas anteriores\n• Tratamientos aplicados\n• Vacunas y medicamentos");
                }
            }

            private void MostrarContenidoMascotas()
            {
                if (!CargarVentanaEnContainer(() => new VentanaMascota(), "VentanaMascota"))
                {
                    MostrarPlaceholder("🐕 Gestión de Mascotas",
                        "• Registrar nuevas mascotas\n• Editar información\n• Ver historial médico\n• Buscar por filtros");
                }
            }

            private void MostrarContenidoGeneros()
            {
                if (!CargarVentanaEnContainer(() => new VentanaGenero(), "VentanaGenero"))
                {
                    MostrarPlaceholder("⚥ Gestión de Géneros",
                        "• Clasificar por género\n• Macho/Hembra\n• Información reproductiva\n• Datos para historiales");
                }
            }

            private void MostrarContenidoRazas()
            {
                if (!CargarVentanaEnContainer(() => new VentanaRaza(), "VentanaRaza"))
                {
                    MostrarPlaceholder("🐕‍🦺 Gestión de Razas",
                        "• Registrar razas por especie\n• Características específicas\n• Editar información\n• Organizar por categorías");
                }
            }

            private void MostrarContenidoEspecies()
            {
                if (!CargarVentanaEnContainer(() => new VentanaEspecie(), "VentanaEspecie"))
                {
                    MostrarPlaceholder("🦎 Gestión de Especies",
                        "• Registrar especies\n• Clasificar animales\n• Editar información\n• Mantener catálogo actualizado");
                }
            }

            private void MostrarContenidoCitas()
            {
                if (!CargarVentanaEnContainer(() => new VentanaCita(), "VentanaCita"))
                {
                    MostrarPlaceholder("📅 Gestión de Citas",
                        "• Agendar nuevas citas\n• Ver calendario\n• Modificar citas existentes\n• Recordatorios automáticos");
                }
            }

            private void MostrarContenidoClientes()
            {
                if (!CargarVentanaEnContainer(() => new VentanaCliente(), "VentanaCliente"))
                {
                    MostrarPlaceholder("👥 Gestión de Clientes",
                        "• Registrar nuevos clientes\n• Editar información de contacto\n• Ver mascotas del cliente\n• Historial de servicios");
                }
            }

            // 🔧 CORRECCIÓN: Mejorar LoadCalendarioView


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

            #endregion

            #region Eventos Adicionales

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
