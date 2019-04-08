using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void ReloadLevel() {
        SetActivePanel();
        SceneManager.LoadScene(1);
    }

    public void SetActivePanel() {
        panel.SetActive(!panel.activeSelf);
    }

    public void ExitTheGame() {
        Application.Quit();
    }
}
