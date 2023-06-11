namespace JewelCollector;
/// <summary>
/// Classe pai que executa todo o jogo, chamando as classes e métodos necessários para o funcionamento do jogo.
/// </summary>
public class JewelCollector {
    /// <summary>
    /// Método estático que serve de ponto inicial para o jogo.\n
    /// Caso o jogador complete todas as fases, ele encerra o jogo e indica que o jogador ganhou.
    /// Caso o jogador perca em alguma fase, ele encerra o jogo e indica que o jogador perdeu.
    /// Caso o jogador escolha sair do jogo, ele encerra o jogo e indica que o jogador perdeu.\n
    /// Possui algumas funções suportes para executar o jogo.
    /// </summary>
    public static void Main() 
    {
        
        // game execution
        bool running = true, winLevel = false;
        int level = 1;
        Map map;
        Robot? player = null;
    
        do
        {
            winLevel = runLevel(level); // retorna se passou da fase

            if(winLevel)
            {
                if(level < 21)  // caso a fase que ganhou não tenha sido a última
                {
                    Console.WriteLine($"Você passou da fase {level}");
                    level++;
                }
                else 
                {
                    running = false; // ganhou o jogo
                    Console.WriteLine("PARABENS, VOCE GANHOU O JOGO!!!");
                }
            }
            else
            {
                running = false;    // perdeu o jogo
                Console.WriteLine($"Que pena... Voce perdeu na fase {level}");
            }
        }
        while(running);
        


        // funções suportes para executar o jogo

        /// <summary>
        /// # Funções suportes:
        /// ## bool runLevel(int level)
        /// Executa a criação e chamada de comandos para um determinado nível
        /// </summary>
        /// <param name="level"> int que se refere ao nível do jogo que vai de 1 a 21 </param>
        /// <returns> retorna um boolean para caso o jogador tenha ganhado ou perdido o nível </returns>
        bool runLevel(int level)
        {
            bool win = false;
            map = new Map(level);

            if(level is 1)
                player = new Robot("ME", map); // cria um robô inicial com energia zerada
            else
                player = new Robot("ME", map, player?.Energy); // cria um robô mantendo a energia do nível anterior

            KeyAction consoleAction = new KeyAction();  // cria um evento de teclado
            consoleAction.KeyCommand+=playerAction;     // adiciona um método a ele referente à ação do jogador

            do
            {
                printGameState(map, player);    // printa o estado do jogo
                Console.Write("Enter the command: ");
                consoleAction.Command = Console.ReadKey().KeyChar;  // recebe o comando do teclado e chama o evento de teclado
                Console.Write("\n");

                if(consoleAction.Command is 'q')    // pedido para sair do jogo
                    break;

                if(player.getAllJewel())    // analisa se jogador ja pegou todas as joias
                {
                    win = true;
                    break;
                }
            }
            while(player.hasEnergy());  // executa o nível enquanto o jogador tem energia

            return win;
        }

        /// <summary>
        /// ## void printGameState(Map map, Robot player)
        /// Printa o estado do mapa e do estado do robô no respectivo nível.
        /// </summary>
        /// <param name="map">Recebe o mapa com parâmetro</param>
        /// <param name="player">Recebe o jogador como parâmetro</param>
        void printGameState(Map map, Robot player)
        {
            map.printMap();
            player.printStatus();
        }

        /// <summary>
        /// ## void playerAction(object? sender, char action)
        /// Chama o jogador para executar o comando solicitado pelo teclado. Possui algumas exceções a serem analisadas:\n
        /// * IndexOutOfRangeException
        /// * NotAllowedPositionException
        /// * NotValidCommandException
        /// </summary>
        /// <param name="sender">Por ser um método adicionado no evento, existe um parâmetro que se refere a quem envia uma mensagem do evento</param>
        /// <param name="action">Parâmetro para especificar qual foi o comando solicitado pelo jogador</param>
        void playerAction(object? sender, char action)
        {
            try
            {
                player.action(action);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("\nVocê chegou no limite do mapa.");
            }
            catch(NotAllowedPositionException)
            {
                Console.WriteLine("\nPosição Ocupada.");
            }
            catch(NotValidCommandException)
            {
                Console.WriteLine("\nComando inválido, digite novamente.");
            }
        }
    }
    /// <summary>
    /// Classe que descreve o funcionamento do Evento de teclado.
    /// Possui um Event que executa uma mensagem para o robô executar o comando solicitado pelo teclado\n
    /// o método protected virtual OnKeyCommand sere para invocar o comando solicitado. Assim sempre que é alterado
    /// o valor da propriedade Command, esse método é executado.
    /// </summary>
    public class KeyAction
    {
        private char command;
        public char Command
        {
            get=>command;
            set
            {
                command = value;
                OnKeyCommand(command);
            }
        }
        public event EventHandler<char>? KeyCommand;

        protected virtual void OnKeyCommand(char e)
        {
            KeyCommand?.Invoke(this, e);
        }
    }
}