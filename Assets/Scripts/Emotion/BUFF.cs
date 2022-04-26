using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public class TestBuff : BuffBase
    {

        private float timer;
        private float nexttime;
        private float dtime = 1;
        private int value;
        public override void OnBuffEnter(bool isEndByEmo)
        {
            value = 100;
            timer = 5;
            IsEndByEmo = isEndByEmo;
        }
        public override void OnBuffStay()
        {
            if (Time.time > nexttime)
            {
                if (timer > 0 && !IsEndByEmo)
                {
                    nexttime = Time.time + dtime;
                    value -= 1; timer -= 1;
                    //Debug.Log(timer);
                    Debug.LogFormat("Buff1: {0}", value);
                }
                else if (IsEndByEmo)
                {
                    nexttime = Time.time + dtime;
                    value -= 1;
                    //Debug.Log(timer);
                    Debug.LogFormat("Buff1: {0}", value);
                }
            }
        }
        public override void OnBuffExit()
        {
            value = 0;
            // Debug.Log(value);
            Debug.LogFormat("Buff1---end: {0}", value);
        }
        public override bool IsBuffEnded()
        {
            if (!IsEndByEmo)
                return timer <= 0;
            else
                return Mathf.Approximately(GameObject.FindWithTag("EmotionSlider").GetComponent<EmotionManager>().getSliderValue(), 0.4f);
        }
    }

    public class TestBuff2 : BuffBase
    {

        private float timer;
        private float nexttime;
        private float dtime = 1;
        private int value;
        public override void OnBuffEnter(bool isEndByEmo)
        {
            value = 50;
            timer = 3;
            IsEndByEmo = isEndByEmo;
        }
        public override void OnBuffStay()
        {
            if (Time.time > nexttime)
            {
                if (timer > 0 && !IsEndByEmo)
                {
                    nexttime = Time.time + dtime;
                    value -= 1; timer -= 1;
                    // Debug.Log(timer);
                    // Debug.Log(value);
                    Debug.LogFormat("Buff2: {0}", value);
                }
                else if (IsEndByEmo)
                {
                    nexttime = Time.time + dtime;
                    value -= 1;
                    // Debug.Log(timer);
                    // Debug.Log(value);
                    Debug.LogFormat("Buff2: {0}", value);
                }
            }
        }
        public override void OnBuffExit()
        {
            value = -50;
            // Debug.Log(value);
            Debug.LogFormat("Buff2---end: {0}", value);
        }
        public override bool IsBuffEnded()
        {
            if (!IsEndByEmo)
                return timer <= 0;
            else
                return Mathf.Approximately(GameObject.FindWithTag("EmotionSlider").GetComponent<EmotionManager>().getSliderValue(), 0.4f);
        }
    }

}