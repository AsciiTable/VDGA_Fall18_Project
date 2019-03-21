using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Charge : MonoBehaviour
{
    //Input Variables
    [Tooltip("Total distance charged")]
    [SerializeField] private float chargeDistance;
    [Tooltip("How Long Enemy Slows For")]
    [SerializeField] private float slowDistance;
    [SerializeField] private int chargeStartupTime;
    //Variables
    private float stopBorder;
    private float slowBorder;
    private bool chargeReady;
    private RaycastHit2D lineSight;
    //Components
    private Transform Enemy_xyz;
    private SpriteRenderer Enemy_sprite;
    //Components from Other Objects
    private Player Player_script;

    private void Awake()
    {
        Enemy_xyz = GetComponent<Transform>();
        Enemy_sprite = GetComponent<SpriteRenderer>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Start readyCharge (wait till player is in line of sight)
        StartCoroutine(readyCharge());
    }

    private void FixedUpdate()
    {
        //Facing right
        if (Enemy_sprite.flipX)
        {
            //Make line right to find player
            lineSight = Physics2D.Raycast(Enemy_xyz.position, Vector2.right, chargeDistance);
        }
        //Facing left
        else if (!Enemy_sprite.flipX)
        {
            //Make line left to find player
            lineSight = Physics2D.Raycast(Enemy_xyz.position, Vector2.left, chargeDistance);
        }
        //Check if player is in line of sight
        if (lineSight.collider.tag == "Player")
        {
            chargeReady = true;
        }
        else if(lineSight.collider.tag != "Player")
        {
            chargeReady = false;
        }
        
    }
    IEnumerator readyCharge()
    {
        //Facing right
        if (Enemy_sprite.flipX)
        {
            stopBorder = Enemy_xyz.position.x + chargeDistance;
            slowBorder = stopBorder-slowDistance;
        }
        //Facing left
        else if(!Enemy_sprite.flipX)
        {

        }
        //activate charge when chargeReady is true (Player in line of site)
        yield return new WaitUntil(()=> chargeReady);
        StartCoroutine(charge());
    }
    //
    IEnumerator charge()
    {
        yield return new WaitForSeconds(5);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Charge Death");
            Player_script.ResetScene();
        }
    }
}
