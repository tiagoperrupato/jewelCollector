namespace JewelCollector;

public class Empty : Cell
{
    private string type;

    public string Type {get => type; set=> type = value;}
    public Empty(string type)
    {
        this.type = type;
    }
}