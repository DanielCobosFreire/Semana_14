using System;
using System.Collections.Generic;
using System.Text;

// ─────────────────────────────────────────────────────────────
//  NODO
// ─────────────────────────────────────────────────────────────
class Nodo
{
    public int    Valor;
    public Nodo   Izquierdo;
    public Nodo   Derecho;

    public Nodo(int valor)
    {
        Valor      = valor;
        Izquierdo  = null;
        Derecho    = null;
    }
}

// ─────────────────────────────────────────────────────────────
//  ÁRBOL BINARIO DE BÚSQUEDA
// ─────────────────────────────────────────────────────────────
class ArbolBST
{
    private Nodo raiz;

    public ArbolBST() => raiz = null;

    // ── Propiedades públicas ──────────────────────────────────
    public bool EstaVacio => raiz == null;

    // ════════════════════════════════════════════════════════
    //  INSERTAR
    // ════════════════════════════════════════════════════════
    public bool Insertar(int valor)
    {
        if (Buscar(valor)) return false;          // no duplicados
        raiz = InsertarRec(raiz, valor);
        return true;
    }

    private Nodo InsertarRec(Nodo nodo, int valor)
    {
        if (nodo == null) return new Nodo(valor);

        if (valor < nodo.Valor)
            nodo.Izquierdo = InsertarRec(nodo.Izquierdo, valor);
        else
            nodo.Derecho   = InsertarRec(nodo.Derecho,   valor);

        return nodo;
    }

    // ════════════════════════════════════════════════════════
    //  BUSCAR
    // ════════════════════════════════════════════════════════
    public bool Buscar(int valor) => BuscarRec(raiz, valor);

    private bool BuscarRec(Nodo nodo, int valor)
    {
        if (nodo == null)          return false;
        if (valor == nodo.Valor)   return true;
        return valor < nodo.Valor
            ? BuscarRec(nodo.Izquierdo, valor)
            : BuscarRec(nodo.Derecho,   valor);
    }

    // ════════════════════════════════════════════════════════
    //  ELIMINAR
    // ════════════════════════════════════════════════════════
    public bool Eliminar(int valor)
    {
        if (!Buscar(valor)) return false;
        raiz = EliminarRec(raiz, valor);
        return true;
    }

    private Nodo EliminarRec(Nodo nodo, int valor)
    {
        if (nodo == null) return null;

        if (valor < nodo.Valor)
            nodo.Izquierdo = EliminarRec(nodo.Izquierdo, valor);
        else if (valor > nodo.Valor)
            nodo.Derecho   = EliminarRec(nodo.Derecho,   valor);
        else
        {
            // Caso 1 y 2: sin hijos o un solo hijo
            if (nodo.Izquierdo == null) return nodo.Derecho;
            if (nodo.Derecho   == null) return nodo.Izquierdo;

            // Caso 3: dos hijos → reemplazar con sucesor inorden (mínimo del subárbol derecho)
            int sucesor  = MinimoRec(nodo.Derecho);
            nodo.Valor   = sucesor;
            nodo.Derecho = EliminarRec(nodo.Derecho, sucesor);
        }
        return nodo;
    }

    // ════════════════════════════════════════════════════════
    //  MÍNIMO Y MÁXIMO
    // ════════════════════════════════════════════════════════
    public int? Minimo() => EstaVacio ? null : MinimoRec(raiz);
    public int? Maximo() => EstaVacio ? null : MaximoRec(raiz);

    private int MinimoRec(Nodo nodo)
    {
        while (nodo.Izquierdo != null) nodo = nodo.Izquierdo;
        return nodo.Valor;
    }

    private int MaximoRec(Nodo nodo)
    {
        while (nodo.Derecho != null) nodo = nodo.Derecho;
        return nodo.Valor;
    }

    // ════════════════════════════════════════════════════════
    //  ALTURA
    // ════════════════════════════════════════════════════════
    public int Altura() => AlturaRec(raiz);

    private int AlturaRec(Nodo nodo)
    {
        if (nodo == null) return -1;   // convención: árbol vacío = -1
        return 1 + Math.Max(AlturaRec(nodo.Izquierdo), AlturaRec(nodo.Derecho));
    }

    // ════════════════════════════════════════════════════════
    //  RECORRIDOS
    // ════════════════════════════════════════════════════════
    public List<int> Inorden()    { var r = new List<int>(); InordenRec(raiz, r);    return r; }
    public List<int> Preorden()   { var r = new List<int>(); PreordenRec(raiz, r);   return r; }
    public List<int> Postorden()  { var r = new List<int>(); PostordenRec(raiz, r);  return r; }

    private void InordenRec  (Nodo n, List<int> r) { if (n==null) return; InordenRec(n.Izquierdo,r);  r.Add(n.Valor); InordenRec(n.Derecho,r); }
    private void PreordenRec  (Nodo n, List<int> r) { if (n==null) return; r.Add(n.Valor); PreordenRec(n.Izquierdo,r);  PreordenRec(n.Derecho,r); }
    private void PostordenRec (Nodo n, List<int> r) { if (n==null) return; PostordenRec(n.Izquierdo,r); PostordenRec(n.Derecho,r); r.Add(n.Valor); }

