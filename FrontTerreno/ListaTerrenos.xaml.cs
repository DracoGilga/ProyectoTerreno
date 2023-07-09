using FrontTerreno.Modelo;
using ServiceReference1;
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
    /// Lógica de interacción para ListaTerrenos.xaml
    /// </summary>
    public partial class ListaTerrenos : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public ListaTerrenos()
        {
            InitializeComponent();
            MostrarTerrenos();
        }
        private async void MostrarTerrenos()
        {
            List<Terreno> terrenos = await terrenoViewModel.ListaTerrenos();
            tablaTerrenos.ItemsSource = terrenos;
        }
    }
}
