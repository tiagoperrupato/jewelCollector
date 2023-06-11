namespace JewelCollector;
/// <summary>
/// Classe que representa uma jóia que pode ser coletada pelo robô.
/// Essa classe tem capacidade de dar pontos ao jogador quando coletada e implementa a interface Cell.
/// </summary>
public class Jewel : Cell
{
    public string Type {get;}
    public int Points {get; protected set;} ///< Propriedade que guarda quantos pontos essa jóia pode oferecer.
    /// <summary>
    /// Construtor da classe jóia.
    /// </summary>
    /// <param name="type">string que representa o tipo do objeto</param>
    public Jewel(string type)
    {
        Type = type;
    }
}