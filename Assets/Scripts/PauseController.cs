using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