    // ════════════════════════════════════════════════════════
    //  CONTEO DE NODOS
    // ════════════════════════════════════════════════════════
    public int ContarNodos() => ContarRec(raiz);
    private int ContarRec(Nodo n) => n == null ? 0 : 1 + ContarRec(n.Izquierdo) + ContarRec(n.Derecho);

    // ════════════════════════════════════════════════════════
    //  LIMPIAR
    // ════════════════════════════════════════════════════════
    public void Limpiar() => raiz = null;

    // ════════════════════════════════════════════════════════
    //  VISUALIZACIÓN EN CONSOLA (árbol rotado 90°)
    // ════════════════════════════════════════════════════════
    public void MostrarArbol()
    {
        if (EstaVacio) { Console.WriteLine("  (árbol vacío)"); return; }
        MostrarArbolRec(raiz, "", false);
    }

    private void MostrarArbolRec(Nodo nodo, string indent, bool esIzquierdo)
    {
        if (nodo == null) return;

        // Rama derecha primero (aparece arriba en consola)
        MostrarArbolRec(nodo.Derecho,
            indent + (esIzquierdo ? "│   " : "    "), false);

        Console.WriteLine(indent + (esIzquierdo ? "└── " : "┌── ") + nodo.Valor);

        MostrarArbolRec(nodo.Izquierdo,
            indent + (esIzquierdo ? "    " : "│   "), true);
    }
}

// ─────────────────────────────────────────────────────────────
//  PROGRAMA PRINCIPAL  —  MENÚ INTERACTIVO
// ─────────────────────────────────────────────────────────────
class Program
{
    static readonly ArbolBST arbol = new ArbolBST();

    // ── Colores de consola ────────────────────────────────────
    static void ColorEscribir(string texto, ConsoleColor color, bool newLine = true)
    {
        Console.ForegroundColor = color;
        if (newLine) Console.WriteLine(texto);
        else         Console.Write(texto);
        Console.ResetColor();
    }

    static void Exito  (string msg) => ColorEscribir($"  ✔  {msg}", ConsoleColor.Green);
    static void Error  (string msg) => ColorEscribir($"  ✘  {msg}", ConsoleColor.Red);
    static void Info   (string msg) => ColorEscribir($"  ℹ  {msg}", ConsoleColor.Cyan);
    static void Advertencia(string msg) => ColorEscribir($"  ⚠  {msg}", ConsoleColor.Yellow);

    // ── Separadores visuales ─────────────────────────────────
    static void LineaDoble()  => ColorEscribir(new string('═', 54), ConsoleColor.DarkBlue);
    static void LineaSimple() => ColorEscribir(new string('─', 54), ConsoleColor.DarkGray);

    // ── Encabezado del menú ───────────────────────────────────
    static void MostrarMenu()
    {
        Console.Clear();
        LineaDoble();
        ColorEscribir("   ÁRBOL BINARIO DE BÚSQUEDA (BST)  —  C#", ConsoleColor.Cyan);
        LineaDoble();

        string estado = arbol.EstaVacio
            ? "vacío"
            : $"{arbol.ContarNodos()} nodo(s) | altura {arbol.Altura()}";

        ColorEscribir($"   Estado del árbol: {estado}", ConsoleColor.DarkCyan);
        LineaSimple();

        ColorEscribir("   OPERACIONES", ConsoleColor.White);
        Console.WriteLine("   1. Insertar valor");
        Console.WriteLine("   2. Buscar valor");
        Console.WriteLine("   3. Eliminar valor");
        Console.WriteLine("   4. Mostrar recorridos");
        Console.WriteLine("   5. Estadísticas (mín, máx, altura)");
        Console.WriteLine("   6. Visualizar árbol");
        Console.WriteLine("   7. Limpiar árbol");
        Console.WriteLine("   0. Salir");
        LineaSimple();
        ColorEscribir("   Seleccione una opción: ", ConsoleColor.Yellow, false);
    }

    // ── Leer entero con validación ────────────────────────────
    static bool LeerEntero(string prompt, out int resultado)
    {
        ColorEscribir(prompt, ConsoleColor.Yellow, false);
        string entrada = Console.ReadLine()?.Trim();
        if (int.TryParse(entrada, out resultado)) return true;
        Error("Entrada inválida. Debe ser un número entero.");
        return false;
    }

