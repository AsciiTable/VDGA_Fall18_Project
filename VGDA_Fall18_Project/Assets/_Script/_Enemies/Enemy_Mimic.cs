using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mimic : MonoBehaviour
{
    //Components from Other Objects
    private Player Player_script;
    private Animator animator;

    private void Awake()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    IEnumerator mimicDeath()
    {
        Player_script.restrained = true;
        Debug.Log("Can Death");
        animator.SetTrigger("Kill");
        yield return new WaitForSeconds(0.5f);
        Player_script.ResetScene();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            StartCoroutine(mimicDeath());
            
        }
    }
}
