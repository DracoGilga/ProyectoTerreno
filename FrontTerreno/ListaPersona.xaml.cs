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
    /// Lógica de interacción para ListaPersona.xaml
    /// </summary>
    public partial class ListaPersona : Window
    {
        public ListaPersona()
        {
            InitializeComponent();
            listaPersona();
        }

        private async void listaPersona()
        {
            PersonaViewModel personaViewModel = new PersonaViewModel();
            List<Persona> lista = await personaViewModel.ListaPersonas();
            tablaPersona.ItemsSource = lista;
        }
    }
}
