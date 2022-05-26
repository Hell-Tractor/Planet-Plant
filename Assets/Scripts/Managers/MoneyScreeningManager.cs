using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class for managing real/fake money screening
/// </summary>
public class MoneyScreeningManager : MonoBehaviour {
    public GameObject RealMoney;
    public GameObject FakeMoney;
    [Range(0f, 1f), Tooltip("The probability of showing fake money")]
    public float FakeMoneyRate = 0.5f;

    [ReadOnly]
    public bool IsRealMoney;

    /// <summary>
    /// All fake points player finded
    /// </summary>
    public int FindedFakePointCount { get; private set; } = 0;
    /// <summary>
    /// Count of all fake points exist.
    /// </summary>
    public int SumFakePointCount { get; private set; } = 0;

    public static MoneyScreeningManager Instance { get; private set; } = null;

    /// <summary>
    /// Show the money screening dialog
    /// </summary>
    /// <param name="isReal">true to show real money</param>
    public void ShowMoney(bool isReal) {
        FindedFakePointCount = 0;
        SumFakePointCount = 0;
        Instance = this;
        // init all fake point tags to transparent
        foreach (Transform child in FakeMoney.transform) {
            if (child.CompareTag("FakePointMark")) {
                Color color = child.GetComponent<Image>().color;
                child.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);

                ++SumFakePointCount;
            }
        }

        // DialogManager.Instance.ShowDialog(14);
        
        // show correct money
        RealMoney.SetActive(isReal);
        FakeMoney.SetActive(!isReal);

        IsRealMoney = isReal;
    }

    /// <summary>
    /// Show money randomly
    /// </summary>
    public void ShowMoney() {
        this.ShowMoney(Random.Range(0f, 1f) < FakeMoneyRate);
    }

    private void Update() {
        // check if the player clicked at the right point
        if (!this.IsRealMoney && Input.GetMouseButtonDown(0)) {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                position = Input.mousePosition
            }, results);
            foreach (RaycastResult result in results) {
                // if the player clicked at the fake point, set the fake point tag visible
                if (result.gameObject.CompareTag("FakePointMark") && Mathf.Approximately(result.gameObject.GetComponent<Image>().color.a, 0)) {
                    Color color = result.gameObject.GetComponent<Image>().color;
                    result.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);                    
                    this.FindedFakePointCount++;
                }
            }
        }
    }

    /// <summary>
    /// Check if the player finds ALL fake points
    /// </summary>
    public bool IsAllFakePointFinded() {
        return this.FindedFakePointCount == this.SumFakePointCount;
    }

    /// <summary>
    /// Check if the player finds SOME fake points
    /// </summary>
    public bool IsSomeFakePointFinded() {
        return this.FindedFakePointCount > 0;
    }

    /// <summary>
    /// Hide money screening dialog
    /// </summary>
    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
