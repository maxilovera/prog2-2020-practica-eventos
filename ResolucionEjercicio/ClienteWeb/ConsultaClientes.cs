using ClienteWeb.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWeb
{
    public class ConsultaClientes
    {
        public void Buscar()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

            ClienteParaCompartir[] resultado = client.GetClientes("maxi");

            foreach (var item in resultado)
            {
                //item.ap
            }
        }
    }
}
