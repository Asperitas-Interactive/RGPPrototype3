using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static gameManager m_instance { get; set; }


    private Scene m_currentScene;
    [SerializeField] public float[] m_enemySpeed = { 8f, 10f };
    public float m_maxWanderTime;
    public float[] m_counterTime = { 1.5f, 3f };
    public float[] m_StunTimer = { 3.4f / 5f };
    public float[] m_defaultStunTimer;
    public string m_State;
    public float[] m_attackCooldown = { 3.5f, 4.5f };
    

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

    public void Arena2()
    {
        m_enemySpeed[0] = 10f;
        m_enemySpeed[1] = 12f;
        m_StunTimer[0] = 1.5f;
        m_StunTimer[1] = 2.5f;
        m_defaultStunTimer[0] = 1.5f;
        m_defaultStunTimer[1] = 2.5f;
        m_attackCooldown[0] = 2f;
        m_attackCooldown[1] = 3f;
        m_counterTime[0] = 0f;
        m_counterTime[1] = 2f;
    }
    
    public void Arena3()
    {
        m_enemySpeed[0] = 11f;
        m_enemySpeed[1] = 13f;
        m_StunTimer[0] = 1f;
        m_StunTimer[1] = 1.8f;
        m_defaultStunTimer[0] = 1.2f;
        m_defaultStunTimer[1] = 2.4f;
        m_attackCooldown[0] = 2f;
        m_attackCooldown[1] = 3f;
        m_counterTime[0] = 0f;
        m_counterTime[1] = 2f;
    }
    
}
