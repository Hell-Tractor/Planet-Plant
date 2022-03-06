namespace Utils
{
    /// <summary>
    /// 单例模式基类，继承该类即可实现子类单例
    /// </summary>
    /// <typeparam name="T">子类类型</typeparam>
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
