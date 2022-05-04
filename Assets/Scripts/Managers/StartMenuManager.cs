using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour {
    public Sprite StartGameBackground = null;
    public float WaitSeconds = 1;
    public AudioClip StartGameSound = null;

    private AudioSource _audioSource;

    private void Start() {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public async void StartGame() {
        if (StartGameBackground != null) {
            this._audioSource.PlayOneShot(StartGameSound);
            this.GetComponent<Image>().sprite = StartGameBackground;
            var sceneLoader = SceneManager.LoadSceneAsync("Field", LoadSceneMode.Single);

            sceneLoader.allowSceneActivation = false;
            await Task.Delay(TimeSpan.FromSeconds(WaitSeconds));
            sceneLoader.allowSceneActivation = true;
        }
    }

    public void EndGame() {
        // 结束游戏
        Application.Quit();
    }
}
