using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] [Tooltip("Disappears depending on BossManager if false")]
    private bool permanent = true;
    [SerializeField] [Tooltip("Flips when spikes pop up if not permanent")]
    private bool inverted;
    [SerializeField][Tooltip("Changes code so spikes re-pop up after wave cooldown")]
    private bool wave = false;
    [SerializeField][Tooltip("Cooldown before spike flips")]
    private float spikeCooldown = 3;
    [SerializeField][Tooltip("Time before wave resets if wave is true (bigger than spikeCooldown)")]
    private float totalCooldown = 3;
    [Space(10)][Header("ScriptUpdated")]
    [SerializeField]private bool spikesUp = false;
    [SerializeField]private bool activated = false;

    private SpriteRenderer sprite;
    private PolygonCollider2D polyCol;
    private BoxCollider2D boxCol;
    private Player Player_script;
    private BossManager manager;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        polyCol = GetComponent<PolygonCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = (BossManager)GameObject.FindObjectOfType(typeof(BossManager));

        StartCoroutine(SpikesUp());
    }
    private void Update()
    {
        //Activate if permanent or if spikes are up (or down if inverted)
        activated = (permanent || (spikesUp && !inverted) || (!spikesUp && inverted)) ? true : false;

        //Hide spikes if not activated
        if (activated)
            polyCol.enabled = boxCol.enabled = sprite.enabled = true;
        else
            polyCol.enabled = boxCol.enabled = sprite.enabled = false;
    }

    IEnumerator SpikesUp()
    {
        spikesUp = !spikesUp;
        yield return new WaitForSeconds(spikeCooldown);
        if (wave)
        {
            spikesUp = !spikesUp;
            yield return new WaitForSeconds(totalCooldown - spikeCooldown);
        }
        StartCoroutine(SpikesUp());
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
