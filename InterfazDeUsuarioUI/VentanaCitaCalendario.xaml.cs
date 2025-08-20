using EntidadDeNegociosEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfazDeUsuarioUI
{
    /// <summary>
    /// Lógica de interacción para VentanaCitaCalendario.xaml
    /// </summary>
    public partial class VentanaCitaCalendario : Window
    {
        // Simulación de citas (cuando tengas la BD, las traerás de ahí)
        private List<CitaCalendarioEN> _citas = new List<CitaCalendarioEN>
        {
            new CitaCalendarioEN { FechaCita = new DateTime(2025, 8, 20), Hora = new TimeSpan(10, 30, 0), NombreCliente = "Juan Pérez" },
            new CitaCalendarioEN { FechaCita = new DateTime(2025, 8, 20), Hora = new TimeSpan(14, 00, 0), NombreCliente = "Ana Gómez" },
            new CitaCalendarioEN { FechaCita = new DateTime(2025, 8, 23), Hora = new TimeSpan( 9, 00, 0), NombreCliente = "Carlos López" },
            new CitaCalendarioEN { FechaCita = new DateTime(2025, 8, 25), Hora = new TimeSpan(16, 15, 0), NombreCliente = "María Torres" },

        };
      public VentanaCitaCalendario()
        {
            InitializeComponent();

            // Pintar días al cargar y cuando cambie el mes
            CalendarioCitas.Loaded += (_, __) => RefrescarDiasConCitas();
            CalendarioCitas.DisplayDateChanged += (_, __) => RefrescarDiasConCitas();
        }

        // Evento: cuando seleccionás un día
        private void CalendarioCitas_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarioCitas.SelectedDate is DateTime fecha)
            {
                var citasDelDia = _citas
                    .Where(c => c.FechaCita.Date == fecha.Date)
                    .OrderBy(c => c.Hora)
                    .ToList();

                if (citasDelDia.Any())
                {
                    string mensaje = "Citas del " + fecha.ToString("dd/MM/yyyy") + ":\n\n";
                    foreach (var cita in citasDelDia)
                    {
                        mensaje += $"- {cita.Hora:hh\\:mm} {cita.NombreCliente}\n";
                    }

                    MessageBox.Show(mensaje, "Citas encontradas", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No hay citas en esta fecha.", "Sin citas", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // MÉTODO FALTANTE: Evento para cuando cambia el modo de visualización del calendario
        private void CalendarioCitas_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            // Cuando el usuario cambia entre Month, Year, Decade
            // Puedes agregar lógica aquí si necesitas hacer algo específico
            // Por ejemplo, refrescar las citas cuando regrese a vista mensual
            if (e.NewMode == CalendarMode.Month)
            {
                RefrescarDiasConCitas();
            }
        }

        // Pintar los días con citas en azul
        private void RefrescarDiasConCitas()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var btn in FindVisualChildren<CalendarDayButton>(CalendarioCitas))
                {
                    if (btn.DataContext is DateTime fecha)
                    {
                        bool hayCita = _citas.Any(c => c.FechaCita.Date == fecha.Date);

                        btn.ClearValue(Control.ToolTipProperty);
                        btn.ClearValue(Control.BackgroundProperty);

                        if (hayCita)
                        {
                            btn.Background = Brushes.LightSkyBlue;
                            btn.ToolTip = "Hay citas este día";
                        }
                    }
                }
            }));
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T match)
                    yield return match;

                foreach (var nested in FindVisualChildren<T>(child))
                    yield return nested;
            }
        }


        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}