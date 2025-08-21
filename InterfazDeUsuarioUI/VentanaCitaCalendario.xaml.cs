
using EntidadDeNegociosEN;
using LogicaDeNegocioBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace InterfazDeUsuarioUI
{
    public partial class VentanaCitaCalendario : Window
    {
        private List<CitaCalendarioEN> _citas;  // ahora vienen de la BD

        public VentanaCitaCalendario()
        {
            InitializeComponent();

            // Obtener citas desde la base de datos
            _citas = CitaCalendarioBL.ObtenerCitas();

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
            }), DispatcherPriority.Loaded);
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