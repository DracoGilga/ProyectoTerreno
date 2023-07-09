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
    /// Lógica de interacción para EliminarPredio.xaml
    /// </summary>
    public partial class EliminarPredio : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public EliminarPredio()
        {
            InitializeComponent();
            BuscarPredios();
        }

        private async void Btn_eliminar(object sender, RoutedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1)
            {
                int idPredio = ((Predio)Cb_predio.SelectedItem).IdPredio;
                bool respuesta = await terrenoViewModel.EliminarPredio(idPredio);
                if (respuesta)
                {
                    MessageBox.Show("Predio eliminado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al eliminar predio");               
            }
            else
                MessageBox.Show("Favor de seleccionar un predio");
        }
        private async void BuscarPredios()
        {
            Cb_predio.ItemTemplate = (DataTemplate)Resources["PredioItemTemplate"];
            
            List<Predio> predios = await terrenoViewModel.ListaPredios();
            Cb_predio.ItemsSource = predios;
        }
        private void Clic_predio(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_predio.SelectedIndex != -1)
            {
                var predio = (Predio)Cb_predio.SelectedItem;
                Tb_nombre.Text = predio.Nombre;
                Tb_ubicacion.Text = predio.Ubicacion;
            }
            else
            {
                Tb_nombre.Text = string.Empty;
                Tb_ubicacion.Text = string.Empty;
            }
        }
    }
}
