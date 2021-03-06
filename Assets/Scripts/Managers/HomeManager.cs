using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Class for managing home scene
/// </summary>
public class HomeManager : MonoBehaviour {
    public BubbleDialogController BubbleDialogController = null;
    public Transform BedPosition = null;
    public GameObject Sister = null;
    private bool _isAfterMarketDialogShown = false;
    private void Start() {
        bool isShowingDialog4 = false;
        
        // if is first time go home, show beignner's guide
        if (GlobalProperties.Instance.isFirstTimeGoHome) {
            GlobalProperties.Instance.isFirstTimeGoHome = false;

            // add action to after dialog change event: move player to given position
            DialogManager.Instance.AfterDialogChange += (Data.DialogData dialogData) => {
                if (dialogData?.ID == 69) {
                    this._movePlayerAsync(1.5f);
                    AI.FSM.CharacterFSM.Instance.GetComponent<SpriteRenderer>().enabled = false;
                    PermanentNode.Instance.EnableBrightnessMask(true, true);
                } else if (dialogData?.ID == 26) {
                    AI.FSM.CharacterFSM.Instance.GetComponent<SpriteRenderer>().enabled = true;
                    PermanentNode.Instance.EnableBrightnessMask(false, true);
                }
            };

            DialogManager.Instance.OnDialogEnd += (int dialogid) => {
                if (dialogid == 4) {
                    EmotionManager.Instance.EmotionValue -= 20;
                } else if (dialogid == 13) {
                    BubbleDialogController?.Show(0.5f);
                }
            };

            _setupBeginnerGuide();
            if (GlobalProperties.Instance.isFristTimeToMarket == false) {
                Sister.SetActive(false);
                DialogManager.Instance.ShowDialog(13);
                _isAfterMarketDialogShown = true;
            } else {
                DialogManager.Instance.ShowDialog(4);
                isShowingDialog4 = true;
            }
        }

        // if has been to market and not shown dialog, show dialog
        if (!_isAfterMarketDialogShown && !isShowingDialog4) {
            Sister.SetActive(false);
            DialogManager.Instance.ShowDialog(13);
            _isAfterMarketDialogShown = true;
        }
    }

    /// <summary>
    /// Init beginner's guide
    /// </summary>
    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("???????????????????????????1?????? ????????????????????????????????? ???????????????????????????????????????", () => {
            return Input.GetMouseButton(0);
        });
    }

    private async void _movePlayerAsync(float delay) {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        AI.FSM.CharacterFSM.Instance.transform.localPosition = BedPosition.localPosition;
    }
}
