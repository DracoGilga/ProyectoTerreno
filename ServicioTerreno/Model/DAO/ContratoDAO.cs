using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class ContratoDAO
    {
        public static int? RegistrarContrato(Contrato contratoNuevo)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                var contrato = new Contrato()
                {
                    TipoPago = contratoNuevo.TipoPago,
                    Costo = contratoNuevo.Costo,
                    IdCliente = contratoNuevo.IdCliente,
                    Testigo1 = contratoNuevo.Testigo1,
                    Testigo2 = contratoNuevo.Testigo2,
                    FechaContrato = contratoNuevo.FechaContrato,
                    IdTipoFecha = contratoNuevo.IdTipoFecha
                };
                DBConexion.Contrato.InsertOnSubmit(contrato);
                DBConexion.SubmitChanges();
                return contrato.IdContrato; // Suponiendo que la propiedad del ID se llame "IdContrato"
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Boolean ModificarContrato(Contrato contrato)
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
                contratoModificar.IdTipoFecha = contrato.IdTipoFecha;

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
                        TipoPago = contrato.TipoPago,
                        Costo = contrato.Costo,
                        IdCliente = contrato.IdCliente,
                        Testigo1 = contrato.Testigo1,
                        Testigo2 = contrato.Testigo2,
                        FechaContrato = contrato.FechaContrato,
                        IdTipoFecha = contrato.IdTipoFecha
                    };
                }
                else
                    return null; 
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
                            TipoPago = contrato.TipoPago,
                            Costo = contrato.Costo,
                            IdCliente = contrato.IdCliente,
                            Testigo1 = contrato.Testigo1,
                            Testigo2 = contrato.Testigo2,
                            FechaContrato = contrato.FechaContrato,
                            IdTipoFecha = contrato.IdTipoFecha
                        });
                    }
                    return contratos;
                }
                else
                    return null;
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