using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    class ItemBag : Entity
    {
        int count;
        Item[] items;

        public ItemBag(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, Vector2 _positionInWorld, bool _hasGravity, Vector2 _size, Item[] _items, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _positionInWorld, _hasGravity, _size, _health, _maxHealth, _armour)
        {
            items = _items;
        }

        public override void OnInteract()
        {
            Debug.WriteLine("interact");
            Action[][] actions = new Action[3][];
            actions[0] = new Action[] { new Action(), new GiveItems(items),new Delete(this) };
            actions[1] = new Action[] { new Action(), new Delete(this) };
            actions[2] = new Action[] { };

            UI.DisplayPrompt("Loot: ", "-Item Bag-", new string[] { "Take All Items", "Take One Item", "Cancel" }, actions);
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if (count > 100)
                count = 0;
            count++;

            if (count % 5 == 0)
                Gravity();
        }

        public override void PostUpdate()
        {

        }
    }
}
