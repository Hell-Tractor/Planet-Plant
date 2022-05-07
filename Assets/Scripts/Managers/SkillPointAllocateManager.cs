using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointAllocateManager : MonoBehaviour {
    public int SumPoints = 10;
    public GameObject ConfirmButton = null;

    private Dictionary<string, SkillPointChanger> _skillPointChangers;

    public event Action OnConfirm;

    public void Start() {
        _skillPointChangers = new Dictionary<string, SkillPointChanger>();
        foreach (Transform child in this.transform) {
            var skillPointChanger = child.GetComponent<SkillPointChanger>();
            if (skillPointChanger != null) {
                _skillPointChangers.Add(child.name, skillPointChanger);
            }
        }
        _skillPointChangers["SumPoints"].Points = SumPoints;
    }

    public void AddPoints(string skillName) {
        if (_skillPointChangers.ContainsKey(skillName) && _skillPointChangers["SumPoints"].Points > 0) {
            _skillPointChangers[skillName].Points++;
            _skillPointChangers["SumPoints"].Points--;
        }
    }

    public void ReducePoints(string skillName) {
        if (_skillPointChangers.ContainsKey(skillName) && _skillPointChangers[skillName].Points > 0) {
            _skillPointChangers[skillName].Points--;
            _skillPointChangers["SumPoints"].Points++;
        }
    }

    private void OnGUI() {
        var _button = ConfirmButton.GetComponent<Button>();
        if (_button != null) {
            _button.interactable = _skillPointChangers["SumPoints"].Points == 0;
        }
    }
    
    public void Close() {
        GlobalProperties.Instance.PlayerIntelligence = _skillPointChangers["Intelligence"].Points;
        GlobalProperties.Instance.PlayerPhysique = _skillPointChangers["Physique"].Points;
        this.gameObject.SetActive(false);

        OnConfirm?.Invoke();
    }
}
