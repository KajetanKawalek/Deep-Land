using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public class Delete : Action
    {
        Cell obj;

        public Delete(Cell _obj)
        {
            obj = _obj;
        }

        public override void Act()
        {
            World.loadedCellsArray[(int)obj.positionInArray.X, (int)obj.positionInArray.Y] = null;
        }
    }
}
