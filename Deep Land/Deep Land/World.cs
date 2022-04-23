using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Deep_Land
{
    static class World
    {
        public static Vector2 edgePoint { get; private set; }
        static Vector2 middlePoint;
        static Vector2 worldSize;
        public static Cell[,] loadedCellsArray = new Cell[45, 45];

        public static void Init(Vector2 size)
        {
            worldSize = size;
        }

        public static void LoadCells(Vector2 pointOfInterestPosition)
        {
            middlePoint = pointOfInterestPosition;
            edgePoint = new Vector2((((float)Math.Ceiling((middlePoint.X + 1) / 15) - 1) * 15) - 15, (((float)Math.Ceiling((middlePoint.Y + 1) / 15) - 1) * 15) - 15);
            Chunk[,] chunks = PickChunks(pointOfInterestPosition);

            loadedCellsArray = chunksToCellArray(chunks);
        }

        public static void SaveLoadedCells()
        {
            Chunk[,] chunks = CellArrayToChunks(loadedCellsArray);

            WriteChunks(chunks, new Vector2((float)Math.Ceiling((middlePoint.Y + 1) / 15), (float)Math.Ceiling((middlePoint.X + 1) / 15)));

            string RunningPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string FileName = string.Format("{0}Debug\\World\\data.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            File.WriteAllLines(FileName, new string[] { PlayerData.player.positionInWorld.ToString() } );
        }

        static Chunk[,] PickChunks(Vector2 pointOfInterestPosition)
        {
            Vector2 middleChunk = new Vector2((float)Math.Ceiling((pointOfInterestPosition.Y + 1) / 15), (float)Math.Ceiling((pointOfInterestPosition.X + 1) / 15));
            string RunningPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Chunk[,] chunks = new Chunk[3, 3];

            //string FileName = string.Format("{0}worlds\\test world\\chunk" + middleChunk.X + "-" + middleChunk.Y + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    string FileName = string.Format("{0}Debug\\World\\chunk" + (middleChunk.X + i2 - 1) + "-" + (middleChunk.Y + i - 1) + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                    if(File.Exists(FileName))
                    {
                        string[] file = File.ReadAllLines(FileName);
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

        static void WriteChunks(Chunk[,] chunks, Vector2 middleChunkPosition)
        {
            string RunningPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    string[] full = new string[15];
                    string FileName = string.Format("{0}Debug\\World\\chunk" + (middleChunkPosition.X + i2 - 1) + "-" + (middleChunkPosition.Y + i - 1) + ".txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                    Chunk ch = chunks[i, i2];
                    if (File.Exists(FileName))
                    {
                        for (int i3 = 0; i3 < 15; i3++)
                        {
                            string line = "";
                            for (int i4 = 0; i4 < 15; i4++)
                            {
                                line = line + ch.array[i3, i4] + '.';
                            }
                            line = line.Remove(line.Length - 1);
                            full[i3] = line;
                        }
                        File.WriteAllLines(FileName, full);
                    }
                }
            }

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
                            int id = 0;
                            string[] str = new string[10];
                            if (ch.array[i3, i4].Contains(","))
                            {
                                str = ch.array[i3, i4].Split(',');
                                id = int.Parse(str[0]);
                                CreateNewCell(id, new Vector2(i3 + (15 * i), i4 + (15 * i2)), cells, true, str);
                            }
                            else
                            {
                                id = int.Parse(ch.array[i3, i4]);
                                CreateNewCell(id, new Vector2(i3 + (15 * i), i4 + (15 * i2)), cells, false, str);
                            }
                        }
                    }
                }
            }

            return cells;
        }

        static Chunk[,] CellArrayToChunks(Cell[,] cells)
        {
            Chunk[,] chunks = new Chunk[3,3];

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    //Chunk ch = chunks[i, i2];
                    //Debug.WriteLineIf(ch == null, "null");
                    string[] arr = new string[15];
                    for (int i3 = 0; i3 < 15; i3++)
                    {

                        for (int i4 = 0; i4 < 15; i4++)
                        {

                            if (loadedCellsArray[i3 + (15 * i), i4 + (15 * i2)] != null)
                            {
                                arr[i3] = arr[i3] + NameToId(i3 + (15 * i), i4 + (15 * i2));
                            }
                            else
                            {
                                arr[i3] = arr[i3] + "0" + ".";
                            }
                        }
                    }
                    chunks[i, i2] = new Chunk(arr, false);
                }
            }

            return chunks;
        }

        public static void DebugChunk()
        {
            string RunningPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(RunningPath);
            Console.ReadKey();
            string FileName = string.Format("{0}Debug\\World\\chunk0-0.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            Console.WriteLine(FileName);
            Console.ReadKey();

            string[] file = System.IO.File.ReadAllLines(FileName);

            Chunk chunk = new Chunk(file, false);

            chunk.DebugWriteChunk();
            Console.ReadLine();
        }

        static void CreateNewCell(int id, Vector2 position, Cell[,] array, bool hasData, string[] data)
        {
            switch (id)
            {
                case 0:
                    array[(int)position.X, (int)position.Y] = null;
                    break;
                case 1:
                    array[(int)position.X, (int)position.Y] = new Block("stone", '█', ConsoleColor.DarkGray, position);
                    break;
                case 2:
                    array[(int)position.X, (int)position.Y] = new Powder("cobbleStone", '▒', ConsoleColor.DarkGray, position, 10);
                    break;
                case 3:
                    array[(int)position.X, (int)position.Y] = new Fluid("water", '≈', ConsoleColor.Cyan, position, 5);
                    break;
                case 4:
                    Player player = new Player("player", '⌋', ConsoleColor.DarkYellow, position, edgePoint + position, true, new Vector2(2, 4), 10, 10, 1);
                    if(hasData)
                    {
                        player.ReloadJump(data[1], int.Parse(data[2]));
                    }
                    array[(int)position.X, (int)position.Y] = player;
                    break;
                case 5:
                    array[(int)position.X, (int)position.Y] = new ItemBag("item bag", '$', ConsoleColor.Yellow, position, edgePoint + position, true, new Vector2(1, 1), new Item[] { new Item("PickAxe"), new Item("PickAxe1") , new Item("PickAxe2") , new Item("PickAxe3") , new Item("PickAxe4") , new Item("PickAxe5") , new Item("PickAxe6") , new Item("PickAxe7") , new Item("PickAxe8"), new Item("PickAxe9") }, 10, 10, 10);
                    break;
                case 6:
                    array[(int)position.X, (int)position.Y] = new Block("bedrock", '▓', ConsoleColor.DarkGray, position);
                    break;
            }
        }

        static string NameToId(int pos1, int pos2)
        {
            string name = loadedCellsArray[pos1, pos2].name;
            string id = "";

            switch (name)
            {
                case "?":
                    id = "0.";
                    break;
                case "stone":
                    id = "1.";
                    break;
                case "cobbleStone":
                    id = "2.";
                    break;
                case "water":
                    id = "3.";
                    break;
                case "player":
                    Player player = (Player)loadedCellsArray[pos1, pos2];
                    if(player.jumpTime == 0)
                    {
                        id = "4," + "f" + "," + player.jumpTime;
                    }else
                    {
                        id = "4," + "t" + "," + player.jumpTime;
                    }
                    id = id + ".";
                    break;
                case "item bag":
                    id = "5.";
                    break;
                case "bedrock":
                    id = "6.";
                    break;
            }

            return id;
        }

        public static void InstanciateAtPositionInArray(int id, Vector2 positionInArray)
        {
            if ((positionInArray.X <= 44 && positionInArray.X >= 0) && (positionInArray.Y <= 44 && positionInArray.Y >= 0))
            {
                CreateNewCell(id, positionInArray, loadedCellsArray, false, new string[10]);
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
