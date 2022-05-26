using UnityEngine;

namespace pp {

    /// <summary>
    /// Enum for all seasons.
    /// </summary>
    public enum Season {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    /// <summary>
    /// Class for Date and Time.
    /// </summary>
    [System.Serializable]
    public class DateTime {
        [Tooltip("每日小时数"), Range(1, 999)]
        public int HourPerDay = 24;
        [Tooltip("每季节天数"), Range(1, 99)]
        public int DayPerSeason = 4;
        public int Season;
        [Range(1, 999)]
        public int Day;
        [Range(0, 999)]
        public int Hour;
        [Range(0, 60)]
        public int Minute;
        [Range(0, 60)]
        public float Second;

        #region Operator Overload
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
        #endregion

        /// <summary>
        /// Add seconds to current time.
        /// </summary>
        /// <param name="seconds">seconds to be added</param>
        public void AddSeconds(float seconds) {
            Second += seconds;
            Minute += (int)Second / 60;
            Second %= 60;
            Hour += (int)Minute / 60;
            Minute %= 60;
            Day += (int)Hour / HourPerDay;
            Hour %= HourPerDay;
        }

        /// <summary>
        /// Get the current season.
        /// </summary>
        /// <returns>the season enum</returns>
        public Season GetSeason() {
            return (Season)Season;
        }

        // ! unfinished
        public override string ToString() {
            return "";
        }
    }
}