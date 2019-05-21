using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sunlight_Controller : MonoBehaviour {

    [Header("Editable")]
    [SerializeField] private bool activated = true;
    [SerializeField] private int timeMax = 20;
    [SerializeField] private int timeCollect = 8;
    [SerializeField] private bool inSun;

    private int timeLeft;
    private Text Timer_text;

    void Start () {

        Timer_text = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        timeLeft = timeMax;

        StartCoroutine(Sun());
    }

    private void Update()
    {
        Timer_text.text = timeLeft.ToString();
    }

    IEnumerator Sun()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft -= (inSun) ? 1 : 0;

            if (timeLeft <= 0)
            {
                Debug.Log("TIMESUP");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sunlight")
            inSun = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Sunlight")
            inSun = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Collectable")
        {
            if (timeLeft + timeCollect > timeMax)
                timeLeft = timeMax;
            else
                timeLeft += timeCollect;
            Destroy(col.gameObject);
        }
    }

}
