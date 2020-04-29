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
        public bool ProductoNuevo
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
            return $"COMPUTADORA {Marca}-{Modelo} {(int)RAM} RAM - {Fabricante}";
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

        public List<Monitor> Monitores = new List<Monitor>();
        public List<Computadora> Computadoras = new List<Computadora>();

        public delegate void ProductoAgregadoEliminadoHandler(ProductoAgregadoEliminadoArgs contexto);
        public event ProductoAgregadoEliminadoHandler ProductoAgregadoEliminado;

        public void AgregarProducto(Monitor producto)
        {
            this.Monitores.Add(producto);

            ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(producto));
        }

        public void AgregarProducto(Computadora producto)
        {
            this.Computadoras.Add(producto);

            ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(producto));
        }

        public void EliminarProducto(string identificador)
        {
            var monitor = Monitores.FirstOrDefault(x => x.Identificador == identificador);
            if (monitor != null)
            {
                this.Monitores.Remove(monitor);
                ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(monitor));
            }

            var computadora = Computadoras.FirstOrDefault(x => x.Identificador == identificador);
            if (computadora != null)
            {
                this.Computadoras.Remove(computadora);
                ProductoAgregadoEliminado(new ProductoAgregadoEliminadoArgs(computadora));
            }            

            //TODO > Refactorizar en clase 
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            productos.AddRange(this.Monitores);
            productos.AddRange(this.Computadoras);

            //TODO > Explicar predicado
            return productos.OrderByDescending(x => x is Computadora).ToList();
        }
    }

    public class ProductoAgregadoEliminadoArgs : EventArgs
    {
        //TODO > Que hay de raro aca? esto escala a futuro?
        public ProductoAgregadoEliminadoArgs(Monitor producto)
        {
            Tipo = "MONITOR";            
            Producto = producto;
        }

        public ProductoAgregadoEliminadoArgs(Computadora producto)
        {
            Tipo = "COMPUTADORA";            
            Producto = producto;
        }

        public Producto Producto { get; set; }
        public string Tipo { get; set; }        
    }
}