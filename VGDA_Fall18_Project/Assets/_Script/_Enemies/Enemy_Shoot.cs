using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    //Variables
    [SerializeField] private int fireDelay;

    //Components
    private SpriteRenderer Enemy_sprite;
    private Transform Enemy_xyz;

    //Components from Other Scripts
    private Player Player_script;
    private Transform Player_xyz;

    void Start()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player_xyz = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Enemy_sprite = GetComponent<SpriteRenderer>();
        Enemy_xyz = GetComponent<Transform>();

        //Activate IEnumerator fireShot()
        StartCoroutine(fireShot());
    }

    // Update is called once per frame
    void Update()
    {
        
       //Face left
       if(Enemy_xyz.position.x > Player_xyz.position.x)
        {
            Enemy_sprite.flipX = false;
        }
       //Face right
       else if (Enemy_xyz.position.x < Player_xyz.position.x)
        {
            Enemy_sprite.flipX = true;
        }
    }

    //Spawn projectile every 4 seconds
    IEnumerator fireShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            Debug.Log("Fire");
        }
        


    }

    //Destroy player if touching
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy Death");
            Player_script.ResetScene();
        }
    }

}
