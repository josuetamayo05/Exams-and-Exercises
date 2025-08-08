using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace ArbolSuma
{
    public interface IArbol<T>
    {
        T Valor { get; } // Valor del nodo
        IEnumerable<IArbol<T>> Hijos { get; } // Hijos
        IEnumerable<T> PostOrden(); // Recorrido PostOrden
        IEnumerable<T> PreOrden(); // Recorrido PreOrden
    }
    public interface IArbolSuma : IArbol<int>
    {
        void InsertarPreOrden(int k, IArbolSuma nuevo);
        void InsertarPostOrden(int k, IArbolSuma nuevo);
        void InsertarKHoja(int k, IArbolSuma nuevo);
    }

    public class ArbolSuma : IArbolSuma
    {
        public int Valor{ get; set; }
        public List<IArbolSuma> Child { get; set; }
        public ArbolSuma(params int[] values)
        {
            Child = new List<IArbolSuma>();
            if (values.Length > 1)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Child.Add(new ArbolSuma(values[i]));
                }
            }
            Valor = values.Sum();
        }

        public IEnumerable<int> PreOrden()
        {
            yield return this.Valor;
            foreach (var child in Child)
            {
                foreach (var val in child.PreOrden())
                {
                    yield return val;
                }
            }
        }
        public IEnumerable<int> PostOrden()
        {
            foreach (var child in Child)
            {
                foreach (var val in child.PostOrden())
                {
                    yield return val;
                }
            }
            yield return this.Valor;
        }

        

        private bool IsLeef(IArbolSuma arbol) => arbol.Hijos == null;

        public IEnumerable<IArbol<int>> Hijos => Child.Cast<IArbol<int>>();

        public void InsertarPreOrden(int k, IArbolSuma nuevo)
        {
            var nodos = GetPreOrden().ToList();
            if (k >= 0 && k < nodos.Count)
            {
                var nodAct = nodos[k];
                (nodAct as ArbolSuma).InsertarHijo(nuevo);
                ActualizarValores();
            }
        }

        public void InsertarPostOrden(int k, IArbolSuma nuevo)
        {
            var nodos = GetPostOrden().ToList();
            if (k >= 0 && k < nodos.Count)
            {
                var actual = nodos[k];
                (actual as ArbolSuma).InsertarHijo(nuevo);
                ActualizarValores();
            } 
        }

        public IEnumerable<IArbolSuma> GetPostOrden()
        {
            foreach (var child in Child)
            {
                foreach (var val in (child as ArbolSuma).GetPostOrden())
                {
                    yield return val;
                }
            }
            yield return this;
        }


        public IEnumerable<IArbolSuma> GetPreOrden()
        {
            yield return this;
            foreach (var child in Child)
            {
                foreach (var val in (child as ArbolSuma).GetPreOrden())
                    yield return val;
            }
        }
        public void InsertarKHoja(int k, IArbolSuma nuevo)
        {
            var nodos = ObtenerHojas().ToList();
            if (k >= 0 && k < nodos.Count)
            {
                var nodoAct = nodos[k];
                (nodoAct as ArbolSuma).InsertarHijo(nuevo);
                ActualizarValores();
            }
        }
        private IEnumerable<IArbolSuma> ObtenerHojas()
        {
            if (!Child.Any())
                yield return this;
            else
            {
                foreach (var child in Child)
                {
                    foreach (var hoja in (child as ArbolSuma).ObtenerHojas())
                    {
                        yield return hoja;
                    }
                }
            }
        }

        public void InsertarHijo(IArbolSuma nuevo) => Child.Add(nuevo);
        private void ActualizarValores()
        {
            if (Child.Any())
            {
                Valor = Child.Sum(x => x.Valor);
            }
        }

        public void Imprimir(string indent = "", bool last = false)
        {
            Console.Write(indent);
            Console.Write("| ");
            indent += "  ";

            Console.WriteLine(Valor);

            for (int i = 0; i < Hijos.Count(); i++)
            {
                (Hijos.ElementAt(i) as ArbolSuma).Imprimir(indent, i == Hijos.Count() - 1);
            }
        }
    }
}