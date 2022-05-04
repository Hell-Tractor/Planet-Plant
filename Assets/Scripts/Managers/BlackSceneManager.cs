using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackSceneManager : MonoBehaviour {
    private DialogManager _dialogManager;
    private void Start() {
        _dialogManager = this.GetComponent<DialogManager>();
        _dialogManager.OnDialogEnd += () => {
            SceneManager.LoadScene("Field");
        };
    }
}
