using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public Text objectText;
    private int mementosCollec;
    public int totalMementos;
    public static gameController instance;
    public GameObject gameOverObj;

    public float TimeLeft;
    public bool TimerOn=false;
    public Text TimerTxt;
    
    void Awake()
    {
        instance=this;
        mementosCollec=0;        
    }

    void Start()
    {
        objectText.text="Mementos collected: "+mementosCollec.ToString()+" / "+totalMementos;
        TimerOn=true;
    }

    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft>0)
            {
                TimeLeft-=Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else{
                TimeLeft=0;
                TimerOn=false;
                GameOver();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime+=1;
        float minutes=Mathf.FloorToInt(currentTime/60);
        float seconds=Mathf.FloorToInt(currentTime%60);
        TimerTxt.text=string.Format("{0:00} : {1:00}", minutes, seconds);

    }

    public void Objective()
    {
        mementosCollec+=1;
        objectText.text="Mementos collected: "+mementosCollec.ToString()+" / "+totalMementos;
        if(mementosCollec>=totalMementos)
        {
            FindObjectOfType<AudioManager>().Stop("Gotcha");
            FindObjectOfType<AudioManager>().Stop("BGM");
            SceneManager.LoadScene(3);
        }
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        FindObjectOfType<AIController>().Timeout();
    }

    public void GameOverScene()
    {
            SceneManager.LoadScene(2);
    }

    public void MenuGo()
    {
            SceneManager.LoadScene(0);        
    }

    public void Restart()
    {
            SceneManager.LoadScene(1);        
    }

}
