using UnityEngine;
namespace Utils {
    /// <summary>
    /// Class for generating random numbers.
    /// </summary>
    public class Random {
        /// <summary>
        /// Generate random number with standard normal distribution.
        /// </summary>
        /// <returns>the random number generated</returns>
        public static float Normal() {
            float u, v, w;
            do {
                u = UnityEngine.Random.Range(-1.0f, 1.0f);
                v = UnityEngine.Random.Range(-1.0f, 1.0f);
                w = u * u + v * v;
            } while (w >= 1.0f || Mathf.Approximately(w, 0f));
            float c = Mathf.Sqrt((-2.0f * Mathf.Log(w)) / w);
            return u * c;
        }

        /// <summary>
        /// Generate random number with normal distribution which mean and deviation are specified.
        /// </summary>
        /// <param name="mean">mean of the distribution</param>
        /// <param name="std_dev">deviation of the distribution</param>
        /// <returns>the random number generated</returns>
        public static float Normal(float mean, float std_dev) {
            return mean + std_dev * Normal();
        }
    }
}