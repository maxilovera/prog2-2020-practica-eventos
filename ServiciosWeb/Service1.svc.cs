using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Logica;

namespace ServiciosWeb
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IServicioGenerico
    {        
        public List<ClienteParaCompartir> GetClientes(string nombre)
        {
            return new ClientesLogica()
                .BuscarClientes(nombre)
                .Select(x=> new ClienteParaCompartir()
            {
                Nombre = x.Nombre,
                Apellido = "No tiene"
            }).ToList();
        }
    }

    [DataContract]
    public class ClienteParaCompartir
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
    }
}
