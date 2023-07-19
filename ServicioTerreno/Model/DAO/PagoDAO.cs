using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class PagoDAO
    {
        public static Boolean RegistrarPago(Pago pago)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                var consulta = new Pago()
                {
                    FechaPago = pago.FechaPago,
                    CantidadPago = pago.CantidadPago,
                    IdContrato = pago.IdContrato,
                    IdTipoPago = pago.IdTipoPago,
                    SerialPago = pago.SerialPago
                };
                DBConexion.Pago.InsertOnSubmit(consulta);
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
                pagoModificar.SerialPago = pago.SerialPago;

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
                List<Pago> pagos = new List<Pago>();
                IQueryable<Pago> consulta = DBConexion.Pago;
                if(consulta != null)
                {
                    foreach (Pago pago in consulta)
                    {
                        pagos.Add(new Pago()
                        {
                            IdPago = pago.IdPago,
                            FechaPago = pago.FechaPago,
                            CantidadPago = pago.CantidadPago,
                            IdContrato = pago.IdContrato,
                            IdTipoPago = pago.IdTipoPago,
                            SerialPago = pago.SerialPago
                        });
                    }
                    return pagos;
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
        public static List<PagosUnion> ListaPagoUnion()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<PagosUnion> resultado = new List<PagosUnion>();

                var pagos = (
                    from p in DBConexion.Pago
                    join c in DBConexion.Contrato on p.IdContrato equals c.IdContrato
                    join tp in DBConexion.TipoPago on p.IdTipoPago equals tp.IdTipoPago
                    join pe in DBConexion.Persona on c.IdCliente equals pe.IdPersona
                    select new 
                    {
                        TipoPago = tp,
                        Pago = p,
                        Contrato = c,
                        Persona = pe
                    }
                ).ToList();

                if(pagos != null)
                {
                    foreach( var iteracion in pagos )
                    {
                        resultado.Add(new PagosUnion()
                        {
                            TipoPago = new TipoPago()
                            {   
                                IdTipoPago = iteracion.TipoPago.IdTipoPago,
                                Descripcion = iteracion.TipoPago.Descripcion
                            },
                            Pago = new Pago()
                            {
                                IdPago = iteracion.Pago.IdPago,
                                FechaPago = iteracion.Pago.FechaPago,
                                CantidadPago = iteracion.Pago.CantidadPago,
                                IdContrato = iteracion.Pago.IdContrato,
                                IdTipoPago = iteracion.Pago.IdTipoPago,
                                SerialPago = iteracion.Pago.SerialPago
                            },
                            Contrato = new Contrato()
                            {
                                IdContrato = iteracion.Contrato.IdContrato,
                                IdCliente = iteracion.Contrato.IdCliente,
                            },
                            Persona = new Persona()
                            {
                                IdPersona = iteracion.Persona.IdPersona,
                                Nombre = iteracion.Persona.Nombre,
                                ApellidoPaterno = iteracion.Persona.ApellidoPaterno,
                                ApellidoMaterno = iteracion.Persona.ApellidoMaterno,
                            }
                        });
                    }
                    return resultado;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static List<Pago> BuscarPagoContrato(int IdContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Pago> pagos = new List<Pago>();
                IQueryable<Pago> consulta = DBConexion.Pago.Where(p => p.IdContrato == IdContrato);
                if (consulta != null)
                {
                    foreach (Pago pago in consulta)
                    {
                        pagos.Add(new Pago()
                        {
                            IdPago = pago.IdPago,
                            FechaPago = pago.FechaPago,
                            CantidadPago = pago.CantidadPago,
                            IdContrato = pago.IdContrato,
                            IdTipoPago = pago.IdTipoPago,
                            SerialPago = pago.SerialPago
                        });
                    }
                    return pagos;
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
        internal static Pago BuscarPago(string folio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                IQueryable<Pago> consulta = DBConexion.Pago.Where(p => p.SerialPago == folio);
                if (consulta.Count() > 0)
                {
                    Pago contrato = consulta.First();
                    return new Pago()
                    {
                        IdPago = contrato.IdPago,
                        FechaPago = contrato.FechaPago,
                        CantidadPago = contrato.CantidadPago,
                        IdContrato = contrato.IdContrato,
                        IdTipoPago = contrato.IdTipoPago,
                        SerialPago = contrato.SerialPago
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

        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}