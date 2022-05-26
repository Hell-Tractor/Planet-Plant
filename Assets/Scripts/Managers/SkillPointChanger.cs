using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for managing one skill point allocating.
/// </summary>
public class SkillPointChanger : MonoBehaviour {
    public Text PointsText;
    private int _currentPoints;

    public int Points { 
        get {
            return _currentPoints;
        }
        set {
            _currentPoints = value;
            // update the text when points changed
            PointsText.text = _currentPoints.ToString();
        }
    }
    
    private void Start() {
        _currentPoints = 0;
    }
}
