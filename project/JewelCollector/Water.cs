namespace JewelCollector;
/// <summary>
/// Classe que representa um obstáculo do tipo Água.
/// Ela herda a classe Obstacle.
/// Essa classe funciona como uma barreira impedindo que o robô ultrapasse uma posição ocupada por ela.
/// </summary>
public class Water : Obstacle
{
    /// <summary>
    /// Construtor do objeto.
    /// </summary>
    /// <param name="type">associa um tipo à água</param>
    public Water(string type) : base(type) {}
}
