namespace JewelCollector;

public class Jewel : Cell
{
    public string Type {get;}
    public int Points {get;}
    public int Energy {get;}
    public Jewel(string type)
    {
        Type = type;
        switch(type)
        {
            case "JR":
                Points = 100;
                Energy = 0;
                break;
            case "JG":
                Points = 50;
                Energy = 0;
                break;
            case "JB":
                Points = 10;
                Energy = 5;
                break;
        }
    }
}