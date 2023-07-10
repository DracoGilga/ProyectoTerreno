using FrontTerreno.Modelo;
using ServiceReference1;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para AgregarLote.xaml
    /// </summary>
    public partial class AgregarLote : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        public AgregarLote()
        {
            InitializeComponent();
            BuscarPredios();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(Cb_predio.SelectedIndex != -1 && Cb_manzana.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(Tb_lote.Text) && !string.IsNullOrWhiteSpace(Tb_superficie.Text))
            {
                Lote lote = new Lote
                {
                    IdManzana = ((Manzana)Cb_manzana.SelectedItem).IdManzana,
                    Manzana = new Manzana()
                    {
                        IdManzana = ((Manzana)Cb_manzana.SelectedItem).IdManzana
                    },
                    NoLote = Tb_lote.Text,
                    Superficie = double.Parse(Tb_superficie.Text)

                };
                bool resultado = await terrenoViewModel.GuardarLote(lote);
                if (resultado)
                {
                    MessageBox.Show("Lote guardado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al guardar lote");
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
            if (Cb_predio.SelectedIndex != -1)
            {
                Cb_manzana.DisplayMemberPath = "NoManzana";
                BuscarManzanas(((Predio)Cb_predio.SelectedItem).IdPredio);
            }
        }
    }
}
