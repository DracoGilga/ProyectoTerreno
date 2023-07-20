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
    /// Lógica de interacción para AgregarManzana.xaml
    /// </summary>
    public partial class AgregarManzana : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public AgregarManzana()
        {
            InitializeComponent();
            BuscarPredios();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1)
            {
                if (!string.IsNullOrWhiteSpace(Tb_nombre.Text))
                {
                    Manzana manzana = new Manzana();
                    manzana.IdPredio = ((Predio)Cb_predio.SelectedItem).IdPredio;
                    manzana.Predio = new Predio()
                    {
                        IdPredio = ((Predio)Cb_predio.SelectedItem).IdPredio,
                    };
                    manzana.NoManzana = Tb_nombre.Text;
                    bool respuesta = await terrenoViewModel.GuardarManzana(manzana);

                    if (respuesta)
                    {
                        MessageBox.Show("Manzana guardada correctamente");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Error al guardar manzana");
                }
                else
                    MessageBox.Show("Favor de llenar todos los campos");
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
    }
}
