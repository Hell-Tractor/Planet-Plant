using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoneyScreeningManager : MonoBehaviour {
    public GameObject RealMoney;
    public GameObject FakeMoney;

    [ReadOnly]
    public bool IsRealMoney;

    public int FindedFakePointCount { get; private set; } = 0;
    public int SumFakePointCount { get; private set; } = 0;

    public static MoneyScreeningManager Instance { get; private set; } = null;

    public void ShowMoney(bool isReal) {
        FindedFakePointCount = 0;
        SumFakePointCount = 0;
        Instance = this;
        foreach (Transform child in FakeMoney.transform) {
            if (child.CompareTag("FakePointMark")) {
                Color color = child.GetComponent<Image>().color;
                child.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);

                ++SumFakePointCount;
            }
        }

        DialogManager.Instance.ShowDialog(14);
        
        RealMoney.SetActive(isReal);
        FakeMoney.SetActive(!isReal);

        IsRealMoney = isReal;
    }

    private void Update() {
        if (!this.IsRealMoney && Input.GetMouseButtonDown(0)) {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                position = Input.mousePosition
            }, results);
            foreach (RaycastResult result in results) {
                if (result.gameObject.CompareTag("FakePointMark") && Mathf.Approximately(result.gameObject.GetComponent<Image>().color.a, 0)) {
                    Color color = result.gameObject.GetComponent<Image>().color;
                    result.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);                    
                    this.FindedFakePointCount++;
                }
            }
        }
    }

    public bool IsAllFakePointFinded() {
        return this.FindedFakePointCount == this.SumFakePointCount;
    }

}
