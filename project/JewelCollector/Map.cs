namespace JewelCollector;

public class Map
{
    private Cell[,] board;
    public Cell[,] Board {get => board;}
    public int Level {get; private set;}
    public int[] initialRobotPos{get; private set;}

    public Map(int level) 
    {   
        if(level is 1)
        {
            makeInitialMap();
        }
        else
        {
            makeRandomMap(level);
        }        
    }

    private void makeInitialMap()
    {
        initialRobotPos = new int[2] {0, 0};
        string[,] posJewels = new string[6,3]
        {
            {"JR", "1", "9"},
            {"JR", "8", "8"},
            {"JG", "9", "1"},
            {"JG", "7", "6"},
            {"JB", "3", "4"},
            {"JB", "2", "1"},
        };
        string[,] posObstacles = new string[12,3]
        {
            {"##", "5", "0"},
            {"##", "5", "1"},
            {"##", "5", "2"},
            {"##", "5", "3"},
            {"##", "5", "4"},
            {"##", "5", "5"},
            {"##", "5", "6"},
            {"$$", "5", "9"},
            {"$$", "3", "9"},
            {"$$", "8", "3"},
            {"$$", "2", "5"},
            {"$$", "1", "4"},
        };

        this.board = new Cell[10, 10];
        Level = 1;

        Cell newCell;
        int i, j;
        for (i = 0; i < board.GetLength(0); i++)
            for (j = 0; j < board.GetLength(1); j++)
            {
                newCell = new Empty("--");
                board[i, j] = newCell;
            }

        fillJewels(posJewels);
        fillObstacles(posObstacles);
    }

    private void makeRandomMap(int level)
    {
        Level = level;
        this.board = new Cell[10+level-1, 10+level-1];
        Cell newCell;
        int i, j;
        for (i = 0; i < board.GetLength(0); i++)
            for (j = 0; j < board.GetLength(1); j++)
            {
                newCell = new Empty("--");
                board[i, j] = newCell;
            }

        Random r = new Random(1);
        for(int k = 0; k < 3; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new JewelBlue("JB"), rowRandom, columnRandom);
        }
        for(int k = 0; k < 3; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new JewelGreen("JG"), rowRandom, columnRandom);
        }
        for(int k = 0; k < 3; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new JewelRed("JR"), rowRandom, columnRandom);
        }
        for(int k = 0; k < 10+2*level-1; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new Water("##"), rowRandom, columnRandom);
        }
        for(int k = 0; k < 10+2*level-1; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new Tree("$$"), rowRandom, columnRandom);
        }
        for(int k = 0; k < level-1; k++)
        {
            int rowRandom = r.Next(0, 10+level-1);
            int columnRandom = r.Next(0, 10+level-1);
            if (Board[rowRandom, columnRandom] is Empty)
                this.insert(new Radiation("!!"), rowRandom, columnRandom);
        }
        
        bool validHeroPosition = false;
        int row, column;
        do
        {
            row = r.Next(0, 10+level-1);
            column = r.Next(0, 10+level-1);
            if(Board[row, column] is Empty)
                validHeroPosition = true;
        }
        while(!validHeroPosition);
        initialRobotPos = new int[2] {row, column};
    }

    private void fillJewels(string[,] posJewels)
    {
        int i, row, column;
        Cell newCell;
        string type;
        for(i = 0; i < posJewels.GetLength(0); i++)
        {
            type = posJewels[i, 0];

            if(type is "JR")
                newCell = new JewelRed(type);
            else if(type is "JG")
                newCell = new JewelGreen(type);
            else
                newCell = new JewelBlue(type);
            
            Int32.TryParse(posJewels[i, 1], out row);
            Int32.TryParse(posJewels[i, 2], out column);
            insert(newCell, row, column);
        }
    }

    private void fillObstacles(string[,] posObstacles)
    {
        int i, row, column;
        Cell newCell;
        string type;
        for(i = 0; i < posObstacles.GetLength(0); i++)
        {
            type = posObstacles[i, 0];
            
            if(type is "##")
                newCell = new Water(type);
            else if(type is "$$")
                newCell = new Tree(type);
            else if(type is "!!")
                newCell = new Radiation(type);
            else
                newCell = new Obstacle(type);

            Int32.TryParse(posObstacles[i, 1], out row);
            Int32.TryParse(posObstacles[i, 2], out column);
            insert(newCell, row, column);
        }
    }

    public void insert(Cell newCell, int posRow, int posColumn)
    {
        Board[posRow, posColumn] = newCell;
    }

    public void printMap()
    {
        Console.WriteLine("Level: {0}", Level);
        int i, j;
        string type;
        for (i = 0; i < Board.GetLength(0); i++)
        {
            for (j = 0; j < Board.GetLength(0); j++)
            {
                type = Board[i, j].Type;
                switch(type)        // Change ConsoleColor depending on the type of Cell to print
                {
                    case "JR":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "JG":
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "JB":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "##":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "$$":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "ME":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case "!!":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "--":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                if (j != (Board.GetLength(0)-1))
                {
                    Console.Write(type);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(type);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n");
                }
            }
        }
    }
}