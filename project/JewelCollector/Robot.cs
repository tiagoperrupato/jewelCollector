namespace JewelCollector;

public class Robot : Cell
{

    public string Type {get;}
    public Map Map {get; set;}
    public int BagItens {get; set;}
    public int BagValue {get; set;}
    public int[] Pos {get; set;}
    public int Energy {get; set;}
    public Robot(string type, Map map)
    {
        Type = type;
        Map = map;
        BagItens = 0;
        BagValue = 0;
        Pos = new int[2];
        Energy = 5;
    }

    public void moveUp()
    {
        Map.Board[Pos[0], Pos[1]] = new Empty("--");
        Map.Board[Pos[0]-1, Pos[1]] = this;
        Pos[0] -= 1;
        Energy--;
    }

    public void moveDown()
    {
        Map.Board[Pos[0], Pos[1]] = new Empty("--");
        Map.Board[Pos[0]+1, Pos[1]] = this;
        Pos[0] += 1;
        Energy--;
    }

    public void moveRight()
    {
        Map.Board[Pos[0], Pos[1]] = new Empty("--");
        Map.Board[Pos[0], Pos[1]+1] = this;
        Pos[1] += 1;
        Energy--;
    }

    public void moveLeft()
    {
        Map.Board[Pos[0], Pos[1]] = new Empty("--");
        Map.Board[Pos[0], Pos[1]-1] = this;
        Pos[1] -= 1;
        Energy--;
    }

    public void useItem()
    {
        int i, j, posRow, posColumn;
        Cell cell;
        Jewel jewel;
        Obstacle obstacle;
        for (i = -1; i < 2; i++)
            for (j = -1; j < 2; j++)
            {
                if ((i == 0 | j == 0) & !(i == 0 & j == 0))
                {
                    posRow = Pos[0]+i;
                    posColumn = Pos[1]+j;
                    if ((posRow >=0 & posRow < Map.Board.GetLength(0)) &
                        (posColumn >=0 & posColumn < Map.Board.GetLength(1)))
                    {
                        cell = Map.Board[Pos[0]+i, Pos[1]+j];
                        if (cell.Type.Equals("JR") |
                            cell.Type.Equals("JG") |
                            cell.Type.Equals("JB"))
                        {
                            jewel = (Jewel)cell;
                            BagItens++;
                            BagValue += jewel.Points;
                            Energy += jewel.Energy;
                            Map.Board[Pos[0]+i, Pos[1]+j] = new Empty("--");
                        }
                        else if (cell.Type.Equals("$$") |
                                cell.Type.Equals("!!"))
                        {
                            obstacle = (Obstacle)cell;
                            Energy += obstacle.Energy;
                            if (cell.Type.Equals("$$"))
                                Map.Board[Pos[0]+i, Pos[1]+j] = new Empty("--");
                        }
                    }
                }
            }
    }
}
