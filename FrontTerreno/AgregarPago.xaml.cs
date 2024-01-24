using FrontTerreno.Modelo;
using FrontTerreno.Recursos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
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
            /*
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
            */
            PdfPago();


        }
        private string GenerarFolio()
        {
            var selectedCliente = Cb_cliente.SelectedItem as dynamic;
            string nombrePredio = selectedCliente.NombrePredio;
            string nombreCompleto = selectedCliente.Nombre;
            string fecha = DateTime.Now.ToString("MMddyy");
            int numeroAleatorio = new Random().Next(1, 1000);
            string[] palabrasPredio = nombrePredio.Split(' ');
            string siglaNombrePredio = palabrasPredio.Length >= 2 ? string.Concat(palabrasPredio.Take(2).Select(p => p.Length > 0 ? p[0] : ' ')) : string.Empty;
            string[] palabrasNombre = nombreCompleto.Split(' ');
            string siglaNombre = palabrasNombre.Length >= 1 ? string.Concat(palabrasNombre.Select(p => p.Length > 0 ? p[0] : ' ')) : string.Empty;
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

        private void PdfPago()
        {
            // Obtenemos la ruta del escritorio del usuario
            string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Creamos la carpeta "PDF" dentro del escritorio, si no existe
            string carpetaPDF = System.IO.Path.Combine(rutaEscritorio, "PDF");
            if (!Directory.Exists(carpetaPDF))
            {
                Directory.CreateDirectory(carpetaPDF);
            }

            // Ruta donde queremos guardar el archivo PDF
            string rutaArchivoPDF = System.IO.Path.Combine(carpetaPDF, "recibo_pago.pdf");

            // Creamos el archivo PDF
            using (FileStream fs = new FileStream(rutaArchivoPDF, FileMode.Create))
            {
                // Creamos el documento en tamaño carta
                Document doc = new Document(PageSize.LETTER);
                PdfWriter.GetInstance(doc, fs);

                // Abrimos el documento para escribir en él
                doc.Open();

                // Creamos una fuente para el contenido
                BaseFont fuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font contenidoFuente = new Font(fuente, 12);
                Font tituloFuente = new Font(fuente, 16, Font.BOLD);

                // Creamos el cuadro principal que rodea todo y le establecemos el borde negro
                PdfPTable cuadroPrincipal = new PdfPTable(1);
                cuadroPrincipal.WidthPercentage = 100f;
                cuadroPrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
                cuadroPrincipal.DefaultCell.BorderColor = BaseColor.BLACK;

                // Agregamos el contenido al cuadro principal
                PdfPCell celdaLenceroRecibo = new PdfPCell(new Phrase("Lencero 3\n\nRecibo de Pago", tituloFuente));
                celdaLenceroRecibo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                celdaLenceroRecibo.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaLenceroRecibo.PaddingBottom = 2f; // Espacio de 2 mm entre Lencero 3 y Recibo de Pago
                cuadroPrincipal.AddCell(celdaLenceroRecibo);

                PdfPCell celdaFolio = new PdfPCell(new Phrase("Folio: 12123", new Font(fuente, 12, Font.BOLD, BaseColor.RED)));
                celdaFolio.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaFolio);

                PdfPCell celdaBueno = new PdfPCell(new Phrase("Bueno por $5000", contenidoFuente));
                celdaBueno.Border = iTextSharp.text.Rectangle.NO_BORDER;
                celdaBueno.HorizontalAlignment = Element.ALIGN_RIGHT;
                cuadroPrincipal.AddCell(celdaBueno);

                PdfPCell celdaFecha = new PdfPCell(new Phrase("Fecha: jueves, 03 de agosto de 2023", contenidoFuente));
                celdaFecha.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaFecha);

                PdfPCell celdaRecibiDe = new PdfPCell(new Phrase("Recibí de: Cesar Gonzalez Lopez", contenidoFuente));
                celdaRecibiDe.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaRecibiDe);

                PdfPCell celdaCantidad = new PdfPCell(new Phrase("La cantidad de cinco mil pesos con 0 centavos mexicanos.", contenidoFuente));
                celdaCantidad.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaCantidad);

                PdfPCell celdaConcepto = new PdfPCell(new Phrase("Por concepto de: PAGO DE MENSUALIDAD", new Font(fuente, 12, Font.BOLD)));
                celdaConcepto.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaConcepto);

                PdfPCell celdaLote = new PdfPCell(new Phrase("Lote no. 40 mza. B, superficie 200m2", contenidoFuente));
                celdaLote.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroPrincipal.AddCell(celdaLote);

                // Agregamos el cuadro principal al documento
                doc.Add(cuadroPrincipal);

                // Agregamos un espacio en blanco
                doc.Add(new iTextSharp.text.Paragraph("\n"));

                // Creamos una tabla para las líneas de firma
                PdfPTable tablaFirmas = new PdfPTable(2);
                tablaFirmas.WidthPercentage = 100f;

                // Línea izquierda
                PdfPCell lineaIzquierda = new PdfPCell(new iTextSharp.text.Paragraph("___________________________"));
                lineaIzquierda.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tablaFirmas.AddCell(lineaIzquierda);

                // Línea derecha
                PdfPCell lineaDerecha = new PdfPCell(new iTextSharp.text.Paragraph("___________________________"));
                lineaDerecha.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tablaFirmas.AddCell(lineaDerecha);

                // Agregamos la tabla de firmas al documento
                doc.Add(tablaFirmas);

                // Agregamos un espacio en blanco
                doc.Add(new iTextSharp.text.Paragraph("\n"));

                // Creamos un cuadro para la nota
                PdfPTable cuadroNota = new PdfPTable(1);
                cuadroNota.WidthPercentage = 100f;
                cuadroNota.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
                cuadroNota.DefaultCell.BorderColor = BaseColor.BLACK;

                PdfPCell celdaNota = new PdfPCell(new Phrase("NOTA: SE COBRARÁ EL 10% DE RECARGOS DESPUÉS DE LOS TRES DÍAS DE\nESTIPULADO DESPUÉS DE SU PAGO. Y EN CASO DE QUE RESCINDA EL\nCONTRATO SE LE DEVOLVERÁ EL 25 % DE ACUERDO COMO LO FUE PAGANDO.", contenidoFuente));
                celdaNota.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cuadroNota.AddCell(celdaNota);

                // Agregamos el cuadro de la nota al documento
                doc.Add(cuadroNota);

                // Cerramos el documento
                doc.Close();
            }
        }

    }
}
