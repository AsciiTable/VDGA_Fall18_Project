using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Floater : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform xyz;
    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;
    private Vector2 leftOffset = new Vector2(-0.5f,0.4f); //not flipped
    private Vector2 rightOffset = new Vector2(0.5f,0.4f); //flipped

    //Components from Other Objects
    private Player Player_script;
    private Transform Player_xyz;
    private void Awake()
    {
        xyz = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();

        Player_script = (Player)FindObjectOfType(typeof(Player));
        Player_xyz = Player_script.gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 playerPos = new Vector2(Player_xyz.position.x,Player_xyz.position.y);
        Vector2 floaterPos = new Vector2(transform.position.x, transform.position.y);

        xyz.position = Vector2.MoveTowards(floaterPos, playerPos, speed * Time.deltaTime);

        if (Mathf.Abs(xyz.position.x - Player_xyz.position.x) > 20f)
            sprite.flipX = (xyz.position.x > Player_xyz.position.x) ? false : true;
        hitbox.offset = (!sprite.flipX) ? leftOffset : rightOffset;

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Stand Death");
            Player_script.ResetScene();
        }
    }
}
