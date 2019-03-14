using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    [HideInInspector] public float direction;
    [HideInInspector] public float speed;
    [HideInInspector] public float startX;
    [HideInInspector] public float distanceMax;

    private Rigidbody2D Projectile_rb2d;
    private Transform Projectile_xyz;

    private Player Player_script;
    private SpriteRenderer Player_sprite;

    void Start()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player_sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        Projectile_rb2d = GetComponent<Rigidbody2D>();
        Projectile_xyz = GetComponent<Transform>();
    }

    void Update()
    {
        //Set speed of projectile
        Projectile_rb2d.velocity = new Vector2(direction * Mathf.Abs(speed), 0f);

        //Destroy projectile when it's out of range
        if(Projectile_xyz.position.x < (startX - distanceMax) || Projectile_xyz.position.x > (startX + distanceMax))
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Projectile Death");
            Player_script.ResetScene();
        }
        if(col.gameObject.tag == "Bat")
        {
            speed = 30;
            //Facing left
            if (Player_sprite.flipX)
            {
                direction = -Mathf.Abs(direction);
            }
            //Facing right
            if (!Player_sprite.flipX)
            {
                direction = Mathf.Abs(direction);
            }
        }
    }
}
