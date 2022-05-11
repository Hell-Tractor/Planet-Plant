using UnityEngine;
namespace Utils {
    public class Random {
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

        public static float Normal(float mean, float std_dev) {
            return mean + std_dev * Normal();
        }
    }
}