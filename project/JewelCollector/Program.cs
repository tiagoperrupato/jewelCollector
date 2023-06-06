﻿namespace JewelCollector;
public class JewelCollector {
    public static void Main() 
    {
        
        // game execution
        bool running = true, winLevel = false;
        int level = 1;
        Map map;
        Robot player;
        Console.WriteLine((ConsoleKey.Q.));
    
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
            player = new Robot();
            KeyAction consoleAction = new KeyAction();
            consoleAction.KeyCommand+=playerAction;

            do
            {
                printGameState(map, player);
                Console.Write("Enter the command: ");
                consoleAction.Command = Console.ReadKey().KeyChar;
                Console.Write("\n");

                if(player.getAllJewel())
                {
                    win = true;
                    break;
                }
            }
            while(player.hasEnergy());







            void printGameState(Map map, Robot player)
            {
                map.printMap();
                player.printStatus();
            }

            void playerAction(object? sender, char action)
            {
                player.action(action);
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


















        while(running)
        {
            Map map = new Map(level);



            

            
            Robot player = fillMap(map, posJewels, posObstacles); 
            printMap(map, player);

            do {
                Console.Write("Enter the command: ");
                ConsoleKeyInfo command = Console.ReadKey();
                Console.Write("\n");

                if (command.Key == ConsoleKey.Q) {
                    running = false;
                } 
                else 
                {
                    if (command.Key == ConsoleKey.W) 
                    {    
                        try
                        {
                            player.moveUp();
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Console.WriteLine("Você já está no limite do mapa");
                        }

                    } else if (command.Key == ConsoleKey.A) 
                    {
                        try
                        {
                            player.moveLeft();
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Console.WriteLine("Você já está no limite do mapa");
                        }
                    } else if (command.Key == ConsoleKey.S) 
                    {
                        try
                        {
                            player.moveDown();
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Console.WriteLine("Você já está no limite do mapa");
                        }
                    } else if (command.Key == ConsoleKey.D) 
                    {
                        try
                        {
                            player.moveRight();
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Console.WriteLine("Você já está no limite do mapa");
                        }
                    } else if (command.Key == ConsoleKey.G) 
                    {
                        player.useItem();
                    }

                    printMap(map, player);
                }

                if (player.Energy == 0)
                {
                    running = false;
                    Console.WriteLine("Você perdeu =(");
                }

            } while (running);
        }


        // supports funtions
        Robot fillMap(Map map, string[,] posJewels, string[,] posObstacles) 
        {
            fillJewels(map, posJewels);
            fillObstacles(map, posObstacles);
            Robot robot = addRobot(map);
            return robot;
        }

        Robot addRobot(Map map)
        {
            Robot newCell = new Robot("ME", map);
            map.Board[0, 0] = newCell;
            return newCell;
        }

        
    }
}