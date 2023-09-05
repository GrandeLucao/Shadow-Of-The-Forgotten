using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{    
    public static menuController instance;

    void Awake()
    {
        instance=this;       
    }

    void Start()
    {
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
    }

    public void GameOverScene()
    {
            SceneManager.LoadScene(2);
    }

    public void MenuGo()
    {
            Application.Quit();      
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
        SceneManager.LoadScene(1);        
    }
}
