using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class ContratoDAO
    {
        public static Boolean RegistrarContrato(Contrato contrato)
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
        public static Boolean ModificarContrato(Contrato contrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Contrato contratoModificar = DBConexion.Contrato.Where(p => p.IdContrato == contrato.IdContrato).First();
                contratoModificar.IdTipoPago = contrato.IdTipoPago;
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
                IQueryable<Contrato> consulta = DBConexion.Contrato.Where(p => p.IdContrato == idContrato);
                if(consulta.Count() > 0)
                {
                    Contrato contrato = consulta.First();
                    return new Contrato()
                    {
                        IdContrato = contrato.IdContrato,
                        IdTipoPago = contrato.IdTipoPago,
                        Costo = contrato.Costo,
                        IdCliente = contrato.IdCliente,
                        Testigo1 = contrato.Testigo1,
                        Testigo2 = contrato.Testigo2,
                        FechaContrato = contrato.FechaContrato,
                        TipoFecha = contrato.TipoFecha
                    };
                }
                else
                {
                    return null;
                }               
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Contrato> ConsultarContrato()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Contrato> contratos = new List<Contrato>();
                IQueryable<Contrato> consulta = DBConexion.Contrato;
                if(consulta != null)
                {
                    foreach (Contrato contrato in consulta)
                    {
                        contratos.Add(new Contrato()
                        {
                            IdContrato = contrato.IdContrato,
                            IdTipoPago = contrato.IdTipoPago,
                            Costo = contrato.Costo,
                            IdCliente = contrato.IdCliente,
                            Testigo1 = contrato.Testigo1,
                            Testigo2 = contrato.Testigo2,
                            FechaContrato = contrato.FechaContrato,
                            TipoFecha = contrato.TipoFecha
                        });
                    }
                    return contratos;
                }
                else
                {
                    return null;
                }
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