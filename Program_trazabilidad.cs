using System;                                   
using System.Collections.Generic;               

namespace Arbol_binario_de_busqueda             
{                                               
    // ==================================================================
    // ENUM TipoOperacion
    // FUNCIÓN: limitar los tipos de operación a 4 valores válidos.
    // CÓMO: cada nombre recibe un número automático (0,1,2,3).
    // ==================================================================
    public enum TipoOperacion                   // Declara el enum
    {                                           // Abre el enum
        transferencia, pagadeservicios, retiro, deposito   // 0 = transferencia, 1 = pagadeservicios, 2 = retiro, 3 = deposito
    }                                           // Cierra el enum

    // ==================================================================
    // CLASE operaciones
    // FUNCIÓN: representa el dato que se guarda (una operación bancaria).
    // CÓMO: propiedades automáticas que almacenan cada campo.
    // ==================================================================
    class operaciones                           // Declara la clase del dato
    {                                           // Abre la clase
        public string cod_id { get; set; }                // Código único: es la CLAVE que usa el árbol
        public TipoOperacion tipoOperacion { get; set; }  // Tipo de operación (valor del enum)
        public double monto { get; set; }                 // Monto de la operación en soles
        public string sede { get; set; }                  // Sede donde se hizo

        public override string ToString()                 // Redefine cómo se convierte la operación a texto
        {                                                 // Abre el método
            return $"[{cod_id}] {tipoOperacion} | S/ {monto:F2} | Sede: {sede}";  // Arma el texto; F2 = 2 decimales. Console.WriteLine(op) lo usa solo
        }                                                 // Cierra el método
    }                                           // Cierra la clase operaciones

    // ------------------------------------------------------------------
    // LISTA DOBLEMENTE ENLAZADA (guarda las operaciones en orden de registro)
    // ------------------------------------------------------------------

    // ==================================================================
    // CLASE NodoDoble
    // FUNCIÓN: casilla de la lista; guarda una operación y conoce a sus vecinos.
    // CÓMO: un dato + referencia al siguiente + referencia al anterior.
    // ==================================================================
    class NodoDoble                             // Declara el nodo de la lista
    {                                           // Abre la clase
        public operaciones dato;                // La operación guardada
        public NodoDoble siguiente;             // Vecino de adelante (null si es el último)
        public NodoDoble anterior;              // Vecino de atrás (null si es el primero)

        public NodoDoble(operaciones dato)      // Constructor: recibe la operación a guardar
        {                                       // Abre el constructor
            this.dato = dato;                   // Guarda la operación en el campo del nodo
            siguiente = null;                   // Nace sin vecino de adelante
            anterior = null;                    // Nace sin vecino de atrás
        }                                       // Cierra el constructor
    }                                           // Cierra la clase NodoDoble

    // ==================================================================
    // CLASE ListaDoble
    // FUNCIÓN: guardar las operaciones en orden de registro, recorribles en ambos sentidos.
    // CÓMO: punteros head (primero) y tail (último) permiten insertar en los extremos en O(1).
    // ==================================================================
    class ListaDoble                            // Declara la lista
    {                                           // Abre la clase
        public NodoDoble head;                  // Apunta al primer nodo
        public NodoDoble tail;                  // Apunta al último nodo

        public ListaDoble()                     // Constructor de la lista
        {                                       // Abre el constructor
            head = null;                        // Lista vacía: no hay primero
            tail = null;                        // Lista vacía: no hay último
        }                                       // Cierra el constructor

        // Devuelven el nodo creado para que el árbol pueda enlazarse a él

