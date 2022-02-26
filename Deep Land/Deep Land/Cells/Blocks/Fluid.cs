using System;
using System.Numerics;

namespace Deep_Land
{
    class Fluid : Cell
    {
        int count;
        int count2;

        int speed;

        Random rng = new Random();

        public Fluid(string blockName, char dis, ConsoleColor col, Vector2 pos, int moveTime)
        {
            name = blockName;
            display = dis;
            color = col;
            positionInArray = pos;
            speed = moveTime;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if (count > speed)
            {
                if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                }

                count = 0;
            }
            count++;

            if (count2 > speed * 2)
            {
                if (CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
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
                count2 = 0;
            }
            count2++;
        }

        public override void PostUpdate()
        {

        }
    }
}
