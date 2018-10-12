using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

    private bool Player_invulnerable_get;

    private BoxCollider2D Environment_collider;

    private Player Player_script;



    private void Awake()
    {
        Environment_collider = GetComponent<BoxCollider2D>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
        Player_invulnerable_get = Player_script.Player_invulnerable;

        //Debug.Log("Invulnerable: " + Player_invulnerable_get);
        //Debug.Log("Collider: " + Environment_collider.enabled);

//Changes if player can pass through block if invulnerable
        if (Player_invulnerable_get == true)
        {
            Environment_collider.isTrigger = true;
        }
        if (Player_invulnerable_get == false)
        {
            Environment_collider.isTrigger = false;
        }
    }

//Changes player_inside when collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player_script.Player_inside = true;
            Debug.Log("Enter");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player_script.Player_inside = true;
            Debug.Log("Enter");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player_script.Player_inside = false;
            Debug.Log("Exit");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player_script.Player_inside = false;
            Debug.Log("Exit");
        }
    }

}
