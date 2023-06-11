namespace JewelCollector;
/// <summary>
/// Classe que representa um espaço vazio no Mapa. Implementa a interface Cell.
/// </summary>
public class Empty : Cell
{
    public string Type {get;}
    /// <summary>
    /// Construtor da Classe.
    /// </summary>
    /// <param name="type">string que representa o tipo da célula</param>
    public Empty(string type)
    {
        Type = type;
    }
}