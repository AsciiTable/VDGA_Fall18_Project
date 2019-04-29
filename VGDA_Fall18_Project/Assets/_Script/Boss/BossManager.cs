using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    public int bossPhase = 0;
    public bool bossStarted = false;
    private SceneLoader sceneLoader;
    private ShadowParker boss;
    private GameObject player;
    private Transform player_xyz;
    private Rigidbody2D player_rb2d;
    private Player player_script;

    [SerializeField] private string[] bossStages = {"BossFight_1", "BossFight_2" , "BossFight_3" };
    [Space(10)]
    [Header("Phase 1")]
    [SerializeField] private int spikeCooldown;
    [HideInInspector] public bool spikesUp = false;
    [SerializeField] [Tooltip("Edge of StageLeft")]
    private float border;
    private bool pushedbacked = false;
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



    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
        boss = (ShadowParker)GameObject.FindObjectOfType(typeof(ShadowParker));
        player = GameObject.FindGameObjectWithTag("Player");
        player_xyz = player.GetComponent<Transform>();
        player_rb2d = player.GetComponent<Rigidbody2D>();
        player_script = player.GetComponent<Player>();

        if(bossPhase == 1)
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
        boss.bossImmunity = player_script.restrained = true;
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
        boss.bossImmunity = player_script.restrained = true;
        Debug.Log("Play Opening Scene");
        yield return new WaitForSeconds(3);

        Debug.Log("Phase 2");
        boss.bossImmunity = player_script.restrained = false;
        bossStarted = true;


        yield return new WaitUntil(() => boss.health == 0);

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


        yield return new WaitUntil(() => boss.health == 0);

        Debug.Log("Transistion to Phase 3");
        boss.bossImmunity = player_script.restrained = true;
        bossStarted = false;
        Debug.Log("Phase 3");
        sceneLoader.LoadScene(bossStages[2].ToString());
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
}
