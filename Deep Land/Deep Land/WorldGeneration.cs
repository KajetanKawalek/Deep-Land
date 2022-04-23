using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Diagnostics;

namespace Deep_Land
{
    static class WorldGeneration
    {
        public static Vector2 PlayerStartPos;

        static Random rand = new Random(Guid.NewGuid().GetHashCode());

        static string[,] world;

        public static void GenerateWorld(Vector2 size)
        {
            world = new string[(int)size.X * 15, (int)size.Y * 15];

            for (int i = 0; i < size.X * 15; i++)
            {
                for (int i2 = 0; i2 < size.Y * 15; i2++)
                {
                    world[i, i2] = "1.";
                }
            }

            for(int i = 0; i < (size.X * size.Y) / 25; i++)
            {
                GenerateCave();
            }
            for (int i = 0; i < (size.X * size.Y) / 18; i++)
            {
                GenerateGravel();
            }
            for (int i = 0; i < (size.X * size.Y) / 18; i++)
            {
                GenerateWater();
            }

            GenerateBorder();

            CreatePlayerAtRandomPoint();

            WriteToChunks();
        }

        static void GenerateCave()
        {
            Vector2 startPoint = new Vector2(rand.Next(0, world.GetLength(0) - 1), rand.Next(0, world.GetLength(1) - 1));
            int length = rand.Next(0, 2000);
            Vector2 point = startPoint;

            for (int i = 0; i < length; i++)
            {
                point.X += rand.Next(1, 4) - 2;
                point.Y += rand.Next(1, 4) - 2;

                int size = 4;

                for (int i2 = 0; i2 < size; i2++)
                {
                    for (int i3 = 0; i3 < size; i3++)
                    {
                        if ((int)point.X + i2 >= 0 && (int)point.X + i2 < world.GetLength(0) && (int)point.Y + i3 >= 0 && (int)point.Y + i3 < world.GetLength(1))
                        {
                            world[(int)point.X + i2, (int)point.Y + i3] = "0.";
                        }
                    }
                }
            }
        }

        static void GenerateGravel()
        {
            Vector2 startPoint = new Vector2(rand.Next(0, world.GetLength(0) - 1), rand.Next(0, world.GetLength(1) - 1));
            int length = rand.Next(0, 100);
            Vector2 point = startPoint;

            for (int i = 0; i < length; i++)
            {
                point.X += rand.Next(1, 4) - 2;
                point.Y += rand.Next(1, 4) - 2;

                int size = rand.Next(1, 2);

                for (int i2 = 0; i2 < size; i2++)
                {
                    for (int i3 = 0; i3 < size; i3++)
                    {
                        if ((int)point.X + i2 >= 0 && (int)point.X + i2 < world.GetLength(0) && (int)point.Y + i3 >= 0 && (int)point.Y + i3 < world.GetLength(1))
                        {
                            world[(int)point.X + i2, (int)point.Y + i3] = "2.";
                        }
                    }
                }
            }
        }

        static void GenerateWater()
        {
            Vector2 startPoint = new Vector2(rand.Next(0, world.GetLength(0) - 1), rand.Next(0, world.GetLength(1) - 1));
            int length = rand.Next(0, 100);
            Vector2 point = startPoint;

            for (int i = 0; i < length; i++)
            {
                point.X += rand.Next(1, 4) - 2;
                point.Y += rand.Next(1, 4) - 2;

                int size = rand.Next(1, 4);

                for (int i2 = 0; i2 < size; i2++)
                {
                    for (int i3 = 0; i3 < size; i3++)
                    {
                        if ((int)point.X + i2 >= 0 && (int)point.X + i2 < world.GetLength(0) && (int)point.Y + i3 >= 0 && (int)point.Y + i3 < world.GetLength(1))
                        {
                            world[(int)point.X + i2, (int)point.Y + i3] = "3.";
                        }
                    }
                }
            }
        }

        static void GenerateBorder()
        {
            for (int i = 0; i < world.GetLength(0); i++)
            {
                world[i, 0] = "6.";
                world[i, world.GetLength(0) - 1] = "6.";
            }

            for (int i = 0; i < world.GetLength(1); i++)
            {
                world[0, i] = "6.";
                world[world.GetLength(1) - 1, i] = "6.";
            }
        }

        static void CreatePlayerAtRandomPoint()
        {
            Vector2 point = new Vector2(rand.Next(0, world.GetLength(0) - 3), rand.Next(4, world.GetLength(1) - 1));
            while(!CheckEmpty(point, new Vector2(2, 4)))
            {
                Debug.WriteLine("re roll");
                point = new Vector2(rand.Next(0, world.GetLength(0) - 3), rand.Next(4, world.GetLength(1) - 1));
            }

            world[(int)point.X, (int)point.Y] = "4.";
            PlayerStartPos = new Vector2((int)point.Y, (int)point.X);
        }

        static void WriteToChunks()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            for (int i = 0; i < world.GetLength(0) / 15; i++)
            {
                for (int i2 = 0; i2 < world.GetLength(1) / 15; i2++)
                {
                    string[] full = new string[15];
                    string FileName = string.Format("{0}worlds\\test world\\chunk" + i + "-" + i2 + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

                    for (int i3 = 0; i3 < 15; i3++)
                    {
                        string line = "";
                        for (int i4 = 0; i4 < 15; i4++)
                        {
                            line = line + world[(i * 15) + i3, (i2 * 15) + i4];
                        }
                        line = line.Remove(line.Length - 1);
                        full[i3] = line;
                    }
                    File.WriteAllLines(FileName, full);
                }
            }
        }

        static bool CheckEmpty(Vector2 point, Vector2 size) //This sometimes doesnt work
        {
            for (int i = 0; i < size.X; i++)
            {
                for (int i2 = 0; i2 > -size.Y; i2--)
                {
                    if(world[(int)point.X + i2, (int)point.Y + i] != "0.")
                    {
                        Debug.WriteLine("no room");
                        return false;
                    }
                    Debug.WriteLine(world[(int)point.X + i, (int)point.Y + i2]);
                }
            }

            Debug.WriteLine("is room");
            return true;
        }
    }
}
