using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSet : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Player player;
    private IsCheckpoint isCheck;

    // Start is called before the first frame update
    void Start()
    {
        isCheck = GameObject.FindGameObjectWithTag("Undying").GetComponent<IsCheckpoint>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck.checkpoint)
            sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCheck.pointX = gameObject.transform.position.x;
            isCheck.pointY = gameObject.transform.position.y;
            isCheck.checkpoint = true;
            Debug.Log("Checkpoint");
        }
    }
    }