using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class PagoDAO
    {
        public static Boolean RegistrarPago(Pago pago)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Pago.InsertOnSubmit(pago);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarPago(Pago pago)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Pago pagoModificar = DBConexion.Pago.Where(p => p.IdPago == pago.IdPago).First();
                pagoModificar.FechaPago = pago.FechaPago;
                pagoModificar.CantidadPago = pago.CantidadPago;
                pagoModificar.IdContrato = pago.IdContrato;
                pagoModificar.IdTipoPago = pago.IdTipoPago;

                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarPago(int idPago)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Pago pagoEliminar = DBConexion.Pago.Where(p => p.IdPago == idPago).First();
                DBConexion.Pago.DeleteOnSubmit(pagoEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Pago> ListarPago()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                return DBConexion.Pago.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Pago BuscarPago(int idPago)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                return DBConexion.Pago.Where(p => p.IdPago == idPago).First();
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