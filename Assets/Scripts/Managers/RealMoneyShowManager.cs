using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager for showing the real money to player.
/// </summary>
public class RealMoneyShowManager : MonoBehaviour {
    // show duration
    public float Duration = 3.0f;

    // text zone to show time left
    public Text _timerText;

    // update interval
    public float Interval = 0.03f;
    private float _timer = 0.0f;
    
    /// <summary>
    /// Show the real money.
    /// </summary>
    /// <param name="onComplete">called after show finished</param>
    public void Show(Action onComplete = null) {
        StartCoroutine(this._showCoroutine(onComplete));
    }

    private IEnumerator _showCoroutine(Action onComplete) {
        // show the dialog
        this.gameObject.SetActive(true);
        // start the timer
        _timer = Duration;
        while (_timer > 0) {
            // update time left showing text
            _timerText.text = String.Format("{0:F2}", _timer);
            yield return new WaitForSeconds(Interval);
            _timer -= Interval;
        }
        // hide the dialog
        this.gameObject.SetActive(false);
        // call the call back
        onComplete?.Invoke();
    }
}
