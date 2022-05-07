using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackSceneManager : MonoBehaviour {
    public GameObject SkillPointDialog = null;
    
    private DialogManager _dialogManager;
    private void Start() {
        _dialogManager = this.GetComponent<DialogManager>();
        SkillPointDialog?.SetActive(true);
        if (SkillPointDialog != null) {
            SkillPointDialog.GetComponent<SkillPointAllocateManager>().OnConfirm += () => {
                _dialogManager.ShowDialog(1);
            };
        }
        _dialogManager.OnDialogEnd += (int id) => {
            SceneManager.LoadScene("Field");
        };

    }
}
