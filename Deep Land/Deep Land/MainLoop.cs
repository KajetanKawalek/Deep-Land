using System;
using System.Diagnostics;
using System.Numerics;
using System.IO;

namespace Deep_Land
{
    public static class MainLoop
    {
        static double timeStep = 10; // In Milliseconds

        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            Start();

            
            while (true)
            {
                stopwatch.Reset();
                stopwatch.Start();

                PreUpdate();
                Update();
                PostUpdate();
                Render();

                while (stopwatch.ElapsedMilliseconds < timeStep) ;
            }
        }

        public static void Start()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            FastConsole.Init(90, 45); // One chunk = 15x15;

            Input.Init();

            MainMenu.Init();

            World.Init(new Vector2(45, 45));
            //World.LoadCells(new Vector2(16, 16));
            if(MainMenu.newGen)
            {
                World.LoadCells(new Vector2(WorldGeneration.PlayerStartPos.X - 15, WorldGeneration.PlayerStartPos.Y - 15));
            }
            else
            {
                string RunningPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string FileName = string.Format("{0}Debug\\World\\data.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                if (File.Exists(FileName))
                {
                    string[] file = File.ReadAllLines(FileName);
                    string[] temp = file[0].Split(',');
                    int x = 0;
                    int.TryParse(temp[0].Remove(0, 1), out x);
                    int y = 0;
                    int.TryParse(temp[1].Remove(temp[1].Length - 1, 1), out y);

                    Debug.WriteLine(x);
                    Debug.WriteLine(y);
                    World.LoadCells(new Vector2(x, y));
                }
            }

            PlayerData.AssignPlayer();

            PlayerData.AddItemToInventory(new PickAxe("PickAxe", 30, 10));
            PlayerData.AddItemToInventory(new Placeable("Stone", 30, 1));
            PlayerData.AddItemToInventory(new Placeable("Gravel", 30, 2));
            PlayerData.AddItemToInventory(new Placeable("Water", 30, 3));
        }

        static void PreUpdate()
        {
            Input.Update();
            PlayerData.PreUpdate();
            UI.PreUpdate();

            foreach (Base cell in World.loadedCellsArray)
            {
                if (cell != null)
                {
                    cell.PreUpdate();
                }
            }
        }

        static void Update()
        {
            PlayerData.Update();
            UI.Update();

            foreach(Base cell in World.loadedCellsArray)
            {
                if(cell != null)
                {
                    cell.Update();
                }
            }
        }

        static void PostUpdate()
        {
            foreach (Base cell in World.loadedCellsArray)
            {
                if (cell != null)
                {
                    cell.PostUpdate();
                }
            }
        }

        static void Render()
        {
            for(int i = 0; i < 90; i++)
            {
                for (int i2 = 0; i2 < 45; i2++)
                {
                    FastConsole.WriteToBuffer(i, i2, ' ', ConsoleColor.Black);
                }
            }

            for (int i = 0; i < 45; i++)
            {
                for (int i2 = 0; i2 < 45; i2++)
                {
                    if(World.loadedCellsArray[i, i2] != null)
                        FastConsole.WriteToBuffer(i, i2, World.loadedCellsArray[i,i2].display, World.loadedCellsArray[i, i2].color);
                    else
                        FastConsole.WriteToBuffer(i, i2, ' ', ConsoleColor.Black);
                }
            }

            PlayerData.Render();
            UI.Render();
            FastConsole.Draw();
        }
    }
}
