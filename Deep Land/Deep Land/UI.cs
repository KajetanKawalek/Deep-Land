using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Deep_Land
{
    public static class UI
    {
        static int currentPage = 1;

        public static void Update()
        {

        }

        public static void Render()
        {
            DrawBorder(new Vector2(45, 0), new Vector2(45, 45), ConsoleColor.DarkGray);

            Stats();
            Inventory();
            Chat();
            Menu();
        }

        static void Stats()
        {
            DrawBorder(new Vector2(46, 1), new Vector2(21, 10), ConsoleColor.DarkGray);
            DrawText(new Vector2(53, 1), "-STATS-", ConsoleColor.DarkGray);
            DrawText(new Vector2(47, 2), "Level: " + PlayerData.level, ConsoleColor.DarkCyan);
            DrawText(new Vector2(50, 3), "XP: " + PlayerData.xp + "/" + PlayerData.xpToNextLevel, ConsoleColor.DarkCyan);
            DrawText(new Vector2(49, 4), "Health: " + PlayerData.health + "/" + PlayerData.maxHealth, ConsoleColor.White);
            DrawText(new Vector2(49, 5), "Armour: " + PlayerData.armour, ConsoleColor.White);
            DrawText(new Vector2(51, 6), "Mana: " + PlayerData.mana + "/" + PlayerData.maxMana, ConsoleColor.White);
            DrawText(new Vector2(47, 7), "Strength: " + PlayerData.strength, ConsoleColor.White);
            DrawText(new Vector2(50, 8), "Intel: " + PlayerData.intelligence, ConsoleColor.White);
            DrawText(new Vector2(51, 9), "Dext: " + PlayerData.dexterity, ConsoleColor.White);
        }

        static void Inventory()
        {
            DrawBorder(new Vector2(46, 11), new Vector2(43, 13), ConsoleColor.DarkGray);
            DrawText(new Vector2(62, 11), "-INVENTORY-", ConsoleColor.DarkGray);
            for(int i = 0; i < 10; i++)
            {
                string itemName = " ";

                if (PlayerData.inventory[i] != null)
                    itemName = PlayerData.inventory[i + ((currentPage - 1) * 10)].name;

                if (i == 9)
                {
                    DrawText(new Vector2(47, 12 + i), "0: " + itemName, ConsoleColor.White);
                }else
                {
                    DrawText(new Vector2(47, 12 + i), (i + 1) + ": " + itemName, ConsoleColor.White);
                }
            }
            DrawText(new Vector2(62, 22), "Page " + currentPage + ": -/=", ConsoleColor.White);
        }

        static void Chat()
        {
            DrawBorder(new Vector2(46, 24), new Vector2(43, 20), ConsoleColor.DarkGray);
            DrawBorder(new Vector2(47, 40), new Vector2(41, 3), ConsoleColor.DarkGray);
            DrawText(new Vector2(64, 24), "-CHAT-", ConsoleColor.DarkGray);
            DrawText(new Vector2(48, 41), "Y:", ConsoleColor.White);
        }

        static void Menu()
        {
            DrawBorder(new Vector2(67, 1), new Vector2(22, 10), ConsoleColor.DarkGray);
            DrawText(new Vector2(75, 1), "-MENU-", ConsoleColor.DarkGray);
            DrawText(new Vector2(68, 3), "O: Options", ConsoleColor.White);
            DrawText(new Vector2(68, 4), "I: Quit", ConsoleColor.White);
        }

        static void DrawBorder(Vector2 topLeft, Vector2 size, ConsoleColor color)
        {
            size = new Vector2(size.X - 1, size.Y - 1);
            FastConsole.WriteToBuffer((int)topLeft.X, (int)topLeft.Y, '╔', color);
            FastConsole.WriteToBuffer((int)topLeft.X + (int)size.X, (int)topLeft.Y, '╗', color);
            FastConsole.WriteToBuffer((int)topLeft.X, (int)topLeft.Y + (int)size.Y, '╚', color);
            FastConsole.WriteToBuffer((int)topLeft.X + (int)size.X, (int)topLeft.Y + (int)size.Y, '╝', color);
            for(int i = 1; i < size.Y; i++)
            {
                FastConsole.WriteToBuffer((int)topLeft.X, (int)topLeft.Y + i, '║', color);
                FastConsole.WriteToBuffer((int)topLeft.X + (int)size.X, (int)topLeft.Y + i, '║', color);
            }
            for (int i = 1; i < size.X; i++)
            {
                FastConsole.WriteToBuffer((int)topLeft.X + i, (int)topLeft.Y, '═', color);
                FastConsole.WriteToBuffer((int)topLeft.X + i, (int)topLeft.Y + (int)size.Y, '═', color);
            }
        }
        
        static void DrawText(Vector2 start, string text, ConsoleColor color, bool writeFromLeft = true)
        {
            char[] characters = text.ToCharArray();

            if(writeFromLeft)
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    FastConsole.WriteToBuffer((int)start.X + i, (int)start.Y, characters[i], color);
                }
            }else
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    FastConsole.WriteToBuffer((int)start.X - i, (int)start.Y, characters[characters.Length - i - 1], color);
                }
            }
        }
    }
}
