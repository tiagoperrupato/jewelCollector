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

        Map map = new Map();
        fillMap(map, posJewels, posObstacles); 

        do {
            Console.WriteLine("Enter the command: ");
            ConsoleKeyInfo command = Console.ReadKey();

            if (command.Key == ConsoleKey.Q) {
                running = false;
            } 
            else 
            {
            if (command.Key == ConsoleKey.W) {
                Console.WriteLine("cima");
            } else if (command.Key == ConsoleKey.A) {
                Console.WriteLine("esquerda");
            } else if (command.Key == ConsoleKey.S) {
            Console.WriteLine("baixo");
            } else if (command.Key == ConsoleKey.D) {
            Console.WriteLine("direita");
            } else if (command.Key == ConsoleKey.G) {
                Console.WriteLine("pegou joia");
            }

            printMap(map);
            }
        } while (running);


        // supports funtions
        void fillMap(Map map, string[,] posJewels, string[,] posObstacles) 
        {
            fillJewels(map, posJewels);
            fillObstacles(map, posObstacles);
            addRobot(map);
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

        void addRobot(Map map)
        {
            Cell newCell = new Robot("ME");
            map.Board[0, 0] = newCell;
        }

        void printMap(Map map)
        {
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
        }
    }
}