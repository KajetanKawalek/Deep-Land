using System;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Deep_Land
{
    public static class UI
    {
        static int currentPage = 1;
        static int promptPage = 1;

        public static bool showPrompt;

        static string promptText = "Hello";
        static string[] promptDisplayText;
        static string promptLabel = "-Prompt-";
        static string[] promptOptions = {"Option 1", "Option 2" };
        static string[] promptDisplayOptions;
        static int promptOptionsStartPage;
        static int promptOptionsDisplayed = 0;
        static Action[][] promptActions;
        static int promptTextEnd;
        static int maxOptionsPerPage;

        static bool promptDisplayOptionsBool;

        static int numberPressed = -1;
        static bool boolNumberPressed = true;

        static int changePage = 0;
        static bool boolChangePage = true;

        static bool hasNextPage;

        public static void PreUpdate()
        {
            numberPressed = -1;
            if (Input.PressedKeys[Input.Keys.Key0])
                numberPressed = 0;
            if (Input.PressedKeys[Input.Keys.Key1])
                numberPressed = 1;
            if (Input.PressedKeys[Input.Keys.Key2])
                numberPressed = 2;
            if (Input.PressedKeys[Input.Keys.Key3])
                numberPressed = 3;
            if (Input.PressedKeys[Input.Keys.Key4])
                numberPressed = 4;
            if (Input.PressedKeys[Input.Keys.Key5])
                numberPressed = 5;
            if (Input.PressedKeys[Input.Keys.Key6])
                numberPressed = 6;
            if (Input.PressedKeys[Input.Keys.Key7])
                numberPressed = 7;
            if (Input.PressedKeys[Input.Keys.Key8])
                numberPressed = 8;
            if (Input.PressedKeys[Input.Keys.Key9])
                numberPressed = 9;

            if (Input.PressedKeys[Input.Keys.Plus])
                changePage = 1;

            if (Input.PressedKeys[Input.Keys.Minus])
                changePage = -1;
        }

        public static void Update()
        {
            if (!showPrompt)
            {
                InventoryScript();
            }
            else
            {
                PromptScript();
            }
        }

        public static void Render()
        {
            DrawBorder(new Vector2(45, 0), new Vector2(45, 45), ConsoleColor.DarkGray);

            Stats();
            if(!showPrompt)
            {
                Inventory();
            }else
            {
                Prompt();
            }
            Chat();
            Menu();
        }

        static void PromptScript()
        {
            promptDisplayText = WrapText(promptText, 41).Skip(((promptPage - 1) * 9)).ToArray();

            if (promptDisplayText.Length > 9)
            {
                hasNextPage = true;
            }
            else
            {
                promptOptionsStartPage = promptPage;
                hasNextPage = false;
            }

            if (promptDisplayText.Length <= 9)
            {
                promptDisplayOptions = promptOptions.Skip(promptOptionsDisplayed).ToArray();

                promptDisplayOptionsBool = true;

                if (promptDisplayText.Length + 1 + promptDisplayOptions.Length > 9)
                {
                    hasNextPage = true;
                }
                else
                {
                    hasNextPage = false;
                }
            }else
            {
                promptDisplayOptionsBool = false;
            }

            if (numberPressed != -1)
            {
                if (boolNumberPressed)
                {
                    if (promptDisplayOptionsBool && numberPressed != 9 && numberPressed != 0)
                    {
                        int index = numberPressed - 1 + promptOptionsDisplayed;
                        Debug.WriteLine(index);
                        Debug.WriteLine(promptOptions.Length - 1);

                        if (index >= 0 && index <= promptOptions.Length - 1)
                        {
                            showPrompt = false;
                            Action[] actions = promptActions[index];
                            foreach (Action action in actions)
                            {
                                action.Act();
                            }
                        }
                    }


                    if (hasNextPage)
                    {
                        if (numberPressed == 9)
                        {
                            promptPage++;
                            if(promptDisplayOptionsBool)
                            {
                                promptOptionsDisplayed += 9 - (promptDisplayText.Length);
                            }

                        }
                    }
                    if (numberPressed == 0)
                    {
                        showPrompt = false;
                    }

                    boolNumberPressed = false;
                }
                numberPressed = -1;
            }
            else
            {
                boolNumberPressed = true;
            }

            /*string[] arr = promptText.Split('#');

            promptTextEnd = 12;

            for (int i = 0; i < arr.Length; i++)
            {
                promptTextEnd++;
            }

            maxOptionsPerPage = 21 - (promptTextEnd + 1);
            Debug.WriteLine(maxOptionsPerPage);

            if (numberPressed != -1)
            {
                if (boolNumberPressed)
                {
                    int index = numberPressed - 1;
                    if (index == -1)
                    {
                        index = 10;
                    }
                    if (promptActions != null)
                    {
                        if(index < (promptOptions.Length) - ((promptPage - 1) * maxOptionsPerPage) && index < maxOptionsPerPage)
                        {
                            showPrompt = false;
                            Action[] actions = promptActions[index + ((promptPage - 1) * maxOptionsPerPage)];
                            foreach (Action action in actions)
                            {
                                action.Act();
                            }
                        }
                    }
                    if (canNext)
                    {
                        if (numberPressed == 9)
                        {
                            promptPage++;
                        }
                    }
                    if (numberPressed == 0)
                    {
                        showPrompt = false;
                    }

                    boolNumberPressed = false;
                }
                numberPressed = -1;
            }
            else
            {
                boolNumberPressed = true;
            }*/
        }

        static void InventoryScript()
        {
            if (changePage != 0)
            {
                if (boolChangePage)
                {
                    currentPage += changePage;
                    if (currentPage < 1)
                    {
                        currentPage = 1;
                    }
                    if (currentPage > 3)
                    {
                        currentPage = 3;
                    }
                    boolChangePage = false;
                }
                changePage = 0;
            }
            else
            {
                boolChangePage = true;
            }

            if (numberPressed != -1)
            {
                if (boolNumberPressed)
                {
                    int index = numberPressed - 1;
                    if (index == -1)
                    {
                        index = 10;
                    }
                    PlayerData.equipedItem = PlayerData.inventory[index + ((currentPage - 1) * 10)];
                    boolNumberPressed = false;
                }
                numberPressed = -1;
            }
            else
            {
                boolNumberPressed = true;
            }
        }

        static void Stats()
        {
            DrawBorder(new Vector2(46, 1), new Vector2(21, 10), ConsoleColor.DarkGray);
            DrawText(new Vector2(53, 1), "-STATS-", ConsoleColor.DarkGray);
            DrawText(new Vector2(50, 2), "Level: " + PlayerData.level, ConsoleColor.DarkCyan);
            DrawText(new Vector2(53, 3), "XP: " + PlayerData.xp + "/" + PlayerData.xpToNextLevel, ConsoleColor.DarkCyan);
            DrawText(new Vector2(49, 4), "Health: " + PlayerData.health + "/" + PlayerData.maxHealth, ConsoleColor.White);
            DrawText(new Vector2(49, 5), "Armour: " + PlayerData.armour, ConsoleColor.White);
            DrawText(new Vector2(51, 6), "Mana: " + PlayerData.mana + "/" + PlayerData.maxMana, ConsoleColor.White);
            DrawText(new Vector2(47, 7), "Strength: " + PlayerData.strength, ConsoleColor.White);
            DrawText(new Vector2(50, 8), "Intel: " + PlayerData.intelligence, ConsoleColor.White);
            DrawText(new Vector2(51, 9), "Dext: " + PlayerData.dexterity, ConsoleColor.White);
        }

        public static void DisplayPrompt(string text, string label, string[] options, Action[][] actions)
        {
            promptText = text;
            promptLabel = label;
            promptOptions = options;
            promptActions = actions;
            showPrompt = true;
            promptPage = 1;
            promptOptionsDisplayed = 0;
        }

        static void Inventory()
        {
            DrawBorder(new Vector2(46, 11), new Vector2(43, 13), ConsoleColor.DarkGray);
            DrawText(new Vector2(62, 11), "-INVENTORY-", ConsoleColor.DarkGray);
            for(int i = 0; i < 10; i++)
            {
                string itemName = " ";

                if (PlayerData.inventory[i + ((currentPage - 1) * 10)] != null)
                    itemName = PlayerData.inventory[i + ((currentPage - 1) * 10)].name;

                if (i == 9)
                {
                    DrawText(new Vector2(47, 12 + i), "0: " + itemName, ConsoleColor.White);

                    if (PlayerData.inventory[i + ((currentPage - 1) * 10)] != null && PlayerData.equipedItem != null)
                    {
                        if (PlayerData.inventory[i + ((currentPage - 1) * 10)] == PlayerData.equipedItem)
                        {
                            DrawText(new Vector2(47, 12 + i), "0: " + itemName, ConsoleColor.Black, ConsoleColor.Gray);
                        }
                    }
                }else
                {
                    DrawText(new Vector2(47, 12 + i), (i + 1) + ": " + itemName, ConsoleColor.White);

                    if (PlayerData.inventory[i + ((currentPage - 1) * 10)] != null && PlayerData.equipedItem != null)
                    {
                        if (PlayerData.inventory[i + ((currentPage - 1) * 10)] == PlayerData.equipedItem)
                        {
                            DrawText(new Vector2(47, 12 + i), (i + 1) + ": " + itemName, ConsoleColor.Black, ConsoleColor.Gray);
                        }
                    }
                }
            }
            DrawText(new Vector2(62, 22), "Page " + currentPage + ": -/=", ConsoleColor.White);
        }

        static void Prompt()
        {
            DrawBorder(new Vector2(46, 11), new Vector2(43, 13), ConsoleColor.DarkGray);
            DrawText(new Vector2(67 - (promptLabel.Length/2), 11), promptLabel, ConsoleColor.DarkGray);

            for (int i = 0; i < promptDisplayText.Length; i++)
            {
                if(i < 9)
                    DrawText(new Vector2(47, 12 + i), promptDisplayText[i], ConsoleColor.White);
            }

            if (promptDisplayOptionsBool)
            {
                //Debug.WriteLine("Render");
                //Debug.WriteLine(promptDisplayOptions.Length);
                for (int i = 0; i < promptDisplayOptions.Length; i++)
                {
                    if (i + promptDisplayText.Length < 9)
                        DrawText(new Vector2(47, 12 + i + promptDisplayText.Length), (i + 1) + ": " + promptDisplayOptions[i], ConsoleColor.White);
                    //Debug.WriteLine("display");
                }
            }

            if (hasNextPage)
            {
                DrawText(new Vector2(47, 21), "9: NEXT", ConsoleColor.White);
            }
            DrawText(new Vector2(47, 22), "0: CANCEL", ConsoleColor.White);

            /*string[] arr = promptText.Split('#');

            for (int i = 0; i < arr.Length; i++)
            {
                DrawText(new Vector2(47, 12 + i), arr[i], ConsoleColor.White);
            }

            //int optionLine = TextEnd + 1 + promptOptions.Length;
            
            if (promptOptions.Length - ((promptPage - 1) * maxOptionsPerPage) <= maxOptionsPerPage)
            {
                for (int i = 0; i < maxOptionsPerPage; i++)
                {
                    if(i + ((promptPage - 1) * maxOptionsPerPage) < promptOptions.Length)
                        DrawText(new Vector2(47, promptTextEnd + i + 1), (i + 1) + ": " + promptOptions[i + ((promptPage - 1) * maxOptionsPerPage)], ConsoleColor.White);
                }
                canNext = false;
            }
            else
            {
                canNext = true;
                for (int i = 0; i < maxOptionsPerPage; i++)
                {
                    Debug.WriteLine("index" + i);
                    if (i + ((promptPage - 1) * maxOptionsPerPage) < promptOptions.Length)
                        DrawText(new Vector2(47, promptTextEnd + i + 1), (i + 1) + ": " + promptOptions[i + ((promptPage - 1) * maxOptionsPerPage)], ConsoleColor.White);
                }
                DrawText(new Vector2(47, 21), "9: NEXT", ConsoleColor.White);
            }

            DrawText(new Vector2(47, 22), "0: CANCEL", ConsoleColor.White);*/
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
        
        static void DrawText(Vector2 start, string text, ConsoleColor color, ConsoleColor bgColor = ConsoleColor.Black,bool writeFromLeft = true)
        {
            char[] characters = text.ToCharArray();

            if(writeFromLeft)
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    FastConsole.WriteToBuffer((int)start.X + i, (int)start.Y, characters[i], color, bgColor);
                }
            }else
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    FastConsole.WriteToBuffer((int)start.X - i, (int)start.Y, characters[characters.Length - i - 1], color, bgColor);
                }
            }
        }

        static List<string> WrapText(string text, int pixels)
        {
            string[] originalLines = text.Split(new string[] { " " },
                StringSplitOptions.None);

            List<string> wrappedLines = new List<string>();

            StringBuilder actualLine = new StringBuilder();
            int actualWidth = 0;

            foreach (var item in originalLines)
            {
                if(item != "\n")
                {
                    actualLine.Append(item + " ");
                }
                actualWidth += item.Length + 1;

                if (actualWidth > pixels || item == "\n")
                {
                    if (item != "\n")
                        actualLine.Remove(actualLine.Length - (item.Length + 1), item.Length + 1);
                    wrappedLines.Add(actualLine.ToString());
                    actualLine.Clear();
                    if (item != "\n")
                        actualLine.Append(item + " ");
                    actualWidth = item.Length + 1;
                }
            }

            if (actualLine.Length > 0)
                wrappedLines.Add(actualLine.ToString());

            return wrappedLines;
        }
    }
}
