using System;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Generic;

namespace Deep_Land
{
    public class Cell : Base
    {
        public string name = "?";
        public char display = '?';
        public ConsoleColor color = ConsoleColor.Magenta;
        public Vector2 positionInArray;

        List<Cell> attachedCells = new List<Cell>();

        public override void PreUpdate()
        {

        }

        public override void Update()
        {

        }

        public override void PostUpdate()
        {

        }

        public void MoveTo(Vector2 newPositionInArray, bool destroyOld = true)
        {
            if((newPositionInArray.X <= 44 && newPositionInArray.X >= 0) && (newPositionInArray.Y <= 44 && newPositionInArray.Y >= 0))
            {
                World.loadedCellsArray[(int)newPositionInArray.X, (int)newPositionInArray.Y] = this;
                if(destroyOld)
                {
                    World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] = null;
                }
                positionInArray = newPositionInArray;
            }
        }

        public void SwitchPlace(Vector2 newPositionInArray)
        {
            if ((newPositionInArray.X <= 44 && newPositionInArray.X >= 0) && (newPositionInArray.Y <= 44 && newPositionInArray.Y >= 0))
            {
                Cell other = World.loadedCellsArray[(int)newPositionInArray.X, (int)newPositionInArray.Y];

                World.loadedCellsArray[(int)newPositionInArray.X, (int)newPositionInArray.Y] = this;
                other.MoveTo(new Vector2(positionInArray.X, positionInArray.Y), false);
                positionInArray = newPositionInArray;
            }
        }

        public bool CheckForCell(Vector2 positionInArray)
        {
            if ((positionInArray.X <= 44 && positionInArray.X >= 0) && (positionInArray.Y <= 44 && positionInArray.Y >= 0))
            {
                return World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] != null;
            }else
            {
                return false;
            }
        }

        public bool CheckForNotEmpty(Vector2 positionInArray)
        {
            if ((positionInArray.X <= 44 && positionInArray.X >= 0) && (positionInArray.Y <= 44 && positionInArray.Y >= 0))
            {
                return World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] != null;
            }
            else
            {
                return true;
            }
        }

        public Cell CheckCell(Vector2 positionInArray)
        {
            if ((positionInArray.X <= 44 && positionInArray.X >= 0) && (positionInArray.Y <= 44 && positionInArray.Y >= 0))
            {
                return World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y];
            }
            else
            {
                return null;
            }
        }

        public void CreateAttachedCell(Vector2 positionInArray, char display, ConsoleColor color)
        {
            if(!CheckForCell(positionInArray))
            {
                Block cell = new Block("?", display, color, positionInArray);
                World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] = cell;
                attachedCells.Add(cell);
            }
        }

        public void ClearAttachedCells(Vector2 CoreCellPosition)
        {
            foreach (Cell cell in attachedCells)
            {
                if(cell.positionInArray != CoreCellPosition)
                    World.loadedCellsArray[(int)cell.positionInArray.X, (int)cell.positionInArray.Y] = null;
            }
            attachedCells.Clear();
        }
    }
}
