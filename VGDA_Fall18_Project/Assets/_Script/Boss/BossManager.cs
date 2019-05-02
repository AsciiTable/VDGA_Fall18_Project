using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private ShadowParker boss;
    private GameObject player;
    private Transform player_xyz;
    private Rigidbody2D player_rb2d;
    private Player player_script;
    private Boss_Charge charge;

    [Header("Phase 1")]
    [SerializeField] private int spikeCooldown;
    [HideInInspector] public bool spikesUp = false;
    [SerializeField] private GameObject floor_1;
    [SerializeField] private GameObject floor_2;
    [SerializeField] private GameObject floor_3;
    [SerializeField] private GameObject spikes_1;
    [SerializeField] private GameObject spikes_2;
    [SerializeField] private GameObject spikes_3;
    [SerializeField] private CinemachineVirtualCamera cam_2;
    [SerializeField] private CinemachineVirtualCamera cam_3;
    [Space(10)]
    [Header("Phase 2")]
    public float fSpikeBottomBorder = -4;
    public float fSpikeSpeed;
    public bool fallingSpikes = false;
    [Space(10)]
    [Header("Phase 3")]
    [SerializeField][Tooltip("Charges While Invincible (Not in part 1)")]
    private int[] numberCharges = new int[2];
    public bool deathReady = false;
    [HideInInspector]public bool hit = false;
    [Space(10)]
    [Header("All Phases")]
    [SerializeField] private string[] bossStages = { "BossFight_1", "BossFight_2", "BossFight_3" };
    [SerializeField][Tooltip("Edge of StageLeft")]
    private float border;
    [SerializeField] private GameObject fSpikes_2;
    [SerializeField] private GameObject fSpikes_3;
    private bool pushedbacked = false;
    public int bossPhase = 0;
    public bool bossStarted = false;



    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
        boss = (ShadowParker)GameObject.FindObjectOfType(typeof(ShadowParker));
        player = GameObject.FindGameObjectWithTag("Player");
        player_xyz = player.GetComponent<Transform>();
        player_rb2d = player.GetComponent<Rigidbody2D>();
        player_script = player.GetComponent<Player>();
        charge = boss.GetComponent<Boss_Charge>();
        if (bossPhase == 1)
        {
            StartCoroutine(StagePhase1());
        }
        if(bossPhase == 2)
        {
            StartCoroutine(StagePhase2());
        }
        if(bossPhase == 3)
        {
            StartCoroutine(StagePhase3());
        }
        
    }

    private void FixedUpdate()
    {
        if (pushedbacked)
        {
            player_rb2d.transform.Translate(-3f, 0f, 0f);
        }
    }

    IEnumerator StagePhase1()
    {
        boss.bossImmunity = player_script.restrained = boss.opening = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);
        Debug.Log("Phase 1");
        boss.bossImmunity = player_script.restrained = false;
        bossStarted = true;
        StartCoroutine(Phase1());
        StartCoroutine(Spikes());
        yield return new WaitUntil(() => boss.health == 0);
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        Debug.Log("Transistion to Phase 2");
        boss.bossImmunity = player_script.restrained = true;
        bossStarted = false;
        Debug.Log("Phase 2");
        sceneLoader.LoadScene(bossStages[1].ToString());
    }
    IEnumerator StagePhase2()
    {
        boss.bossImmunity = player_script.restrained = boss.opening = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);
        Debug.Log("Phase 2");
        boss.opening = boss.bossImmunity = player_script.restrained = false;
        bossStarted = true;
        StartCoroutine(Phase2());
        yield return new WaitUntil(() => boss.health == 0);
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        Debug.Log("Transistion to Phase 3");
        boss.bossImmunity = player_script.restrained = true;
        bossStarted = false;
        Debug.Log("Phase 3");
        sceneLoader.LoadScene(bossStages[2].ToString());
    }
    IEnumerator StagePhase3()
    {
        boss.bossImmunity = player_script.restrained = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);

        Debug.Log("Phase 3");
        boss.bossImmunity = player_script.restrained = false;
        bossStarted = true;
        StartCoroutine(Phase3());

        yield return new WaitUntil(() => boss.health == 0);

        Debug.Log("Transistion to Phase 3");
        boss.bossImmunity = player_script.restrained = true;
        bossStarted = false;
        Debug.Log("Phase 3");
    }
    //When Boss gets hit during Phase 1
    IEnumerator Phase1()
    {
        spikes_1.SetActive(true);
        yield return new WaitUntil(() => boss.health == 2);
        spikes_1.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;
        spikes_2.SetActive(true);
        floor_2.SetActive(true);
        floor_1.SetActive(false);
        cam_2.Priority = 2;
        yield return new WaitUntil(() => boss.health == 1);
        spikes_2.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;
        spikes_3.SetActive(true);
        floor_3.SetActive(true);
        floor_2.SetActive(false);
        cam_3.Priority = 3;
        yield return new WaitUntil(() => boss.health == 0);
        spikes_3.SetActive(false);
        boss.bossImmunity = pushedbacked = player_script.restrained = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        boss.bossImmunity = pushedbacked = player_script.restrained = false;


    }

    IEnumerator Spikes()
    {
        while (bossPhase == 1 && bossStarted)
        {
            spikesUp = !spikesUp;
            yield return new WaitForSeconds(spikeCooldown);
        }
    }

    IEnumerator Phase2()
    {
        yield return new WaitUntil(() => boss.health == 2);
        boss.bossImmunity = pushedbacked = player_script.restrained = boss.opening = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        boss.bossImmunity = pushedbacked = player_script.restrained = boss.opening = false;
        fSpikes_2.SetActive(true);
        fallingSpikes = true;
        yield return new WaitUntil(() => boss.health == 1);
        fallingSpikes = false;
        boss.bossImmunity = player_script.restrained = boss.opening = true;
        fSpikeSpeed = fSpikeSpeed * 20;
        yield return new WaitForSeconds(0.5f);
        fSpikeSpeed = fSpikeSpeed / 20;
        fSpikes_2.SetActive(false);
        pushedbacked = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        boss.bossImmunity = pushedbacked = player_script.restrained = boss.opening = false;
        fSpikes_3.SetActive(true);
        fallingSpikes = true;
        yield return new WaitUntil(() => boss.health == 0);
        fallingSpikes = false;
        boss.bossImmunity = player_script.restrained = boss.opening = true;
        fSpikeSpeed = fSpikeSpeed * 20;
        yield return new WaitForSeconds(0.5f);
        fSpikeSpeed = fSpikeSpeed / 20;
        fSpikes_3.SetActive(false);
        pushedbacked = true;
        yield return new WaitUntil(() => player_xyz.position.x <= border);
        pushedbacked = false;

    }

    IEnumerator Phase3()
    {
        while(boss.health == 3)
        {
            boss.bossImmunity = false;
            charge.readyRegCharge = true;
            StartCoroutine(charge.regCharge());
            yield return new WaitUntil(() => !charge.readyRegCharge || boss.health != 3);
            boss.bossImmunity = true;
        }
        boss.bossImmunity = hit = true;
        yield return new WaitUntil(() => !charge.readyRegCharge);
        hit = false;
        yield return new WaitForSeconds(2);
        deathReady = true;

        while (boss.health == 2)
        {
            for (int i = 0; i < numberCharges[0]; i++)
            {
                charge.readyCharge = true;
                StartCoroutine(charge.charge());
                yield return new WaitUntil(() => !charge.readyCharge);
            }
            boss.bossImmunity = false;
            charge.readyRegCharge = true;
            StartCoroutine(charge.regCharge());
            yield return new WaitUntil(() => !charge.readyRegCharge || boss.health != 2);
            boss.bossImmunity = true;
        }
        boss.bossImmunity = hit = true;
        yield return new WaitUntil(() => !charge.readyRegCharge);
        hit = false;
        yield return new WaitForSeconds(2);

        while (boss.health == 1)
        {
            for (int i = 0; i < numberCharges[1]; i++)
            {
                charge.readyCharge = true;
                StartCoroutine(charge.charge());
                yield return new WaitUntil(() => !charge.readyCharge);
            }
            boss.bossImmunity = false;
            charge.readyRegCharge = true;
            StartCoroutine(charge.regCharge());
            yield return new WaitUntil(() => !charge.readyRegCharge || boss.health != 1);
            boss.bossImmunity = true;
        }
        boss.bossImmunity = hit = true;
    }
}
