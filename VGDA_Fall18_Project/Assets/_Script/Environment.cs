using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

    private bool Player_invulnerable_get;

    private BoxCollider2D Environment_collider;

    public Player_2point0 Player_2Point0_get;
    

    private void Awake()
    {
        Environment_collider = GetComponent<BoxCollider2D>();
        Player_2Point0_get = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_2point0>();
        Player_invulnerable_get = Player_2Point0_get.Player_invulnerable;
    }

    void Update()
    {

        Player_invulnerable_get = Player_2Point0_get.Player_invulnerable;

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
