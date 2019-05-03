using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Boss : MonoBehaviour
{
    private ShadowParker parent;
    private Transform xyz;

    private Player player;
    private BossManager manager;
    private Boss_Charge charge;

    void Awake()
    {
        parent = GetComponentInParent<ShadowParker>();
        xyz = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = FindObjectOfType<BossManager>().GetComponent<BossManager>();
        charge = (Boss_Charge)FindObjectOfType(typeof(Boss_Charge));
    }

    private void Update()
    {
        if(charge.chargeDirection == -1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(0.45f, 0, 0);
        }
        else if (charge.chargeDirection == 1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(-0.45f, 0, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boss Death");
            player.ResetScene();
        }
        if (collision.gameObject.tag == "Bat" && !parent.bossImmunity && parent.opening == true)
        {
            parent.health -= 1;
        }
    }
}
