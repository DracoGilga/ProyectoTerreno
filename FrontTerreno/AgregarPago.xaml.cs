using FrontTerreno.Modelo;
using FrontTerreno.Recursos;
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
    /// Lógica de interacción para AgregarPago.xaml
    /// </summary>
    public partial class AgregarPago : Window
    {
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        TiposViewModel tiposViewModel = new TiposViewModel();
        PagoViewModel pagoViewModel = new PagoViewModel();
        public AgregarPago()
        {
            InitializeComponent();
            MostrarClientes();
            TipoPagos();
            Dp_fecha.SelectedDate = DateTime.Now;
        }

        private async void Btn_registrar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tb_cantidad.Text) && !string.IsNullOrWhiteSpace(Dp_fecha.Text) &&
                Cb_cliente.SelectedIndex != -1 && Cb_tipoPago.SelectedIndex != -1)
            {
                Pago pago = new Pago()
                {
                    IdContrato = ((dynamic)Cb_cliente.SelectedItem).IdContrato,
                    Contrato = new Contrato()
                    {
                        IdContrato = ((dynamic)Cb_cliente.SelectedItem).IdContrato
                    },
                    SerialPago = Tb_folio.Text,
                    FechaPago = Dp_fecha.SelectedDate.Value,
                    CantidadPago = (double)Convert.ToDecimal(Tb_cantidad.Text),
                    IdTipoPago = ((dynamic)Cb_tipoPago.SelectedItem).IdTipoPago,
                    TipoPago = new TipoPago()
                    {
                        IdTipoPago = ((dynamic)Cb_tipoPago.SelectedItem).IdTipoPago
                    }
                };
                if (await pagoViewModel.GuardarPago(pago))
                {
                    MessageBox.Show("Pago registrado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al registrar pago");
            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
            
        }
        private string GenerarFolio()
        {
            var selectedCliente = Cb_cliente.SelectedItem as dynamic;
            string nombrePredio = selectedCliente.NombrePredio;
            string nombreCompleto = selectedCliente.Nombre;
            string fecha = DateTime.Now.ToString("MMddyy");
            int numeroAleatorio = new Random().Next(1, 1000);
            string[] palabrasPredio = nombrePredio.Split(' ');
            string siglaNombrePredio = string.Join("", palabrasPredio.Take(2).Select(p => p[0]));
            string[] palabrasNombre = nombreCompleto.Split(' ');
            string siglaNombre = string.Join("", palabrasNombre.Select(p => p[0]));
            string fechaNumeroAleatorio = fecha + numeroAleatorio.ToString();
            string hexFechaNumeroAleatorio = Convert.ToInt32(fechaNumeroAleatorio).ToString("X");

            string folio = $"{siglaNombrePredio}{siglaNombre}{hexFechaNumeroAleatorio}";

            return folio;
        }


        private async void MostrarClientes()
        {
            List<ContratoUnion> contratos = await contratoViewModel.DesplegarListaContratos();
            Cb_cliente.DisplayMemberPath = "Nombre";
            foreach (var item in contratos)
            {
                // Obtener el primer terreno de la lista
                Terreno primerTerreno = item.Terreno.FirstOrDefault();

                var contratoLista = new
                {
                    item.Contrato.IdContrato,
                    Nombre = item.Persona.Nombre + " " + item.Persona.ApellidoPaterno + " " + item.Persona.ApellidoMaterno,
                    NombrePredio = primerTerreno?.Nombre,
                    primerTerreno?.NoManzana,
                    NoLote = string.Join(", ", item.Terreno.Select(t => t.NoLote))
                };
                Cb_cliente.Items.Add(contratoLista);
            }
        }
        private void Clic_cliente(object sender, SelectionChangedEventArgs e)
        {
            if(Cb_cliente.SelectedIndex != -1)
            {
                Tb_predio.Text = ((dynamic)Cb_cliente.SelectedItem).NombrePredio;
                var selectedCliente = Cb_cliente.SelectedItem as dynamic;
                if (selectedCliente != null && selectedCliente.NoManzana != null)
                {
                    Tb_mazana.Text = selectedCliente.NoManzana.ToString();
                }
                Tb_lote.Text = ((dynamic)Cb_cliente.SelectedItem).NoLote;
                Tb_folio.Text = GenerarFolio();
            }
        }

        private async void TipoPagos()
        {
            List<TipoPago> tipoPagos = await tiposViewModel.tipoPagos();
            Cb_tipoPago.DisplayMemberPath = "Descripcion";
            Cb_tipoPago.ItemsSource = tipoPagos;

        }
        private void Tb_cantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                e.Handled = true;
            else if (e.Text == "." && ((TextBox)sender).Text.Contains("."))
                e.Handled = true;
            else if (((TextBox)sender).Text.Contains("."))
            {
                string currentText = ((TextBox)sender).Text;
                int decimalIndex = currentText.IndexOf(".");
                if (decimalIndex != -1 && currentText.Length - decimalIndex > 2)
                    e.Handled = true;
            }
        }
        private void Tb_cantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(Tb_cantidad.Text, out double cantidad))
                Tb_cantidadL.Text = ConvertidorPesosMexicanos.ConvertirNumeroAPesos(cantidad);
            else
                Tb_cantidadL.Text = string.Empty;
        }

        private void Dp_fecha_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ConversionFecha();
        }
        private void Dp_fecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ConversionFecha();
        }
        private void ConversionFecha()
        {
            if (Dp_fecha.SelectedDate.HasValue)
            {
                DateTime fechaSeleccionada = Dp_fecha.SelectedDate.Value;
                string fechaFormateada = fechaSeleccionada.ToString("dddd dd 'de' MMMM 'del' yyyy");
                Tb_fechaL.Text = fechaFormateada;
            }
        }
    }
}
