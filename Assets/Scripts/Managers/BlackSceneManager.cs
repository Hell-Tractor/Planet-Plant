using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manager for scene: blackscene.
/// </summary>
public class BlackSceneManager : MonoBehaviour {
    public GameObject SkillPointDialog = null;
    
    private DialogManager _dialogManager;
    private void Start() {
        _dialogManager = this.GetComponent<DialogManager>();
        // show the skill point allocate dialog, and show dialog after allocating
        SkillPointDialog?.SetActive(true);
        if (SkillPointDialog != null) {
            SkillPointDialog.GetComponent<SkillPointAllocateManager>().OnConfirm += () => {
                _dialogManager.ShowDialog(1);
            };
        }
        // load Field scene after dialog
        _dialogManager.OnDialogEnd += (int id) => {
            SceneManager.LoadScene("Field");
        };

    }
}
