using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Standing : MonoBehaviour
{
    //Components from Other Objects
    private Player Player_script;

    private void Awake()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Stand Death");
            Player_script.ResetScene();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Stand Death");
            Player_script.ResetScene();
        }
    }
}
