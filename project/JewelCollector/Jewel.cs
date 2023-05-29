namespace JewelCollector;

public class Jewel : Cell
{
    public string Type {get;}
    public int Points {get;}
    public Jewel(string type)
    {
        Type = type;
        switch(type)
        {
            case "JR":
                Points = 100;
                break;
            case "JG":
                Points = 50;
                break;
            case "JB":
                Points = 10;
                break;
        }
    }
}