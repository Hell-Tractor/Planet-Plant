using UnityEngine;
using UnityEngine.UI;

public class SkillPointChanger : MonoBehaviour {
    public Text PointsText;
    private int _currentPoints;

    public int Points { 
        get {
            return _currentPoints;
        }
        set {
            _currentPoints = value;
            PointsText.text = _currentPoints.ToString();
        }
    }
    
    private void Start() {
        _currentPoints = 0;
    }
}
