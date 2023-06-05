namespace JewelCollector;

public class Obstacle : Cell
{
    public string Type {get;}
    public int Energy{get;}
    public Obstacle(string type)
    {
        Type = type;
        switch(type)
        {
            case "$$":
                Energy = 3;
                break;
            case "##":
                Energy = 0;
                break;
            case "!!":
                Energy = -10;
                break;
        }
    }
}