using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    static class MainMenu
    {
        static bool selecting = true;

        public static bool newGen;

        public static void Init()
        {
            newGen = false;
            UI.DrawBorder(new Vector2(0, 0), new Vector2(90, 45), ConsoleColor.DarkGray);

            UI.DrawText(new Vector2(1, 1), "▓▓  ▓▓▓ ▓▓▓ ▓▓▓     ▓   ▓▓▓ ▓▓▓ ▓▓", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 2), "▓ ▓ ▓   ▓   ▓ ▓ ╱╲  ▓   ▓ ▓ ▓ ▓ ▓ ▓", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 3), "▓ ▓ ▓▓▓ ▓▓▓ ▓▓▓ ··  ▓   ▓▓▓ ▓ ▓ ▓ ▓", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 4), "▓ ▓ ▓   ▓   ▓   ⎛⎞  ▓   ▓ ▓ ▓ ▓ ▓ ▓", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 5), "▓▓  ▓▓▓ ▓▓▓ ▓   ⌋⌊  ▓▓▓ ▓ ▓ ▓ ▓ ▓▓", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 7), "1: New World", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 8), "2: Continue", ConsoleColor.White);
            UI.DrawText(new Vector2(1, 9), "3: Quit", ConsoleColor.White);

            int num = 2;

            FastConsole.Draw();

            while (selecting)
            {
                Input.Update();
                if (Input.PressedKeys[Input.Keys.Key1])
                {
                    num = 0;
                    selecting = false;
                }
                else if (Input.PressedKeys[Input.Keys.Key2])
                {
                    num = 1;
                    selecting = false;
                }
                else if(Input.PressedKeys[Input.Keys.Key3])
                {
                    num = 2;
                    selecting = false;
                }
            }

            if (num == 0)
            {
                WorldGeneration.GenerateWorld(new Vector2(15, 15)); //333 x 333 will be final
                newGen = true;
            }
            if (num == 2)
            {
                Environment.Exit(0);
            }
        }
    }
}
