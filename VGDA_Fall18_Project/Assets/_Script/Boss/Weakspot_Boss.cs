using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Boss : MonoBehaviour
{
    [SerializeField]private ShadowParker parent;

    private Player player;

    void Awake()
    {
        parent = GetComponentInParent<ShadowParker>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boss Death");
            player.ResetScene();
        }
        if (collision.gameObject.tag == "Bat" && !parent.bossImmunity)
        {
            parent.health -= 1;
        }
    }
}
