using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class ContratoDAO
    {
        public Boolean RegistrarContrato(Contrato contrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Contrato.InsertOnSubmit(contrato);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Boolean ModificarContrato(Contrato contrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Contrato contratoModificar = DBConexion.Contrato.Where(p => p.IdContrato == contrato.IdContrato).First();
                contratoModificar.TipoPago = contrato.TipoPago;
                contratoModificar.Costo = contrato.Costo;
                contratoModificar.IdCliente = contrato.IdCliente;
                contratoModificar.Testigo1 = contrato.Testigo1;
                contratoModificar.Testigo2 = contrato.Testigo2;
                contratoModificar.FechaContrato = contrato.FechaContrato;
                contratoModificar.TipoFecha = contrato.TipoFecha;

                DBConexion.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarContrato(int idContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Contrato contratoEliminar = DBConexion.Contrato.Where(p => p.IdContrato == idContrato).First();
                DBConexion.Contrato.DeleteOnSubmit(contratoEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Contrato BuscarContrato(int idContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                return DBConexion.Contrato.Where(p => p.IdContrato == idContrato).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Contrato> ListarContrato()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                return DBConexion.Contrato.ToList();
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