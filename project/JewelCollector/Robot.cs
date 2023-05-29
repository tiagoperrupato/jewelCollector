namespace JewelCollector;

public class Robot : Cell
{
    private string type;

    public string Type {get => type; set=> type = value;}
    public Robot(string type)
    {
        this.type = type;
    }
}
