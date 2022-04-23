using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace Deep_Land
{
    class Placeable : Item
    {
        int count;

        int mineTime;
        int blockID;

        bool canUse = true;

        public Placeable(string _name, int _mineTime, int _blockId) : base(_name)
        {
            name = _name;
            mineTime = _mineTime;
            blockID = _blockId;
        }

        public override void Use()
        {
            if (canUse)
            {
                Cell cell = World.loadedCellsArray[(int)PlayerData.cursorPositionInArray.X, (int)PlayerData.cursorPositionInArray.Y];
                if (cell == null)
                {
                    World.InstanciateAtPositionInArray(blockID, PlayerData.cursorPositionInArray);
                }
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
            if (!canUse)
            {
                if (count >= mineTime)
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
