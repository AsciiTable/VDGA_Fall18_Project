using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_Controller : MonoBehaviour {

    [Header("Editable")]
    public int timeMax;
    public int timeCollect;

    [Space(10)]

    [Header("Resets by Start Command")]
    [Tooltip("Starts as timeMax")]
    public int timeLeft;

    private Text Timer_text;

    void Start () {

        Timer_text = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        timeLeft = timeMax;

        //Start Every one Second
        InvokeRepeating("TimerPass", 0, 1);
	}

    private void Update()
    {
        Timer_text.text = timeLeft.ToString();
    }

    private void TimerPass()
    {
        timeLeft -= 1;
        

        if (timeLeft <= 0)
        {
            Debug.Log("TIMESUP");
            CancelInvoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
