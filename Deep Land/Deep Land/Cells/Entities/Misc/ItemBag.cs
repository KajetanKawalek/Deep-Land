using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Deep_Land
{
    class ItemBag : Entity
    {
        public ItemBag(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, Vector2 _positionInWorld, bool _hasGravity, Vector2 _size, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _hasGravity, _size, _health, _maxHealth, _armour)
        {
            positionInWorld = _positionInWorld;
        }

        public override void OnInteract()
        {

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
