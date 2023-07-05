using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class TipoPagoDAO
    {
        public static List<TipoPago> ListarTipoPago()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                return DBConexion.TipoPago.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}