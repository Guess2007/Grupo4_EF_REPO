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



    public override string ToString()

    {

      return $"[{cod_id}] {tipoOperacion} | S/ {monto:F2} | Sede: {sede}";

    }

  }



  // ------------------------------------------------------------------

  // LISTA DOBLEMENTE ENLAZADA (guarda las operaciones en orden de registro)

  // ------------------------------------------------------------------

  class NodoDoble

  {

    public operaciones dato;

    public NodoDoble siguiente;

    public NodoDoble anterior;



    public NodoDoble(operaciones dato)

    {

      this.dato = dato;

      siguiente = null;

      anterior = null;

    }

  }



  class ListaDoble

  {

    public NodoDoble head;

    public NodoDoble tail;



    public ListaDoble()

    {

      head = null;

      tail = null;

    }



    // Devuelven el nodo creado para que el árbol pueda enlazarse a él

    public NodoDoble agregarAlInicio(operaciones dato)

    {

      NodoDoble nuevo = new NodoDoble(dato);



      if (head == null)

      {

        head = nuevo;

        tail = nuevo;

      }

      else

      {

        nuevo.siguiente = head;

        head.anterior = nuevo;

        head = nuevo;

      }

      return nuevo;

    }



    public NodoDoble agregarAlFinal(operaciones dato)

    {

      NodoDoble nuevo = new NodoDoble(dato);



      if (head == null)

      {

        head = nuevo;

        tail = nuevo;

      }

      else

      {

        tail.siguiente = nuevo;

        nuevo.anterior = tail;

        tail = nuevo;

      }

      return nuevo;

    }



    // Elimina un nodo ya conocido, sin recorrer la lista: O(1)

    public void eliminarNodo(NodoDoble nodo)

    {

      if (nodo == null) return;



      if (nodo.anterior != null) nodo.anterior.siguiente = nodo.siguiente;

      else head = nodo.siguiente;



      if (nodo.siguiente != null) nodo.siguiente.anterior = nodo.anterior;

      else tail = nodo.anterior;



      nodo.anterior = null;

      nodo.siguiente = null;

    }



    public void imprimirAdelante()

    {

      if (head == null)

      {

        Console.WriteLine("La lista está vacía.");

        return;

      }



      NodoDoble actual = head;

      while (actual != null)

      {

        Console.WriteLine(actual.dato);

        actual = actual.siguiente;

      }

    }



    public void imprimirAtras()

    {

      if (tail == null)

      {

        Console.WriteLine("La lista está vacía.");

        return;

      }



      NodoDoble actual = tail;

      while (actual != null)

      {

        Console.WriteLine(actual.dato);

        actual = actual.anterior;

      }

    }

  }



  // ------------------------------------------------------------------

  // ÁRBOL AVL (indexa por cod_id y apunta a los nodos de la lista)

  // ------------------------------------------------------------------

  class CNodo

  {

    public NodoDoble enlace { get; set; }  // nodo de la lista doble

    public CNodo izq { get; set; }

    public CNodo der { get; set; }

    public int altura { get; set; } = 1;



    public string clave => enlace.dato.cod_id;



    public CNodo(NodoDoble enlace)

    {

      this.enlace = enlace;

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



    // ---------- Insertar ----------

    public bool Insertar(NodoDoble nodoLista)

    {

      bool insertado = false;

      raiz = InsertarRec(raiz, nodoLista, ref insertado);

      return insertado;

    }



    private CNodo InsertarRec(CNodo actual, NodoDoble nodoLista, ref bool insertado)

    {

      if (actual == null)

      {

        insertado = true;

        return new CNodo(nodoLista);

      }



      int cmp = string.CompareOrdinal(nodoLista.dato.cod_id, actual.clave);



      if (cmp < 0)

        actual.izq = InsertarRec(actual.izq, nodoLista, ref insertado);

      else if (cmp > 0)

        actual.der = InsertarRec(actual.der, nodoLista, ref insertado);

      else

        return actual; // código duplicado



      return Rebalancear(actual);

    }



    // ---------- Eliminar ----------

    public bool Eliminar(string cod_id)

    {

      bool eliminado = false;

      raiz = EliminarRec(raiz, cod_id, ref eliminado);

      return eliminado;

    }



    private CNodo EliminarRec(CNodo n, string cod_id, ref bool eliminado)

    {

      if (n == null) return null;



      int cmp = string.CompareOrdinal(cod_id, n.clave);



      if (cmp < 0)

        n.izq = EliminarRec(n.izq, cod_id, ref eliminado);

      else if (cmp > 0)

        n.der = EliminarRec(n.der, cod_id, ref eliminado);

      else

      {

        eliminado = true;



        if (n.izq == null) return n.der;

        if (n.der == null) return n.izq;



        // Dos hijos: se reemplaza por el sucesor in-orden

        CNodo sucesor = n.der;

        while (sucesor.izq != null) sucesor = sucesor.izq;



        n.enlace = sucesor.enlace;

        bool aux = false;

        n.der = EliminarRec(n.der, sucesor.clave, ref aux);

      }



      return Rebalancear(n);

    }



    // ---------- Buscar ----------

    // Devuelve el nodo de la lista doble, desde el cual se puede ir a anterior/siguiente

    public NodoDoble BuscarNodo(string cod_id)

    {

      CNodo actual = raiz;



      while (actual != null)

      {

        int cmp = string.CompareOrdinal(cod_id, actual.clave);



        if (cmp == 0)

          return actual.enlace;



        actual = cmp < 0 ? actual.izq : actual.der;

      }



      return null;

    }



    public operaciones Buscar(string cod_id) => BuscarNodo(cod_id)?.dato;



    // ---------- Recorrido ----------

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

      lista.Add(n.enlace.dato);

      InOrdenRec(n.der, lista);

    }



    public int AlturaArbol() => Altura(raiz);

  }



  // ------------------------------------------------------------------

  // PROGRAMA PRINCIPAL

  // ------------------------------------------------------------------

  internal class Program

  {

    static ListaDoble lista = new ListaDoble();

    static ArbolAVL arbol = new ArbolAVL();



    static void Main(string[] args)

    {

      CargarDatosIniciales();



      int opcion;



      do

      {

        Console.Clear();

        Console.WriteLine("==========================================");

        Console.WriteLine("  OPERACIONES: LISTA DOBLE + ARBOL AVL");

        Console.WriteLine("==========================================");

        Console.WriteLine("1. Registrar operación al final");

        Console.WriteLine("2. Registrar operación al inicio");

        Console.WriteLine("3. Eliminar operación por código");

        Console.WriteLine("4. Buscar por código (árbol) y ver vecinos");

        Console.WriteLine("5. Mostrar lista hacia adelante");

        Console.WriteLine("6. Mostrar lista hacia atrás");

        Console.WriteLine("7. Mostrar árbol en orden (por código)");

        Console.WriteLine("8. Altura del árbol");

        Console.WriteLine("9. Salir");

        Console.WriteLine("==========================================");



        opcion = LeerEntero("Seleccione una opción: ");

        Console.Clear();



        switch (opcion)

        {

          case 1: Registrar(true); break;

          case 2: Registrar(false); break;

          case 3: Eliminar(); break;

          case 4: BuscarConVecinos(); break;

          case 5:

            Console.WriteLine("LISTA HACIA ADELANTE\n");

            lista.imprimirAdelante();

            break;

          case 6:

            Console.WriteLine("LISTA HACIA ATRÁS\n");

            lista.imprimirAtras();

            break;

          case 7:

            Console.WriteLine("ÁRBOL EN ORDEN\n");

            var ordenadas = arbol.InOrden();

            if (ordenadas.Count == 0) Console.WriteLine("El árbol está vacío.");

            foreach (var op in ordenadas) Console.WriteLine(op);

            break;

          case 8:

            Console.WriteLine("Altura del árbol: " + arbol.AlturaArbol());

            break;

          case 9:

            Console.WriteLine("Programa finalizado.");

            break;

          default:

            Console.WriteLine("Opción no válida.");

            break;

        }



        if (opcion != 9)

        {

          Console.WriteLine("\nPresione ENTER para continuar...");

          Console.ReadLine();

        }



      } while (opcion != 9);

    }



    // Precarga 30 operaciones (lista = orden de este arreglo, árbol = orden por código)

    static void CargarDatosIniciales()

    {

      var datos = new (string cod, TipoOperacion tipo, double monto, string sede)[]

      {

        ("OP015", TipoOperacion.deposito,    1500.00, "Lima Centro"),

        ("OP008", TipoOperacion.retiro,      300.50, "Miraflores"),

        ("OP022", TipoOperacion.transferencia,  2750.00, "San Isidro"),

        ("OP004", TipoOperacion.pagadeservicios, 120.75, "Surco"),

        ("OP011", TipoOperacion.deposito,     5000.00, "Lima Centro"),

        ("OP019", TipoOperacion.retiro,      800.00, "Callao"),

        ("OP027", TipoOperacion.transferencia,  950.30, "La Molina"),

        ("OP002", TipoOperacion.deposito,     85.00, "Miraflores"),

        ("OP006", TipoOperacion.pagadeservicios, 210.40, "San Borja"),

        ("OP009", TipoOperacion.retiro,      450.00, "Surco"),

        ("OP013", TipoOperacion.transferencia,  3200.00, "San Isidro"),

        ("OP017", TipoOperacion.deposito,     1250.90, "Callao"),

        ("OP020", TipoOperacion.pagadeservicios,  65.20, "Lima Centro"),

        ("OP024", TipoOperacion.retiro,     1000.00, "La Molina"),

        ("OP029", TipoOperacion.deposito,     7800.00, "San Borja"),

        ("OP001", TipoOperacion.transferencia,  400.00, "Surco"),

        ("OP003", TipoOperacion.retiro,      150.00, "Miraflores"),

        ("OP005", TipoOperacion.deposito,     2200.00, "Callao"),

        ("OP007", TipoOperacion.pagadeservicios, 330.10, "San Isidro"),

        ("OP010", TipoOperacion.transferencia,  1800.00, "Lima Centro"),

        ("OP012", TipoOperacion.retiro,      620.00, "San Borja"),

        ("OP014", TipoOperacion.deposito,     3400.00, "La Molina"),

        ("OP016", TipoOperacion.pagadeservicios, 175.60, "Surco"),

        ("OP018", TipoOperacion.transferencia,  720.00, "Miraflores"),

        ("OP021", TipoOperacion.retiro,      250.00, "Callao"),

        ("OP023", TipoOperacion.deposito,     980.00, "San Isidro"),

        ("OP025", TipoOperacion.pagadeservicios,  95.00, "Lima Centro"),

        ("OP026", TipoOperacion.transferencia,  4100.00, "La Molina"),

        ("OP028", TipoOperacion.retiro,      560.75, "San Borja"),

        ("OP030", TipoOperacion.deposito,     1900.00, "Surco"),

      };



      foreach (var d in datos)

      {

        var op = new operaciones

        {

          cod_id = d.cod,

          tipoOperacion = d.tipo,

          monto = d.monto,

          sede = d.sede

        };



        NodoDoble nodo = lista.agregarAlFinal(op);

        arbol.Insertar(nodo);

      }

    }



    static void Registrar(bool alFinal)

    {

      Console.Write("Código de operación: ");

      string cod = (Console.ReadLine() ?? "").Trim();



      if (cod == "")

      {

        Console.WriteLine("El código no puede estar vacío.");

        return;

      }



      // Se valida contra el árbol (O(log n)) antes de tocar la lista

      if (arbol.BuscarNodo(cod) != null)

      {

        Console.WriteLine("Ya existe una operación con ese código.");

        return;

      }



      Console.WriteLine("Tipo: 0 = transferencia, 1 = pagadeservicios, 2 = retiro, 3 = deposito");

      int tipo = LeerEntero("Tipo: ");

      if (!Enum.IsDefined(typeof(TipoOperacion), tipo))

      {

        Console.WriteLine("Tipo no válido.");

        return;

      }



      double monto = LeerDouble("Monto: ");



      Console.Write("Sede: ");

      string sede = Console.ReadLine();



      var op = new operaciones

      {

        cod_id = cod,

        tipoOperacion = (TipoOperacion)tipo,

        monto = monto,

        sede = sede

      };



      NodoDoble nodo = alFinal ? lista.agregarAlFinal(op) : lista.agregarAlInicio(op);

      arbol.Insertar(nodo); // el árbol queda enlazado al nodo de la lista



      Console.WriteLine("Operación registrada.");

    }



    static void Eliminar()

    {

      Console.Write("Código a eliminar: ");

      string cod = (Console.ReadLine() ?? "").Trim();



      // El árbol localiza el nodo y la lista lo elimina directamente, sin recorrerse

      NodoDoble nodo = arbol.BuscarNodo(cod);

      if (nodo == null)

      {

        Console.WriteLine("Dato no encontrado.");

        return;

      }



      lista.eliminarNodo(nodo);

      arbol.Eliminar(cod);



      Console.WriteLine("Operación eliminada correctamente.");

    }



    static void BuscarConVecinos()

    {

      Console.Write("Código a buscar: ");

      string cod = (Console.ReadLine() ?? "").Trim();



      NodoDoble nodo = arbol.BuscarNodo(cod);

      if (nodo == null)

      {

        Console.WriteLine("Dato no encontrado.");

        return;

      }



      Console.WriteLine("Encontrada: " + nodo.dato);

      Console.WriteLine("Anterior:  " + (nodo.anterior != null ? nodo.anterior.dato.ToString() : "(ninguna, es el primero)"));

      Console.WriteLine("Siguiente:  " + (nodo.siguiente != null ? nodo.siguiente.dato.ToString() : "(ninguna, es el último)"));

    }



    static int LeerEntero(string mensaje)

    {

      int valor;

      Console.Write(mensaje);

      while (!int.TryParse(Console.ReadLine(), out valor))

        Console.Write("Entrada inválida. " + mensaje);

      return valor;

    }



    static double LeerDouble(string mensaje)

    {

      double valor;

      Console.Write(mensaje);

      while (!double.TryParse(Console.ReadLine(), out valor))

        Console.Write("Entrada inválida. " + mensaje);

      return valor;

    }

  }

}
