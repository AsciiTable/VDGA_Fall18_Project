using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
    //Variables (get changed by Enemy_Shoot script)
    [HideInInspector] public float speed;
    [HideInInspector] public float startX;
    [HideInInspector] public float distanceMax;
    [HideInInspector] public bool  playerProjectile = false;

    //Components
    private Rigidbody2D Projectile_rb2d;
    private Transform Projectile_xyz;
    private ShadowParker boss;
    //Components from other scripts
    private Player Player_script;
    private SpriteRenderer Player_sprite;

    void Start()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player_sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        Projectile_rb2d = GetComponent<Rigidbody2D>();
        Projectile_xyz = GetComponent<Transform>();
        boss = FindObjectOfType<ShadowParker>().GetComponent<ShadowParker>();
    }

    void Update()
    {
        //Set speed of projectile
        Projectile_rb2d.velocity = (playerProjectile) ? new Vector2(Mathf.Abs(speed), 0f) : new Vector2(-Mathf.Abs(speed), 0f);

        //Destroy projectile when it's out of range
        if(Projectile_xyz.position.x < (startX - distanceMax) || Projectile_xyz.position.x > (startX + distanceMax))
        {
            Destroy(gameObject);
        }
        if (boss.opening)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Kill player on hit
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Projectile Death");
            Player_script.ResetScene();
        }
        //Reflect projectile back if bat hits it
        if(col.gameObject.tag == "Bat")
        {
            playerProjectile = true;
            speed = 100;
            //Facing left
        }
    }
}
