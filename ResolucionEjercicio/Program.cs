using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResolucionEjercicio
{
    static class Validadores
    {
        static bool EsRAMCompatible_Op1(this int nro)
        {
            try
            {
                RAM ram = (RAM)nro;
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool EsRAMCompatible_Op2(this string valor)
        {
            return Enum.IsDefined(typeof(RAM), valor);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Deposito.Instance.ProductoAgregadoEliminado += Instance_ProductoAgregadoEliminado;

            Deposito.Instance.AgregarProducto(new Teclado() { Marca = "T1", Modelo = "Mod T1", NumeroSerie = "S001" });
            Console.ReadLine();

            Deposito.Instance.AgregarProducto(new Monitor() { Marca = "M1", Modelo = "Mod1", NumeroSerie = "001002", AñoFabricacion = 2019 });
            Console.ReadLine();
            Deposito.Instance.AgregarProducto(new Computadora() { Marca = "C2", Modelo = "Mod5", NumeroSerie = "009003", RAM = RAM.Cuatro, Fabricante = "F1", Procesador = "P1" });
            Console.ReadLine();
            Deposito.Instance.AgregarProducto(new Monitor() { Marca = "M2", Modelo = "Mod2", NumeroSerie = "001003", AñoFabricacion = 2020 });
            Console.ReadLine();
            Deposito.Instance.AgregarProducto(new Computadora() { Marca = "C1", Modelo = "Mod4", NumeroSerie = "009002", RAM = RAM.Cuatro, Fabricante = "F1", Procesador = "P1" });
            Console.ReadLine();
            Deposito.Instance.EliminarProducto("M1-Mod1-001002");
            Console.ReadLine();
            Deposito.Instance.AgregarProducto(new Monitor() { Marca = "M1", Modelo = "Mod3", NumeroSerie = "001004", AñoFabricacion = 2019, Pulgadas = 32 });
            Console.ReadLine();
            Deposito.Instance.AgregarProducto(new Monitor() { Marca = "M2", Modelo = "Mod1", NumeroSerie = "001005", AñoFabricacion = 2020, Pulgadas = 55 });
            Console.ReadLine();
            Deposito.Instance.EliminarProducto("C2-Mod5-009003");

            Console.WriteLine("Finalizado...");
            Console.ReadLine();
        }

        private static void Instance_ProductoAgregadoEliminado(ProductoAgregadoEliminadoArgs contexto)
        {
            Console.Clear();
            foreach (var item in Deposito.Instance.ObtenerProductos())
            {
                if (item.Identificador == contexto.Producto.Identificador)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine(item.ObtenerDescripcion());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void BuscarClientes(string nombre, string apellido, int dni)
        {

        }

        public void BuscarClientes(FiltroCliente filtro)
        {

        }        
    }

    class FiltroCliente
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
