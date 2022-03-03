namespace Utils
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        protected static T _instance = null;
        private static readonly object locker = new object();
        protected Singleton() { }
        public static T Instance {
            get {
                lock (locker) {
                    if (_instance == null) {
                        _instance = new T();
                    }
                    return _instance;
                }
            }
        }
        
    }
}
