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

            // Inicializa el diccionario de ventanas abiertas
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_contrato(object sender, RoutedEventArgs e)
        {
            AbrirVentana(typeof(MenuContrato));
        }

        private void Btn_persona(object sender, RoutedEventArgs e)
        {
            AbrirVentana(typeof(MenuPersona));
        }

        private void Btn_pago(object sender, RoutedEventArgs e)
        {
            AbrirVentana(typeof(MenuPagos));
        }

        private void AbrirVentana(Type tipoVentana)
        {
            // Verifica si la ventana ya está abierta
            if (ventanasAbiertas.ContainsKey(tipoVentana))
            {
                Window ventanaExistente = ventanasAbiertas[tipoVentana];
                ventanaExistente.Activate();
                return;
            }

            // Crea una nueva instancia de la ventana
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

            // Elimina la ventana cerrada del diccionario de ventanas abiertas
            if (ventanasAbiertas.ContainsKey(tipoVentana))
            {
                ventanasAbiertas.Remove(tipoVentana);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Cierra todas las ventanas abiertas al cerrar el menú principal
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Close();
            }
            Login login = new Login();
            login.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(typeof(MenuTerrenos));
        }
    }
}
