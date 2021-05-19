using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    private static gameManager _instance;

    private Scene currentScene;

    public static gameManager Instance { get { return _instance; } }


    private void Awake()
    {
       if (_instance != null && _instance != this)
       {
           Destroy(this.gameObject);
       }
       else
       {
           DontDestroyOnLoad(this.gameObject);
       }

        currentScene = SceneManager.GetActiveScene();
    }

    public void gameOver()
    {
        //Get the scene after the current scene
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void gameLost()
    {
        SceneManager.LoadScene("gameOver");
    }
    
}
