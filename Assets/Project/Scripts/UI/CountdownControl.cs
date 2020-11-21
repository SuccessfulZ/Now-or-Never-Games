using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownControl : MonoBehaviour
{
    public Image cooldown;
    private bool coolingDown = false;
    private float waitTime = 7.0f; //Game time
    private int secondsWait = 2;
    public SceneManager sceneManager;

    private void Start()
    {
        StartTimeCount();
    }
   
   
    void Update()
    {

        ReduceTime();
        CheckTime();

    }

    public void StartTimeCount()
    {
        // sceneManager.GameEndControl(); // turn off pause panel    
        WaitForStart();//Few seconds for make User ready for the game
         coolingDown = true;
    }

    public void ReduceTime()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 3 minutes
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }
     public void CheckTime()
    { //check if time of game is over
        if (cooldown.fillAmount == 0)
        {
            sceneManager.GameEndControl(); //turn on pause panel
            coolingDown = false;
            cooldown.fillAmount = 1;
        }
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(secondsWait);
    }

}
