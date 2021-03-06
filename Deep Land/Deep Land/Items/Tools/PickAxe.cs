using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace Deep_Land
{
    class PickAxe : Item
    {
        int count;

        int mineTime;
        int power;

        bool canUse = true;

        public PickAxe(string _name, int _mineTime, int _power) : base(_name)
        {
            name = _name;
            mineTime = _mineTime;
            power = _power;
        }

        public override void Use()
        {
            if(canUse)
            {
                Cell cell = World.loadedCellsArray[(int)PlayerData.cursorPositionInArray.X, (int)PlayerData.cursorPositionInArray.Y];
                if(cell != null)
                    if (cell.name != "bedrock")
                        World.loadedCellsArray[(int)PlayerData.cursorPositionInArray.X, (int)PlayerData.cursorPositionInArray.Y] = null;
            }
        }

        public override void ShowInfo()
        {
            Action[][] actions = new Action[1][];
            actions[0] = new Action[] { new Action() };

            UI.DisplayPrompt(name, "-" + name + "-", new string[] { "Drop" }, actions);
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if(!canUse)
            {
                if(count >= mineTime)
                {
                    canUse = true;
                }
                count++;
            }
        }

        public override void PostUpdate()
        {

        }
    }
}
