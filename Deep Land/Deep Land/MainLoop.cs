using System;
using System.Diagnostics;
using System.Numerics;

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

        static void Start()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            FastConsole.Init(90, 45); // One chunk = 15x15;

            Input.Init();

            WorldGeneration.GenerateWorld(new Vector2(15, 15)); //333 x 333 will be final

            World.Init(new Vector2(45, 45));
            //World.LoadCells(new Vector2(16, 16));
            World.LoadCells(new Vector2(WorldGeneration.PlayerStartPos.X - 15, WorldGeneration.PlayerStartPos.Y - 15));

            PlayerData.AssignPlayer();

            PlayerData.AddItemToInventory(new PickAxe("Ol' Reliable", 30, 10));
            //World.InstanciateAtPositionInArray(4, new Vector2(16, 16));
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
