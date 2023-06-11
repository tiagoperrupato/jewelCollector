namespace JewelCollector;

public class Radiation : Cell, IDamage
{

    public string Type {get;}
    public Radiation(string type)
    {
        Type = type;
    }

    public void damage(Robot robot)
    {
        robot.Energy -= 10;
    }

    public void destruction(Robot robot)
    {
        robot.Energy -= 30;
    }
}
