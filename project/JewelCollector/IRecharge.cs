namespace JewelCollector;
/// <summary>
/// Interface que representa um objeto com capacidade de recarregar energia do jogador.
/// </summary>
public interface IRecharge
{
    /// <summary>
    /// Função que executa a recarga de energia.
    /// </summary>
    /// <param name="robot">referência para o alvo</param>
    public void recharge(Robot robot);
}
