using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowParker : MonoBehaviour
{

    public int health = 3;
    [HideInInspector]
    public bool bossImmunity = true;

    private Player Player_script;
    [SerializeField]private BossManager manager;

    void Awake()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = (BossManager)GameObject.FindObjectOfType(typeof(BossManager));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Boss Death");
            Player_script.ResetScene();
        }

    }

}
