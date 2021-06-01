using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void GOToLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void GOToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
