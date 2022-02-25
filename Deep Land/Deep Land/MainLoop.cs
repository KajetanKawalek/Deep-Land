using System;
using System.Diagnostics;

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

                //Cell cell = new Cell();
                //cell.Update();
                Input();
                Update();
                Render();

                while (stopwatch.ElapsedMilliseconds < timeStep) ;
            }
        }

        static void Start()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            FastConsole.Init(50, 50);
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

        static int count;

        static void Update()
        {
            
            count++;
            if(count < 50)
            {
                FastConsole.WriteToBuffer(x, y, '@', ConsoleColor.Red);
            }
            else if(count < 100)
            {
                FastConsole.WriteToBuffer(x, y, '#', ConsoleColor.Green);
            }
            else
            {
                count = 0;
            }
        }

        static void Render()
        {
            FastConsole.Draw();
        }
    }
}
