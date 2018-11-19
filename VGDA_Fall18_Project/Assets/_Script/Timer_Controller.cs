using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Controller : MonoBehaviour {

    [Header("Editable")]
    public int timeMax;
    public int timeCollect;

    [Space(10)]

    [Header("Resets by Start Command")]
    [Tooltip("Starts as timeMax")]
    public int timeLeft;


    void Start () {

        timeLeft = timeMax;

        //Start Every one Second
        InvokeRepeating("TimerPass", 0, 1);
	}

    private void TimerPass()
    {
        timeLeft -= 1;
        Debug.Log(timeLeft);

        if (timeLeft <= 0)
        {
            Debug.Log("TIMESUP");
            CancelInvoke();
        }
    }

    public void CollectTime()
    {
        timeLeft += timeCollect;
        if (timeLeft > timeMax)
        {
            timeLeft = timeMax;
        }
    }

}
