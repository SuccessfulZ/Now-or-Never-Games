using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownControl : MonoBehaviour
{
    //public float cooldown;
    private bool coolingDown = false;
    private float gameTime = 180.0f; //Game time
    private float secondsWait = 3;
    public Text timeText;
    public SceneManager sceneManager;
    public float TimeLeft => gameTime;

    private void Start()
    {
        StartTimeCount();
    }
   
   
    void Update()
    {
        DisplayTime(gameTime);
        ReduceTime();
        CheckTime();

    }

    public void StartTimeCount()
    {
       //sceneManager.MenuControl(); // turn off pause panel    
        WaitForStart();//Few seconds for make User ready for the game
         coolingDown = true;
    }

    public void ReduceTime()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 3 minutes
            gameTime -= Time.deltaTime;
        }       
    }
     public void CheckTime()
    { //check if time of game is over
        if (gameTime <= 0)
        {
           sceneManager.GameEndControl(); //turn on pause panel
            coolingDown = false;
            gameTime = 180;
        }
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(secondsWait);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
