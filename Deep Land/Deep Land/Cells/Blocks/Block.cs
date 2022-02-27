using System;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    public class Block : Cell
    {
        public Block(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray)
        {
            name = _name;
            display = _display;
            color = _color;
            positionInArray = _positionInArray;
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
