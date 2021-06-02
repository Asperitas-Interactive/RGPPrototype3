using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static gameManager m_instance { get; set; }


    private Scene m_currentScene;
    [SerializeField] public float m_enemySpeed;
    public float m_maxWanderTime;
    public float m_defaultStunTimer = 3f;
    public float m_counterTime = 1.5f;
    public float m_StunTimer = 3f;
    public string m_State;
    public float m_attackCooldown = 4.5f;

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
