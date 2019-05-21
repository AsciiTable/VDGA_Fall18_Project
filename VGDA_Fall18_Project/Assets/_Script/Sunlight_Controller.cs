using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sunlight_Controller : MonoBehaviour {

    [Header("Editable")]
    [SerializeField] private int timeMax;
    [SerializeField] private bool inSun;
    [SerializeField] private Collider2D shade;

    private int timeLeft;
    private Text Timer_text;

    void Start () {

        Timer_text = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        timeLeft = timeMax;

        StartCoroutine(Sun());
        StartCoroutine(Shade());
    }

    private void Update()
    {
        Timer_text.text = timeLeft.ToString();

        if (shade != null)
        {
            inSun = (shade.tag == "Shade") ? false : true;
        }
        else
            inSun = true;
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
            yield return new WaitUntil(() => inSun);
        }
    }
    IEnumerator Shade()
    {
        while (true)
        {
            
            yield return new WaitUntil(() => !inSun);
            timeLeft += (!inSun && timeLeft <= (timeMax - 1)) ? 1 : 0;
            yield return new WaitForSeconds(0.1f);
            timeLeft += (!inSun && timeLeft < (timeMax - 5)) ? 1 : 0;
            yield return new WaitForSeconds(0.1f);
            timeLeft += (!inSun && timeLeft < (timeMax - 5)) ? 1 : 0;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        shade = col;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(shade.tag == "Shade")
            shade = null;
    }

}
