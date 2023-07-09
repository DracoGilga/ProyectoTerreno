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
    /// Lógica de interacción para ModificarPredio.xaml
    /// </summary>
    public partial class ModificarPredio : Window
    {
        public ModificarPredio()
        {
            InitializeComponent();
            BuscarPredios();
        }
        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tb_nombre.Text) && !string.IsNullOrWhiteSpace(Tb_ubicacion.Text))
            {
                Predio predio = new Predio();
                predio.Nombre = Tb_nombre.Text;
                predio.Ubicacion = Tb_ubicacion.Text;
                predio.IdPredio = ((Predio)Cb_predio.SelectedItem).IdPredio;
                TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
                bool resultado = await terrenoViewModel.ModificarPredio(predio);

                if (resultado)
                {
                    MessageBox.Show("Predio modificado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al modificar predio");

            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
        }

        private async void BuscarPredios()
        {
            Cb_predio.ItemTemplate = (DataTemplate)Resources["PredioItemTemplate"];
            TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
            List<Predio> predios = await terrenoViewModel.ListaPredios();
            Cb_predio.ItemsSource = predios;
        }
        private void Clic_predio(object sender, SelectionChangedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1)
            {
                var predio = (Predio)Cb_predio.SelectedItem;
                Tb_nombre.Text = predio.Nombre;
                Tb_ubicacion.Text = predio.Ubicacion;
            }
            else
            {
                Tb_nombre.Text=string.Empty;
                Tb_ubicacion.Text=string.Empty;
            }   
        }
    }
}
