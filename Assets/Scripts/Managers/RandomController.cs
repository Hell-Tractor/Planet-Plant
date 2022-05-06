using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RandomController : MonoBehaviour {

    public float Duration = 2.0f;
    public float Delay = 1.0f;
    
    private Image _image;

    public List<Sprite> ImageList;

    private void Awake() {
        _image = this.GetComponent<Image>();
        _image.sprite = ImageList[0];
    }
    
    public void Roll(Action<int> onComplete) {
        StartCoroutine(_rollCoroutine(onComplete));
    }

    private IEnumerator _rollCoroutine(Action<int> onComplete) {
        float timer = 0;
        float interval = 0.05f;
        int counter = 0;
        
        do {
            _image.sprite = ImageList[counter];
            yield return new WaitForSeconds(interval);
            timer += interval;
            if (++counter >= ImageList.Count) {
                counter -= ImageList.Count;
            }
        } while (timer < Duration);

        int result = UnityEngine.Random.Range(0, ImageList.Count);
        _image.sprite = ImageList[result];
        
        yield return new WaitForSeconds(Delay);
        onComplete?.Invoke(result + 1);
    }
}
