using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Recursos
{
    internal class ConvertidorPesosMexicanos
    {
        private static readonly string[] unidades = { "", "un", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
        private static readonly string[] decenas = { "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
        private static readonly string[] veintenas = { "", "", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
        private static readonly string[] centenas = { "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };

        public static string ConvertirNumeroAPesos(double numero)
        {
            if (numero == 0)
                return "cero pesos mexicanos";

            if (numero >= 1000000)
                return "El número ingresado excede el límite de un millón de pesos mexicanos.";

            int enteros = (int)numero;
            int decimales = (int)((numero - enteros) * 100);

            string resultado = "";

            if (enteros > 0)
            {
                resultado += ConvertirParteEntera(enteros);
                resultado += " Mxn";
            }

            if (decimales > 0)
            {
                resultado += " con ";
                resultado += ConvertirParteDecimal(decimales);
                resultado += " centavos";
            }

            return resultado;
        }

        private static string ConvertirParteEntera(int numero)
        {
            if (numero == 0)
                return "";

            string parteEntera = "";

            if (numero >= 1000)
            {
                parteEntera += ConvertirParteEntera(numero / 1000) + "mil ";
                numero %= 1000;
            }

            if (numero >= 100)
            {
                if (numero == 100)
                    parteEntera += "cien ";
                else
                    parteEntera += centenas[numero / 100] + " ";

                numero %= 100;
            }

            if (numero >= 20)
            {
                parteEntera += veintenas[numero / 10] + " ";
                numero %= 10;

                if (numero > 0)
                    parteEntera += unidades[numero] + " ";
            }
            else if (numero >= 10)
                parteEntera += decenas[numero - 10] + " ";
            else if (numero > 0)
                parteEntera += unidades[numero] + " ";

            return parteEntera;
        }

        private static string ConvertirParteDecimal(int numero)
        {
            string parteDecimal = "";

            if (numero >= 20)
            {
                parteDecimal += veintenas[numero / 10];
                parteDecimal += " ";
                numero %= 10;

                if (numero > 0)
                {
                    parteDecimal += unidades[numero];
                    parteDecimal += " ";
                }
            }
            else if (numero >= 10)
            {
                parteDecimal += decenas[numero - 10];
                parteDecimal += " ";
            }
            else if (numero > 0)
            {
                parteDecimal += unidades[numero];
                parteDecimal += " ";
            }

            return parteDecimal;
        }
    }
}
