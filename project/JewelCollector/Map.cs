namespace JewelCollector;
/// <summary>
/// Classe que representa o mapa do jogo.
/// Nessa classe existem formas de montar esse mapa a depender do nível, que pode ir de 1 a 21.
/// Além disso, existem métodos para alterar sua disposição e printar seu estado.
/// </summary>
public class Map
{
    private Cell[,] board;
    public Cell[,] Board {get => board;} ///< Matriz de Cell para representar fisicamente o Mapa
    public int Level {get; private set;} ///< Nível atual do Mapa
    public int[] initialRobotPos{get; private set;} ///< Posição inicial do Robô para criar o Mapa

    /// <summary>
    /// Construtor do objeto. 
    /// Pode assumir dois modos, o fixo (caso seja no nível 1) ou o aleatório (para os demais).
    /// </summary>
    /// <param name="level">identifica o nível do mapa</param>
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

    /// <summary>
    /// Função que cria o mapa inicial, que tem um formato fixo.
    /// </summary>
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

    /// <summary>
    /// Função que cria um mapa aleatório a depender do nível especificado.
    /// </summary>
    /// <param name="level">identifica o nível do mapa</param>
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

    /// <summary>
    /// Método que adiciona as jóias do mapa inicial
    /// </summary>
    /// <param name="posJewels">Vetor de string que representa o tipo de joia e sua posição</param>
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

    /// <summary>
    /// Método que adiciona os obstáculos do mapa inicial.
    /// </summary>
    /// <param name="posObstacles">Vetor de string que representa o tipo de obstáculo e sua posição</param>
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

    /// <summary>
    /// Método para inserir uma célula em uma determinada posição do Mapa.
    /// </summary>
    /// <param name="newCell">referência para a célula a ser adicionada</param>
    /// <param name="posRow">posição da linha a ser adicionada</param>
    /// <param name="posColumn">posição da coluna a ser adicionada</param>
    public void insert(Cell newCell, int posRow, int posColumn)
    {
        Board[posRow, posColumn] = newCell;
    }

    /// <summary>
    /// Método para printar no Console o estado atual do Mapa. 
    /// Foi feita uma associação de cores para deixar mais visualizável o jogo.
    /// </summary>
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