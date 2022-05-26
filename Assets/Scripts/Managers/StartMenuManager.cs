using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class for managing start menu scene.
/// </summary>
public class StartMenuManager : MonoBehaviour {
    public Sprite StartGameBackground = null;
    /// <summary>
    /// time to wait before scene change
    /// </summary>
    public float WaitSeconds = 1;
    public AudioClip StartGameSound = null;

    private AudioSource _audioSource;

    private void Start() {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public async void StartGame() {
        if (StartGameBackground != null) {
            // play the game start audio
            this._audioSource.PlayOneShot(StartGameSound);
            this.GetComponent<Image>().sprite = StartGameBackground;
            // load the scene and disable auto scene change
            var sceneLoader = SceneManager.LoadSceneAsync("BlackScene", LoadSceneMode.Single);
            sceneLoader.allowSceneActivation = false;

            // wait given time, then enable scene change
            await Task.Delay(TimeSpan.FromSeconds(WaitSeconds));
            sceneLoader.allowSceneActivation = true;
        }
    }

    public void EndGame() {
        // 结束游戏
        Application.Quit();
    }
}
