namespace JewelCollector;

public class Map
{
    private Cell[,] board;
    public Cell[,] Board {get => board;}
    public int Level {get; set;}

    public Map(int level) 
    {
        this.board = new Cell[10, 10];
        Level = level;

        Cell newCell;
        int i, j;
        for (i = 0; i < board.GetLength(0); i++)
            for (j = 0; j < board.GetLength(1); j++)
            {
                newCell = new Empty("--");
                board[i, j] = newCell;
            }
    }
}