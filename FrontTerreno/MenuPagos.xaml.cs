using System;
using System.Collections.Generic;
using System.Windows;

namespace FrontTerreno
{
    public partial class MenuPagos : Window
    {
        private Dictionary<Type, Window> ventanasAbiertas;

        public MenuPagos()
        {
            InitializeComponent();
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_AgregarPago(object sender, RoutedEventArgs e) => AbrirVentana(typeof(AgregarPago));

        private void Btn_ModificarPago(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarPago));

        private void Btn_listaPago(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ListaPagos));

        private void Btn_eliminarPago(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarPago));

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

        private void MenuPagos_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Closed -= VentanaCerrada;
                ventana.Close();
            }
        }
    }
}
