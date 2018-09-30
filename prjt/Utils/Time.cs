using System;
using System.Text.RegularExpressions;

namespace prjt.Utils
{
    public class Time
    {
        private int _hours;
        public int Hours
        {
            get { return _hours; }
            private set { _hours = value; }
        }


        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            private set { _minutes = value; }
        }


        private int _seconds;
        public int Seconds
        {
            get { return _seconds; }
            private set { _seconds = value; }
        }


        private int _totalSeconds;
        public int TotalSeconds
        {
            get { return _totalSeconds; }
            private set { _totalSeconds = value; }
        }


        private string _text;
        public string Text
        {
            get { return _text; }
            private set { _text = value; }
        }


        private string _hoursAndMinutes;
        public string HoursAndMinutes
        {
            get { return _hoursAndMinutes; }
            private set { _hoursAndMinutes = value; }
        }


        public bool IsNegative
        {
            get { return Seconds < 0; }
        }


        public Time()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            TotalSeconds = 0;
            Text = "00:00:00";
            HoursAndMinutes = "00:00";
        }


        public Time(int seconds)
        {
            Text = Seconds2Time(seconds);
            ProcessTotalSeconds(seconds);
            HoursAndMinutes = GetHoursAndMinutes(this);
        }


        public Time(string time)
        {
            TotalSeconds = Time2Seconds(time);
            ProcessTotalSeconds(TotalSeconds);
            Text = Seconds2Time(TotalSeconds);
            HoursAndMinutes = GetHoursAndMinutes(this);
        }


        public Time(Time time)
        {
            Hours = time.Hours;
            Minutes = time.Minutes;
            Seconds = time.Seconds;
            TotalSeconds = time.TotalSeconds;
            Text = time.Text;
            HoursAndMinutes = time.HoursAndMinutes;
        }


        private string GetHoursAndMinutes(Time time)
        {
            return time.Text.Substring(0, time.Text.LastIndexOf(":"));
        }


        public Time GetNegative()
        {
            return new Time(TotalSeconds * -1);
        }


        public static string Seconds2Time(int seconds)
        {
            bool isNegative = seconds < 0;

            seconds = Math.Abs(seconds);
            int hours = seconds / 3600;
            int minutes = (seconds % 3600) / 60;
            int secs = (seconds % 3600) % 60;

            return string.Format("{0}{1}:{2}:{3}", isNegative ? "-" : null, (hours > -10 && hours < 10) ? hours.ToString("D2") : hours.ToString(), minutes.ToString("D2"), secs.ToString("D2"));
        }


        public static int Time2Seconds(string time)
        {
            MatchCollection matches = Regex.Matches(time, @"^(?<hours>-?[0-9]+):(?<minutes>[0-5][0-9])(:(?<seconds>[0-5][0-9]))?$");
            if (matches.Count == 0) {
                // todo
            }

            int.TryParse(matches[0].Groups["hours"].Value, out int hours);
            int.TryParse(matches[0].Groups["minutes"].Value, out int minutes);
            int.TryParse(matches[0].Groups["seconds"].Value, out int seconds);

            return ((Math.Abs(hours) * 3600) + (minutes * 60) + seconds) * (hours < 0 ? -1 : 1);
        }


        private void ProcessTotalSeconds(int totalSeconds)
        {
            TotalSeconds = totalSeconds;
            Hours = totalSeconds / 3600;
            Minutes = (totalSeconds % 3600) / 60;
            Seconds = (totalSeconds % 3600) % 60;
        }


        public override bool Equals(object obj)
        {
            Time t = (Time)obj;
            return this.TotalSeconds == t.TotalSeconds;
        }


        public override int GetHashCode()
        {
            return this.TotalSeconds;
        }


        public static Time operator +(Time a, Time b)
        {
            return new Time(a.TotalSeconds + b.TotalSeconds);
        }


        public static Time operator +(Time a, int b)
        {
            return new Time(a.TotalSeconds + b);
        }


        public static Time operator +(int a, Time b)
        {
            return new Time(a + b.TotalSeconds);
        }


        public static Time operator -(Time a, Time b)
        {
            return new Time(a.TotalSeconds - b.TotalSeconds);
        }


        public static Time operator -(Time a, int b)
        {
            return new Time(a.TotalSeconds - b);
        }


        public static Time operator -(int a, Time b)
        {
            return new Time(a - b.TotalSeconds);
        }


        public static bool operator >(Time a, Time b)
        {
            return a.TotalSeconds > b.TotalSeconds;
        }


        public static bool operator >(Time a, int b)
        {
            return a.TotalSeconds > b;
        }


        public static bool operator >(int a, Time b)
        {
            return a > b.TotalSeconds;
        }


        public static bool operator <(Time a, Time b)
        {
            return a.TotalSeconds < b.TotalSeconds;
        }


        public static bool operator <(Time a, int b)
        {
            return a.TotalSeconds < b;
        }


        public static bool operator <(int a, Time b)
        {
            return a < b.TotalSeconds;
        }


        public static bool operator <=(Time a, Time b)
        {
            return a.TotalSeconds <= b.TotalSeconds;
        }


        public static bool operator <=(Time a, int b)
        {
            return a.TotalSeconds <= b;
        }


        public static bool operator <=(int a, Time b)
        {
            return a <= b.TotalSeconds;
        }


        public static bool operator >=(Time a, Time b)
        {
            return a.TotalSeconds >= b.TotalSeconds;
        }


        public static bool operator >=(Time a, int b)
        {
            return a.TotalSeconds >= b;
        }


        public static bool operator >=(int a, Time b)
        {
            return a >= b.TotalSeconds;
        }


        public static bool operator ==(Time a, Time b)
        {
            if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null)) {
                return true;
            }

            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null)) {
                return false;
            }

            return a.TotalSeconds == b.TotalSeconds;
        }


        public static bool operator ==(Time a, int b)
        {
            return a.TotalSeconds == b;
        }


        public static bool operator ==(int a, Time b)
        {
            return a == b.TotalSeconds;
        }


        public static bool operator !=(Time a, Time b)
        {
            if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null)) {
                return false;
            }

            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null)) {
                return true;
            }

            return a.TotalSeconds != b.TotalSeconds;
        }


        public static bool operator !=(Time a, int b)
        {
            return a.TotalSeconds != b;
        }


        public static bool operator !=(int a, Time b)
        {
            return a != b.TotalSeconds;
        }        

    }

}
