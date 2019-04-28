using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Spike : MonoBehaviour
{
    private float startingY;
    [SerializeField] private float bottomBorder;
    [SerializeField] private float speed = 0;
    [SerializeField] private int delaySpawn;


    private Transform xyz;
    private Rigidbody2D rb2d;
    private Player Player_script;

    void Awake()
    {
        xyz = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        startingY = xyz.position.y;

        Player_script = GameObject.FindGameObjectsWithTag("Player").Get

        StartCoroutine(resetSpike());
    }

    private void Update()
    {
        if(xyz.position.y >= bottomBorder)
        {
            xyz.position += Vector3.down *  speed * .1f;
        }
    }

    IEnumerator resetSpike()
    {
        yield return new WaitUntil(() => xyz.position.y < bottomBorder);
        yield return new WaitForSeconds(delaySpawn);
        xyz.position = new Vector3(xyz.position.x, startingY, xyz.position.z);
        StartCoroutine(resetSpike());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Enemy Death");
            Player_script.ResetScene();
        }
    }
}
