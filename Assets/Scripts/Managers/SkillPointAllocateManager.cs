using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for managing skill point allocate UI.
/// </summary>
public class SkillPointAllocateManager : MonoBehaviour {
    // sum of skill point for player to allocate
    public int SumPoints = 10;
    public GameObject ConfirmButton = null;

    // all type of skill points
    private Dictionary<string, SkillPointChanger> _skillPointChangers;

    public AudioSource AudioSource = null;
    public event Action OnConfirm;

    public void Start() {
        // init the skill point dictionary
        _skillPointChangers = new Dictionary<string, SkillPointChanger>();
        foreach (Transform child in this.transform) {
            var skillPointChanger = child.GetComponent<SkillPointChanger>();
            if (skillPointChanger != null) {
                _skillPointChangers.Add(child.name, skillPointChanger);
            }
        }
        _skillPointChangers["SumPoints"].Points = SumPoints;
    }

    /// <summary>
    /// Add points to given skill.
    /// </summary>
    /// <param name="skillName">name of the skill to be Added</param>
    public void AddPoints(string skillName) {
        // update points if operation is valid
        if (_skillPointChangers.ContainsKey(skillName) && _skillPointChangers["SumPoints"].Points > 0) {
            _skillPointChangers[skillName].Points++;
            _skillPointChangers["SumPoints"].Points--;
        }
    }

    /// <summary>
    /// Reduce points from given skill.
    /// </summary>
    /// <param name="skillName">name of the skill to be reduced</param>
    public void ReducePoints(string skillName) {
        // update points if operation is valid
        if (_skillPointChangers.ContainsKey(skillName) && _skillPointChangers[skillName].Points > 0) {
            _skillPointChangers[skillName].Points--;
            _skillPointChangers["SumPoints"].Points++;
        }
    }

    private void OnGUI() {
        // if confirm button can only be clicked when all the points have been used
        var _button = ConfirmButton.GetComponent<Button>();
        if (_button != null) {
            _button.interactable = _skillPointChangers["SumPoints"].Points == 0;
        }
    }
    
    /// <summary>
    /// Close the dialog and save the changes.
    /// </summary>
    public async void Close() {
        // Play sound
        AudioSource.Play();
        await Task.Delay(350);

        // save to GlobalProperties
        GlobalProperties.Instance.PlayerIntelligence = _skillPointChangers["Intelligence"].Points;
        GlobalProperties.Instance.PlayerPhysique = _skillPointChangers["Physique"].Points;

        // call the confirm event
        OnConfirm?.Invoke();

        this.gameObject.SetActive(false);
    }
}
