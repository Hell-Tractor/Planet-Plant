using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Controller of Random point getting object
/// </summary>
public class RandomController : MonoBehaviour {

    /// <summary>
    /// Random time
    /// </summary>
    public float Duration = 2.0f;
    /// <summary>
    /// Dely time before hiding
    /// </summary>
    public float Delay = 1.0f;
    
    private Image _image;

    public List<Sprite> ImageList;

    private void Awake() {
        _image = this.GetComponent<Image>();
        _image.sprite = ImageList[0];
    }
    
    /// <summary>
    /// Get a random number between 1 and image count inclusive
    /// </summary>
    /// <param name="onComplete">called after rolling finished</param>
    public void Roll(Action<int> onComplete) {
        StartCoroutine(_rollCoroutine(onComplete));
    }

    private IEnumerator _rollCoroutine(Action<int> onComplete) {
        float timer = 0;
        float interval = 0.05f;
        int counter = 0;
        
        // show images in a loop
        do {
            _image.sprite = ImageList[counter];
            yield return new WaitForSeconds(interval);
            timer += interval;
            if (++counter >= ImageList.Count) {
                counter -= ImageList.Count;
            }
        } while (timer < Duration);

        // get a random number and show the result
        int result = UnityEngine.Random.Range(0, ImageList.Count);
        _image.sprite = ImageList[result];
        
        // call the complete function after delay
        yield return new WaitForSeconds(Delay);
        onComplete?.Invoke(result + 1);
    }
}