        // FUNCIÓN: insertar una operación al INICIO.
        public NodoDoble agregarAlInicio(operaciones dato)   // Recibe la operación, devuelve su nodo
        {                                       // Abre el método
            NodoDoble nuevo = new NodoDoble(dato);   // Crea el nodo con la operación

            if (head == null)                   // ¿La lista está vacía?
            {                                   // Sí:
                head = nuevo;                   // El nuevo es el primero
                tail = nuevo;                   // y también el último (único nodo)
            }                                   // Fin del caso vacío
            else                                // No está vacía:
            {                                   // Abre el caso con nodos
                nuevo.siguiente = head;         // El nuevo apunta adelante al antiguo primero
                head.anterior = nuevo;          // El antiguo primero apunta atrás al nuevo
                head = nuevo;                   // Ahora el nuevo es el primero
            }                                   // Cierra el caso con nodos
            return nuevo;                       // Devuelve el nodo para que el árbol se enlace a él
        }                                       // Cierra agregarAlInicio

        // FUNCIÓN: insertar una operación al FINAL.
        public NodoDoble agregarAlFinal(operaciones dato)    // Recibe la operación, devuelve su nodo
        {                                       // Abre el método
            NodoDoble nuevo = new NodoDoble(dato);   // Crea el nodo con la operación

            if (head == null)                   // ¿La lista está vacía?
            {                                   // Sí:
                head = nuevo;                   // El nuevo es el primero
                tail = nuevo;                   // y también el último
            }                                   // Fin del caso vacío
            else                                // No está vacía:
            {                                   // Abre el caso con nodos
                tail.siguiente = nuevo;         // El antiguo último apunta adelante al nuevo
                nuevo.anterior = tail;          // El nuevo apunta atrás al antiguo último
                tail = nuevo;                   // Ahora el nuevo es el último
            }                                   // Cierra el caso con nodos
            return nuevo;                       // Devuelve el nodo para que el árbol se enlace a él
        }                                       // Cierra agregarAlFinal

        // Elimina un nodo ya conocido, sin recorrer la lista: O(1)

        // FUNCIÓN: quitar un nodo concreto de la lista.
        // CÓMO: el nodo conoce a sus vecinos, así que se "puentean" entre sí.
        public void eliminarNodo(NodoDoble nodo)     // Recibe el nodo a quitar
        {                                       // Abre el método
            if (nodo == null) return;           // Si no hay nodo, no hace nada

            if (nodo.anterior != null) nodo.anterior.siguiente = nodo.siguiente;   // Hay anterior: este salta al siguiente del nodo
            else head = nodo.siguiente;         // No hay anterior (era el primero): head pasa al siguiente

            if (nodo.siguiente != null) nodo.siguiente.anterior = nodo.anterior;   // Hay siguiente: este salta al anterior del nodo
            else tail = nodo.anterior;          // No hay siguiente (era el último): tail pasa al anterior

            nodo.anterior = null;               // Desconecta el nodo eliminado (enlace atrás)
            nodo.siguiente = null;              // Desconecta el nodo eliminado (enlace adelante)
        }                                       // Cierra eliminarNodo

        // FUNCIÓN: mostrar de primero a último.
        public void imprimirAdelante()          // Método de recorrido hacia adelante
        {                                       // Abre el método
            if (head == null)                   // ¿Lista vacía?
            {                                   // Sí:
                Console.WriteLine("La lista está vacía.");   // Avisa
                return;                         // Termina el método
            }                                   // Cierra el if

            NodoDoble actual = head;            // Empieza en el primer nodo
            while (actual != null)              // Repite mientras haya nodo
            {                                   // Abre el ciclo
                Console.WriteLine(actual.dato); // Imprime la operación (usa ToString)
                actual = actual.siguiente;      // Avanza al siguiente nodo
            }                                   // Cierra el ciclo (termina al llegar a null)
        }                                       // Cierra imprimirAdelante

        // FUNCIÓN: mostrar de último a primero.
        public void imprimirAtras()             // Método de recorrido hacia atrás
        {                                       // Abre el método
            if (tail == null)                   // ¿Lista vacía?
            {                                   // Sí:
                Console.WriteLine("La lista está vacía.");   // Avisa
                return;                         // Termina el método
            }                                   // Cierra el if

            NodoDoble actual = tail;            // Empieza en el último nodo
            while (actual != null)              // Repite mientras haya nodo
            {                                   // Abre el ciclo
                Console.WriteLine(actual.dato); // Imprime la operación
                actual = actual.anterior;       // Retrocede al nodo anterior
            }                                   // Cierra el ciclo (termina al pasar el primero)
        }                                       // Cierra imprimirAtras
    }                                           // Cierra la clase ListaDoble

