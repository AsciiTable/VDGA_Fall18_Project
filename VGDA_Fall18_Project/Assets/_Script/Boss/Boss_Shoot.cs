using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour
{
    //Variables
    [SerializeField] private int fireDelay;
    [SerializeField] private int openingTime;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private int projDistanceMAX;
    [SerializeField] private Vector3 projChangePosition;
    private Vector3 projectilePosition;

    //Components
    private SpriteRenderer Enemy_sprite;
    private Transform Enemy_xyz;

    //Components from Other Objects
    [SerializeField] private GameObject projectile;
    private Boss_Projectile Projectile_script;
    private ShadowParker boss;

    void Start()
    {
        Projectile_script = projectile.GetComponent<Boss_Projectile>();
        Enemy_sprite = GetComponent<SpriteRenderer>();
        Enemy_xyz = GetComponent<Transform>();
        boss = GetComponent<ShadowParker>();

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

        //Change Spawn Position of Bullet
        projectilePosition.x = Enemy_xyz.position.x - projChangePosition.x;
        projectilePosition.y = Enemy_xyz.position.y + projChangePosition.y;
        projectilePosition.z = Enemy_xyz.position.z + projChangePosition.z;
    }

    //Spawn projectile every 4 seconds
    IEnumerator fireShot()
    {
        yield return new WaitForSeconds(fireDelay);
        if (!boss.opening)
        {
            Instantiate(projectile, projectilePosition, Quaternion.identity);
        }
        StartCoroutine(fireShot());
    }
    //Stop fireShot and turn on bool for stunTime seconds (stop player collision)
    IEnumerator Opening()
    {
        boss.opening = true;
        yield return new WaitForSeconds(openingTime);
        if(!boss.bossImmunity)
            boss.opening = false;
    }
    //Get stunned if player hits back projectile
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile" && !boss.opening)
        {
            if (other.gameObject.GetComponent<Boss_Projectile>().playerProjectile)
            {
                Destroy(other.gameObject);
                StartCoroutine(Opening());
            }
        }
    }
}
