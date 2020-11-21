using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownControl : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    
    void Start()
    {
        
    }

  
    IEnumerator CountdownToEnd()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }



        countdownDisplay.text = "Game Over";
    }

    


}
