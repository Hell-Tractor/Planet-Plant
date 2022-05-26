using UnityEngine;

namespace Minigame.GM {

/// <summary>
/// Class for saving player properties in the minigame: Going Market.
/// </summary>
public class PlayerProperties : MonoBehaviour {
    public float Speed = 3.0f;
    public float JumpSpeed = 8.0f;

    /// <summary>
    /// Collected coin count of player.
    /// </summary>
    [ReadOnly]
    public int CoinCount = 0;
}

}