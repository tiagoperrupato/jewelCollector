namespace JewelCollector;
/// <summary>
/// Classe que representa um objeto que funciona como obstáculo no Mapa.
/// Essa classe implementa a interface Cell.
/// </summary>
public class Obstacle : Cell
{
    public string Type {get;}
    /// <summary>
    /// Construtor do objeto.
    /// </summary>
    /// <param name="type">associa um tipo ao obstáculo</param>
    public Obstacle(string type)
    {
        Type = type;
    }
}