using System.IO;

namespace ConsoleApp1.Files
{
    class Files
    {
        public void IsFile(string path)
        {
            if (!File.Exists(path))
            {
                CreataFile(path);
            }

        }
        public void CreataFile(string path)
        {

            File.AppendAllText(path, "Kod klawisza;Czas dobowy;Czas miedzy poprzednim a aktualnym kliknieciem;Pozycja  myszy x;" +
                "Pozycja  myszy y;Stan Caps lock;Stan Num lock;Stan Shift;Stan Control;Stan P Alt;Aktualny fokus\n");

        }
        public void CreataFile1(string path)
        {
            StreamWriter sw;
            sw = File.CreateText(path);
            sw.Close();
        }
        public void ApendText(string path, string priperText)
        {
            //File.AppendAllText(path, "\n");
            string oneWord = null;
            for (int i = 0; i < priperText.Length; i++)
            {
                if ((i != priperText.Length - 1) && (priperText[i] == ';' && priperText[i + 1] == ';'))
                {
                    oneWord += "..";
                }
                else if (priperText[i] != ';')
                {
                    oneWord += priperText[i];
                }
                else
                {
                    File.AppendAllText(path, (oneWord + ';'));
                    oneWord = null;
                }
            }
            File.AppendAllText(path, (oneWord + '\n'));
            oneWord = null;

        }


    }
}
