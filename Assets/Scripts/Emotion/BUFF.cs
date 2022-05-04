namespace BUFF
{
    public abstract class BuffBase
    {
        public virtual void OnBuffEnter(bool isEndByEmo) { }
        public virtual void OnBuffStay() { }
        public virtual void OnBuffExit() { }
        public abstract bool IsBuffEnded();

        public bool IsEndByEmo;

    }
}