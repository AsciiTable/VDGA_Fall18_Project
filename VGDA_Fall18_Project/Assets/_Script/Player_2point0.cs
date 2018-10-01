using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2point0 : MonoBehaviour {

    public float Player_moveForce = 0.5f;
    public float Player_jumpForce = 500f;
    public float Player_jumpForceMax = 10f;
    private int Player_jumpsMax = 2;

    private int Player_jumps = 0;
    private bool Player_flipped = false;
    private bool Player_grounded = false;



    private Rigidbody2D Player_rb2d;
    private Collider2D Player_col2D;
    private Transform groundCheck1_transform;
    private Transform groundCheck2_transform;

    void Awake()
    {
        Player_rb2d = GetComponent<Rigidbody2D>();
        Player_col2D = GetComponent<Collider2D>();

        groundCheck1_transform = transform.Find("groundCheck_1");
        groundCheck2_transform = transform.Find("groundCheck_2");

    }

    void FixedUpdate()
    {

        float Player_horizontal = Input.GetAxis("Horizontal");

        //move player left and right
        Player_rb2d.transform.Translate(Player_horizontal * Player_moveForce, 0f, 0f);

        //--------------------------------------------------------------------------------------------------------------------------------

        //Check if on floor
        Player_grounded = Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Platforms"));

        if(Player_grounded)
        {
                //Debug.Log("Grounded");
            Player_jumps = 0;
        }

        //Jump in air
        if (Input.GetButtonDown("Jump") && Player_jumps < Player_jumpsMax && Player_jumps > 0)
        {
            Debug.Log("Double Lifted");
            Player_rb2d.AddForce(new Vector2(0f, Player_jumpForce)); // NOTE: Change this to make it less powerful
            Player_jumps++;
        }
        //Jump off floor
        if (Input.GetButtonDown("Jump") && Player_jumps == 0)
        {
            Debug.Log("Lifted");
            Player_rb2d.AddForce(new Vector2(0f, Player_jumpForce));
            Player_jumps++;
        }



    }

    void Update () {
		
	}
}
