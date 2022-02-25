using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace Deep_Land
{
    static class World
    {
        public static Vector2 worldSize;

        public static void LoadCells(Vector2 cameraPosition)
        {
            Chunk[,] chunks = PickChunks(new Vector2(16,16));
        }

        static Chunk[,] PickChunks(Vector2 pointOfIntrestPosition)
        {
            Vector2 middleChunk = new Vector2((float)Math.Ceiling(pointOfIntrestPosition.X / 15), (float)Math.Ceiling(pointOfIntrestPosition.Y / 15));
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            Chunk[,] chunks = new Chunk[3, 3];

            //string FileName = string.Format("{0}worlds\\test world\\chunk" + middleChunk.X + "-" + middleChunk.Y + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    string FileName = string.Format("{0}worlds\\test world\\chunk" + (middleChunk.X + i - 1) + "-" + (middleChunk.Y + i2 - 1) + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                    string[] file = System.IO.File.ReadAllLines(FileName);
                    chunks[i, i2] = new Chunk(file, false);
                }
            }

            return chunks;
        }

        public static void DebugChunk()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(RunningPath);
            Console.ReadKey();
            string FileName = string.Format("{0}worlds\\test world\\chunk0-0.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            Console.WriteLine(FileName);
            Console.ReadKey();

            string[] file = System.IO.File.ReadAllLines(FileName);

            Chunk chunk = new Chunk(file, false);

            chunk.DebugWriteChunk();
            Console.ReadLine();
        }
    }

    class Chunk
    {
        string[,] array = new string[15, 15];

        public Chunk(string[] stringArray, bool empty)
        {
            if(!empty)
            {
                for (int i = 0; i < 15; i++)
                {
                    string[] str = stringArray[i].Split('.');
                    for (int i2 = 0; i2 < 15; i2++)
                    {
                        array[i, i2] = str[i2];
                    }
                }
            }else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int i2 = 0; i2 < 3; i2++)
                    {
                        array[i, i2] = "0";
                    }
                }
            }
        }

        public void DebugWriteChunk()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int i2 = 0; i2 < 15; i2++)
                {
                    Console.Write(array[i, i2]);
                }
                Console.Write("\n");
            }
        }
    }
}
