using UnityEngine;

public class PermanentNode : MonoBehaviour {
    public static PermanentNode Instance;

    private void Awake() {
        Instance = this;
    }
}