    // ------------------------------------------------------------------
    // ÁRBOL AVL (indexa por cod_id y apunta a los nodos de la lista)
    // ------------------------------------------------------------------

    // ==================================================================
    // CLASE CNodo
    // FUNCIÓN: nodo del árbol. No guarda la operación: guarda un ENLACE al nodo de la lista.
    // CÓMO: enlace + hijo izquierdo + hijo derecho + altura.
    // ==================================================================
    class CNodo                                 // Declara el nodo del árbol
    {                                           // Abre la clase
        public NodoDoble enlace { get; set; }   // Nodo de la lista que contiene la operación
        public CNodo izq { get; set; }          // Hijo izquierdo: códigos menores
        public CNodo der { get; set; }          // Hijo derecho: códigos mayores
        public int altura { get; set; } = 1;    // Altura del nodo; una hoja nueva vale 1

        public string clave => enlace.dato.cod_id;   // La clave se lee del cod_id de la operación enlazada

        public CNodo(NodoDoble enlace)          // Constructor: recibe el nodo de la lista
        {                                       // Abre el constructor
            this.enlace = enlace;               // Guarda el enlace
        }                                       // Cierra el constructor
    }                                           // Cierra la clase CNodo

    // ==================================================================
    // CLASE ArbolAVL
    // FUNCIÓN: indexar por código para buscar/insertar/eliminar en O(log n).
    // CÓMO: árbol de búsqueda que se auto-balancea con rotaciones (diferencia de alturas <= 1).
    // ==================================================================
    class ArbolAVL                              // Declara el árbol
    {                                           // Abre la clase
        private CNodo raiz;                     // Nodo raíz (null = árbol vacío)

        public ArbolAVL() { raiz = null; }      // Constructor: árbol vacío

        internal CNodo Raiz => raiz;            // Acceso de solo lectura a la raíz

        private int Altura(CNodo n) => n == null ? 0 : n.altura;   // Altura de un nodo; null cuenta como 0

        private int Balance(CNodo n) => n == null ? 0 : Altura(n.izq) - Altura(n.der);   // Balance = alt. izquierda - alt. derecha

        private void ActualizarAltura(CNodo n)  // Recalcula la altura de un nodo
        {                                       // Abre el método
            n.altura = 1 + Math.Max(Altura(n.izq), Altura(n.der));   // 1 + la mayor altura de sus hijos
        }                                       // Cierra el método

        // FUNCIÓN: corregir desbalance a la izquierda (caso izq-izq).
        private CNodo RotarDerecha(CNodo y)     // y es el nodo desbalanceado
        {                                       // Abre el método
            CNodo x = y.izq;                    // x = hijo izquierdo de y; será la nueva raíz del subárbol
            y.izq = x.der;                      // El hijo derecho de x pasa a ser hijo izquierdo de y
            x.der = y;                          // y baja y queda como hijo derecho de x

            ActualizarAltura(y);                // Primero y (quedó abajo)
            ActualizarAltura(x);                // Luego x (quedó arriba)
            return x;                           // x es la nueva raíz de este subárbol
        }                                       // Cierra RotarDerecha

        // FUNCIÓN: corregir desbalance a la derecha (caso der-der).
        private CNodo RotarIzquierda(CNodo x)   // x es el nodo desbalanceado
        {                                       // Abre el método
            CNodo y = x.der;                    // y = hijo derecho de x; será la nueva raíz del subárbol
            x.der = y.izq;                      // El hijo izquierdo de y pasa a ser hijo derecho de x
            y.izq = x;                          // x baja y queda como hijo izquierdo de y

            ActualizarAltura(x);                // Primero x (quedó abajo)
            ActualizarAltura(y);                // Luego y (quedó arriba)
            return y;                           // y es la nueva raíz de este subárbol
        }                                       // Cierra RotarIzquierda

