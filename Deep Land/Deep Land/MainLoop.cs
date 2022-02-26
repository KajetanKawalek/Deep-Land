using System;
using System.Diagnostics;
using System.Numerics;

namespace Deep_Land
{
    public static class MainLoop
    {
        static int x = 2;
        static int y = 2;

        static double timeStep = 10; // In Milliseconds

        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            Start();

            
            while (true)
            {
                stopwatch.Reset();
                stopwatch.Start();

                Input();
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
            FastConsole.Init(45, 45); // One chunk = 15x15;

            World.Init(new Vector2(45, 45));
            World.LoadCells(new Vector2(16, 16));
        }

        static void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);

                if (pressed.Key == ConsoleKey.A)
                {
                    x--;
                }

                if (pressed.Key == ConsoleKey.S)
                {
                    y++;
                }

                if (pressed.Key == ConsoleKey.D)
                {
                    x++;
                }

                if (pressed.Key == ConsoleKey.W)
                {
                    y--;
                }
            }
        }

        static void PreUpdate()
        {
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
            for(int i = 0; i < 45; i++)
            {
                for (int i2 = 0; i2 < 45; i2++)
                {
                    if(World.loadedCellsArray[i, i2] != null)
                        FastConsole.WriteToBuffer(i, i2, World.loadedCellsArray[i,i2].display, World.loadedCellsArray[i, i2].color);
                    else
                        FastConsole.WriteToBuffer(i, i2, ' ', ConsoleColor.Black);
                }
            }

            FastConsole.Draw();
        }
    }
}
