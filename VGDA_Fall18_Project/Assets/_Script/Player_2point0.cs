using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2point0 : MonoBehaviour {

    public float Player_moveForce = 0.4f;
    public float Player_jumpForce = 40f;
    public float Player_doubleJumpForce = 30f;
    public float Player_gravity = 10f;
    public int Player_jumpsMax = 2;

    [SerializeField] private int Player_jumps = 0;
    [SerializeField] private bool Player_flipped = false;
    [SerializeField] private bool Player_groundCheck = false;



    private Rigidbody2D Player_rb2d;
    private Transform groundCheck1_transform;
    private Transform groundCheck2_transform;

    void Awake()
    {
        Player_rb2d = GetComponent<Rigidbody2D>();

        groundCheck1_transform = transform.Find("groundCheck_1");
        groundCheck2_transform = transform.Find("groundCheck_2");

    }

    void FixedUpdate()
    {
        Player_rb2d.gravityScale = Player_gravity;
        float Player_horizontal = Input.GetAxis("Horizontal");

        //move player left and right
        Player_rb2d.transform.Translate((Player_horizontal * Player_moveForce), 0f, 0f);

        //--------------------------------------------------------------------------------------------------------------------------------

    }

    void Update () {
        //Check if on floor
        Player_groundCheck = Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Platforms"));

        if (Player_groundCheck)
        {
            //Debug.Log("Grounded");
            Player_jumps = 0;
        }

        //Jump in air
        if (Input.GetButtonDown("Jump") && Player_jumps != Player_jumpsMax && Player_jumps != 0 && !(Player_groundCheck))
        {
            Debug.Log("Double Lifted");
            Player_jumps++;
            if (Player_rb2d.velocity.x < Player_doubleJumpForce)
            {
                Player_rb2d.velocity = new Vector3(0f, Player_doubleJumpForce, 0f);
            }
        }
        //Jump off floor
        else if (Input.GetButtonDown("Jump") && Player_jumps == 0 && Player_groundCheck)
        {
            Debug.Log("Lifted");
            Player_rb2d.velocity = new Vector3(0f, Player_jumpForce, 0f);
            Player_jumps++;
        }
        //Cancels first jump if in air
        else if (!(Player_groundCheck) && Player_jumps == 0)
        {
            Debug.Log("Unlifted");
            Player_jumps++;
        }
    }
}
