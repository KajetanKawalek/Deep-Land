using System;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    public class Cell : Base
    {
        public string name = "?";
        public char display = '?';
        public ConsoleColor color = ConsoleColor.Magenta;
        public Vector2 positionInArray;

        public override void PreUpdate()
        {

        }

        public override void Update()
        {

        }

        public override void PostUpdate()
        {

        }

        public void MoveTo(Vector2 newPositionInArray)
        {
            if((newPositionInArray.X <= 44 && newPositionInArray.X >= 0) && (newPositionInArray.Y <= 44 && newPositionInArray.Y >= 0))
            {
                World.loadedCellsArray[(int)newPositionInArray.X, (int)newPositionInArray.Y] = this;
                World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] = null;
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
    }
}
