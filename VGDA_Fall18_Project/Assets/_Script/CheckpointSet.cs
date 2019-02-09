using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSet : MonoBehaviour
{
    private Player player;
    private IsCheckpoint isCheck;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCheck = GameObject.Find("UndyingScriptHolder").GetComponent<IsCheckpoint>();
            isCheck.checkpoint = true;
            isCheck.pointX = gameObject.transform.position.x;
            isCheck.pointY = gameObject.transform.position.y;
        }
    }
    }