using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class for managing the field scene
/// </summary>
public class FieldManager : MonoBehaviour {
    public BubbleDialogController _bubbleDialog;
    public int CountNeedToPlant = 3;
    private int _plantedCount = 0;

    /// <summary>
    /// Init beginner's guide
    /// </summary>
    private void _setupBeginnerGuide() {
        _bubbleDialog.Clear();

        _bubbleDialog.AddDialog("鼠标左键点击右上角背包图标打开物品栏", () => {
            // hide when clicked bag icon
            if (Input.GetMouseButtonDown(0)) {
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                }, hits);
                
                if (hits.Count == 0)
                    return false;

                if (hits[0].gameObject.name == "bag") {
                    return true;
                }

                return false;
            }
            return false;
        });

        _bubbleDialog.AddDialog("左键点击物品栏中的种子", () => {
            // hide when clicked seed in bag
            if (Input.GetMouseButtonDown(0)) {
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                }, hits);
                
                foreach (var hit in hits) {
                    if (hit.gameObject.CompareTag("InventorySlot")) {
                        Slot slot = hit.gameObject.GetComponent<Slot>();
                        if (slot?.GetItem()?.Id == "planetplant:seed") {
                            return true;
                        }
                    }
                }

                return false;
            }
            return false;
        });

        _bubbleDialog.AddDialog("右键点击要种下的地块", () => {
            // hide when right clicked on plantable Field
            if (Input.GetMouseButtonDown(1)) {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Farmland"));
                return hit.collider?.GetComponent<Farmland>() != null;
            }
            return false;
        });

        _bubbleDialog.AddDialog("种植成功！随时间推移，种植的作物将会成熟，收割后可以出售，是非常重要的经济来源哦~", () => {
            // hide after mouse clicked
            return Input.GetMouseButtonDown(0);
        });
    }
    
    private void Start() {
        // if is first time to field scene, show beginner's guide
        if (GlobalProperties.Instance.isFirstTimeToField) {
            GlobalProperties.Instance.isFirstTimeToField = false;

            // DialogManager.Instance.ShowDialog(19);
            this._setupBeginnerGuide();
            _bubbleDialog.Show();
        }

        // resume time system
        TimeManager.Instance.Resume();
    }

    private void Update() {
        // record the planted count
        if (Input.GetMouseButtonDown(1)) {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Farmland"));
            _plantedCount += hit.collider?.GetComponent<Farmland>() != null ? 1 : 0;

            
        }
        // if planted enough plants, show go back home dialog after delay
        if (_plantedCount >= CountNeedToPlant && !GlobalProperties.Instance.goHomeDialogShowed && Inventory._itemFollowMouse == null) {
            GlobalProperties.Instance.goHomeDialogShowed = true;
            Invoke("_showGoHomeDialog", 1f);
        }
    }

    /// <summary>
    /// Set time to evening and show go home dialog
    /// </summary>
    private void _showGoHomeDialog() {
        // PermanentNode.Instance.EnableBrightnessMask(true);
        DialogManager.Instance.ShowDialog(5);
    }
}
