namespace JewelCollector;
/// <summary>
/// Interface para representar um objeto que tem capacidade de causar dano ao robô.
/// </summary>
public interface IDamage
{
    /// <summary>
    /// Função que executa um dano no alvo.
    /// </summary>
    /// <param name="robot">referência para o alvo</param>
    public void damage(Robot robot);
    /// <summary>
    /// Função que executa quando esse objeto é destruido.
    /// </summary>
    /// <param name="robot">referência para o alvo</param>
    public void destruction(Robot robot);
}