        // FUNCIÓN: revisar el balance de un nodo y corregirlo.
        private CNodo Rebalancear(CNodo n)      // Recibe el nodo a revisar
        {                                       // Abre el método
            ActualizarAltura(n);                // Actualiza su altura tras el cambio
            int balance = Balance(n);           // Calcula su balance

            if (balance > 1)                    // Muy cargado a la izquierda
            {                                   // Abre el caso
                if (Balance(n.izq) < 0)         // ¿El hijo izquierdo está cargado a la derecha? (caso izq-der)
                    n.izq = RotarIzquierda(n.izq);   // Primero se rota ese hijo a la izquierda

                return RotarDerecha(n);         // Luego rotación a la derecha del nodo
            }                                   // Cierra el caso

            if (balance < -1)                   // Muy cargado a la derecha
            {                                   // Abre el caso
                if (Balance(n.der) > 0)         // ¿El hijo derecho está cargado a la izquierda? (caso der-izq)
                    n.der = RotarDerecha(n.der);     // Primero se rota ese hijo a la derecha

                return RotarIzquierda(n);       // Luego rotación a la izquierda del nodo
            }                                   // Cierra el caso

            return n;                           // Balance entre -1 y 1: no se cambia nada
        }                                       // Cierra Rebalancear

        // ---------- Insertar ----------

        // FUNCIÓN: indexar un nodo de la lista en el árbol. Devuelve true si se insertó.
        public bool Insertar(NodoDoble nodoLista)    // Recibe el nodo de la lista
        {                                       // Abre el método
            bool insertado = false;             // Bandera: aún no se insertó
            raiz = InsertarRec(raiz, nodoLista, ref insertado);   // Inserta recursivamente y actualiza la raíz (puede cambiar por rotaciones)
            return insertado;                   // true si se insertó, false si era duplicado
        }                                       // Cierra Insertar

        // FUNCIÓN: bajar por el árbol hasta el hueco correcto y rebalancear al volver.
        private CNodo InsertarRec(CNodo actual, NodoDoble nodoLista, ref bool insertado)   // actual = nodo que se examina
        {                                       // Abre el método
            if (actual == null)                 // Llegó a un hueco: aquí va el nuevo
            {                                   // Abre el caso base
                insertado = true;               // Marca que se insertó
                return new CNodo(nodoLista);    // Crea y devuelve el nodo nuevo
            }                                   // Cierra el caso base

            int cmp = string.CompareOrdinal(nodoLista.dato.cod_id, actual.clave);   // Compara códigos: <0 menor, 0 igual, >0 mayor

            if (cmp < 0)                        // El código nuevo es menor
                actual.izq = InsertarRec(actual.izq, nodoLista, ref insertado);   // Baja por la izquierda
            else if (cmp > 0)                   // El código nuevo es mayor
                actual.der = InsertarRec(actual.der, nodoLista, ref insertado);   // Baja por la derecha
            else                                // Son iguales
                return actual; // código duplicado   // No inserta; devuelve el nodo tal cual

            return Rebalancear(actual);         // Al regresar, revisa y corrige el balance de este nodo
        }                                       // Cierra InsertarRec

        // ---------- Eliminar ----------

        // FUNCIÓN: quitar del árbol el código dado. Devuelve true si existía.
        public bool Eliminar(string cod_id)     // Recibe el código a quitar
        {                                       // Abre el método
            bool eliminado = false;             // Bandera: aún no se eliminó
            raiz = EliminarRec(raiz, cod_id, ref eliminado);   // Elimina recursivamente y actualiza la raíz
            return eliminado;                   // true si se eliminó
        }                                       // Cierra Eliminar

