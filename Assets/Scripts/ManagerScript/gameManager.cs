using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static gameManager m_instance { get; set; }

    private Scene m_currentScene;

    public static gameManager Instance
    {
        get { return m_instance; }
        
    }


    private void Awake()
    {
       if (m_instance != null && m_instance != this)
       {
           Destroy(this.gameObject);
       }
       else
       {
           m_instance = this;
           DontDestroyOnLoad(this.gameObject);
       } 
       m_currentScene = SceneManager.GetActiveScene();
    }

    public void GameOver()
    {
        //Get the scene after the current scene
        SceneManager.LoadScene(m_currentScene.buildIndex + 1);
    }

    public void GameLost()
    {
        SceneManager.LoadScene("gameOver");
    }
    
}
