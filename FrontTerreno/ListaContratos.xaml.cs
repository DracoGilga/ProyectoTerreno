using FrontTerreno.Modelo;
using ServiceReference1;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para ListaContratos.xaml
    /// </summary>
    public partial class ListaContratos : Window
    {
        ContratoViewModel contratoViewModel = new ContratoViewModel();
        public ListaContratos()
        {
            InitializeComponent();
            MostrarTabla();
        }

        private async void MostrarTabla()
        {
            List<ContratoUnion> contratos = await contratoViewModel.DesplegarListaContratos();
            foreach (var item in contratos)
            {
                // Obtener el primer terreno de la lista
                Terreno primerTerreno = item.Terreno.FirstOrDefault();

                var contratoLista = new
                {
                    item.Contrato.IdContrato,
                    item.Persona.Nombre,
                    Apellido = item.Persona.ApellidoPaterno + " " + item.Persona.ApellidoMaterno,
                    NombrePredio = primerTerreno?.Nombre,
                    primerTerreno?.NoManzana,
                    NoLote = string.Join(", ", item.Terreno.Select(t => t.NoLote)),
                    item.Contrato.FechaContrato,
                    TipoFecha = item.Descripcion,
                    item.Contrato.Costo,
                    Pagado = item.Saldo,
                    Faltante = item.Contrato.Costo - item.Saldo,
                    item.Contrato.IdTipoFecha
                };
                tablaContrato.Items.Add(contratoLista);
            }
        }
        private void AlertaRetraso()
        {
            //implementacion en la segunda version  del proyecto
        }
    }
}
