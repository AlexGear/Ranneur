using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject dieText;
    [SerializeField] GameObject buttonContinue;
    PlayerController player;
    private bool died = false;
        
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!player.isAlive && !died) {
            died = true;
            dieText.SetActive(true);
            buttonContinue.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void ReloadLevel() {
        SetActivePanel(false);
        SceneManager.LoadScene(1);
    }

    public void SetActivePanel(bool isActive) {
        if(player.isAlive)
        {
            if (isActive)
                Time.timeScale = 0;
            else 
               Time.timeScale = 1;     
        }
        panel.SetActive(isActive);
    }

    public void ToggleActivePanel()
    {
        SetActivePanel(!panel.activeSelf);
    }

    public void ExitTheGame() {
        Application.Quit();
    }
}
