
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject runButton;
    [SerializeField] GameObject music;
    [SerializeField] GameObject sound;

    private AudioSource track;
    private AudioSource laugh;

    private void Awake()
    {
        track = music.GetComponent<AudioSource>();
        laugh = sound.GetComponent<AudioSource>();
        DontDestroyOnLoad(music);
        DontDestroyOnLoad(laugh);
    }

    public void StartGame() {
        laugh.Play();               
        SceneManager.LoadScene(1);
    }

    public void Settings() {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        runButton.SetActive(!settingsPanel.activeSelf);
    }

    public void CanvasClicked() {
        settingsPanel.SetActive(false);
        runButton.SetActive(true);
    }

    public void setMusicVolume(float value) {
        track.volume = value;
    }

    public void setSoundsVolume(float value) {
        laugh.volume = value;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
