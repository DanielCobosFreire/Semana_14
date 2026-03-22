Estructura del código
class Nodo — almacena el valor entero y las referencias Izquierdo / Derecho.
class ArbolBST — contiene toda la lógica del árbol:
MétodoDescripciónInsertar(int)Inserción recursiva, rechaza duplicadosBuscar(int)Búsqueda recursiva O(log n) promedioEliminar(int)Maneja los 3 casos: sin hijos, un hijo, dos hijos (sucesor inorden)Inorden / Preorden / PostordenDevuelven List<int> con el recorridoMinimo() / Maximo()Recorren el extremo izquierdo/derechoAltura()Recursiva, retorna -1 para árbol vacíoMostrarArbol()Imprime el árbol rotado 90° con caracteres UnicodeLimpiar()Asigna null a la raíz (el GC libera el resto)
class Program — menú interactivo con colores por consola:

🟢 Verde → operaciones exitosas
🔴 Rojo → errores / no encontrado
🟡 Amarillo → advertencias / prompts
🔵 Cian → títulos de sección
