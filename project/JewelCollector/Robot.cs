namespace JewelCollector;

public class Robot : Cell
{

    public string Type {get;}
    public Map Map {get; set;}
    public List<Jewel> Bag{get; private set;}
    public int[] Pos {get; set;}
    public int? Energy {get; set;}
    public Robot(string type, Map map, int? energy=5)
    {
        Type = type;
        Map = map;
        Bag = new List<Jewel>();
        Pos = new int[2] {map.initialRobotPos[0], map.initialRobotPos[1]};
        map.insert(this, Pos[0], Pos[1]);
        Energy = energy;
    }

    public void action(char action)
    {
        if(action is 'w' or 'a' or 's' or 'd')
        {
            move(action);
        }
        else if(action is 'g')
        {
            getItem();
        }
        else if(action is 'q')
            return;
        else
        {
            //throw new NotValidCommandException();
        }
    }

    private void move(char mov)
    {
        switch(mov)
        {
            case 'w':
                moveUp();
                break;
            case 'a':
                moveLeft();
                break;
            case 's':
                moveDown();
                break;
            case 'd':
                moveRight();
                break;
        }
        //Energy--;
    }

    private void getItem()
    {
        int[,] searchCoords = new int[4, 2]
        {
            {Pos[0]-1, Pos[1]},
            {Pos[0], Pos[1]+1},
            {Pos[0]+1, Pos[1]},
            {Pos[0], Pos[1]-1}
        };

        for(int i = 0; i < searchCoords.GetLength(0); i++)
        {
            searchJewel(searchCoords[i, 0], searchCoords[i, 1]);
            searchRecharge(searchCoords[i, 0], searchCoords[i, 1]);
        }
    }

    private void searchJewel(int posRow, int posColumn)
    {
        if(Map.Board[posRow, posColumn] is Jewel jewel)
        {
            Bag.Add(jewel);
            Map.insert(new Empty("--"), posRow, posColumn);
            if(jewel is IRecharge r)
                r.recharge(this);
        }
    }

    private void searchRecharge(int posRow, int posColumn)
    {
        if(Map.Board[posRow, posColumn] is IRecharge r)
            r.recharge(this);
    }

    public bool getAllJewel()
    {   
        bool getAll = true;
        int numRows = Map.Board.GetLength(0);
        int numColumns = Map.Board.GetLength(1);

        int i, j;
        for(i = 0; i < numRows; i++)
        {
            for(j = 0; j < numColumns; j++)
            {
                if(Map.Board[i, j] is Jewel jewel)
                {
                    getAll = false;
                    return getAll;
                }
            }
        }

        return getAll;
    }

    public bool hasEnergy()
    {
        if(Energy > 0) return true;
        else return false;
    }

    private void moveUp()
    {   
        Map.insert(this, Pos[0]-1, Pos[1]);
        Map.insert(new Empty("--"), Pos[0], Pos[1]);
        Pos[0] -= 1;
    }

    private void moveDown()
    {   
        Map.insert(this, Pos[0]+1, Pos[1]);
        Map.insert(new Empty("--"), Pos[0], Pos[1]);
        Pos[0] += 1;
    }

    private void moveRight()
    {
        Map.Board[Pos[0], Pos[1]+1] = this;
        Map.Board[Pos[0], Pos[1]] = new Empty("--");
        Pos[1] += 1;
    }

    private void moveLeft()
    {
        Map.insert(this, Pos[0], Pos[1]-1);
        Map.insert(new Empty("--"), Pos[0], Pos[1]);
        Pos[1] -= 1;
    }

    public void printStatus()
    {
        int totalPoints = 0;
        foreach(Jewel jewel in Bag)
        {
            totalPoints += jewel.Points;
        }
        Console.WriteLine($"Bag total items: {Bag.Count} | Bag total value: {totalPoints}");
        Console.WriteLine($"Remaining energy: {Energy}");
    }
}
