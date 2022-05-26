using UnityEngine;

namespace BUFF {
    /// <summary>
    /// Base class for buff.
    /// </summary>
    public abstract class BuffBase {
        [Tooltip("Buff持续的游戏时间")]
        public int Duration;
        private pp.DateTime _endTime;
        public BuffID BuffID { get; protected set; }
        public BuffType BuffType { get; protected set; }
        /// <summary>
        /// is this buff end by emotion value
        /// </summary>
        public bool IsEndByEmo;

        /// <summary>
        /// 子类必须初始化BuffID, BuffType
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Called when buff enter.
        /// </summary>
        /// <param name="target">target of this buff</param>
        public virtual void OnBuffEnter(bool isEndByEmo, GameObject target) {
            IsEndByEmo = isEndByEmo;

            _endTime = TimeManager.Instance.CurrentTime;
            _endTime.AddSeconds(Duration);
        }
        public virtual void OnBuffStay(GameObject target) { }
        public virtual void OnBuffExit(GameObject target) { }
        /// <summary>
        /// return true if buff is ended
        /// </summary>
        public virtual bool IsBuffEnded() {
            return TimeManager.Instance.CurrentTime >= _endTime;
        }
    }
}