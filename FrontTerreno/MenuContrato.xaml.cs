using System;
using System.Collections.Generic;
using System.Windows;

namespace FrontTerreno
{
    public partial class MenuContrato : Window
    {
        private Dictionary<Type, Window> ventanasAbiertas;

        public MenuContrato()
        {
            InitializeComponent();
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_crearContrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(CrearContrato));

        private void Btn_modificarContrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarContrato));

        private void Btn_buscarContrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(BuscarContrato));

        private void Btn_listaContrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ListaContratos));

        private void Btn_eliminarContrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarContrato));

        private void AbrirVentana(Type tipoVentana)
        {
            if (!ventanasAbiertas.ContainsKey(tipoVentana))
            {
                Window ventanaNueva = Activator.CreateInstance(tipoVentana) as Window;

                if (ventanaNueva != null)
                {
                    ventanaNueva.Closed += VentanaCerrada;
                    ventanaNueva.Show();
                    ventanasAbiertas[tipoVentana] = ventanaNueva;
                }
            }
            else
            {
                Window ventanaExistente = ventanasAbiertas[tipoVentana];
                ventanaExistente.Activate();
            }
        }

        private void VentanaCerrada(object sender, EventArgs e)
        {
            Type tipoVentana = sender.GetType();
            ventanasAbiertas.Remove(tipoVentana);
        }

        private void MenuContrato_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Closed -= VentanaCerrada;
                ventana.Close();
            }
        }
    }
}
