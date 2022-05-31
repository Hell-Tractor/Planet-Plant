using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager for scene: blackscene.
/// </summary>
public class BlackSceneManager : MonoBehaviour {
    public GameObject SkillPointDialog = null;
    public AudioClip Bgm;    
    private DialogManager _dialogManager;
    private void Start() {
        _dialogManager = this.GetComponent<DialogManager>();
        // show the skill point allocate dialog, and show dialog after allocating
        SkillPointDialog?.SetActive(true);
        if (SkillPointDialog != null) {
            SkillPointDialog.GetComponent<SkillPointAllocateManager>().OnConfirm += () => {
                this.GetComponent<AudioSource>().Play();
                _dialogManager.ShowDialog(1);
            };
        }
        // load Field scene after dialog
        _dialogManager.OnDialogEnd += (int id) => {
            if (id == 1) {
                SceneManager.LoadScene("Field");
            }
        };

    }

    private void OnDisable() {
        // change bgm
        var audioSrouce = PermanentNode.Instance.GetComponent<AudioSource>();
        audioSrouce.Stop();
        audioSrouce.clip = Bgm;
        audioSrouce.Play();
    }
}
