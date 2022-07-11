using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1.Char
{
    class Check
    {
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 Stateshift);
        public bool CapsLockStatus()
        {
            if (Console.CapsLock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NumLock()
        {
            if (Console.NumberLock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Shift()
        {
            var LShift = GetAsyncKeyState(160);
            var PShift = GetAsyncKeyState(161);

            if (LShift == 32768 || PShift == 32769)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Ctrl()
        {
            var LCtrl = GetAsyncKeyState(17);
            var PCtrl = GetAsyncKeyState(163);
            //Console.WriteLine(OtherCodeCtrl);

            if (LCtrl != 0 || PCtrl != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AltState()
        {
            var PAlt = GetAsyncKeyState(18);
            var OtherAlt = GetAsyncKeyState(165);
            if (PAlt == 32768 || OtherAlt == 32768)
                return true;
            else
                return false;
        }
    }
}
