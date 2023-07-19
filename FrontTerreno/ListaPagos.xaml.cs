using FrontTerreno.Modelo;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para ListaPagos.xaml
    /// </summary>
    public partial class ListaPagos : Window
    {
        PagoViewModel pagoViewModel = new PagoViewModel();
        public ListaPagos()
        {
            InitializeComponent();
            MostrarPagos();
            Dp_fechaInicio.SelectedDate = DateTime.Now;
            Dp_fechaFin.SelectedDate = DateTime.Now;
        }
        private async void MostrarPagos()
        {
            listaPagos.ItemsSource = null;

            List<PagosUnion> pagos = await pagoViewModel.ListaPagosUnidos();
            listaPagos.ItemsSource = pagos;
        }
        private async void Btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaInicio = Dp_fechaInicio.SelectedDate ?? DateTime.MinValue;
            DateTime fechaFin = Dp_fechaFin.SelectedDate ?? DateTime.MaxValue;

            MostrarPagos();

            // Esperar a que se carguen los pagos nuevamente
            await Task.Delay(100);

            List<PagosUnion> pagos = listaPagos.ItemsSource as List<PagosUnion>;
            pagos = pagos.Where(p => p.Pago.FechaPago >= fechaInicio && p.Pago.FechaPago <= fechaFin).ToList();
            listaPagos.ItemsSource = pagos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Crear el documento
            Document document = new Document(PageSize.A4);

            try
            {
                // Especificar la ruta y el nombre de archivo para guardar el PDF
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(folderPath, "PDF", "Reporte_de_pagos.pdf");

                // Crear el directorio si no existe
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                // Crear el escritor PDF
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                // Abrir el documento
                document.Open();

                // Agregar título al documento
                Font titleFont = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);
                Paragraph title = new Paragraph("Reporte de Pagos", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20;
                document.Add(title);

                // Agregar fechas al documento
                DateTime fechaInicio = Dp_fechaInicio.SelectedDate ?? DateTime.MinValue;
                DateTime fechaFin = Dp_fechaFin.SelectedDate ?? DateTime.MaxValue;
                Font dateFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
                Paragraph dateRange = new Paragraph($"Fecha inicio: {fechaInicio.ToShortDateString()} - Fecha fin: {fechaFin.ToShortDateString()}", dateFont);
                dateRange.Alignment = Element.ALIGN_CENTER;
                dateRange.SpacingAfter = 10;
                document.Add(dateRange);

                // Agregar tabla al documento
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });

                // Agregar encabezados de columna
                table.AddCell("Folio");
                table.AddCell("Nombre");
                table.AddCell("Apellido Paterno");
                table.AddCell("Apellido Materno");
                table.AddCell("Fecha de pago");
                table.AddCell("Tipo de pago");
                table.AddCell("Cantidad");

                // Obtener los pagos filtrados
                List<PagosUnion> pagos = listaPagos.ItemsSource as List<PagosUnion>;
                pagos = pagos.Where(p => p.Pago.FechaPago >= fechaInicio && p.Pago.FechaPago <= fechaFin).ToList();

                // Variables para la suma de cantidades
                double totalAmount = 0;

                // Agregar datos de los pagos a la tabla y sumar las cantidades
                foreach (PagosUnion pago in pagos)
                {
                    table.AddCell(pago.Pago.SerialPago);
                    table.AddCell(pago.Persona.Nombre);
                    table.AddCell(pago.Persona.ApellidoPaterno);
                    table.AddCell(pago.Persona.ApellidoMaterno);
                    table.AddCell(pago.Pago.FechaPago.ToShortDateString());
                    table.AddCell(pago.TipoPago.Descripcion);
                    table.AddCell(pago.Pago.CantidadPago.ToString());

                    totalAmount += pago.Pago.CantidadPago;
                }

                // Agregar la tabla al documento
                document.Add(table);

                // Agregar el texto de "Cantidad Total: $"
                Font totalFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.RED);
                Paragraph totalParagraph = new Paragraph($"Cantidad Total: ${totalAmount}", totalFont);
                totalParagraph.Alignment = Element.ALIGN_RIGHT;
                totalParagraph.SpacingBefore = 10;
                document.Add(totalParagraph);

                // Cerrar el documento
                document.Close();
                MessageBox.Show("PDF generado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }
    }
}
