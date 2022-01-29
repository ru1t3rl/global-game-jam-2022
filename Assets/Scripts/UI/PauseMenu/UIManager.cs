using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public void ResumeGame(GameObject panel) {
        panel.SetActive(false);
    }

    public void ReturnToScene(string sceneName) { 
        SceneManager.LoadScene(sceneName);
    }

   
    private void FixedUpdate()
    {
        PauseGame(pausePanel);
    }

    private void PauseGame(GameObject pausePanel)
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
        }
    }
}
