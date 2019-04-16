using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] [Tooltip("Disappears depending on BossManager if false")]
    private bool permanent = true;
    [SerializeField] [Tooltip("Flips when spikes pop up if not permanent")]
    private bool inverted;

    private bool activated = false;

    private SpriteRenderer sprite;
    private PolygonCollider2D col;

    private Player Player_script;
    private BossManager manager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<PolygonCollider2D>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = (BossManager)GameObject.FindObjectOfType(typeof(BossManager));
    }
    private void Update()
    {
        //Activate if permanent or if spikes are up (or down if inverted)
        activated = (permanent || (manager.spikesUp && !inverted) || (!manager.spikesUp && inverted)) ? true : false;

        //Hide spikes if not activated
        if (!activated)
        {
            sprite.enabled = false;
            col.enabled = false;
        }
        else
        {
            sprite.enabled = true;
            col.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch if not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Spike Death");
            Player_script.ResetScene();
        }
    }
}
