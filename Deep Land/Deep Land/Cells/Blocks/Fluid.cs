using System;
using System.Numerics;

namespace Deep_Land
{
    class Fluid : Cell
    {
        int count;

        int moveTime;

        Random rng = new Random(Guid.NewGuid().GetHashCode());

        public Fluid(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, int _moveTime)
        {
            name = _name;
            display = _display;
            color = _color;
            positionInArray = _positionInArray;
            moveTime = _moveTime;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if (count > 100)
                count = 0;
            count++;

            if (count % moveTime == 0)
            {
                if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                }
            }

            if (count % (moveTime * 2) == 0)
            {
                if (CheckForNotEmpty(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                {
                    int rand = rng.Next(1, 3);

                    if (rand == 1)
                    {
                        if (!CheckForCell(new Vector2(positionInArray.X + 1, positionInArray.Y)))
                        {
                            MoveTo(new Vector2(positionInArray.X + 1, positionInArray.Y));
                        }
                    }
                    else
                    {
                        if (!CheckForCell(new Vector2(positionInArray.X - 1, positionInArray.Y)))
                        {
                            MoveTo(new Vector2(positionInArray.X - 1, positionInArray.Y));
                        }
                    }
                }
            }
        }

        public override void PostUpdate()
        {

        }
    }
}
