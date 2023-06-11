namespace JewelCollector;
/// <summary>
/// Classe que representa uma jóia do tipo vermelho que possui capacidade de dar pontos ao jogador.
/// Ela herda a classe Jewel.
/// Ao ser coletada pode dar 100 pontos ao jogador.
/// </summary>
public class JewelRed : Jewel
{
    /// <summary>
    /// Construtor da Classe. Associa a quantidade de pontos a sua propriedade.
    /// </summary>
    /// <param name="type">string que representa o tipo de célula</param>
    public JewelRed(string type) : base(type)
    {
        Points = 100;
    }
}
