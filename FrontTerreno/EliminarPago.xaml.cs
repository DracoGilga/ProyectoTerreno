using FrontTerreno.Modelo;
using FrontTerreno.Recursos;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para EliminarPago.xaml
    /// </summary>
    public partial class EliminarPago : Window
    {
        PagoViewModel pagoViewModel = new PagoViewModel();
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        TiposViewModel tiposViewModel = new TiposViewModel();
        Pago pago = new Pago();
        public EliminarPago()
        {
            InitializeComponent();
            MostrarClientes();
            TipoPagos();
        }
        private async void Btn_eliminar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tb_folio.Text) && !string.IsNullOrWhiteSpace(Tb_cantidad.Text)
                && !string.IsNullOrWhiteSpace(Dp_fecha.Text))
            {
                if(await pagoViewModel.EliminarPago(pago.IdPago))
                {
                    MessageBox.Show("Pago eliminado");
                    this.Close();
                }
                else
                    MessageBox.Show("No se pudo eliminar el pago");
            }
            else
                MessageBox.Show("Llene todos los campos");
        }

        private async void Btn_Buscar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tb_folioBuscar.Text))
            {
                pago = await pagoViewModel.BuscarPago(Tb_folioBuscar.Text);
                if (pago != null)
                {
                    Tb_folio.Text = pago.SerialPago;
                    Tb_cantidad.Text = pago.CantidadPago.ToString();
                    Dp_fecha.Text = pago.FechaPago.ToString();
                    Cb_cliente.SelectedIndex = -1;
                    Cb_tipoPago.SelectedIndex = -1;

                    var cliente = Cb_cliente.Items.Cast<dynamic>().FirstOrDefault(c => c.IdContrato == pago.IdContrato);
                    if (cliente != null)
                        Cb_cliente.SelectedItem = cliente;
                    var tipoPago = Cb_tipoPago.Items.Cast<TipoPago>().FirstOrDefault(t => t.IdTipoPago == pago.IdTipoPago);
                    if (tipoPago != null)
                        Cb_tipoPago.SelectedItem = tipoPago;
                }
                else
                    MessageBox.Show("No se encontro el pago");
            }
            else
                MessageBox.Show("Ingrese un folio");
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
