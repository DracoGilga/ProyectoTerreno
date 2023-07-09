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

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para MenuPagos.xaml
    /// </summary>
    public partial class MenuPagos : Window
    {
        public MenuPagos()
        {
            InitializeComponent();
        }

        private void Btn_AgregarPago(object sender, RoutedEventArgs e)
        {
            AgregarPago agregarPago = new AgregarPago();
            agregarPago.Show();
        }

        private void Btn_ModificarPago(object sender, RoutedEventArgs e)
        {
            ModificarPago modificarPago = new ModificarPago();
            modificarPago.Show();
        }

        private void Btn_listaPago(object sender, RoutedEventArgs e)
        {
            ListaPagos listaPagos = new ListaPagos();
            listaPagos.Show();
        }

        private void Btn_eliminarPago(object sender, RoutedEventArgs e)
        {
            EliminarPago eliminarPago = new EliminarPago();
            eliminarPago.Show();
        }
    }
}
