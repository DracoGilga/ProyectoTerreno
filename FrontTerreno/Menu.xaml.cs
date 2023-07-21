using System;
using System.Collections.Generic;
using System.Windows;

namespace FrontTerreno
{
    public partial class Menu : Window
    {
        private Dictionary<Type, Window> ventanasAbiertas;

        public Menu()
        {
            InitializeComponent();
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_contrato(object sender, RoutedEventArgs e) => AbrirVentana(typeof(MenuContrato));
        private void Btn_persona(object sender, RoutedEventArgs e) => AbrirVentana(typeof(MenuPersona));
        private void Btn_pago(object sender, RoutedEventArgs e) => AbrirVentana(typeof(MenuPagos));
        private void Btn_terrenos(object sender, RoutedEventArgs e) => AbrirVentana(typeof(MenuTerrenos));
        private void AbrirVentana(Type tipoVentana)
        {
            if (ventanasAbiertas.ContainsKey(tipoVentana))
            {
                Window ventanaExistente = ventanasAbiertas[tipoVentana];
                ventanaExistente.Activate();
                return;
            }

            Window ventanaNueva = Activator.CreateInstance(tipoVentana) as Window;

            if (ventanaNueva != null)
            {
                ventanaNueva.Closed += VentanaCerrada;
                ventanaNueva.Show();
                ventanasAbiertas.Add(tipoVentana, ventanaNueva);
            }
        }
        private void VentanaCerrada(object sender, EventArgs e)
        {
            Type tipoVentana = sender.GetType();
            if (ventanasAbiertas.ContainsKey(tipoVentana))
                ventanasAbiertas.Remove(tipoVentana);
        }
        private void Menu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Closed -= VentanaCerrada;
                ventana.Close();
            }
        }
    }
}
