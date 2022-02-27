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
        public static Cell[,] loadedCellsArray = new Cell[45, 45];

        public static void Init(Vector2 size)
        {
            worldSize = size;
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
                    string FileName = string.Format("{0}worlds\\test world\\chunk" + (middleChunk.X + i2 - 1) + "-" + (middleChunk.Y + i - 1) + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
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
                            int id = int.Parse(ch.array[i3, i4]);

                            CreateNewCell(id, new Vector2(i3 + (15 * i), i4 + (15 * i2)), cells);

                            /*if(key != 0)
                            {
                                Cell newCell = allCells[key];
                                newCell.positionInArray = new Vector2(i3 + (15 * i), i4 + (15 * i2));
                                Debug.WriteLine(new Vector2(i3 + (15 * i), i4 + (15 * i2)));

                                cells[i3 + (15 * i), i4 + (15 * i2)] = newCell;

                            }else
                            {
                                cells[i3 + (15 * i), i4 + (15 * i2)] = null;
                            }*/
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

        static void CreateNewCell(int id, Vector2 position, Cell[,] array)
        {
            switch (id)
            {
                case 0:
                    array[(int)position.X, (int)position.Y] = null;
                    break;
                case 1:
                    array[(int)position.X, (int)position.Y] = new Block("stone", '█', ConsoleColor.White, position);
                    break;
                case 2:
                    array[(int)position.X, (int)position.Y] = new Powder("gravel", '▒', ConsoleColor.Gray, position, 10);
                    break;
                case 3:
                    array[(int)position.X, (int)position.Y] = new Fluid("water", '≈', ConsoleColor.Cyan, position, 5);
                    break;
                case 4:
                    array[(int)position.X, (int)position.Y] = new Player("player", '@', ConsoleColor.Yellow, position, true, 10, 10, 1);
                    break;
            }
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
                        array[i2, i] = str[i2];
                    }
                }
            }else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int i2 = 0; i2 < 3; i2++)
                    {
                        array[i2, i] = "0";
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
