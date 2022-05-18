using UnityEngine;

public class HomeManager : MonoBehaviour {
    private void Start() {
        if (GlobalProperties.Instance.isFirstTimeGoHome) {
            GlobalProperties.Instance.isFirstTimeGoHome = false;

            // DialogManager.Instance.ShowDialog(22);
        }
    }
}
