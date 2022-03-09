using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Deep_Land
{
    public class ItemBag : Entity
    {
        int count;
        public List<Item> items = new List<Item>();

        public ItemBag(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, Vector2 _positionInWorld, bool _hasGravity, Vector2 _size, Item[] _items, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _positionInWorld, _hasGravity, _size, _health, _maxHealth, _armour)
        {
            items.AddRange(_items);
        }

        public override void OnInteract()
        {
            Debug.WriteLine("interact");
            Action[][] actions = new Action[2][];
            actions[0] = new Action[] { new GiveItems(items.ToArray()), new Delete(this) };

            string[] itemNames = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                itemNames[i] = items[i].name;
            }
            Action[][] actions2 = new Action[items.Count][];
            for (int i = 0; i < items.Count; i++)
            {
                actions2[i] = new Action[] { new GiveItems(new Item[] { items[i]}), new RemoveFromItemBag(items[i], this) };
            }
            actions[1] = new Action[] { new DisplayPrompt("Select Item: ", "-Item Bag-", itemNames, actions2) };

            string displayItemNames = items[0].name;

            for(int i = 1; i < items.Count; i++)
            {
                displayItemNames = displayItemNames + ", " + items[i].name;
            }

            Regex.Replace(displayItemNames, ".{41}", "$0#");

            UI.DisplayPrompt("Loot: " + displayItemNames, "-Item Bag-", new string[] { "Take All Items", "Take One Item", "1", "2", "3", "4", "5", "6", "7", "8"}, actions);
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
