using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class RandomController : MonoBehaviour {

    public int Min = 1;
    public int Max = 6;
    public float Duration = 2.0f;
    public float Delay = 1.0f;
    
    private Text _text;

    private void Awake() {
        _text = this.GetComponentInChildren<Text>();
    }
    
    public void Roll(Action<int> onComplete) {
        StartCoroutine(_rollCoroutine(onComplete));
    }

    private IEnumerator _rollCoroutine(Action<int> onComplete) {
        float timer = 0;
        float interval = 0.05f;
        int counter = Min;
        
        do {
            _text.text = counter.ToString();
            yield return new WaitForSeconds(interval);
            timer += interval;
            if (++counter > Max) {
                counter = Min;
            }
        } while (timer < Duration);
        
        yield return new WaitForSeconds(Delay);

        onComplete?.Invoke(UnityEngine.Random.Range(Min, Max + 1));
    }
}
