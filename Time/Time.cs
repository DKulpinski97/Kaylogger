using System;

namespace ConsoleApp1.Time
{
    class Time
    {
        public long takeTimeInMilliseconds()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();


        }
        public string ActualTime()
        {
            return DateTimeOffset.Now.ToString("HH:mm:ss");
        }
        public string ActualDay()
        {

            return DateTimeOffset.Now.Year.ToString() + "." + DateTimeOffset.Now.Month.ToString() + "." + DateTimeOffset.Now.Day.ToString(); ;
        }
        public string ClickTime(long oldMilliseconds, long nowMilliseconds, int badTime)
        {
            if ((nowMilliseconds - oldMilliseconds) > badTime)
            {
                return "-1";
            }
            else
            {
                return ((nowMilliseconds - oldMilliseconds) + "milisekund").ToString();
            }
        }

    }
}
