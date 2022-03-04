using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_Land
{
    public static class Input
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int key);

        public static Dictionary<Keys, bool> PressedKeys = new Dictionary<Keys, bool>();

        public static void Init()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                PressedKeys.Add(key, false);
            }
        }
        public static void Update()
        {
            foreach (Keys key in PressedKeys.Keys.ToList())
            {
                //0x8000 most significant bit
                //if MSB is set, key is down
                PressedKeys[key] = (0x8000 & GetAsyncKeyState((char)key)) != 0;
            }
        }

        //https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        public enum Keys : short
        {
            W = 0x57,
            A = 0x41,
            S = 0x53,
            D = 0x44,

            Up = 0x26,
            Left = 0x25,
            Down = 0x28,
            Right = 0x27,

            SpaceBar = 0x20,
            Esc = 0x1B,
            End = 0x23
        }
    }
}
