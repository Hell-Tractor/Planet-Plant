using UnityEngine;

/// <summary>
/// Class for managing the market scene
/// </summary>
public class MarketManager : MonoBehaviour {
    public RealMoneyShowManager RealMoneyShower = null;
    public MoneyScreeningManager MoneyScreeningManager = null;
    public GameObject Buyer = null;
    public ShopInventory Shop = null;
    public BubbleDialogController BubbleDialogController = null;
    public Item SpecialItem = null;
    public Transform SellPoint = null;
    private DialogManager _dialogManager;
    // after dialog with id in list is shown, RealMoneyShowDialog will be shown
    private int[] _jumpToRealMoneyShowDialogIDList = new int[] {
        39, 41, 61
    };
    private void Start() {
        // init random number generator
        Random.InitState(System.DateTime.Now.Millisecond);
        
        _dialogManager = DialogManager.Instance;
        // hide character: buyer
        Buyer?.SetActive(false);
        if (_dialogManager != null) {
            // register event handler
            _dialogManager.AfterDialogChange += this._onMarketDialogChange;

            if (GlobalProperties.Instance.isFristTimeToMarket) {
                // if is first time to market, show dialog
                GlobalProperties.Instance.isFristTimeToMarket = false;

                _dialogManager.ShowDialog(7);
                _dialogManager.OnDialogEnd += (int dialogid) => {
                    if (dialogid == 7) {
                        // show beginner's guide after dialog
                        _setupBeginnerGuide();
                        // to avoid key processing error, delay showing bubble dialog
                        BubbleDialogController?.Show(0.3f);
                    } else if (dialogid == 18) {
                        // give special item to player
                        QuickSlot.Instance.AddItem(Instantiate<Item>(SpecialItem));
                    }
                };
            }
        }

        // _dialogManager.ShowDialog(8);
    }

    /// <summary>
    /// Init beginner's guide
    /// </summary>
    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("右边是情绪条，你现在的情绪值有点低，可以通过购买喜欢的物品来提高情绪值。情绪值若太低，有一定几率做出不理智事件哦！", () => {
            if (Input.GetMouseButton(0)) {
                // show shop UI, and show the dialog after shop UI closed
                Shop.Show(() => {
                    AI.FSM.CharacterFSM.Instance.transform.position = SellPoint.position;
                    _dialogManager.ShowDialog(8);
                });
                return true;
            }
            return false;
        });
    }

    private void OnDestory() {
        if (_dialogManager != null) {
            // unregister event handler
            _dialogManager.AfterDialogChange -= this._onMarketDialogChange;
        }
    }

    private void _onMarketDialogChange(Data.DialogData dialogData) {
        if (dialogData.ID == 34) {
            Buyer?.SetActive(true);
            return;
        }
        
        // 跳转到真币展示
        foreach (var id in _jumpToRealMoneyShowDialogIDList) {
            if (dialogData.ID == id) {
                _dialogManager.ShowDialog(15);
                return;
            }
        }
        
        // 真币展示
        if (dialogData.ID == 63) {
            _dialogManager.UI.PreventKeyEventProcessing = true;
            RealMoneyShower?.gameObject.SetActive(true);
            RealMoneyShower?.Show(() => {
                _dialogManager.ShowNext();
                _dialogManager.UI.PreventKeyEventProcessing = false;
            });
            return;
        }

        // 假币甄别
        if (dialogData.ID == 64) {
            MoneyScreeningManager?.gameObject.SetActive(true);
            MoneyScreeningManager?.ShowMoney();
            return;
        }
    }
}
