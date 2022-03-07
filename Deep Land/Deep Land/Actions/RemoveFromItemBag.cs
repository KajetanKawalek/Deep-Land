using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public class RemoveFromItemBag : Action
    {
        Item item;
        ItemBag itembag;

        public RemoveFromItemBag(Item _item, ItemBag _itembag)
        {
            item = _item;
            itembag = _itembag;
        }

        public override void Act()
        {
            itembag.items.RemoveAt(Array.IndexOf(itembag.items.ToArray(), item));
        }
    }
}
