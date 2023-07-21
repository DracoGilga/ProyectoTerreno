using System;
using System.Collections.Generic;
using System.Windows;

namespace FrontTerreno
{
    public partial class MenuTerrenos : Window
    {
        private Dictionary<Type, Window> ventanasAbiertas;

        public MenuTerrenos()
        {
            InitializeComponent();
            ventanasAbiertas = new Dictionary<Type, Window>();
        }

        private void Btn_agregarPredio(object sender, RoutedEventArgs e) => AbrirVentana(typeof(AgregarPredio));
        private void Btn_modificarPredio(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarPredio));
        private void Btn_eliminarPredio(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarPredio));
        private void Btn_agregarManzana(object sender, RoutedEventArgs e) => AbrirVentana(typeof(AgregarManzana));
        private void Btn_modificarManzana(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarManzana));
        private void Btn_eliminarManzana(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarManzana));
        private void Btn_agregarLote(object sender, RoutedEventArgs e) => AbrirVentana(typeof(AgregarLote));
        private void Btn_modificarLote(object sender, RoutedEventArgs e) => AbrirVentana(typeof(ModificarLote));
        private void Btn_eliminarLote(object sender, RoutedEventArgs e) => AbrirVentana(typeof(EliminarLote));

        private void Btn_listaTerrenos(object sender, RoutedEventArgs e)
        {
            if (!ventanasAbiertas.ContainsKey(typeof(ListaTerrenos)))
            {
                ListaTerrenos listaTerrenos = new ListaTerrenos();
                listaTerrenos.Closed += VentanaCerrada;
                listaTerrenos.Show();
                ventanasAbiertas[typeof(ListaTerrenos)] = listaTerrenos;
            }
            else
            {
                Window ventanaExistente = ventanasAbiertas[typeof(ListaTerrenos)];
                ventanaExistente.Activate();
            }
        }

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

        private void MenuTerrenos_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var ventana in ventanasAbiertas.Values)
            {
                ventana.Closed -= VentanaCerrada;
                ventana.Close();
            }
        }
    }
}