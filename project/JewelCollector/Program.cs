﻿namespace JewelCollector;

public class JewelCollector {
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
                if(level < 30)
                {
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
                running = false;
                Console.WriteLine($"Que pena... Voce perdeu na fase {level}");
            }
        }
        while(running);
        
        bool runLevel(int level)
        {
            bool win = false;
            map = new Map(level);

            if(level is 1)
                player = new Robot("ME", map);
            else
                player = new Robot("ME", map, player?.Energy);

            KeyAction consoleAction = new KeyAction();
            consoleAction.KeyCommand+=playerAction;

            do
            {
                printGameState(map, player);
                Console.Write("Enter the command: ");
                consoleAction.Command = Console.ReadKey().KeyChar;
                Console.Write("\n");

                if(consoleAction.Command is 'q')
                    break;

                if(player.getAllJewel())
                {
                    win = true;
                    break;
                }
            }
            while(player.hasEnergy());

            return win;
        }

        void printGameState(Map map, Robot player)
        {
            map.printMap();
            player.printStatus();
        }

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