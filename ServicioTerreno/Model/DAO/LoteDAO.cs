using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class LoteDAO
    {
        public static Boolean RegistrarLote(Lote lote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Lote.InsertOnSubmit(lote);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarLote(Lote lote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteModificar = DBConexion.Lote.Where(p => p.IdLote == lote.IdLote).First();
                loteModificar.NoLote = lote.NoLote;
                loteModificar.Superficie = lote.Superficie;
                loteModificar.IdContrato = lote.IdContrato;

                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarContratosLote(int IdContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Lote> loteModificar = DBConexion.Lote.Where(p => p.IdContrato == IdContrato).ToList();
                foreach (Lote lote in loteModificar)
                {
                    lote.IdContrato = null;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarLote(int idLote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteEliminar = DBConexion.Lote.Where(p => p.IdLote == idLote).First();
                DBConexion.Lote.DeleteOnSubmit(loteEliminar);
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