using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Projectile : MonoBehaviour
{
    [SerializeField] private float startingY = 41f;
    [SerializeField] private float delay;
    [SerializeField] private float bottomBorder;
    [SerializeField] private float spikeSpeed;

    private Transform xyz;  
    private Rigidbody2D rb2d;
    private Player Player_script;
    private BossManager manager;

    void Awake()
    {
        xyz = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();

        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        StartCoroutine(resetSpike());
    }

    private void Update()
    {
        if(xyz.position.y >= bottomBorder)
        {
            xyz.position += Vector3.down *  spikeSpeed * .1f;
        }
    }

    IEnumerator resetSpike()
    {
        yield return new WaitUntil(() => xyz.position.y < bottomBorder);
        yield return new WaitForSeconds(delay);
        xyz.position = new Vector3(xyz.position.x, startingY, xyz.position.z);
        StartCoroutine(resetSpike());
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Projectile Death");
            Player_script.ResetScene();
        }
    }
}
