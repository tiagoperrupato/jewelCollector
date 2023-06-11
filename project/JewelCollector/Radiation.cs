namespace JewelCollector;
/// <summary>
/// Classe que representa um objeto que tem característica de ser radioativo.
/// Essa classe implementa as interfaces Cell e IDamage.
/// Quando o robô chega em uma posição adjascente, ele perde 10 de energia pelo damage.
/// Quando o robô ocupa a posição da radiação, ela é destruida e retira 30 pontos de energia do robô.
/// </summary>
public class Radiation : Cell, IDamage
{

    public string Type {get;}
    /// <summary>
    /// Construtor do objeto.
    /// </summary>
    /// <param name="type">associa um tipo à radiação</param>
    public Radiation(string type)
    {
        Type = type;
    }

    public void damage(Robot robot)
    {
        robot.Energy -= 10;
    }

    public void destruction(Robot robot)
    {
        robot.Energy -= 30;
    }
}