        // FUNCIÓN: buscar el nodo, quitarlo y rebalancear al volver.
        private CNodo EliminarRec(CNodo n, string cod_id, ref bool eliminado)   // n = nodo que se examina
        {                                       // Abre el método
            if (n == null) return null;         // Llegó a un hueco: el código no existe

            int cmp = string.CompareOrdinal(cod_id, n.clave);   // Compara el código buscado con el del nodo

            if (cmp < 0)                        // Es menor
                n.izq = EliminarRec(n.izq, cod_id, ref eliminado);   // Busca por la izquierda
            else if (cmp > 0)                   // Es mayor
                n.der = EliminarRec(n.der, cod_id, ref eliminado);   // Busca por la derecha
            else                                // Lo encontró
            {                                   // Abre el caso "encontrado"
                eliminado = true;               // Marca que se eliminó

                if (n.izq == null) return n.der;    // Sin hijo izquierdo: lo reemplaza el derecho (o null)
                if (n.der == null) return n.izq;    // Sin hijo derecho: lo reemplaza el izquierdo

                // Dos hijos: se reemplaza por el sucesor in-orden
                CNodo sucesor = n.der;          // Empieza en el hijo derecho
                while (sucesor.izq != null) sucesor = sucesor.izq;   // Baja a la izquierda: el menor de la derecha

                n.enlace = sucesor.enlace;      // Este nodo toma el enlace del sucesor (cambia su clave)
                bool aux = false;               // Bandera auxiliar (no se usa)
                n.der = EliminarRec(n.der, sucesor.clave, ref aux);   // Elimina el sucesor original de la derecha
            }                                   // Cierra el caso "encontrado"

            return Rebalancear(n);              // Al regresar, corrige el balance de este nodo
        }                                       // Cierra EliminarRec

        // ---------- Buscar ----------
        // Devuelve el nodo de la lista doble, desde el cual se puede ir a anterior/siguiente

        // FUNCIÓN: localizar una operación por código y devolver su nodo de la lista.
        public NodoDoble BuscarNodo(string cod_id)   // Recibe el código
        {                                       // Abre el método
            CNodo actual = raiz;                // Empieza en la raíz

            while (actual != null)              // Mientras haya nodo que revisar
            {                                   // Abre el ciclo
                int cmp = string.CompareOrdinal(cod_id, actual.clave);   // Compara con la clave del nodo

                if (cmp == 0)                   // ¿Es igual?
                    return actual.enlace;       // Encontrado: devuelve el nodo de la lista

                actual = cmp < 0 ? actual.izq : actual.der;   // Menor: va a la izquierda; mayor: a la derecha
            }                                   // Cierra el ciclo

            return null;                        // Se acabó el camino: no existe
        }                                       // Cierra BuscarNodo

        // FUNCIÓN: obtener directamente la operación por código.
        public operaciones Buscar(string cod_id) => BuscarNodo(cod_id)?.dato;   // Si el nodo no es null, devuelve su dato; si no, null

        // ---------- Recorrido ----------

        // FUNCIÓN: devolver las operaciones ordenadas por código.
        public List<operaciones> InOrden()      // Método público
        {                                       // Abre el método
            var lista = new List<operaciones>();    // Lista vacía para acumular
            InOrdenRec(raiz, lista);            // Recorre desde la raíz llenando la lista
            return lista;                       // Devuelve la lista ordenada
        }                                       // Cierra InOrden

        // FUNCIÓN: recorrido in-orden (izquierda, nodo, derecha) = orden ascendente.
        private void InOrdenRec(CNodo n, List<operaciones> lista)   // n = nodo actual
        {                                       // Abre el método
            if (n == null) return;              // Caso base: sin nodo, termina
            InOrdenRec(n.izq, lista);           // 1) Visita todo el subárbol izquierdo (menores)
            lista.Add(n.enlace.dato);           // 2) Agrega la operación de este nodo
            InOrdenRec(n.der, lista);           // 3) Visita todo el subárbol derecho (mayores)
        }                                       // Cierra InOrdenRec