    // ════════════════════════════════════════════════════════
    //  MAIN
    // ════════════════════════════════════════════════════════
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "BST — Árbol Binario de Búsqueda";

        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (opcion)
            {
                // ── 1. INSERTAR ─────────────────────────────
                case "1":
                    ColorEscribir("  INSERTAR VALOR", ConsoleColor.Cyan);
                    LineaSimple();
                    if (LeerEntero("  Ingrese el valor a insertar: ", out int valIns))
                    {
                        if (arbol.Insertar(valIns))
                            Exito($"Valor {valIns} insertado correctamente.");
                        else
                            Advertencia($"El valor {valIns} ya existe en el árbol (no se permiten duplicados).");
                    }
                    break;

                // ── 2. BUSCAR ───────────────────────────────
                case "2":
                    ColorEscribir("  BUSCAR VALOR", ConsoleColor.Cyan);
                    LineaSimple();
                    if (LeerEntero("  Ingrese el valor a buscar: ", out int valBus))
                    {
                        if (arbol.Buscar(valBus))
                            Exito($"Valor {valBus} ENCONTRADO en el árbol.");
                        else
                            Error($"Valor {valBus} NO encontrado en el árbol.");
                    }
                    break;

                // ── 3. ELIMINAR ─────────────────────────────
                case "3":
                    ColorEscribir("  ELIMINAR VALOR", ConsoleColor.Cyan);
                    LineaSimple();
                    if (arbol.EstaVacio)
                    {
                        Advertencia("El árbol está vacío. No hay nada que eliminar.");
                        break;
                    }
                    if (LeerEntero("  Ingrese el valor a eliminar: ", out int valElim))
                    {
                        if (arbol.Eliminar(valElim))
                            Exito($"Valor {valElim} eliminado correctamente.");
                        else
                            Error($"Valor {valElim} NO existe en el árbol.");
                    }
                    break;

                // ── 4. RECORRIDOS ───────────────────────────
                case "4":
                    ColorEscribir("  RECORRIDOS DEL ÁRBOL", ConsoleColor.Cyan);
                    LineaSimple();
                    if (arbol.EstaVacio)
                    {
                        Advertencia("El árbol está vacío.");
                        break;
                    }
                    MostrarRecorrido("  Preorden   (Raíz → Izq → Der):", arbol.Preorden(),  ConsoleColor.Magenta);
                    MostrarRecorrido("  Inorden    (Izq → Raíz → Der):", arbol.Inorden(),   ConsoleColor.Green);
                    MostrarRecorrido("  Postorden  (Izq → Der → Raíz):", arbol.Postorden(), ConsoleColor.Yellow);
                    Info("Inorden produce los valores en orden ascendente.");
                    break;

                // ── 5. ESTADÍSTICAS ─────────────────────────
                case "5":
                    ColorEscribir("  ESTADÍSTICAS DEL ÁRBOL", ConsoleColor.Cyan);
                    LineaSimple();
                    if (arbol.EstaVacio)
                    {
                        Advertencia("El árbol está vacío.");
                        break;
                    }
                    ColorEscribir($"  Nodos  : {arbol.ContarNodos()}",   ConsoleColor.White);
                    ColorEscribir($"  Mínimo : {arbol.Minimo()}",        ConsoleColor.Green);
                    ColorEscribir($"  Máximo : {arbol.Maximo()}",        ConsoleColor.Red);
                    ColorEscribir($"  Altura : {arbol.Altura()} nivel(s)", ConsoleColor.Yellow);
                    break;

                // ── 6. VISUALIZAR ───────────────────────────
                case "6":
                    ColorEscribir("  VISUALIZACIÓN DEL ÁRBOL", ConsoleColor.Cyan);
                    LineaSimple();
                    Info("El árbol se muestra rotado 90° (raíz a la izquierda).");
                    Console.WriteLine();
                    arbol.MostrarArbol();
                    break;

                // ── 7. LIMPIAR ──────────────────────────────
                case "7":
                    ColorEscribir("  LIMPIAR ÁRBOL", ConsoleColor.Cyan);
                    LineaSimple();
                    if (arbol.EstaVacio)
                    {
                        Advertencia("El árbol ya está vacío.");
                        break;
                    }
                    ColorEscribir("  ¿Confirma que desea eliminar todos los nodos? (s/n): ",
                        ConsoleColor.Yellow, false);
                    string conf = Console.ReadLine()?.Trim().ToLower();
                    if (conf == "s" || conf == "si" || conf == "sí")
                    {
                        arbol.Limpiar();
                        Exito("Árbol limpiado. Todos los nodos han sido eliminados.");
                    }
                    else
                        Info("Operación cancelada.");
                    break;

                // ── 0. SALIR ────────────────────────────────
                case "0":
                    salir = true;
                    Console.Clear();
                    LineaDoble();
                    ColorEscribir("  Gracias por usar el BST. ¡Hasta pronto!", ConsoleColor.Cyan);
                    LineaDoble();
                    break;

                default:
                    Error("Opción no válida. Elija una opción del menú (0-7).");
                    break;
            }

            if (!salir)
            {
                Console.WriteLine();
                ColorEscribir("  Presione cualquier tecla para continuar...",
                    ConsoleColor.DarkGray, false);
                Console.ReadKey(true);
            }
        }
    }

    // ── Helper para imprimir recorridos ───────────────────────
    static void MostrarRecorrido(string titulo, List<int> valores, ConsoleColor color)
    {
        ColorEscribir(titulo, ConsoleColor.White);
        ColorEscribir("  " + string.Join("  →  ", valores), color);
        Console.WriteLine();
    }
}