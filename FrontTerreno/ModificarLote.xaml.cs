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
    /// Lógica de interacción para ModificarLote.xaml
    /// </summary>
    public partial class ModificarLote : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public ModificarLote()
        {
            InitializeComponent();
            BuscarPredios();
        }

        private async void Btn_modificar(object sender, RoutedEventArgs e)
        {
            if (Cb_predio.SelectedIndex != -1 && Cb_manzana.SelectedIndex != -1 && Cb_Lote.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(Tb_lote.Text) && !string.IsNullOrWhiteSpace(Tb_superficie.Text))
            {
                Lote lote = new Lote
                {
                    IdLote = ((Lote)Cb_Lote.SelectedItem).IdLote,
                    IdManzana = ((Manzana)Cb_manzana.SelectedItem).IdManzana,
                    Manzana = new Manzana()
                    {
                        IdManzana = ((Manzana)Cb_manzana.SelectedItem).IdManzana
                    },
                    NoLote = Tb_lote.Text,
                    Superficie = double.Parse(Tb_superficie.Text)

                };
                bool resultado = await terrenoViewModel.ModificarLote(lote);
                if (resultado)
                {
                    MessageBox.Show("Lote modificado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al modificar lote");
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
        private async void BuscarLotes(int idManzana)
        {
            List<Lote> lotes = await terrenoViewModel.ListaLotes(idManzana);
            Cb_Lote.ItemsSource = lotes;
        }
        private void Clic_predio(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_predio.SelectedIndex != -1)
            {
                Cb_manzana.DisplayMemberPath = "NoManzana";
                BuscarManzanas(((Predio)Cb_predio.SelectedItem).IdPredio);
            }
        }
        private void Clic_manzana(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_manzana.SelectedIndex != -1)
            {
                Cb_Lote.DisplayMemberPath = "NoLote";
                BuscarLotes(((Manzana)Cb_manzana.SelectedItem).IdManzana);
            }
        }
        private void Clic_lote(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_Lote.SelectedIndex != -1)
            {
                Tb_superficie.Text = ((Lote)Cb_Lote.SelectedItem).Superficie.ToString();
                Tb_lote.Text = ((Lote)Cb_Lote.SelectedItem).NoLote;
            }
        }
    }
}
