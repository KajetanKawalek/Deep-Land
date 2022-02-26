using System;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    public class Block : Cell
    {
        public Block(string blockName, char dis, ConsoleColor col, Vector2 pos)
        {
            name = blockName;
            display = dis;
            color = col;
            positionInArray = pos;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            
        }

        public override void PostUpdate()
        {

        }
    }
}
