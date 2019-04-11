using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public int bossPhase = 0;
    private ShadowParker boss;
    private GameObject player;
    private Transform player_xyz;
    private Rigidbody2D player_rb2d;
    private Player player_script;

    [Header("Phase 1")]
    [SerializeField] private int spikeCooldown;
    [HideInInspector] public bool spikesUp = false;
    [SerializeField] [Tooltip("Edge of StageLeft")]
    private float border;
    private bool pushedbacked = false;
    [SerializeField] private GameObject spikes_1;
    [SerializeField] private GameObject spikes_2;
    [SerializeField] private GameObject spikes_3;



    private void Awake()
    {
        boss = (ShadowParker)GameObject.FindObjectOfType(typeof(ShadowParker));
        player = GameObject.FindGameObjectWithTag("Player");
        player_xyz = player.GetComponent<Transform>();
        player_rb2d = player.GetComponent<Rigidbody2D>();
        player_script = player.GetComponent<Player>();
        if(bossPhase == 1)
        {
            StartCoroutine(OpeningPhase1());
        }
        if(bossPhase == 2)
        {
            StartCoroutine(OpeningPhase2());
        }
        if (bossPhase == 2)
        {
            StartCoroutine(OpeningPhase3());
        }

    }

    private void FixedUpdate()
    {
        if (pushedbacked)
        {
            player_rb2d.transform.Translate(-3f, 0f, 0f);
        }
    }

    IEnumerator OpeningPhase1()
    {
        boss.bossImmunity = player_script.restrained = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);
        Debug.Log("Phase 1");
        boss.bossImmunity = player_script.restrained = false;
        bossPhase = 1;
        StartCoroutine(Phase1());
        StartCoroutine(Spikes());
        yield return new WaitUntil(() => boss.health == 0);
        Debug.Log("Transistion to Phase 2 Scene");
    }
    IEnumerator OpeningPhase2()
    {
        boss.bossImmunity = player_script.restrained = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);
        Debug.Log("Phase 2");
        boss.bossImmunity = player_script.restrained = false;
        bossPhase = 2;
        yield return new WaitUntil(() => boss.health == 0);
        Debug.Log("Transistion to Phase 3 Scene");
    }
    IEnumerator OpeningPhase3()
    {
        yield return new WaitUntil(() => boss.health == 0);
    }

        //When Boss gets hit during Phase 1
    IEnumerator Phase1()
    {
        spikes_1.SetActive(true);
        yield return new WaitUntil(() => boss.health == 2);
        spikes_1.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border+3);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;
        spikes_2.SetActive(true);
        yield return new WaitUntil(() => boss.health == 1);
        spikes_2.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border+3);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;
        spikes_3.SetActive(true);
        yield return new WaitUntil(() => boss.health == 0);
        spikes_3.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border+3);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;


    }

    IEnumerator Spikes()
    {
        spikesUp = !spikesUp;
        yield return new WaitForSeconds(spikeCooldown);
    }
}
