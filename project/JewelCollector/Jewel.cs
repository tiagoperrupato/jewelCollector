namespace JewelCollector;

public class Jewel : Cell
{
    private string type;

    public string Type {get => type; set=> type = value;}
    public Jewel(string type)
    {
        this.type = type;
    }
}