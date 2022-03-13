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
                if(PlayerData.cursorPositionInArray.X - PlayerData.player.positionInArray.X < 6 && PlayerData.cursorPositionInArray.X - PlayerData.player.positionInArray.X > -5)
                {
                    if (PlayerData.cursorPositionInArray.Y - PlayerData.player.positionInArray.Y < 4 && PlayerData.cursorPositionInArray.Y - PlayerData.player.positionInArray.Y > -7)
                    {
                        Cell cell = World.loadedCellsArray[(int)PlayerData.cursorPositionInArray.X, (int)PlayerData.cursorPositionInArray.Y];
                        if (cell is Block)
                        {
                            World.loadedCellsArray[(int)PlayerData.cursorPositionInArray.X, (int)PlayerData.cursorPositionInArray.Y] = null;
                            World.InstanciateAtPositionInArray(2, PlayerData.cursorPositionInArray);
                        }
                    }
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
