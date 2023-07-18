using ServicioTerreno.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServicioTerreno
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        //Registrar
        [OperationContract]
        Boolean RegistrarPredio(Predio predio);
        [OperationContract]
        Boolean RegistrarManzana(Manzana manzana);
        [OperationContract]
        Boolean RegistrarLote(Lote lote);
        [OperationContract]
        int? RegistrarContrato(Contrato contrato);
        [OperationContract]
        Boolean RegistrarPago(Pago pago);
        [OperationContract]
        Boolean RegistrarPersona(Persona persona);

        //Modificar
        [OperationContract]
        Boolean ModificarPredio(Predio predio);
        [OperationContract]
        Boolean ModificarManzana(Manzana manzana);
        [OperationContract]
        Boolean ModificarLote(Lote lote);
        [OperationContract]
        Boolean ModificarContratoLote(int IdContrato, int IdLote);
        [OperationContract]
        Boolean ModificarContrato(Contrato contrato);
        [OperationContract]
        Boolean ModificarPago(Pago pago);
        [OperationContract]
        Boolean ModificarPersona(Persona persona);

        //Eliminar
        [OperationContract]
        Boolean EliminarPredio(int idPredio);
        [OperationContract]
        Boolean EliminarManzana(int idManzana);
        [OperationContract]
        Boolean EliminarLote(int idLote);
        [OperationContract]
        Boolean EliminarContrato(int idContrato);
        [OperationContract]
        Boolean EliminarPago(int idPago);
        [OperationContract]
        Boolean EliminarPersona(int idPersona);
        [OperationContract]
        Boolean EliminarLoteContrato(int idLote);

        //Consultar
        [OperationContract]
        List<Predio> ListarPredio();
        [OperationContract]
        List<Manzana> ListarManzana(int IdPredio);
        [OperationContract]
        List<Lote> ListarLoteManzana(int IdManzana);
        [OperationContract]
        List<Lote> ListarLoteContrato(int IdContrato);
        [OperationContract]
        List<Contrato> ListarContrato();
        [OperationContract]
        Contrato BuscarContrato(int IdContrato);
        [OperationContract]
        List<Pago> ListarPago();
        [OperationContract]
        List<Pago> ListarPagoContrato(int IdContrato);
        [OperationContract]
        List<Persona> ListarPersona();
        [OperationContract]
        List<TipoPago> ListarTipoPago();
        [OperationContract]
        List<Terreno> ListaTerrenos();
        [OperationContract]
        List<TipoFecha> ListarTipoFecha();
        [OperationContract]
        List<ContratoPersona> ListarContratoPersona();
        [OperationContract]
        List<Terreno> ListarTerrenoContrato(int IdContrato);
        [OperationContract]
        List<ContratoUnion> ListaContratoUnion();

        //Login 
        [OperationContract]
        Usuario Login(string usuario, string contraseña);   
    }
}