        public int AlturaArbol() => Altura(raiz);   // Altura total del árbol = altura de la raíz
    }                                           // Cierra la clase ArbolAVL

    // ------------------------------------------------------------------
    // PROGRAMA PRINCIPAL
    // ------------------------------------------------------------------
    internal class Program                      // Clase que contiene Main
    {                                           // Abre la clase
        static ListaDoble lista = new ListaDoble();   // Lista compartida: guarda el orden de registro
        static ArbolAVL arbol = new ArbolAVL();       // Árbol compartido: indexa por código

        // FUNCIÓN: punto de entrada; muestra el menú y ejecuta la opción.
        static void Main(string[] args)         // Aquí empieza la ejecución
        {                                       // Abre Main
            int opcion;                         // Guardará la opción elegida

            do                                  // Repite el menú al menos una vez
            {                                   // Abre el ciclo
                Console.Clear();                // Limpia la pantalla
                Console.WriteLine("==========================================");   // Línea decorativa
                Console.WriteLine("   OPERACIONES: LISTA DOBLE + ARBOL AVL");      // Título
                Console.WriteLine("==========================================");   // Línea decorativa
                Console.WriteLine("1. Registrar operación al final");               // Opción 1
                Console.WriteLine("2. Registrar operación al inicio");              // Opción 2
                Console.WriteLine("3. Eliminar operación por código");              // Opción 3
                Console.WriteLine("4. Buscar por código (árbol) y ver vecinos");    // Opción 4
                Console.WriteLine("5. Mostrar lista hacia adelante");               // Opción 5
                Console.WriteLine("6. Mostrar lista hacia atrás");                  // Opción 6
                Console.WriteLine("7. Mostrar árbol en orden (por código)");        // Opción 7
                Console.WriteLine("8. Altura del árbol");                           // Opción 8
                Console.WriteLine("9. Salir");                                      // Opción 9
                Console.WriteLine("==========================================");   // Línea decorativa

                opcion = LeerEntero("Seleccione una opción: ");   // Lee un entero validado
                Console.Clear();                // Limpia antes de mostrar el resultado

                switch (opcion)                 // Elige qué hacer según la opción
                {                               // Abre el switch
                    case 1: Registrar(true); break;     // Registra al final de la lista
                    case 2: Registrar(false); break;    // Registra al inicio de la lista
                    case 3: Eliminar(); break;          // Elimina por código
                    case 4: BuscarConVecinos(); break;  // Busca y muestra vecinos
                    case 5:                             // Mostrar hacia adelante
                        Console.WriteLine("LISTA HACIA ADELANTE\n");   // Título
                        lista.imprimirAdelante();       // Recorre head -> tail
                        break;                          // Sale del case
                    case 6:                             // Mostrar hacia atrás
                        Console.WriteLine("LISTA HACIA ATRÁS\n");      // Título
                        lista.imprimirAtras();          // Recorre tail -> head
                        break;                          // Sale del case
                    case 7:                             // Mostrar árbol en orden
                        Console.WriteLine("ÁRBOL EN ORDEN\n");         // Título
                        var ordenadas = arbol.InOrden();    // Obtiene las operaciones ordenadas por código
                        if (ordenadas.Count == 0) Console.WriteLine("El árbol está vacío.");   // Aviso si no hay datos
                        foreach (var op in ordenadas) Console.WriteLine(op);   // Imprime cada operación
                        break;                          // Sale del case
                    case 8:                             // Altura del árbol
                        Console.WriteLine("Altura del árbol: " + arbol.AlturaArbol());   // Muestra la altura
                        break;                          // Sale del case
                    case 9:                             // Salir
                        Console.WriteLine("Programa finalizado.");     // Mensaje de despedida
                        break;                          // Sale del case
                    default:                            // Cualquier otro número
                        Console.WriteLine("Opción no válida.");        // Aviso de error
                        break;                          // Sale del case
                }                               // Cierra el switch

                if (opcion != 9)                // Si no se salió
                {                               // Abre el if
                    Console.WriteLine("\nPresione ENTER para continuar...");   // Pausa para leer el resultado
                    Console.ReadLine();         // Espera ENTER
                }                               // Cierra el if

            } while (opcion != 9);              // Vuelve al menú mientras no elija 9
        }                                       // Cierra Main

