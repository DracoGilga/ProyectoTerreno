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
        public static List<ContratoUnion> ConsultarUnionContrato()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<ContratoUnion> resultado = new List<ContratoUnion>();
                var contratos = (
                    from c in DBConexion.Contrato
                    join p in DBConexion.Persona on c.IdCliente equals p.IdPersona
                    join l in DBConexion.Lote on c.IdContrato equals l.IdContrato
                    join m in DBConexion.Manzana on l.IdManzana equals m.IdManzana
                    join pr in DBConexion.Predio on m.IdPredio equals pr.IdPredio
                    join tf in DBConexion.TipoFecha on c.IdTipoFecha equals tf.IdTipoFecha
                    join pa in DBConexion.Pago on c.IdContrato equals pa.IdContrato into pagos
                    select new
                    {
                        Descripcion = tf.Descripcion,
                        Contrato = c,
                        Persona = p,
                        Terreno = (
                            from lt in DBConexion.Lote
                            where lt.IdContrato == c.IdContrato
                            select new Terreno
                            {
                                Nombre = pr.Nombre,
                                Ubicacion = pr.Ubicacion,
                                NoManzana = m.NoManzana,
                                NoLote = lt.NoLote,
                                Superficie = (float)lt.Superficie,
                                IdContrato = lt.IdContrato,
                                IdLote = lt.IdLote
                            }
                        ).Distinct().ToList(),
                        Saldo = pagos.Sum(p => p.CantidadPago)
                    }
                ).ToList();

                if (contratos != null)
                {
                    foreach(var contrato in contratos)
                    {
                        if (contrato.Contrato.IdContrato != resultado.LastOrDefault()?.Contrato.IdContrato)
                        {
                            resultado.Add(new ContratoUnion()
                            {
                                Contrato = new Contrato()
                                {
                                    IdContrato = contrato.Contrato.IdContrato,
                                    TipoPago = contrato.Contrato.TipoPago,
                                    Costo = contrato.Contrato.Costo,
                                    IdCliente = contrato.Contrato.IdCliente,
                                    Testigo1 = contrato.Contrato.Testigo1,
                                    Testigo2 = contrato.Contrato.Testigo2,
                                    FechaContrato = contrato.Contrato.FechaContrato,
                                    IdTipoFecha = contrato.Contrato.IdTipoFecha,
                                },

                                Persona = new Persona()
                                {
                                    IdPersona = contrato.Persona.IdPersona,
                                    Nombre = contrato.Persona.Nombre,
                                    ApellidoPaterno = contrato.Persona.ApellidoPaterno,
                                    ApellidoMaterno = contrato.Persona.ApellidoMaterno,
                                    Direccion = contrato.Persona.Direccion,
                                    Telefono = contrato.Persona.Telefono
                                },
                                Terreno = contrato.Terreno,
                                Descripcion = contrato.Descripcion,
                                Saldo = contrato.Saldo
                            });
                        }
                        
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


        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}