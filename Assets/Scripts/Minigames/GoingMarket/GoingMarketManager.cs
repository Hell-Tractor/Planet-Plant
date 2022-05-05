using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingMarketManager : MonoBehaviour {
    public Transform Exit;
    public float ExitDectectionRadius = 0.1f;

    private void Update() {
        RaycastHit2D hit = Physics2D.CircleCast(Exit.position, ExitDectectionRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null) {
            SceneManager.LoadScene("Market");
        }
    }
}
