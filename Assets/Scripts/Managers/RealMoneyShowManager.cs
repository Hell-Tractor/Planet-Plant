using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RealMoneyShowManager : MonoBehaviour {
    public float Duration = 3.0f;

    public Text _timerText;

    public float Interval = 0.03f;
    private float _timer = 0.0f;
    
    public void Show(Action onComplete = null) {
        StartCoroutine(this._showCoroutine(onComplete));
    }

    private IEnumerator _showCoroutine(Action onComplete) {
        this.gameObject.SetActive(true);
        _timer = Duration;
        while (_timer > 0) {
            _timerText.text = String.Format("{0:F2}", _timer);
            yield return new WaitForSeconds(Interval);
            _timer -= Interval;
        }
        this.gameObject.SetActive(false);
        onComplete?.Invoke();
    }
}
