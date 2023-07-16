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
    /// Lógica de interacción para ModificarContrato.xaml
    /// </summary>
    public partial class ModificarContrato : Window
    {
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        PersonaViewModel personaViewModel = new PersonaViewModel();
        TiposViewModel tiposViewModel = new TiposViewModel();
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        List<Terreno> agregados = new List<Terreno>();
        List<Terreno> eliminados = new List<Terreno>();

        public ModificarContrato()
        {
            InitializeComponent();
            MostrarContrato();
            BuscarPredios();
            BuscarClientes();
            BuscarTiposFecha();
        }
        public async void MostrarContrato()
        {
            Cb_clienteContrato.ItemTemplate = (DataTemplate)Resources["ClienteContratoItemTemplate"];
            List<ContratoPersona> lista = await contratoViewModel.MostrarContratosPersona();
            Cb_clienteContrato.ItemsSource = lista;

        }
        private async void BuscarClientes()
        {
            Cb_cliente.ItemTemplate = (DataTemplate)Resources["ClienteItemTemplate"];

            List<Persona> clientes = await personaViewModel.ListaPersonas();
            Cb_cliente.ItemsSource = clientes;
            Cb_vendedor.ItemsSource = clientes;
        }
        private async void BuscarTiposFecha()
        {
            Cb_fechaPago.ItemTemplate = (DataTemplate)Resources["TipoFechaItemTemplate"];

            List<TipoFecha> tiposFecha = await tiposViewModel.tipoFechas();
            Cb_fechaPago.DisplayMemberPath = "Descripcion";
            Cb_fechaPago.ItemsSource = tiposFecha;
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
            Cb_lote.ItemsSource = lotes;
        }
        private async void ListaTerrenos(int idContrato)
        {
            List<Terreno> terrenos = await terrenoViewModel.ListaTerrenosContrato(idContrato);
            Dg_terreno.ItemsSource = terrenos;

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
                Cb_lote.DisplayMemberPath = "NoLote";
                BuscarLotes(((Manzana)Cb_manzana.SelectedItem).IdManzana);
            }
        }
        private void Clic_contrato(object sender, SelectionChangedEventArgs e)
        {
            if(Cb_clienteContrato.SelectedItem != null)
            {
                var contrato = (ContratoPersona)Cb_clienteContrato.SelectedItem;
                Tb_tipoPago.Text = contrato.Contrato.TipoPago;
                Tb_costo.Text = contrato.Contrato.Costo.ToString();
                Tb_testigo1.Text = contrato.Contrato.Testigo1;
                Tb_testigo2.Text = contrato.Contrato.Testigo2;
                Dp_fechaContrato.SelectedDate = contrato.Contrato.FechaContrato;
                Cb_cliente.SelectedIndex = -1;
                Cb_fechaPago.SelectedIndex = -1;
                Cb_vendedor.SelectedIndex = -1;

                var cliente = Cb_cliente.Items.OfType<Persona>().FirstOrDefault(p => p.IdPersona == contrato.Contrato.IdCliente);
                if (cliente != null)
                    Cb_cliente.SelectedItem = cliente;
                var tipoFecha = Cb_fechaPago.Items.OfType<TipoFecha>().FirstOrDefault(tf => tf.IdTipoFecha == contrato.Contrato.IdTipoFecha);
                if (tipoFecha != null)
                    Cb_fechaPago.SelectedItem = tipoFecha;
                var vendedor = Cb_vendedor.Items.OfType<Persona>().FirstOrDefault(p => p.IdPersona == contrato.Contrato.IdVendedor);
                if (vendedor != null)
                    Cb_vendedor.SelectedItem = vendedor;
                ListaTerrenos(contrato.Contrato.IdContrato);
            }
        }
        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if (Cb_cliente.SelectedIndex != -1 && Cb_fechaPago.SelectedIndex != -1 && Cb_predio.SelectedIndex == -1
                && Cb_manzana.SelectedIndex == -1 && Cb_lote.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(Tb_tipoPago.Text)
                && !string.IsNullOrWhiteSpace(Tb_costo.Text) && !string.IsNullOrWhiteSpace(Tb_testigo1.Text)
                && Dp_fechaContrato.SelectedDate != null && (Dg_terreno.ItemsSource != null || Dg_terreno.Items.Count != 0))
            {
                Contrato contrato = new Contrato()
                {
                    IdCliente = ((Persona)Cb_cliente.SelectedItem).IdPersona,
                    Persona = new Persona()
                    {
                        IdPersona = ((Persona)Cb_cliente.SelectedItem).IdPersona
                    },
                    TipoPago = Tb_tipoPago.Text,
                    Costo = double.Parse(Tb_costo.Text),
                    Testigo1 = Tb_testigo1.Text,
                    Testigo2 = Tb_testigo2.Text,
                    FechaContrato = Dp_fechaContrato.SelectedDate.Value,
                    IdTipoFecha = ((TipoFecha)Cb_fechaPago.SelectedItem).IdTipoFecha,
                    TipoFecha = new TipoFecha()
                    {
                        IdTipoFecha = ((TipoFecha)Cb_fechaPago.SelectedItem).IdTipoFecha
                    },
                };
                contrato.Persona1 = new Persona() { };
                if (Cb_vendedor.SelectedIndex != -1)
                {
                    contrato.IdVendedor = ((Persona)Cb_vendedor.SelectedItem).IdPersona;
                    contrato.Persona1.IdPersona = ((Persona)Cb_vendedor.SelectedItem).IdPersona;
                }
                contrato.IdContrato = Cb_clienteContrato.SelectedIndex != -1 ? ((ContratoPersona)Cb_clienteContrato.SelectedItem).Contrato.IdContrato : 0;

                bool resultado = await contratoViewModel.ModificarContrato(contrato);
                if (resultado)
                {
                    MessageBox.Show("Contrato modificado correctamente");
                    foreach (Terreno terreno in eliminados)
                    {
                        bool mensajeDel = await terrenoViewModel.EliminarContratoLote((int)terreno.IdLote);
                        if (mensajeDel)
                            MessageBox.Show("Terreno eliminado correctamente");
                        else
                            MessageBox.Show("Error al eliminar el terreno");
                    }
                    foreach (Terreno terreno in agregados)
                    {
                        bool mensajeAdd = await terrenoViewModel.ModificarContratoLote(contrato.IdContrato, (int)terreno.IdLote);
                        if (mensajeAdd)
                            MessageBox.Show("Terreno agregado correctamente");
                        else
                            MessageBox.Show("Error al agregar el terreno");
                    }
                    this.Close();
                }
                else
                    MessageBox.Show("Error al modificar el contrato");
            }
            else
                MessageBox.Show("Faltan campos por llenar");
        }
        private void Btn_Eliminar(object sender, RoutedEventArgs e)
        {
            if (Dg_terreno.SelectedItem != null)
            {
                var terrenos = Dg_terreno.ItemsSource as List<Terreno>;
                var terrenoSeleccionado = Dg_terreno.SelectedItem as Terreno;
                terrenos.Remove(terrenoSeleccionado);
                eliminados.Add(terrenoSeleccionado);
                Dg_terreno.ItemsSource = null;
                Dg_terreno.ItemsSource = terrenos;
            }
        }
        private void Btn_Aniadir(object sender, RoutedEventArgs e)
        {
            if (Cb_lote.SelectedIndex != -1)
            {
                if (((Lote)Cb_lote.SelectedItem).IdContrato == null)
                {
                    var terreno = new Terreno()
                    {
                        IdLote = ((Lote)Cb_lote.SelectedItem).IdLote,
                        NoLote = ((Lote)Cb_lote.SelectedItem).NoLote,
                        Superficie = ((Lote)Cb_lote.SelectedItem).Superficie,
                        NoManzana = ((Manzana)Cb_manzana.SelectedItem).NoManzana,
                        Nombre = ((Predio)Cb_predio.SelectedItem).Nombre
                    };
                    var terrenos = Dg_terreno.ItemsSource as List<Terreno>;
                    terrenos.Add(terreno);
                    agregados.Add(terreno);
                    Dg_terreno.ItemsSource = null;
                    Dg_terreno.ItemsSource = terrenos;
                }
                else
                {
                    MessageBox.Show("El lote seleccionado tiene contrato");
                }
                Cb_predio.SelectedIndex = -1;
                Cb_manzana.SelectedIndex = -1;
                Cb_lote.SelectedIndex = -1;
            }
        }

    }
}
