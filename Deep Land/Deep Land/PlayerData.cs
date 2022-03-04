﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    static class PlayerData
    {
        static Player player;

        static Item[] inventory;

        static int count;
        static int cursorMoveTime = 15;

        static Vector2 cursorPosition = Vector2.Zero; //Relative to Player
        public static Vector2 cursorPositionInArray { get; private set; } = Vector2.Zero; //Relative to Player
        static Vector2 cursorMovement = Vector2.Zero;

        public static void PreUpdate()
        {
            cursorMovement = Vector2.Zero;
            if (Input.PressedKeys[Input.Keys.Up])
                cursorMovement += new Vector2(0, -1);
            if (Input.PressedKeys[Input.Keys.Left])
                cursorMovement += new Vector2(-1, 0);
            if (Input.PressedKeys[Input.Keys.Down])
                cursorMovement += new Vector2(0, 1);
            if (Input.PressedKeys[Input.Keys.Right])
                cursorMovement += new Vector2(1, 0);
        }

        public static void AssignPlayer()
        {
            player = (Player)FindCellInLoadedCells<Player>();
        }

        public static void Update()
        {
            if(cursorMovement != Vector2.Zero)
            {
                if (count == 0)
                {
                    cursorPosition += cursorMovement;
                    if (cursorPosition.X > 14)
                    {
                        cursorPosition = new Vector2(14, cursorPosition.Y);
                    }
                    if (cursorPosition.X < -15)
                    {
                        cursorPosition = new Vector2(-15, cursorPosition.Y);
                    }
                    if (cursorPosition.Y > 14)
                    {
                        cursorPosition = new Vector2(cursorPosition.X, 14);
                    }
                    if (cursorPosition.Y < -15)
                    {
                        cursorPosition = new Vector2(cursorPosition.X, -15);
                    }

                    cursorMovement = Vector2.Zero;
                }
                count++;
                if(count >= cursorMoveTime)
                {
                    cursorMoveTime = 2;
                    count = 0;
                }
            }else
            {
                count = 0;
                cursorMoveTime = 15;
            }

            cursorPositionInArray = player.positionInArray + cursorPosition;
        }

        public static void Render()
        {
            FastConsole.WriteToBuffer((int)(player.positionInArray.X + cursorPosition.X), (int)(player.positionInArray.Y + cursorPosition.Y), 'x', ConsoleColor.Red);
        }

        static Cell FindCellInLoadedCells<T>()
        {
            int w = World.loadedCellsArray.GetLength(0); // width
            int h = World.loadedCellsArray.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if(World.loadedCellsArray[x, y] != null)
                    {
                        if (World.loadedCellsArray[x, y].GetType() == typeof(T))
                            return World.loadedCellsArray[x, y];
                    }
                }
            }

            return null;
        }
    }

}