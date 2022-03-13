using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Deep_Land
{
    public class Item : Base
    {
        public string name;

        public Item(string _name)
        {
            name = _name;
        }

        public virtual void Use()
        {
            Debug.WriteLine("Use " + name);
        }

        public virtual void ShowInfo()
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

        }

        public override void PostUpdate()
        {

        }
    }
}
