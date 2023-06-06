namespace JewelCollector;

public class Jewel : Cell
{
    public string Type {get;}
    public int Points {get; protected set;}
    public Jewel(string type)
    {
        Type = type;
    }
}