using System;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    public class Block : Cell
    {
        bool hasGravity;
        int count;

        public Block(char dis, ConsoleColor col, Vector2 pos, bool gravity)
        {
            display = dis;
            color = col;
            positionInArray = pos;
            hasGravity = gravity;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if(hasGravity)
            {
                if(count > 10)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                    {
                        MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                    }
                    count = 0;
                }
                count++;
            }
        }

        public override void PostUpdate()
        {

        }

    }
}
