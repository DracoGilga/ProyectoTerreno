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
    /// Lógica de interacción para MenuPersona.xaml
    /// </summary>
    public partial class MenuPersona : Window
    {
        public MenuPersona()
        {
            InitializeComponent();
        }
        private void Btn_AgregarPersona(object sender, RoutedEventArgs e)
        {
            AgregarPersona agregarPersona = new AgregarPersona();
            agregarPersona.Show();
        }

        private void Btn_ModificarPersona(object sender, RoutedEventArgs e)
        {
            ModificarPersona modificarPersona = new ModificarPersona();
            modificarPersona.Show();
        }

        private void Btn_listaPersona(object sender, RoutedEventArgs e)
        {
            ListaPersona listaPersona = new ListaPersona();
            listaPersona.Show();
        }

        private void Btn_eliminarPersona(object sender, RoutedEventArgs e)
        {
            EliminarPersona eliminarPersona = new EliminarPersona();
            eliminarPersona.Show();
        }
    }
}
