using FrontTerreno.Modelo;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para CrearContrato.xaml
    /// </summary>
    public partial class CrearContrato : Window
    {
        TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
        PersonaViewModel personaViewModel = new PersonaViewModel();
        TiposViewModel tiposViewModel = new TiposViewModel();
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        public CrearContrato()
        {
            InitializeComponent();
            BuscarPredios();
            BuscarClientes();
            BuscarTiposFecha();
            Dp_fechaContrato.SelectedDate = DateTime.Today;
        }
        
        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(Cb_cliente.SelectedIndex != -1 && Cb_fechaPago.SelectedIndex != -1 && Cb_predio.SelectedIndex == -1 
                && Cb_manzana.SelectedIndex == -1 && Cb_lote.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(Tb_tipoPago.Text) 
                && !string.IsNullOrWhiteSpace(Tb_costo.Text) && !string.IsNullOrWhiteSpace(Tb_testigo1.Text) 
                && Dp_fechaContrato.SelectedDate != null && (Dg_terreno.ItemsSource != null || Dg_terreno.Items.Count != 0) )
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
                contrato.Persona1 = new Persona(){};
                if(Cb_vendedor.SelectedIndex != -1)
                {
                    contrato.IdVendedor = ((Persona)Cb_vendedor.SelectedItem).IdPersona;
                    contrato.Persona1.IdPersona = ((Persona)Cb_vendedor.SelectedItem).IdPersona;
                }

                int? resultado = await contratoViewModel.GuardarContrato(contrato);
                if (resultado != null)
                {
                    MessageBox.Show("Contrato guardado correctamente");
                    foreach (dynamic terreno in Dg_terreno.Items)
                    {
                        bool resultadoTerreno = await terrenoViewModel.ModificarContratoLote(resultado.Value, terreno.IdLote);
                        if (resultadoTerreno)
                        {
                            MessageBox.Show("Terreno guardado correctamente");
                            this.Close();
                        }
                        else    
                            MessageBox.Show("Error al guardar el terreno");
                    }
                }
                else
                    MessageBox.Show("Error al guardar el contrato");
            }
            else
                MessageBox.Show("Faltan campos por llenar");
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

        private void Btn_Aniadir(object sender, RoutedEventArgs e)
        {
            if(Cb_lote.SelectedIndex != -1)
            {
                if(((Lote)Cb_lote.SelectedItem).IdContrato == null)
                {
                    Terreno item = new Terreno()
                    {
                        IdLote = ((Lote)Cb_lote.SelectedItem).IdLote,
                        NoLote = ((Lote)Cb_lote.SelectedItem).NoLote,
                        Superficie = ((Lote)Cb_lote.SelectedItem).Superficie,
                        NoManzana = ((Manzana)Cb_manzana.SelectedItem).NoManzana,
                        Nombre = ((Predio)Cb_predio.SelectedItem).Nombre
                    };
                    Dg_terreno.Items.Add(item);
                }
                else
                    MessageBox.Show("El lote seleccionado tiene contrato");
                Cb_predio.SelectedIndex = -1;
                Cb_manzana.SelectedIndex = -1;
                Cb_lote.SelectedIndex = -1;
            }
        }
        private void Btn_Eliminar(object sender, RoutedEventArgs e)
        {
            Dg_terreno.Items.Remove(Dg_terreno.SelectedItem);
        }
    }
}