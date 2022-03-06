using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public class GiveItems : Action
    {
        Item[] items;

        public GiveItems(Item[] _items)
        {
            items = _items;
        }

        public override void Act()
        {
            PlayerData.AddItemsToInventory(items);
        }
    }
}
