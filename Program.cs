// ===== DEMO
// =====

var inventario = new Inventario();           // Crea el sujeto: el objeto que va a avisar cuando algo cambie

var dashboard = new PanelDashboard();        // Crea un observador que muestra el stock en un panel
var correo = new AlertaCorreo();             // Crea otro observador que manda alertas cuando el stock es bajo

inventario.Suscribir(dashboard);             // Apunta al dashboard en la lista de suscriptores
inventario.Suscribir(correo);                // Apunta al correo en la lista de suscriptores

Console.WriteLine("--- Entra mercadería ---");           // Título para que la salida se entienda
inventario.ActualizarStock("Martillo", 20);              // Cambia el stock a 20 y dispara el aviso a todos

Console.WriteLine("\n--- Se vende casi todo ---");       // Título (\n deja una línea en blanco antes)
inventario.ActualizarStock("Martillo", 4);               // Stock en 4: el correo sí reacciona porque 4 <= 5

Console.WriteLine("\n--- El correo se desuscribe ---");  // Título de la última parte
inventario.Desuscribir(correo);                          // Quita al correo de la lista de suscriptores
inventario.ActualizarStock("Martillo", 2);               // Stock en 2: ahora solo el dashboard se entera

// ===== PATRÓN OBSERVER =====

// 1) CONTRATO: cualquiera que quiera enterarse de los cambios debe implementar esto
public interface IObservador                                  // interface = lista de métodos que una clase está obligada a tener
{
    void Actualizar(string producto, int stock);              // Todo observador debe tener este método; aquí no se dice qué hace, solo que existe
}

// 2) SUJETO: el que guarda la lista de suscriptores y los avisa
public class Inventario                                       // Clase que representa el inventario de la tienda
{
    private readonly List<IObservador> _observadores = new(); // Lista de suscriptores. private: solo esta clase la toca; readonly: no se puede reemplazar por otra lista; new(): empieza vacía

    public void Suscribir(IObservador observador) =>          // Método para suscribirse (=> es una forma corta de escribir un método de una línea)
        _observadores.Add(observador);                        // Agrega el observador a la lista

    public void Desuscribir(IObservador observador) =>        // Método para dejar de recibir avisos
        _observadores.Remove(observador);                     // Quita el observador de la lista

    public void ActualizarStock(string producto, int cantidad) // Método que cambia el stock; es el que dispara todo
    {
        Console.WriteLine($"Inventario: {producto} cambió a {cantidad}"); // Imprime el cambio ($ permite meter variables dentro del texto con llaves)
        Notificar(producto, cantidad);                        // Avisa a todos los suscritos
    }

    private void Notificar(string producto, int stock)        // Corazón del patrón. private: solo el inventario decide cuándo avisar
    {
        foreach (var observador in _observadores)             // Recorre uno por uno a todos los suscritos
            observador.Actualizar(producto, stock);           // Le avisa a cada uno; el inventario no sabe qué hace cada observador con la noticia
    }
}

// 3) OBSERVADORES CONCRETOS: cada uno reacciona a su manera
public class PanelDashboard : IObservador                     // Los dos puntos (:) significan "implementa la interfaz IObservador"
{
    public void Actualizar(string producto, int stock) =>     // Método obligatorio por el contrato
        Console.WriteLine($"  [DASHBOARD] {producto}: {stock} unidades"); // Su reacción: siempre muestra el stock actual
}

public class AlertaCorreo : IObservador                       // Otro observador que implementa el mismo contrato
{
    public void Actualizar(string producto, int stock)        // Método obligatorio por el contrato
    {
        if (stock <= 5)                                       // Solo reacciona si el stock es 5 o menos
            Console.WriteLine($"  [CORREO] Stock bajo de {producto}, quedan {stock}"); // Su reacción: imprime la alerta de stock bajo
    }
}