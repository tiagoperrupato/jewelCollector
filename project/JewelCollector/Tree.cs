namespace JewelCollector;
/// <summary>
/// Classe que representa um obstáculo do tipo árvore.
/// Ela herda a classe Obstacle e implementa a interface IRecharge
/// A árvore impede que o robô ultrapasse a posição ocupada por ela, servindo como barreira.
/// Além disso, pode servir para recarregar 3 unidades de energia do robô.
/// </summary>
public class Tree : Obstacle, IRecharge
{
    /// <summary>
    /// Construtor do objeto.
    /// </summary>
    /// <param name="type">associa um tipo à árvore</param>
    public Tree(string type) : base(type) {}
    public void recharge(Robot robot)
    {
        robot.Energy += 3;
    }
}
