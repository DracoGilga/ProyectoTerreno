using ServicioTerreno.Model;
using System;
using System.Collections.Generic;

namespace ServicioTerreno
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public Contrato BuscarContrato(int IdContrato)
        {
            return Model.DAO.ContratoDAO.BuscarContrato(IdContrato);
        }

        public Boolean EliminarContrato(int idContrato)
        {
            return Model.DAO.ContratoDAO.EliminarContrato(idContrato);
        }

        public Boolean EliminarLote(int idLote)
        {
            return Model.DAO.LoteDAO.EliminarLote(idLote);
        }

        public Boolean EliminarManzana(int idManzana)
        {
            return Model.DAO.ManzanaDAO.EliminarManzana(idManzana);
        }

        public Boolean EliminarPago(int idPago)
        {
            return Model.DAO.PagoDAO.EliminarPago(idPago);
        }

        public Boolean EliminarPersona(int idPersona)
        {
            return Model.DAO.PersonaDAO.EliminarPersona(idPersona);
        }

        public Boolean EliminarPredio(int idPredio)
        {
            return Model.DAO.PredioDAO.EliminarPredio(idPredio);
        }

        public List<Contrato> ListarContrato()
        {
            return Model.DAO.ContratoDAO.ConsultarContrato();
        }

        public List<Lote> ListarLoteContrato(int IdContrato)
        {
            return Model.DAO.LoteDAO.ConsultarLoteContrato(IdContrato);
        }

        public List<Lote> ListarLoteManzana(int IdManzana)
        {
            return Model.DAO.LoteDAO.ConsultarLoteManzana(IdManzana);
        }

        public List<Manzana> ListarManzana(int IdPredio)
        {
            return Model.DAO.ManzanaDAO.ConsultarManzana(IdPredio);
        }

        public List<Pago> ListarPago()
        {
            return Model.DAO.PagoDAO.ListarPago();
        }

        public List<Pago> ListarPagoContrato(int IdContrato)
        {
            return Model.DAO.PagoDAO.BuscarPagoContrato(IdContrato);
        }

        public List<Persona> ListarPersona()
        {
            return Model.DAO.PersonaDAO.ObtenerPersonas();
        }

        public List<Predio> ListarPredio()
        {
            return Model.DAO.PredioDAO.ConsultarPredio();
        }

        public List<TipoPago> ListarTipoPago()
        {
            return Model.DAO.TipoPagoDAO.ConsultarTipoPago();
        }

        public Usuario Login(string usuario, string contraseña)
        {
            return Model.DAO.UsuarioDAO.Login(usuario, contraseña);
        }

        public Boolean ModificarContrato(Contrato contrato)
        {
            return Model.DAO.ContratoDAO.ModificarContrato(contrato);
        }

        public Boolean ModificarContratoLote(int IdContrato)
        {
            return Model.DAO.LoteDAO.ModificarContratosLote(IdContrato);
        }
        public Boolean ModificarLote(Lote lote)
        {
            return Model.DAO.LoteDAO.ModificarLote(lote);
        }
        public Boolean ModificarManzana(Manzana manzana)
        {
            return Model.DAO.ManzanaDAO.ModificarManzana(manzana);
        }

        public Boolean ModificarPago(Pago pago)
        {
            return Model.DAO.PagoDAO.ModificarPago(pago);
        }

        public Boolean ModificarPersona(Persona persona)
        {
            return Model.DAO.PersonaDAO.ModificarPersona(persona);
        }

        public Boolean ModificarPredio(Predio predio)
        {
            return Model.DAO.PredioDAO.ModificarPredio(predio);
        }

        public Boolean RegistrarContrato(Contrato contrato)
        {
            return Model.DAO.ContratoDAO.RegistrarContrato(contrato);
        }

        public Boolean RegistrarLote(Lote lote)
        {
            return Model.DAO.LoteDAO.RegistrarLote(lote);
        }

        public Boolean RegistrarManzana(Manzana manzana)
        {
            return Model.DAO.ManzanaDAO.RegistrarManzana(manzana);
        }

        public Boolean RegistrarPago(Pago pago)
        {
            return Model.DAO.PagoDAO.RegistrarPago(pago);
        }

        public Boolean RegistrarPersona(Persona persona)
        {
            return Model.DAO.PersonaDAO.RegistrarPersona(persona);
        }

        public Boolean RegistrarPredio(Predio predio)
        {
            return Model.DAO.PredioDAO.RegistrarPredio(predio);
        }
    }
}
