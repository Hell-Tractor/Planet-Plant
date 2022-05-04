using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using BUFF;

public class EmotionManager : MonoBehaviour
{
    public Slider EmotionSlider;
    public float UpdateRate;
    public float IntervalHour;
    private float _nextHour;
    public Image Icon;

    private List<BuffBase> _buffList = new List<BuffBase>();
    private List<BuffBase> _deBuffList = new List<BuffBase>();
    private List<BuffBase> _currentBuffList = new List<BuffBase>();
    private List<int> _rangeList = new List<int>();

    //情绪高于45%时，获得的随机buff数
    public int RangeBuffCount;

    //随机BUFF是否存在
    bool IsSetRandomBuff = false;

    //恶性事件触发概率
    public float PassiveEventRate;

    public void AddBuff(BuffBase buff) {
        _buffList.Add(buff);
    }

    void Update()
    {
        MoodSwing();

        if (EmotionSlider.value >= 0.45f && !IsSetRandomBuff)
        {
            SetRandomBuff();
        }
        if (Mathf.Approximately(EmotionSlider.value, 0.4f))
        {
            IsSetRandomBuff = false;
        }

        for (int i = 0; i < _currentBuffList.Count; i++)
        {
            if (_currentBuffList[i] != null)
            {
                _currentBuffList[i].OnBuffStay();
                if (_currentBuffList[i].IsBuffEnded())
                {
                    _currentBuffList[i].OnBuffExit();
                    _currentBuffList.RemoveAt(i);
                }
            }
        }

        if (Mathf.Approximately(EmotionSlider.value, -0.4f))
        {
            //概率触发恶性事件
            System.Random ra = new System.Random(unchecked((int)DateTime.Now.Ticks));
            int temp = ra.Next(1, 100);
            if (temp <= PassiveEventRate)
                PassiveEvent();
        }
        else if (Mathf.Approximately(EmotionSlider.value, -0.5f))
        {
            //必然发生恶性事件
            PassiveEvent();
        }
    }


    public void MoodSwing() {
        if (EmotionSlider.value > 0.41 && Time.time > _nextHour)
        {
            _nextHour = Time.time + IntervalHour;
            EmotionSlider.value -= UpdateRate * 0.01f; ;
        }
        else if (EmotionSlider.value < -0.4 && EmotionSlider.value > -0.5 && Time.time > _nextHour)
        {
            _nextHour = Time.time + IntervalHour;
            EmotionSlider.value -= UpdateRate * 0.01f;
        }
    }

    //事件影响
    public void AffectByEvent(float value)
    {
        if (EmotionSlider.value < 0.5)
        {
            EmotionSlider.value += value * 0.01f;
        }

        BuffBase buff;
        if (value > 0)//随机获取一个buff
        {
            int select = Random.Range(0, _buffList.Count);
            buff = _buffList[select];
            //StartCoroutine(BuffType.ValueChange());
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(false);
        }
        else//随机获取一个debuff
        {
            int select = Random.Range(0, _deBuffList.Count);
            buff = _deBuffList[select];
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(false);
        }


    }

    public void ChangeImage(string imgType)
    {
        Sprite sp = Resources.Load("Drawings/EmotionType/" + imgType, typeof(Sprite)) as Sprite;
        Icon.sprite = sp;
    }

    public void SetRandomBuff()
    {
        _rangeList.Clear();
        IsSetRandomBuff = true;
        while (_rangeList.Count < RangeBuffCount)
        {
            int temp = Random.Range(0, RangeBuffCount);
            if (!_rangeList.Contains(temp))
            {
                _rangeList.Add(temp);
            }
            else
                continue;
        }
        BuffBase buff;
        for (int i = 0; i < RangeBuffCount; i++)
        {
            buff = _buffList[_rangeList[i]];
            _currentBuffList.Add(buff);
            buff.OnBuffEnter(true);
        }

    }

    public void PassiveEvent()
    {


    }

    public float getSliderValue()
    {
        return EmotionSlider.value;
    }
}
