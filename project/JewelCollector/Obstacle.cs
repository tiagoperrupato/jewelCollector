namespace JewelCollector;

public class Obstacle : Cell
{
    public string Type {get;}
    public Obstacle(string type)
    {
        Type = type;
    }
}