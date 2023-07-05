using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class PredioDAO
    {
        public static Boolean RegistrarPredio(Predio predio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Predio.InsertOnSubmit(predio);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarPredio(Predio predio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Predio predioModificar = DBConexion.Predio.Where(p => p.IdPredio == predio.IdPredio).First();
                predioModificar.Nombre = predio.Nombre;
                predioModificar.Ubicacion = predio.Ubicacion;

                DBConexion.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }



        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}