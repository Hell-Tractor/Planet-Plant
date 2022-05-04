using System.Collections.Generic;
using UnityEngine;

public class SkillPointAllocateManager : MonoBehaviour {
    public int SumPoints = 10;

    private Dictionary<string, SkillPointChanger> _skillPointChangers;

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
}
