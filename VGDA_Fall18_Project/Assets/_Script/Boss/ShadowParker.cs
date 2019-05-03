using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowParker : MonoBehaviour
{
    [Header("Phase 2")]
    [SerializeField] private GameObject platform;
    [Space(10)][Header("Phase 3")]
    [SerializeField] private GameObject deathZone;
    [Space(10)][Header("General")]
    public int health = 3;
    [HideInInspector] public bool bossImmunity = true;
    [HideInInspector] public bool opening;

    private Player Player_script;
    private Transform xyz;
    [SerializeField]private BossManager manager;
    
    private Transform platXyz;
    private AIMovement platAI;
    private SpriteRenderer platSprite;
    private BoxCollider2D platCollider;
    private Boss_Shoot shooter;
    private Boss_Charge charger;

    void Awake()
    {
        xyz = GetComponent<Transform>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = (BossManager)GameObject.FindObjectOfType(typeof(BossManager));
        shooter = GetComponent<Boss_Shoot>();
        charger = GetComponent<Boss_Charge>();

        if (platform != null)
        {
            platXyz = platform.GetComponent<Transform>();
            platAI = platform.GetComponent<AIMovement>();
            platSprite = platform.GetComponent<SpriteRenderer>();
            platCollider = platform.GetComponent<BoxCollider2D>();
        }
    }
    private void Update()
    {
        if(manager.bossPhase == 2)
        {
            xyz.position = new Vector3(platXyz.position.x + 6f, platXyz.position.y + 4.5f, platXyz.position.z);
            if(manager.bossStarted && !bossImmunity)
                shooter.enabled = true;
        }
        if (platform != null)
        {
            platAI.enabled = (opening) ? false : true;
            platSprite.enabled = (opening) ? true : false;
            platCollider.enabled = (opening) ? true : false;
        }

        if (manager.bossPhase == 3)
            deathZone.SetActive(charger.charging && health != 3);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && opening)
        {
            Debug.Log("Boss Death");
            Player_script.ResetScene();
        }

    }

}
