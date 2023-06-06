namespace JewelCollector;

public class JewelBlue : Jewel, IRecharge
{
    public JewelBlue(string type) : base(type)
    {
        Points = 100;
    }

    public void recharge(Robot robot)
    {
        robot.Energy += 5;
    }
}
