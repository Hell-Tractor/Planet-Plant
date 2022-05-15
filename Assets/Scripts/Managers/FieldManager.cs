using UnityEngine;

public class FieldManager : MonoBehaviour {
    private void Start() {
        if (GlobalProperties.Instance.isFirstTimeToField) {
            GlobalProperties.Instance.isFirstTimeToField = false;

            DialogManager.Instance.ShowDialog(19);
        }

        TimeManager.Instance.Resume();
    }
}
