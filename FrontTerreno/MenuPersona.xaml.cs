using System;
using System.Collections.Generic;
using System.Windows;

namespace FrontTerreno
{
    public partial class MenuPersona : Window
    {
        private Dictionary<Type, Window> ventanasAbiertas;

        public MenuPersona()
        {
            InitializeComponent();
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_AgregarPersona(object sender, RoutedEventArgs e) => AbrirVentana(typeof(AgregarPersona));

        private void Btn_ModificarPersona(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarPersona));

        private void Btn_listaPersona(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ListaPersona));

        private void Btn_eliminarPersona(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarPersona));

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

        private void MenuPersona_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Closed -= VentanaCerrada;
                ventana.Close();
            }
        }
    }
}
