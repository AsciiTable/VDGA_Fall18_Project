using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpot : MonoBehaviour
{
    private Transform xyz;
    private Player player;
    private Transform xyzBoss;
    private BossManager manager;
    private Boss_Charge charger;


    private void Awake()
    {
        xyz = GetComponent<Transform>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
        manager = (BossManager)FindObjectOfType(typeof(BossManager));
        xyzBoss = FindObjectOfType<ShadowParker>().GetComponent<Transform>();
        charger = (Boss_Charge)FindObjectOfType (typeof(Boss_Charge));
    }

    private void Update()
    {
        xyz.position = new Vector3(xyz.position.x, xyzBoss.position.y, xyz.position.z);
        xyz.localScale = new Vector3(xyz.localScale.x, xyzBoss.localScale.y, xyz.localScale.z);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && charger.charging && manager.deathReady)
        {
            Debug.Log("Charge Death");
            player.ResetScene();
        }
    }
}
