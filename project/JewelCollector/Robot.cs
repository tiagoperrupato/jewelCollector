namespace JewelCollector;
/// <summary>
/// Classe que representa o robô no jogo, que é controlado pelo jogador.
/// Essa classe implementa a interface Cell para ser visualizável no mapa.
/// Existem métodos para executar os comandos solicitados pelo teclado, como fazer movimentações, pegar itens pelo mapa e sair do jogo.
/// </summary>
public class Robot : Cell
{

    public string Type {get;}
    public Map Map {get; set;} ///< referência para o objeto que representa o Mapa do jogo.
    public List<Jewel> Bag{get; private set;}   ///< lista de Jóias coletadas pelo robô.
    public int[] Pos {get; set;}    ///< vetor da posição atual do robô o Mapa.
    public int? Energy {get; set;} ///< Quantidade de energia do robô.
    
    /// <summary>
    /// Construtor do objeto.
    /// </summary>
    /// <param name="type">associa um tipo ao robô</param>
    /// <param name="map">cria a referência para o mapa</param>
    /// <param name="energy">parâmetro opicional que representa a quantidade de energia do robô (por padrão é 5)</param>
    public Robot(string type, Map map, int? energy=5)
    {
        Type = type;
        Map = map;
        Bag = new List<Jewel>();
        Pos = new int[2] {map.initialRobotPos[0], map.initialRobotPos[1]};
        map.insert(this, Pos[0], Pos[1]);
        Energy = energy;
    }

    /// <summary>
    /// Método que executa o comand osolicitado pelo teclado e verifica se ele foi válido.
    /// Esse comando pode ser de movimentação para cima, baixo, direita e esquerda, ou para coletar um item no mapa, ou para sair do jogo.
    /// </summary>
    /// <param name="action"></param>
    public void action(char action)
    {
        if(action is 'w' or 'a' or 's' or 'd')
        {
            move(action);
            receiveDamage();
        }
        else if(action is 'g')
        {
            getItem();
        }
        else if(action is 'q')
            return;
        else
        {
            throw new NotValidCommandException();
        }
    }

    // função que analisa se o robô receberá dano de algum objeto que esteja em uma posição adjascente a ele.
    private void receiveDamage()
    {
        int[,] searchCoords = new int[4, 2]
        {
            {Pos[0]-1, Pos[1]},
            {Pos[0], Pos[1]+1},
            {Pos[0]+1, Pos[1]},
            {Pos[0], Pos[1]-1}
        };

        for(int i = 0; i < searchCoords.GetLength(0); i++)
            if(Map.Board[searchCoords[i, 0], searchCoords[i, 1]] is  IDamage cell)
                cell.damage(this);
    }
    // executa a movimentação solicitada.
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
        Energy--;
    }

    // procura um item nas posições adjascentes ao robô e pega ele caso encontre.
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
            int row = searchCoords[i, 0];
            int column = searchCoords[i, 1];

            if(row >= 0 && row < Map.Board.GetLength(0) && column >=0 && column < Map.Board.GetLength(1))
            {
                searchJewel(row, column);
                searchRecharge(row, column);
            }
        }
    }

    // procura por joias em uma determinada posição e pega ela caso encontre.
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

    // Procura por objetos de recarga em uma determinada posição e executa a recarga caso encontre.
    private void searchRecharge(int posRow, int posColumn)
    {
        if(Map.Board[posRow, posColumn] is IRecharge r)
        {
            Console.WriteLine("Entrei aqui.");
            r.recharge(this);
        }
    }

    /// <summary>
    /// Método para analisar se o robô já pegou todas as jóias do Mapa.
    /// Esse método é executado sempre após um comando de teclado
    /// </summary>
    /// <returns>boolean para caso tenha ou não coletado todas as jóias</returns>
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

    /// <summary>
    /// Método para verificar se o robô ainda tem energia para executar comandos no jogo.
    /// É analisado sempre ao final de uma rodada e antes do próximo comando.
    /// </summary>
    /// <returns>boolean se ainda tem ou não energia para jogar</returns>
    public bool hasEnergy()
    {
        if(Energy > 0) return true;
        else return false;
    }

    // método para se movimentar para cima.
    private void moveUp()
    {   
        if(Map.Board[Pos[0]-1, Pos[1]] is not (Obstacle or Jewel))
        {
            if(Map.Board[Pos[0]-1, Pos[1]] is IDamage cell)
                cell.destruction(this);
            Map.insert(this, Pos[0]-1, Pos[1]);
            Map.insert(new Empty("--"), Pos[0], Pos[1]);
            Pos[0] -= 1;
        }
        else
        {
            throw new NotAllowedPositionException();
        }
    }

    // método para se movimentar para baixo
    private void moveDown()
    {   
        if(Map.Board[Pos[0]+1, Pos[1]] is not (Obstacle or Jewel))
        {
            if(Map.Board[Pos[0]+1, Pos[1]] is IDamage cell)
                cell.destruction(this);
            Map.insert(this, Pos[0]+1, Pos[1]);
            Map.insert(new Empty("--"), Pos[0], Pos[1]);
            Pos[0] += 1;
        }
        else
        {
            throw new NotAllowedPositionException();
        }
    }

    // método para se movimentar para a direita
    private void moveRight()
    {
        if(Map.Board[Pos[0], Pos[1]+1] is not (Obstacle or Jewel))
        {
            if(Map.Board[Pos[0], Pos[1]+1] is IDamage cell)
                cell.destruction(this);
            Map.Board[Pos[0], Pos[1]+1] = this;
            Map.Board[Pos[0], Pos[1]] = new Empty("--");
            Pos[1] += 1;
        }
        else
        {
            throw new NotAllowedPositionException();
        }
    }

    // método para de movimentar para a esquerda.
    private void moveLeft()
    {
        if(Map.Board[Pos[0], Pos[1]-1] is not (Obstacle or Jewel))
        {
            if(Map.Board[Pos[0], Pos[1]-1] is IDamage cell)
                cell.destruction(this);
            Map.insert(this, Pos[0], Pos[1]-1);
            Map.insert(new Empty("--"), Pos[0], Pos[1]);
            Pos[1] -= 1;
        }
        else
        {
            throw new NotAllowedPositionException();
        }
    }

    /// <summary>
    /// Método para printar os status atuais do robô em uma rodada.
    /// Mostra informações como A quantidade pontos totais, a quantidade de jóias coletadas e a energia restante.
    /// </summary>
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
