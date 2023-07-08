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
    /// Lógica de interacción para MenuContrato.xaml
    /// </summary>
    public partial class MenuContrato : Window
    {
        public MenuContrato()
        {
            InitializeComponent();
        }

        private void Btn_crearContrato(object sender, RoutedEventArgs e)
        {
            CrearContrato crearContrato = new CrearContrato();
            crearContrato.Show();
        }

        private void Btn_modificarContrato(object sender, RoutedEventArgs e)
        {
            ModificarContrato modificarContrato = new ModificarContrato();
            modificarContrato.Show();
        }

        private void Btn_buscarContrato(object sender, RoutedEventArgs e)
        {
            BuscarContrato buscarContrato = new BuscarContrato();
            buscarContrato.Show();
        }

        private void Btn_listaContrato(object sender, RoutedEventArgs e)
        {
            ListaContratos listaContratos = new ListaContratos();
            listaContratos.Show();
        }

        private void Btn_eliminarContrato(object sender, RoutedEventArgs e)
        {
            EliminarContrato eliminarContrato = new EliminarContrato();
            eliminarContrato.Show();
        }
    }
}
