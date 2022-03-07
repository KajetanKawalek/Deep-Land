using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public class DisplayPrompt : Action
    {
        string text;
        string label;
        string[] options;
        Action[][] actions;

        public DisplayPrompt(string _text, string _label, string[] _options, Action[][] _actions)
        {
            text = _text;
            label = _label;
            options = _options;
            actions = _actions;
        }

        public override void Act()
        {
            UI.DisplayPrompt(text, label, options, actions);
        }
    }
}
