using System.Security.Cryptography.X509Certificates;
using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Toy_Robot
{
    public enum Direction
    {
        NORTH, EAST, SOUTH, WEST
    }
    class Robot
    {
        int x, y, border;
        Direction facing;
        bool placed = false;
        public Robot(int tableSize)
        {
            x = -1;
            y = -1;
            facing = Direction.NORTH;
            border = tableSize;

        }

        public void Place(int x_, int y_, Direction facing_)
        {
            if ((x_ >= 0 && x_ < border) && (y_ >= 0 && y_ < border))
            {
                x = x_;
                y = y_;
                facing = facing_;
                placed = true;
            }

        }

        public void Move()
        {
            if (placed)
            {
                switch (facing)
                {
                    case Direction.NORTH:
                        if (y != border - 1) { y += 1; }
                        break;
                    case Direction.SOUTH:
                        if (y != 0) { y -= 1; }
                        break;
                    case Direction.EAST:
                        if (x != border - 1) { x += 1; }
                        break;
                    case Direction.WEST:
                        if (x != 0) { x -= 1; }
                        break;

                }
            }
        }

        public void Right()
        {
            if (placed)
                facing = (Direction)(((int)facing + 1) % 4);
        }

        public void Left()
        {
            if (placed)
                facing = (Direction)(((int)facing + 3) % 4);
        }

        public void Report()
        {
            if (placed)
                Console.WriteLine("Robot position : " + x + "," + y + "," + facing);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int tableSize = 5;
            var robot = new Robot(tableSize);

            //read command 
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestCases.txt");
            //file path - ..\Toy_robot\Toy_robot\bin\Debug\net8.0
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string command;
                    while ((command = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(command);
                        //var command = Console.ReadLine();
                        if (command == null) { break; }
                        var splitCommand = command.Split(' ');
                        if (splitCommand == null) { continue; }
                        switch (splitCommand[0])
                        {
                            case "PLACE":
                                if (splitCommand.Length != 2) { continue; }
                                var placeArg = splitCommand[1].Split(',');
                                if (placeArg == null) { continue; }
                                else if (placeArg.Length != 3) { continue; }
                                try
                                {
                                    robot.Place(int.Parse(placeArg[0]), int.Parse(placeArg[1]), (Direction)Enum.Parse(typeof(Direction), placeArg[2], true));
                                }
                                catch
                                {
                                    continue;
                                }
                                break;
                            case "MOVE":
                                robot.Move();
                                break;
                            case "LEFT":
                                robot.Left();
                                break;
                            case "RIGHT":
                                robot.Right();
                                break;
                            case "REPORT":
                                robot.Report();
                                break;
                            default: continue;
                        }

                    }

                }

                //move
            }
            catch
            {
                Console.WriteLine("File not found");
            }

        }
    }
}