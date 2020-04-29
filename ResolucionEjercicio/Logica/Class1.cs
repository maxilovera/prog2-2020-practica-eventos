using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{    
    public class Cliente
    {        
        public int Id { get; set; }        
        public string Nombre { get; set; }

        public int Sueldo { get; set; }
    }

    public class ClientesLogica 
    {
        public List<Cliente> BuscarClientes(string nombre)
        {
            return new List<Cliente>();
        }
    }
}
