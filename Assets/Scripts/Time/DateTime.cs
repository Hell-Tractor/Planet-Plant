using UnityEngine;

namespace pp {

    public enum Season {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    [System.Serializable]
    public class DateTime {
        public int HourPerDay { get; set; }
        public int DayPerSeason { get; set; }
        public int Season;
        [Range(1, 999)]
        public int Day;
        [Range(0, 999)]
        public int Hour;
        [Range(0, 60)]
        public int Minute;
        [Range(0, 60)]
        public float Second;
        public static bool operator <= (DateTime a, DateTime b) {
            if (a.Season != b.Season)
                return a.Season <= b.Season;
            if (a.Day != b.Day)
                return a.Day <= b.Day;
            if (a.Hour != b.Hour)
                return a.Hour <= b.Hour;
            if (a.Minute != b.Minute)
                return a.Minute <= b.Minute;
            return a.Second <= b.Second;
        }
        public static bool operator >= (DateTime a, DateTime b) {
            if (a.Season != b.Season)
                return a.Season >= b.Season;
            if (a.Day != b.Day)
                return a.Day >= b.Day;
            if (a.Hour != b.Hour)
                return a.Hour >= b.Hour;
            if (a.Minute != b.Minute)
                return a.Minute >= b.Minute;
            return a.Second >= b.Second;
        }
        public void AddSeconds(float seconds) {
            Second += seconds;
            Minute += (int)Second / 60;
            Second %= 60;
            Hour += (int)Minute / 60;
            Minute %= 60;
            Day += (int)Hour / HourPerDay;
            Hour %= HourPerDay;
        }

        public Season GetSeason() {
            return (Season)Season;
        }

        public override string ToString() {
            return "";
        }
    }
}