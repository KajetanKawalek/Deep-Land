using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Deep_Land
{
    class Entity : Cell
    {
        protected int heath;
        protected int maxHealth;
        protected int armour = 1;
        protected bool hasGravity;

        int count;

        public Entity(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, bool _hasGravity, int _health = 10, int _maxHealth = 10, int _armour = 1)
        {
            name = _name;
            display = _display;
            color = _color;
            positionInArray = _positionInArray;
            hasGravity = _hasGravity;
            heath = _health;
            maxHealth = _maxHealth;
            armour = _armour;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if(hasGravity)
            {
                if (count > 10)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                    {
                        MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X, positionInArray.Y + 1)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y + 1));
                        }
                    }

                    count = 0;
                }
                count++;
            }
        }

        public override void PostUpdate()
        {

        }

        public void TakeDamage(int damage)
        {
            int damageToTake = damage - armour;
            if (damageToTake < 0)
                damageToTake = 0;

            heath -= damageToTake;
        }
    }
}
