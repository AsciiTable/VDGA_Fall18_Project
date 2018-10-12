using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

    /*TODO  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
    add prefabs on everything
    touching deathness when player not invulnerable kills him
         */

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

        //       Debug.Log("Invulnerable: " + Player_invulnerable_get);
        //       Debug.Log("Collider: " + Environment_collider.enabled);

        if (Player_invulnerable_get == true)
        {
            Environment_collider.enabled = false;
        }
        if (Player_invulnerable_get == false)
        {
            Environment_collider.enabled = true;
        }
    }
}
