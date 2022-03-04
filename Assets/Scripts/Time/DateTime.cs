using UnityEngine;

namespace pp {
    // todo 根据策划需求调整
    [System.Serializable]
    public class DateTime {
        public int HourPerDay { get; set; }
        [Range(1, 999)]
        public int Day;
        [Range(0, 999)]
        public int Hour;
        [Range(0, 60)]
        public int Minute;
        [Range(0, 60)]
        public float Second;
        public static bool operator <= (DateTime a, DateTime b) {
            return a.Day <= b.Day;
        }
        public static bool operator >= (DateTime a, DateTime b) {
            return a.Day >= b.Day;
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

        public override string ToString() {
            return "";
        }
    }
}