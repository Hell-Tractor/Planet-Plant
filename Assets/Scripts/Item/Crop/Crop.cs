using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour {
    public WaterProperty Water = new WaterProperty();
    public PestProperty Pest = new PestProperty();
    public WeedProperty Weed = new WeedProperty();
    public FertilityProperty Fertility = new FertilityProperty();
    
    public bool IsAquatic;
    public Inventory PlayerInventory;
    public List<int> StageDuration = new List<int>();
    public int Productivity;
    public GameObject ProductPrefab;
    [SerializeField, ReadOnly]
    private int _currentStage;
    private int _passedTime;
    private float _quantityCoefficient;
    private void Start() {
        // Disable crop system
        
        // Water.IsAquatic = IsAquatic;
        // Water.Init();
        // Pest.Init();
        // Weed.Init();
        // Fertility.Init();

        // _currentStage = 0;
        // _passedTime = 0;
        // _quantityCoefficient = 0;

        // TimeManager.Instance.OnDayChange += _onDayChange;
        // TimeManager.Instance.OnSeasonChange += _onSeasonChange;
    }

    private void _onDayChange() {
        if (TimeManager.Instance.CurrentTime.GetSeason() == pp.Season.Summer)
            Water.Add(-40);
        else
            Water.Add(-20);
        Fertility.Add(-20);
        if (Random.Range(0, 1) < 0.3f)
            Weed.Add(1);
        if (Random.Range(0, 1) < 0.3f)
            Pest.Add(1);
        if (_currentStage < StageDuration.Count - 1 && ++_passedTime == StageDuration[_currentStage]) {
            _passedTime = 0;
            ++_currentStage;
        }
    }

    private void _onSeasonChange() {
        if (TimeManager.Instance.CurrentTime.GetSeason() == pp.Season.Autumn) {
            _currentStage = StageDuration.Count - 1;
            _quantityCoefficient = _calculateQuantity();
        }
    }

    private float _calculateQuantity() {
        float res = 1.0f;
        res -= (100 - Water.Value) / 100.0f;
        res -= (100 - Fertility.Value) / 100.0f;
        res -= Weed.Value * 0.2f;
        res -= Pest.Value * 0.2f;
        return Mathf.Max(res, 0.3f);
    }

    public void Harvest() {
        if (_currentStage < StageDuration.Count - 1)
            return;
        TimeManager.Instance.OnDayChange -= _onDayChange;
        TimeManager.Instance.OnSeasonChange -= _onSeasonChange;

        int finalProduct = Mathf.RoundToInt(Productivity * _quantityCoefficient);
        Item item = Instantiate<GameObject>(ProductPrefab).GetComponent<Item>();
        item.Count = finalProduct;

        if (!PlayerInventory.AddItem(item)) {
            // todo 提示背包已满
        }
        Destroy(gameObject);
    }
}
