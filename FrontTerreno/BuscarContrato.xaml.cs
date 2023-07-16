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
    /// Lógica de interacción para BuscarContrato.xaml
    /// </summary>
    public partial class BuscarContrato : Window
    {
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        PersonaViewModel personaViewModel = new PersonaViewModel();
        TiposViewModel tiposViewModel = new TiposViewModel();
        public BuscarContrato()
        {
            InitializeComponent();
            MostrarContrato();
            BuscarClientes();
            BuscarTiposFecha();
        }
        private void Btn_imprimir(object sender, RoutedEventArgs e)
        {

        }

        private void Clic_contrato(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_clienteContrato.SelectedItem != null)
            {
                var contrato = (ContratoPersona)Cb_clienteContrato.SelectedItem;
                Tb_tipoPago.Text = contrato.Contrato.TipoPago;
                Tb_costo.Text = contrato.Contrato.Costo.ToString();
                Tb_testigo1.Text = contrato.Contrato.Testigo1;
                Tb_testigo2.Text = contrato.Contrato.Testigo2;
                Dp_fechaContrato.SelectedDate = contrato.Contrato.FechaContrato;
                Cb_fechaPago.SelectedIndex = -1;
                Cb_vendedor.SelectedIndex = -1;

                Cb_cliente.Text = contrato.Persona.Nombre + " " + contrato.Persona.ApellidoPaterno + " " + contrato.Persona.ApellidoMaterno;
                var tipoFecha = Cb_fechaPago.Items.OfType<TipoFecha>().FirstOrDefault(tf => tf.IdTipoFecha == contrato.Contrato.IdTipoFecha);
                if (tipoFecha != null)
                    Cb_fechaPago.SelectedItem = tipoFecha;
                var vendedor = Cb_vendedor.Items.OfType<Persona>().FirstOrDefault(p => p.IdPersona == contrato.Contrato.IdVendedor);
                if (vendedor != null)
                    Cb_vendedor.SelectedItem = vendedor;

                ListaTerrenos(contrato.Contrato.IdContrato);
            }
        }
        private async void ListaTerrenos(int idContrato)
        {
            List<Terreno> terrenos = await terrenoViewModel.ListaTerrenosContrato(idContrato);
            Dg_terreno.ItemsSource = terrenos;

        }
        private async void BuscarClientes()
        {
            Cb_vendedor.ItemTemplate = (DataTemplate)Resources["ClienteItemTemplate"];
            List<Persona> clientes = await personaViewModel.ListaPersonas();
            Cb_vendedor.ItemsSource = clientes;
        }
        private async void BuscarTiposFecha()
        {
            List<TipoFecha> tiposFecha = await tiposViewModel.tipoFechas();
            Cb_fechaPago.DisplayMemberPath = "Descripcion";
            Cb_fechaPago.ItemsSource = tiposFecha;
        }
        public async void MostrarContrato()
        {
            Cb_clienteContrato.ItemTemplate = (DataTemplate)Resources["ClienteContratoItemTemplate"];
            List<ContratoPersona> lista = await contratoViewModel.MostrarContratosPersona();
            Cb_clienteContrato.ItemsSource = lista;

        }
    }
}