        // FUNCIÓN: registrar una operación en la lista y en el árbol.
        // CÓMO: valida los datos, la crea, la pone en la lista y enlaza su nodo al árbol.
        static void Registrar(bool alFinal)     // alFinal: true = final, false = inicio
        {                                       // Abre el método
            Console.Write("Código de operación: ");   // Pide el código
            string cod = (Console.ReadLine() ?? "").Trim();   // Lee, evita null con ?? "" y quita espacios

            if (cod == "")                      // ¿Código vacío?
            {                                   // Sí:
                Console.WriteLine("El código no puede estar vacío.");   // Avisa
                return;                         // Cancela el registro
            }                                   // Cierra el if

            // Se valida contra el árbol (O(log n)) antes de tocar la lista
            if (arbol.BuscarNodo(cod) != null)  // ¿Ya existe ese código en el árbol?
            {                                   // Sí:
                Console.WriteLine("Ya existe una operación con ese código.");   // Avisa
                return;                         // Cancela el registro
            }                                   // Cierra el if

            Console.WriteLine("Tipo: 0 = transferencia, 1 = pagadeservicios, 2 = retiro, 3 = deposito");   // Muestra las opciones
            int tipo = LeerEntero("Tipo: ");    // Lee el tipo como entero validado
            if (!Enum.IsDefined(typeof(TipoOperacion), tipo))   // ¿El número no es un valor del enum?
            {                                   // Sí:
                Console.WriteLine("Tipo no válido.");   // Avisa
                return;                         // Cancela el registro
            }                                   // Cierra el if

            double monto = LeerDouble("Monto: ");   // Lee el monto como decimal validado

            Console.Write("Sede: ");            // Pide la sede
            string sede = Console.ReadLine();   // Lee la sede

            var op = new operaciones            // Crea la operación con los datos leídos
            {                                   // Abre el inicializador
                cod_id = cod,                   // Asigna el código
                tipoOperacion = (TipoOperacion)tipo,   // Convierte el entero al enum
                monto = monto,                  // Asigna el monto
                sede = sede                     // Asigna la sede
            };                                  // Cierra el inicializador

            NodoDoble nodo = alFinal ? lista.agregarAlFinal(op) : lista.agregarAlInicio(op);   // Inserta en la lista y obtiene el nodo creado
            arbol.Insertar(nodo); // el árbol queda enlazado al nodo de la lista   // Indexa ese nodo en el árbol

            Console.WriteLine("Operación registrada.");   // Confirma
        }                                       // Cierra Registrar

        // FUNCIÓN: eliminar una operación por código.
        // CÓMO: el árbol halla el nodo (O(log n)); la lista lo quita directo (O(1)); luego se quita del árbol.
        static void Eliminar()                  // Método de eliminación
        {                                       // Abre el método
            Console.Write("Código a eliminar: ");   // Pide el código
            string cod = (Console.ReadLine() ?? "").Trim();   // Lee y limpia

            // El árbol localiza el nodo y la lista lo elimina directamente, sin recorrerse
            NodoDoble nodo = arbol.BuscarNodo(cod);   // Busca el nodo de la lista usando el árbol
            if (nodo == null)                   // ¿No existe?
            {                                   // Sí:
                Console.WriteLine("Dato no encontrado.");   // Avisa
                return;                         // Termina
            }                                   // Cierra el if

            lista.eliminarNodo(nodo);           // Quita el nodo de la lista
            arbol.Eliminar(cod);                // Quita el código del árbol

            Console.WriteLine("Operación eliminada correctamente.");   // Confirma
        }                                       // Cierra Eliminar

