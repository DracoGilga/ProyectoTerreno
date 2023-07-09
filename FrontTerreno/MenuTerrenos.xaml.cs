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
    /// Lógica de interacción para MenuTerrenos.xaml
    /// </summary>
    public partial class MenuTerrenos : Window
    {
        public MenuTerrenos()
        {
            InitializeComponent();
        }

        private void Btn_agregarPredio(object sender, RoutedEventArgs e)
        {
            AgregarPredio agregarPredio = new AgregarPredio();
            agregarPredio.Show();
        }

        private void Btn_modificarPredio(object sender, RoutedEventArgs e)
        {
            ModificarPredio modificarPredio = new ModificarPredio();
            modificarPredio.Show();
        }

        private void Btn_eliminarPredio(object sender, RoutedEventArgs e)
        {
            EliminarPredio eliminarPredio = new EliminarPredio();
            eliminarPredio.Show();
        }

        private void Btn_agregarManzana(object sender, RoutedEventArgs e)
        {
            AgregarManzana agregarManzana = new AgregarManzana();
            agregarManzana.Show();
        }

        private void Btn_modificarManzana(object sender, RoutedEventArgs e)
        {
            ModificarManzana modificarManzana = new ModificarManzana();
            modificarManzana.Show();
        }

        private void Btn_eliminarManzana(object sender, RoutedEventArgs e)
        {
            EliminarManzana eliminarManzana = new EliminarManzana();
            eliminarManzana.Show();
        }

        private void Btn_agregarLote(object sender, RoutedEventArgs e)
        {
            AgregarLote agregarLote = new AgregarLote();
            agregarLote.Show();
        }

        private void Btn_modificarLote(object sender, RoutedEventArgs e)
        {
            ModificarLote modificarLote = new ModificarLote();
            modificarLote.Show();
        }

        private void Btn_eliminarLote(object sender, RoutedEventArgs e)
        {
            EliminarLote eliminarLote = new EliminarLote();
            eliminarLote.Show();
        }

        private void Btn_listaTerrenos(object sender, RoutedEventArgs e)
        {
            ListaTerrenos listaTerrenos = new ListaTerrenos();
            listaTerrenos.Show();
        }
    }
}
