using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResolucionEjercicio
{
    public abstract class Producto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string Identificador
        {
            get
            {
                return $"{Marca}-{Modelo}-{NumeroSerie}";
            }
        }

        public abstract string ObtenerDescripcion();
    }

    public sealed class Monitor : Producto
    {
        public int AñoFabricacion { get; set; }
        public bool EsProductoNuevo
        {
            get
            {
                return AñoFabricacion == DateTime.Today.Year;
            }
        }
        public int? Pulgadas { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"MONITOR {Marca}-{Modelo} {(Pulgadas.HasValue ? Pulgadas.Value.ToString() : string.Empty)}";
        }
    }

    public enum RAM
    {
        Dos = 2, Cuatro = 4, Ocho = 8, Dieciseis = 16
    }

    public sealed class Computadora : Producto
    {
        public string Procesador { get; set; }
        public RAM RAM { get; set; }
        public string Fabricante { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"COMPUTADORA {Marca}-{Modelo} {(int)RAM}GB RAM - {Fabricante}";
        }
    }

    public sealed class Teclado : Producto
    {
        public bool EsMecanico { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"TECLADO {Marca} {Modelo} - {EsMecanico}";
        }
    }

    /// <summary>
    /// Clase Deposito definida como Singleton
    /// </summary>
    public class Deposito
    {
        private static Deposito instance;

        private Deposito()
        {

        }

        public static Deposito Instance
        {
            get
            {
                if (instance == null)
                    instance = new Deposito();

                return instance;
            }
        }

        public static List<Monitor> Monitores = new List<Monitor>();
        public static List<Computadora> Computadoras = new List<Computadora>();
        public static List<Teclado> Teclados = new List<Teclado>();

        public delegate void ProductoAgregadoEliminadoHandler(ProductoAgregadoEliminadoArgs contexto);
        public event ProductoAgregadoEliminadoHandler ProductoAgregadoEliminado;

        public void AgregarProducto(Producto producto)
        {
            AgregarProductoInterno(producto, this.ObtenerProductos());
        }
        
        private void AgregarProductoInterno(Producto producto, List<Producto> lista)
        {
            lista.Add(producto);
            ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(producto));
        }

        public void EliminarProducto(string identificador)
        {            
            Eliminar(identificador, this.ObtenerProductos());            
        }

        private void Eliminar(string identificador, List<Producto> lista)
        {
            var producto = lista.FirstOrDefault(x => x.Identificador == identificador);
            if (producto != null)
            {
                lista.Remove(producto);
                ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(producto));
            }
        }        

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            
            productos.AddRange(Deposito.Monitores);
            productos.AddRange(Deposito.Computadoras);
            productos.AddRange(Deposito.Teclados);

            return productos.OrderByDescending(x => x is Computadora).ToList();            
        }
    }    

    public class ProductoAgregadoEliminadoArgs : EventArgs
    {
        //TODO > Que hay de raro aca? esto escala a futuro?
        public ProductoAgregadoEliminadoArgs(Producto producto)
        {
            Producto = producto;
        }
        
        public Producto Producto { get; set; }
        public string Tipo { get; set; }        
    }
}