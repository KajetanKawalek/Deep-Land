using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Deep_Land
{
    class Powder : Cell
    {
        int count;

        int moveTime;

        Random rng = new Random(Guid.NewGuid().GetHashCode());

        public Powder(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, int _moveTime)
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
                }else
                {
                    if(CheckCell(new Vector2(positionInArray.X, positionInArray.Y + 1)) is Fluid)
                    {
                        SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y + 1));
                    }
                }
            }

            if (count % (moveTime * 3) == 0)
            {
                if (CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                {
                    int rand = rng.Next(1, 3);

                    if(rand == 1)
                    {
                        if (!CheckForCell(new Vector2(positionInArray.X + 1, positionInArray.Y + 1)))
                        {
                            MoveTo(new Vector2(positionInArray.X + 1, positionInArray.Y + 1));
                        }else
                        {
                            if (CheckCell(new Vector2(positionInArray.X + 1, positionInArray.Y + 1)) is Fluid)
                            {
                                SwitchPlace(new Vector2(positionInArray.X + 1, positionInArray.Y + 1));
                            }
                        }
                    }
                    else
                    {
                        if (!CheckForCell(new Vector2(positionInArray.X - 1, positionInArray.Y + 1)))
                        {
                            MoveTo(new Vector2(positionInArray.X - 1, positionInArray.Y + 1));
                        }else
                        {
                            if (CheckCell(new Vector2(positionInArray.X - 1, positionInArray.Y + 1)) is Fluid)
                            {
                                SwitchPlace(new Vector2(positionInArray.X - 1, positionInArray.Y + 1));
                            }
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
