using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public class HidePrompt : Action
    {
        public override void Act()
        {
            UI.showPrompt = false;
        }
    }
}
