using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para CrearContrato.xaml
    /// </summary>
    public partial class CrearContrato : Window
    {
        public CrearContrato()
        {
            InitializeComponent();
            Dp_fechaContrato.SelectedDate = DateTime.Today;
        }

        private void Btn_guardar(object sender, RoutedEventArgs e)
        {
            DateTime fecha = Dp_fechaContrato.SelectedDate ?? DateTime.MinValue;
            string formatoFecha = fecha.ToString("yyyy-MM-dd");

            MessageBox.Show($"Fecha seleccionada: {formatoFecha}");
        }

        private void Btn_Aniadir(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Eliminar(object sender, RoutedEventArgs e)
        {

        }
    }
}