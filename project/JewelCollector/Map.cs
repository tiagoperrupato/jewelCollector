namespace JewelCollector;

public class Map
{
    private Cell[,] board;
    private string[,] posJewels;
    private string[,] posObstacles;
    public Cell[,] Board {get => board;}
    public int Level {get; private set;}

    public Map(int level) 
    {   
        if(level is 1)
        {
            makeInitialMap();
        }
        else
        {
            //makeRandomMap(level);
        }        
    }

    private void makeInitialMap()
    {
        posJewels = new string[6,3]
        {
            {"JR", "1", "9"},
            {"JR", "8", "8"},
            {"JG", "9", "1"},
            {"JG", "7", "6"},
            {"JB", "3", "4"},
            {"JB", "2", "1"},
        };
        posObstacles = new string[12,3]
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

    private void fillJewels(string[,] posJewels)
    {
        int i, row, column;
        Cell newCell;
        for(i = 0; i < posJewels.GetLength(0); i++)
            {
                newCell = new Jewel((posJewels[i, 0]));
                Int32.TryParse(posJewels[i, 1], out row);
                Int32.TryParse(posJewels[i, 2], out column);
                insert(newCell, row, column);
            }
    }

    private void fillObstacles(string[,] posObstacles)
    {
        int i, row, column;
        Cell newCell;
        for(i = 0; i < posObstacles.GetLength(0); i++)
            {
                newCell = new Obstacle((posObstacles[i, 0]));
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
        Console.WriteLine("Bag total itens: {0} | Bag total value: {1}", player.BagItens, player.BagValue);
        Console.WriteLine("Energy: {0}", player.Energy);
    }
}