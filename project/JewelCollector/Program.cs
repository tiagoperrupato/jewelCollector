﻿namespace JewelCollector;
public class JewelCollector {

    public static void Main() {
        
        // game execution
        bool running = true;

        string[,] posJewels = new string[6,3]
        {
            {"JR", "1", "9"},
            {"JR", "8", "8"},
            {"JG", "9", "1"},
            {"JG", "7", "6"},
            {"JB", "3", "4"},
            {"JB", "9", "4"},
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

        Map map = new Map(1);
        Robot player = fillMap(map, posJewels, posObstacles); 
        printMap(map);

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
                    if (player.Pos[0] > 0)
                        player.moveUp();
                } else if (command.Key == ConsoleKey.A) 
                {
                    if (player.Pos[1] > 0)
                        player.moveLeft();
                } else if (command.Key == ConsoleKey.S) 
                {
                    if (player.Pos[0] < map.Board.GetLength(0)-1)
                        player.moveDown();
                } else if (command.Key == ConsoleKey.D) 
                {
                    if (player.Pos[1] < map.Board.GetLength(1)-1)
                        player.moveRight();
                } else if (command.Key == ConsoleKey.G) 
                {
                    player.useItem();
                }

                printMap(map);
            }

            if (player.Energy == 0)
            {
                running = false;
                Console.WriteLine("Você perdeu =(");
            }
            
        } while (running);


        // supports funtions
        Robot fillMap(Map map, string[,] posJewels, string[,] posObstacles) 
        {
            fillJewels(map, posJewels);
            fillObstacles(map, posObstacles);
            Robot robot = addRobot(map);
            return robot;
        }
    
        void fillJewels(Map map, string[,] posJewels)
        {
            int i, row, column;
            Cell newCell;
            for(i = 0; i < posJewels.GetLength(0); i++)
                {
                    newCell = new Jewel((posJewels[i, 0]));
                    Int32.TryParse(posJewels[i, 1], out row);
                    Int32.TryParse(posJewels[i, 2], out column);
                    map.Board[row, column] = newCell;
                }
        }

        void fillObstacles(Map map, string[,] posObstacles)
        {
            int i, row, column;
            Cell newCell;
            for(i = 0; i < posObstacles.GetLength(0); i++)
                {
                    newCell = new Obstacle((posObstacles[i, 0]));
                    Int32.TryParse(posObstacles[i, 1], out row);
                    Int32.TryParse(posObstacles[i, 2], out column);
                    map.Board[row, column] = newCell;
                }
        }

        Robot addRobot(Map map)
        {
            Robot newCell = new Robot("ME", map);
            map.Board[0, 0] = newCell;
            return newCell;
        }

        void printMap(Map map)
        {
            Console.WriteLine("Level: {0}", map.Level);
            int i, j;
            string type;
            for (i = 0; i < map.Board.GetLength(0); i++)
            {
                for (j = 0; j < map.Board.GetLength(0); j++)
                {
                    type = map.Board[i, j].Type;
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

                    if (j != (map.Board.GetLength(0)-1))
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
}