        // FUNCIÓN: buscar por código y mostrar la operación anterior y la siguiente.
        // CÓMO: el árbol da el nodo de la lista; sus enlaces dan los vecinos sin recorrer.
        static void BuscarConVecinos()          // Método de búsqueda
        {                                       // Abre el método
            Console.Write("Código a buscar: ");     // Pide el código
            string cod = (Console.ReadLine() ?? "").Trim();   // Lee y limpia

            NodoDoble nodo = arbol.BuscarNodo(cod);   // Busca en el árbol
            if (nodo == null)                   // ¿No existe?
            {                                   // Sí:
                Console.WriteLine("Dato no encontrado.");   // Avisa
                return;                         // Termina
            }                                   // Cierra el if

            Console.WriteLine("Encontrada:  " + nodo.dato);   // Muestra la operación hallada
            Console.WriteLine("Anterior:    " + (nodo.anterior != null ? nodo.anterior.dato.ToString() : "(ninguna, es el primero)"));   // Vecino de atrás o aviso
            Console.WriteLine("Siguiente:   " + (nodo.siguiente != null ? nodo.siguiente.dato.ToString() : "(ninguna, es el último)"));   // Vecino de adelante o aviso
        }                                       // Cierra BuscarConVecinos

        // FUNCIÓN: leer un entero válido.
        static int LeerEntero(string mensaje)   // mensaje: texto que se muestra al pedir el dato
        {                                       // Abre el método
            int valor;                          // Aquí se guarda el número
            Console.Write(mensaje);             // Muestra el mensaje
            while (!int.TryParse(Console.ReadLine(), out valor))   // Intenta convertir; repite mientras falle
                Console.Write("Entrada inválida. " + mensaje);     // Avisa y vuelve a pedir
            return valor;                       // Devuelve el entero válido
        }                                       // Cierra LeerEntero

        // FUNCIÓN: leer un decimal válido.
        static double LeerDouble(string mensaje)    // mensaje: texto que se muestra al pedir el dato
        {                                       // Abre el método
            double valor;                       // Aquí se guarda el número
            Console.Write(mensaje);             // Muestra el mensaje
            while (!double.TryParse(Console.ReadLine(), out valor))   // Intenta convertir; repite mientras falle
                Console.Write("Entrada inválida. " + mensaje);        // Avisa y vuelve a pedir
            return valor;                       // Devuelve el decimal válido
        }                                       // Cierra LeerDouble
    }                                           // Cierra la clase Program
}                                               // Cierra el namespace

/* ======================================================================
   TRAZABILIDAD DE EJEMPLO (rotación AVL al registrar A, B, C)
   ----------------------------------------------------------------------
   1) Registrar "A" al final
      Lista:  head -> [A] <- tail
      Árbol:  A (altura 1)
   2) Registrar "B" al final
      Lista:  [A] <-> [B]
      Árbol:  A -> der = B ; altura(A) = 2 ; balance(A) = 0 - 1 = -1 (OK)
   3) Registrar "C" al final
      Lista:  [A] <-> [B] <-> [C]
      Árbol antes de rebalancear:  A -> B -> C (cadena a la derecha)
      balance(A) = 0 - 2 = -2  (< -1)  => caso derecha-derecha
      Balance(A.der = B) = 0 - 1 = -1 (no es > 0) => rotación simple
      RotarIzquierda(A): B sube, A queda a su izquierda, C a su derecha
      Árbol final:      B
                       / \
                      A   C        altura = 2
   4) Opción 4, buscar "B"
      arbol.BuscarNodo("B") -> raíz coincide -> devuelve el NodoDoble de B
      Anterior = A ; Siguiente = C  (leídos de los enlaces de la lista)
   5) Opción 3, eliminar "B"
      arbol.BuscarNodo("B") -> NodoDoble de B
      lista.eliminarNodo: A.siguiente = C ; C.anterior = A
      arbol.Eliminar("B"): dos hijos -> sucesor = C ; la raíz toma el enlace de C ;
      se borra la hoja C original ; el árbol queda C -> izq = A
   ====================================================================== */
