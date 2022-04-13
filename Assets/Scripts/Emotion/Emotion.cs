using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//using pp;

public abstract class BUFFType
{  
    public float Value;
    public abstract IEnumerator ValueChange();
    public abstract void StartRangeBuff();
    public abstract void EndRangeBuff();
    public float GetBuffValue()
    {
        return this.Value;
    }
}

public class TestBuff : BUFFType
{
    public TestBuff()
    {
        Value = 0;
    }
    public override IEnumerator ValueChange()
    {
        Value += 10;
        Debug.Log(Value);
        yield return new WaitForSeconds(5);
        Value -= 10;
        Debug.Log(Value);
    }
    public override void StartRangeBuff()
    {
        Value += 10; 
        Debug.Log(Value);
    }
    public override void EndRangeBuff()
    {
        Value -= 10; 
        Debug.Log(Value);
    }
}

public class TestBuff2 :BUFFType
{
    public TestBuff2()
    {
        Value = 50;
    }
    public override IEnumerator ValueChange()
    {
        Value += 2;
        Debug.Log(Value);
        yield return new WaitForSeconds(5);
        Value -= 2;
        Debug.Log(Value);
    }

    public override void StartRangeBuff()
    {
        Value += 2; 
        Debug.Log(Value);
    }
    public override void EndRangeBuff()
    {
        Value -= 2; 
        Debug.Log(Value);
    }
}

public class Emotion : MonoBehaviour
{
    public Slider EmotionSlider;
    public float UpdateRate;
    public float IntervalHour;
    private float _nextHour;
    public BUFFType BuffType;
    TestBuff _testbuff = new TestBuff();
    TestBuff2 _testbuff2 = new TestBuff2();
    private List<BUFFType> _bufftypeList = new List<BUFFType>();
    private List<BUFFType> _debufftypeList = new List<BUFFType>();
    public Image Icon;

    private List<int> _rangeList = new List<int>();
    //情绪高于45%时，获得的随机buff数
    public int RangeBuffCount;

    //是否清除随机获得的buff（情绪低于40%）
    bool IsBuffClear = false;

    public float PassiveEventRate;

    // Start is called before the first frame update
    void Start()
    {
        _bufftypeList.Add(_testbuff);
        _bufftypeList.Add(_testbuff2);
    }

    // Update is called once per frame
    void Update()
    {
        MoodSwing();

        if(!IsBuffClear && EmotionSlider.value < 0.41f)
        {
            ClearRangeBuff();
        }

        if(EmotionSlider.value<-0.41f)
        {

            System.Random ra = new System.Random(unchecked((int)DateTime.Now.Ticks));
            int temp = ra.Next(1, 100);
            if (temp <= PassiveEventRate)
                PassiveEvent();
        }
        else if(EmotionSlider.value<=0.5f)
        {
            PassiveEvent();
        }
    }


    public void MoodSwing()
    {
        //if(EmotionSlider.value > 0.4 && GetComponent<DateTime>().Hour > _nextHour)
        //{
        //    _nextHour = GetComponent<DateTime>().Hour + IntervalHour;
        //    EmotionSlider.value -= UpdateRate * 0.01f; ;
        //}
        //else if(EmotionSlider.value < -0.4 && EmotionSlider.value > -0.5 && GetComponent<DateTime>().Hour > _nextHour)
        //{
        //    _nextHour = GetComponent<DateTime>().Hour + IntervalHour;
        //    EmotionSlider.value -= UpdateRate * 0.01f;
        //}
        if (EmotionSlider.value > 0.41 && Time.time>_nextHour)
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
        if(EmotionSlider.value < 0.5)
        {
            EmotionSlider.value += value * 0.01f;
        }

    
        if (value > 0)//随机获取一个buff
        {
            int select = Random.Range(0, _bufftypeList.Count);
            BuffType = _bufftypeList[select];
            StartCoroutine(BuffType.ValueChange());
        }
        else//随机获取一个debuff
        {
            int select = Random.Range(0, _debufftypeList.Count);
            BuffType = _debufftypeList[select];
            StartCoroutine(BuffType.ValueChange());
        }
        

    }

    public void ChangeImage(string imgType)
    {
        Sprite sp = Resources.Load("Drawings/EmotionType/" + imgType, typeof(Sprite)) as Sprite;
        Icon.sprite = sp;   
    }

    public void AddRangeBuff()
    {
        _rangeList.Clear();
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

        for (int i = 0; i < RangeBuffCount; i++)
        {
            BuffType = _bufftypeList[_rangeList[i]];
            BuffType.StartRangeBuff();
        }
        IsBuffClear = false;
    }

    public void ClearRangeBuff()
    {
        for (int i = 0; i < RangeBuffCount; i++)
        {
            BuffType = _bufftypeList[_rangeList[i]];
            BuffType.EndRangeBuff();
        }
        IsBuffClear = true;
    }
    
    public void PassiveEvent()
    {
   
      
    }
}
