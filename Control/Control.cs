using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApp1.Char;
using ConsoleApp1.Focus;
using System.Runtime.InteropServices;
using ConsoleApp1.Configuration;

namespace ConsoleApp1.Control
{
    class Control
    {

        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 Stateshift);




        int earlierKeyNumber = 13;
        long oldMilisekends = 0;
        string oldFokus = null;
        public void control(int keyNumber, string path, int BadTime)
        {
            //ładowanie klas i obiektów
            Console.WriteLine(keyNumber + "   " + GetAsyncKeyState(keyNumber));
            FocusMonitor focus = new FocusMonitor();
            Files.Files files = new Files.Files();
            Time.Time time = new Time.Time();
            Check check = new Check();
            long nowMilisekends = time.takeTimeInMilliseconds();
            bool StatusCtrl = false;
            bool StatusNumlock = false;
            string nowMesage = null;
            if (GetAsyncKeyState(18) == 0 || GetAsyncKeyState(165) == 0)
            {
                StatusCtrl = check.Ctrl();
            }
            //Sprawdzanie czy potrzebny jest namlok do znaku
            if ((keyNumber >= 97 && keyNumber <= 105) || keyNumber == 110)
            {
                StatusNumlock = true;
            }



            //Console.WriteLine(button + "\t" + time.ActualTime() + "\t" + time.ClickTime(oldMilisekends, nowMilisekends) + " milisekund\t"+ focus.mausePozition());
            nowMesage = (string)(keyNumber + ";" + time.ActualTime() + ";" + time.ClickTime(oldMilisekends, nowMilisekends, BadTime) + ";" + focus.mausePozition() + ";"
            + check.CapsLockStatus() + ";" + StatusNumlock + ";" + check.Shift() + ";" + StatusCtrl + ";" + check.AltState() + ";" + focus.Focus());



            files.ApendText(path, nowMesage);
            Console.WriteLine(nowMesage);


            earlierKeyNumber = keyNumber;
            oldMilisekends = nowMilisekends;
        }
        public void CloseProgram(object conf)
        {
            //Podajemy w parametrze po ilu milisekundach ma nastąpic restart programu prowadzący do utworzenia nowego pliku
            Conf conf1 = (Conf)conf;

            List<string> config = new List<string>();
            config.Add(Convert.ToString(conf1.time));
            config.Add(Convert.ToString(conf1.ComputerName));
            config.Add(Convert.ToString(conf1.User));
            Thread.Sleep(3600000);

            Program.start(config);
            System.Environment.Exit(0);

        }
    }
}
