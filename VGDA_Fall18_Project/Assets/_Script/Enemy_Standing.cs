using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Standing : MonoBehaviour
{

    private Player Player_script;

    private BoxCollider2D Enemy_Standing_collider;

    private bool Player_invulnerable_get;

    private void Awake()
    {
        Enemy_Standing_collider = GetComponent<BoxCollider2D>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Player_invulnerable_get = Player_script.Player_invulnerable;

        if (Player_invulnerable_get == true)
        {
            Enemy_Standing_collider.isTrigger = true;
        }
        if (Player_invulnerable_get == false)
        {
            Enemy_Standing_collider.isTrigger = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy Death");
            Player_script.ResetScene();
        }
    }
}
