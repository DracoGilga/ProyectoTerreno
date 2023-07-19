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
    /// Lógica de interacción para ModificarManzana.xaml
    /// </summary>
    public partial class ModificarManzana : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public ModificarManzana()
        {
            InitializeComponent();
            BuscarPredios();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1 && Cb_manzana.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(Tb_noManzana.Text))
            {
                Manzana manzana = new Manzana();
                manzana.IdPredio = ((Predio)Cb_predio.SelectedItem).IdPredio;
                manzana.IdManzana = ((Manzana)Cb_manzana.SelectedItem).IdManzana;
                manzana.NoManzana = Convert.ToInt32(Tb_noManzana.Text);
                manzana.Predio = new Predio()
                {
                    IdPredio = ((Predio)Cb_predio.SelectedItem).IdPredio
                };
                bool resultado = await terrenoViewModel.ModificarManzana(manzana);
                if (resultado)
                {
                    MessageBox.Show("Manzana modificada correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al modificar manzana");
            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
        }
        private async void BuscarPredios()
        {
            Cb_predio.ItemTemplate = (DataTemplate)Resources["PredioItemTemplate"];

            List<Predio> predios = await terrenoViewModel.ListaPredios();
            Cb_predio.ItemsSource = predios;
        }
        private async void BuscarManzanas(int idPredio)
        {
            List<Manzana> manzanas = await terrenoViewModel.ListaManzanas(idPredio);
            Cb_manzana.ItemsSource = manzanas;
        }
        private void Clic_predio(object sender, SelectionChangedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1)
            {
                Cb_manzana.DisplayMemberPath = "NoManzana";
                BuscarManzanas(((Predio)Cb_predio.SelectedItem).IdPredio);
            }
        }
        private void Clic_manzana(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_predio.SelectedIndex != -1 && Cb_manzana.SelectedItem != null)
            {
                Tb_noManzana.Text = ((Manzana)Cb_manzana.SelectedItem).NoManzana.ToString();
            }
            else
                Tb_noManzana.Text = string.Empty;
        }

        private void Tb_nombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
                e.Handled = true;
        }


    }
}
