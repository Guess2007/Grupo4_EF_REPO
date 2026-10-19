using System;
using System.Collections.Generic;

namespace Arbol_binario_de_busqueda
{
    public enum TipoOperacion
    {
        transferencia, pagadeservicios, retiro, deposito
    }

    class operaciones
    {
        public string cod_id { get; set; }
        public TipoOperacion tipoOperacion { get; set; }
        public double monto { get; set; }
        public string sede { get; set; }
    }

    class CNodo
    {
        public operaciones dato { get; set; }
        public CNodo izq { get; set; }
        public CNodo der { get; set; }
        public int altura { get; set; } = 1;

        public CNodo(operaciones dato)
        {
            this.dato = dato;
        }
    }

    class ArbolAVL
    {
        private CNodo raiz;

        public ArbolAVL() { raiz = null; }

        internal CNodo Raiz => raiz;

        private int Altura(CNodo n) => n == null ? 0 : n.altura;

        private int Balance(CNodo n) => n == null ? 0 : Altura(n.izq) - Altura(n.der);

        private void ActualizarAltura(CNodo n)
        {
            n.altura = 1 + Math.Max(Altura(n.izq), Altura(n.der));
        }

        private CNodo RotarDerecha(CNodo y)
        {
            CNodo x = y.izq;
            y.izq = x.der;
            x.der = y;

            ActualizarAltura(y);
            ActualizarAltura(x);
            return x;
        }

        private CNodo RotarIzquierda(CNodo x)
        {
            CNodo y = x.der;
            x.der = y.izq;
            y.izq = x;

            ActualizarAltura(x);
            ActualizarAltura(y);
            return y;
        }

        public bool Insertar(operaciones datos)
        {
            bool insertado = false;
            raiz = InsertarRec(raiz, datos, ref insertado);
            return insertado;
        }

        private CNodo InsertarRec(CNodo actual, operaciones datos, ref bool insertado)
        {
            if (actual == null)
            {
                insertado = true;
                return new CNodo(datos);
            }

            int cmp = string.CompareOrdinal(datos.cod_id, actual.dato.cod_id);

            if (cmp < 0)
                actual.izq = InsertarRec(actual.izq, datos, ref insertado);
            else if (cmp > 0)
                actual.der = InsertarRec(actual.der, datos, ref insertado);
            else
                return actual;

            return Rebalancear(actual);
        }

        private CNodo Rebalancear(CNodo n)
        {
            ActualizarAltura(n);
            int balance = Balance(n);

            if (balance > 1)
            {
                if (Balance(n.izq) < 0)
                    n.izq = RotarIzquierda(n.izq);

                return RotarDerecha(n);
            }

            if (balance < -1)
            {
                if (Balance(n.der) > 0)
                    n.der = RotarDerecha(n.der);

                return RotarIzquierda(n);
            }

            return n;
        }

        public operaciones Buscar(string cod_id)
        {
            CNodo actual = raiz;

            while (actual != null)
            {
                int cmp = string.CompareOrdinal(cod_id, actual.dato.cod_id);

                if (cmp == 0)
                    return actual.dato;

                actual = cmp < 0 ? actual.izq : actual.der;
            }

            return null;
        }

        public List<operaciones> InOrden()
        {
            var lista = new List<operaciones>();
            InOrdenRec(raiz, lista);
            return lista;
        }

        private void InOrdenRec(CNodo n, List<operaciones> lista)
        {
            if (n == null) return;
            InOrdenRec(n.izq, lista);
            lista.Add(n.dato);
            InOrdenRec(n.der, lista);
        }

        public int AlturaArbol() => Altura(raiz);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ArbolAVL arbol = new ArbolAVL();

            for (int i = 1; i <= 1000; i++)
            {
                arbol.Insertar(new operaciones
                {
                    cod_id = i.ToString("D4"),
                    tipoOperacion = (TipoOperacion)(i % 4),
                    monto = i * 10,
                    sede = i % 2 == 0 ? "Lima" : "Cusco"
                });
            }

            Console.WriteLine($"Altura del árbol con 1000 datos: {arbol.AlturaArbol()}");

            operaciones encontrada = arbol.Buscar("0500");

            if (encontrada != null)
                Console.WriteLine($"Encontrada: {encontrada.cod_id} - {encontrada.tipoOperacion} - S/ {encontrada.monto} - {encontrada.sede}");
            else
                Console.WriteLine("No existe esa operación.");

            Console.WriteLine(arbol.Buscar("9999") == null
                ? "9999: no existe (correcto)"
                : "9999: encontrada (error)");

            Console.ReadKey();
        }
    }
}
