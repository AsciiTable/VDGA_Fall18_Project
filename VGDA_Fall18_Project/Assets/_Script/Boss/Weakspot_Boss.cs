using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Boss : MonoBehaviour
{
    private ShadowParker parent;
    private Transform xyz;

    private Player player;
    private SpriteRenderer bossSprite;
    private SpriteRenderer playerSprite;
    private BossManager manager;
    private Boss_Charge charge;

    void Awake()
    {
        parent = GetComponentInParent<ShadowParker>();
        xyz = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bossSprite = FindObjectOfType<ShadowParker>().GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<BossManager>();
        charge = FindObjectOfType<Boss_Charge>();
    }

    private void Update()
    {
        if(charge.chargeDirection == -1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(0.2f, 0, 0);
        }
        else if (charge.chargeDirection == 1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(-0.2f, 0, 0);
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
            if(manager.bossPhase != 3 || bossSprite.flipX == playerSprite.flipX)
                parent.health -= 1;
        }
    }
}
