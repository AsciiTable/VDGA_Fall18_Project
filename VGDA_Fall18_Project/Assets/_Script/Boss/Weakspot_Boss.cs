using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Boss : MonoBehaviour
{
    [SerializeField]private ShadowParker parent;

    private Player player;
    private BossManager manager;

    void Awake()
    {
        parent = GetComponentInParent<ShadowParker>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = FindObjectOfType<BossManager>().GetComponent<BossManager>();
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
