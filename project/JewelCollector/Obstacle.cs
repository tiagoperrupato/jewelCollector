namespace JewelCollector;

public class Obstacle : Cell
{
    private string type;

    public string Type {get => type; set=> type = value;}
    public Obstacle(string type)
    {
        this.type = type;
    }
}