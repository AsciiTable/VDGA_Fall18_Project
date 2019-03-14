using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    //Variables
    [SerializeField] private int fireDelay;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private int projDistanceMAX;
    [SerializeField] private Vector3 projChangePosition;
    private Vector3 projectilePosition;

    //Components
    private SpriteRenderer Enemy_sprite;
    private Transform Enemy_xyz;
    private Rigidbody2D Enemy_rb2d;

    //Components from Other Objects
    [SerializeField] private GameObject projectile;
    private Enemy_Projectile Projectile_script;
    private Player Player_script;
    private Transform Player_xyz;

    void Start()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player_xyz = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Projectile_script = projectile.GetComponent<Enemy_Projectile>();
        Enemy_sprite = GetComponent<SpriteRenderer>();
        Enemy_xyz = GetComponent<Transform>();
        Enemy_rb2d = GetComponent<Rigidbody2D>();

        //Activate IEnumerator fireShot()
        StartCoroutine(fireShot());
    }

    // Update is called once per frame
    void Update()
    {
        //Set projectile speed and max distance
        Projectile_script.speed = Mathf.Abs(projectileSpeed);
        Projectile_script.distanceMax = Mathf.Abs(projDistanceMAX);
        Projectile_script.startX = projectilePosition.x;

       //Face left
       if(Enemy_xyz.position.x > Player_xyz.position.x)
        {
            Enemy_sprite.flipX = false;
            Projectile_script.direction = -1f;
            //Change Spawn Position of Bullet
            projectilePosition.x = Enemy_xyz.position.x - projChangePosition.x;
            projectilePosition.y = Enemy_xyz.position.y + projChangePosition.y;
            projectilePosition.z = Enemy_xyz.position.z + projChangePosition.z;
        }
       //Face right
       else if (Enemy_xyz.position.x < Player_xyz.position.x)
        {
            Enemy_sprite.flipX = true;
            Projectile_script.direction = 1f;
            //Change Spawn Position of Bullet
            projectilePosition = Enemy_xyz.position + projChangePosition;
        }
    }

    //Spawn projectile every 4 seconds
    IEnumerator fireShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            Instantiate(projectile, projectilePosition,Quaternion.identity);
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
