using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Diagnostics;

namespace Deep_Land
{
    static class World
    {
        static Vector2 worldSize;
        static Dictionary<int, Cell> allCells = new Dictionary<int, Cell>();
        public static Cell[,] loadedCellsArray = new Cell[45, 45];

        public static void Init(Vector2 size)
        {
            worldSize = size;

            allCells.Add(1, new Block('#', ConsoleColor.White, Vector2.Zero, false));//Stone
            allCells.Add(2, new Block('&', ConsoleColor.Gray, Vector2.Zero, true));//Gravel
        }

        public static void LoadCells(Vector2 pointOfInterestPosition)
        {
            Chunk[,] chunks = PickChunks(pointOfInterestPosition);

            loadedCellsArray = chunksToCellArray(chunks);
        }

        static Chunk[,] PickChunks(Vector2 pointOfInterestPosition)
        {
            Vector2 middleChunk = new Vector2((float)Math.Ceiling(pointOfInterestPosition.X / 15), (float)Math.Ceiling(pointOfInterestPosition.Y / 15));
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            Chunk[,] chunks = new Chunk[3, 3];

            //string FileName = string.Format("{0}worlds\\test world\\chunk" + middleChunk.X + "-" + middleChunk.Y + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    string FileName = string.Format("{0}worlds\\test world\\chunk" + (middleChunk.X + i - 1) + "-" + (middleChunk.Y + i2 - 1) + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                    Debug.WriteLine(FileName);
                    if(File.Exists(FileName))
                    {
                        string[] file = System.IO.File.ReadAllLines(FileName);
                        chunks[i, i2] = new Chunk(file, false);
                    }
                    else
                    {
                        string[] empty = new string[15] {
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                            "0.0.0.0.0.0.0.0.0.0.0.0.0.0.0",
                        };
                        chunks[i, i2] = new Chunk(empty, false);
                    }
                }
            }

            return chunks;
        }

        static Cell[,] chunksToCellArray(Chunk[,] chunks)
        {
            Cell[,] cells = new Cell[45, 45];

            for(int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    Chunk ch = chunks[i, i2];
                    for (int i3 = 0; i3 < 15; i3++)
                    {
                        for (int i4 = 0; i4 < 15; i4++)
                        {
                            int key = int.Parse(ch.array[i3, i4]);

                            if(key != 0)
                            {
                                Cell newCell = allCells[key];
                                newCell.positionInArray = new Vector2(i3 + (15 * i), i4 + (15 * i2));
                                cells[i3 + (15 * i), i4 + (15 * i2)] = newCell;

                            }else
                            {
                                cells[i3 + (15 * i), i4 + (15 * i2)] = null;
                            }
                        }
                    }
                }
            }

            return cells;
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
        public string[,] array = new string[15, 15];

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
