using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using BUFF;

public class EmotionManager : MonoBehaviour
{
    public Transform MoodFill;
    public int UpdateRate = 1;
    public int IntervalHour = 2;
    private int _nextHour;
    // public Image Icon;

    public int EmotionValue {
        get {
            return Mathf.RoundToInt(MoodFill.localScale.y * 100) - 50;
        }
        set {
            MoodFill.localScale = new Vector3(1, Mathf.Clamp01((value + 50) / 100f), 1);
        }
    }

    private List<BuffBase> _buffList = new List<BuffBase>();
    private List<BuffBase> _deBuffList = new List<BuffBase>();
    private List<BuffBase> _currentBuffList = new List<BuffBase>();
    private List<int> _rangeList = new List<int>();

    [Tooltip("情绪高于45%时，获得的随机buff数")]
    public int RandomBuffCount;

    [Tooltip("随机BUFF是否存在")]
    public bool IsSetRandomBuff = false;

    [Tooltip("恶性事件触发概率"), Range(0, 1)]
    public float PassiveEventRate;

    public GameObject Player;

    public static EmotionManager Instance = null;

    private void Start() {
        Instance = this;

        foreach (BuffID id in Enum.GetValues(typeof(BuffID))) {
            var type = Type.GetType("BUFF." + id.ToString());
            BuffBase buff = Activator.CreateInstance(type) as BuffBase;
            if (buff != null) {
                buff.Init();
                if (buff.BuffType == BuffType.Buff) {
                    _buffList.Add(buff);
                } else {
                    _deBuffList.Add(buff);
                }
            }
        }
    }

    private void Update()
    {
        MoodSwing();

        if (this.EmotionValue >= 45 && !IsSetRandomBuff)
        {
            SetRandomBuff();
        }
        if (this.EmotionValue == 40)
        {
            IsSetRandomBuff = false;
        }

        for (int i = 0; i < _currentBuffList.Count; i++)
        {
            if (_currentBuffList[i] != null)
            {
                _currentBuffList[i].OnBuffStay(Player);
                if (_currentBuffList[i].IsBuffEnded() || _currentBuffList[i].IsEndByEmo && EmotionValue < 40)
                {
                    _currentBuffList[i].OnBuffExit(Player);
                    _currentBuffList.RemoveAt(i);
                }
            }
        }

        if (EmotionValue == -40)
        {
            //概率触发恶性事件
            System.Random ra = new System.Random(unchecked((int)DateTime.Now.Ticks));
            int temp = ra.Next(1, 100);
            if (temp <= PassiveEventRate)
                PassiveEvent();
        }
        else if (EmotionValue == -50)
        {
            //必然发生恶性事件
            PassiveEvent();
        }
    }


    private void MoodSwing() {
        if (EmotionValue > 41 && TimeManager.Instance.CurrentTime.Hour > _nextHour)
        {
            _nextHour = TimeManager.Instance.CurrentTime.Hour + IntervalHour;
            EmotionValue -= UpdateRate;
        }
        else if (EmotionValue < -40 && EmotionValue > -50 && TimeManager.Instance.CurrentTime.Hour > _nextHour)
        {
            _nextHour = TimeManager.Instance.CurrentTime.Hour + IntervalHour;
            EmotionValue -= UpdateRate;
        }
    }

    //事件影响
    public void AffectByEvent(int value)
    {
        EmotionValue += value;

        BuffBase buff;
        if (value > 0)//随机获取一个buff
        {
            int select = Random.Range(0, _buffList.Count);
            buff = _buffList[select];
            //StartCoroutine(BuffType.ValueChange());
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(false, Player);
        }
        else//随机获取一个debuff
        {
            int select = Random.Range(0, _deBuffList.Count);
            buff = _deBuffList[select];
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(false, Player);
        }


    }

    // public void ChangeImage(string imgType)
    // {
    //     Sprite sp = Resources.Load("Drawings/EmotionType/" + imgType, typeof(Sprite)) as Sprite;
    //     Icon.sprite = sp;
    // }

    private void SetRandomBuff()
    {
        _rangeList.Clear();
        IsSetRandomBuff = true;
        while (_rangeList.Count < RandomBuffCount)
        {
            int temp = Random.Range(0, RandomBuffCount);
            if (!_rangeList.Contains(temp))
            {
                _rangeList.Add(temp);
            }
            else
                continue;
        }
        BuffBase buff;
        for (int i = 0; i < RandomBuffCount; i++)
        {
            buff = _buffList[_rangeList[i]];
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(true, Player);
        }

    }

    public void PassiveEvent()
    {


    }
}
