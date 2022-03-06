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

            E = 0x45,

            Key0 = 0x30,
            Key1 = 0x31,
            Key2 = 0x32,
            Key3 = 0x33,
            Key4 = 0x34,
            Key5 = 0x35,
            Key6 = 0x36,
            Key7 = 0x37,
            Key8 = 0x38,
            Key9 = 0x39,

            Minus = 0xBD,
            Plus = 0xBB,

            Esc = 0x1B,
            End = 0x23
        }
    }
}
