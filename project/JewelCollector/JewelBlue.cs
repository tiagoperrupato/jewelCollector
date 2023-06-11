namespace JewelCollector;
/// <summary>
/// Classe que representa uma jóia do tipo azul que possui capacidade de dar pontos e energia ao jogador.
/// Ela herda a classe Jewel e implementa a interface IRecharge.
/// Essa joia pode dar 100 pontos ao jogador e 5 unidades de energia ao ser coletada.
/// </summary>
public class JewelBlue : Jewel, IRecharge
{
    /// <summary>
    /// Construtor da Classe. Associa a quantidade de pontos a sua propriedade.
    /// </summary>
    /// <param name="type">string que representa o tipo de célula</param>
    public JewelBlue(string type) : base(type)
    {
        Points = 10;
    }

    public void recharge(Robot robot)
    {
        robot.Energy += 5;
    }
}
