namespace JewelCollector;

public class Tree : Obstacle, IRecharge
{
    public Tree(string type) : base(type) {}
    public void recharge(Robot robot)
    {
        robot.Energy += 3;
    }
}
