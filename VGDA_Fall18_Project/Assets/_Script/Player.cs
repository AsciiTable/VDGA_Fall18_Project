using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [Header("Move & Jump")]
    public float Player_moveForce = 0.4f;
    public float Player_jumpForce = 35f;
    public float Player_doubleJumpForce = 20f;
    public int Player_jumpsMax = 1;
        private bool Player_groundCheck = false;
        private int Player_jumps = 0;
    [Space(10)]

    [Header("Invulnerable")]
    [Tooltip("How many seconds invulnerability lasts")]
    public float Player_invulnerableTimer_Max = 3f;
    [Tooltip("How many seconds before invulnerability can be used again")]
    public float Player_invulnerableCooldown_Max = 3f;
    public bool Player_invulnerable = false;
    public bool Player_inside = false;
        private float Player_invulnerableTimer_stamp;
        private float Player_invulnerableCooldown_stamp;
        private bool Player_invulnerableReady = true;
        
    [Space(10)]

    [Header("Misc.")]
    [Tooltip("Rigidbody2D Gravity Scale")]
    public float Player_gravity = 10f;
    [Tooltip("The Height Player Dies")]
    public float SuicidePoint = -15f;

    private Rigidbody2D Player_rb2d;
    private Transform Player_xyz;
    private SpriteRenderer Player_sprite;
    private Animator Player_animation;

    private Transform groundCheck1_transform;
    private Transform groundCheck2_transform;

    private Transform Environment_transform;

    void Awake()
    {
        Player_rb2d = GetComponent<Rigidbody2D>();
        Player_xyz = GetComponent<Transform>();
        Player_sprite = GetComponent<SpriteRenderer>();
        Player_animation = GetComponent<Animator>();

        groundCheck1_transform = transform.Find("groundCheck_1");
        groundCheck2_transform = transform.Find("groundCheck_2");
    }

    void FixedUpdate()
    {
        /*  Moving  */
        Player_rb2d.gravityScale = Player_gravity;
        float Player_horizontal = Input.GetAxis("Horizontal");


        //move player left and right
        Player_rb2d.transform.Translate((Player_horizontal * Player_moveForce), 0f, 0f);

        /*  Flipping  */
        if (Player_horizontal > 0 && Player_sprite.flipX)
        {
            Player_sprite.flipX = false;
        }
        else if (Player_horizontal < 0 && !(Player_sprite.flipX))
        {
            Player_sprite.flipX = true;
        }
    }

    void Update()
    {
        /*  Animations */
//Jump animation
        Player_animation.SetBool("onGround", Player_groundCheck);

        /*  Jumping  */
//Check if on floor (enemies, platforms, and enemies)
        if (Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Platforms")) ||
            Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Environment")) ||
            Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Enemy"))  )
        {
            Player_groundCheck = true;
            Player_jumps = 0;
            //Debug.Log("Grounded");
        }
        else
        {
            Player_groundCheck = false;
        }
//Jump in air
        if (Input.GetButtonDown("Jump") && Player_jumps != Player_jumpsMax && Player_jumps != 0 && !(Player_groundCheck))
        {
            //Debug.Log("Double Lifted");
            Player_jumps++;
            if (Player_rb2d.velocity.y < Player_doubleJumpForce)
            {
                Player_rb2d.velocity = new Vector3(0f, Player_doubleJumpForce, 0f);
            }
        }
//Jump off floor
        else if (Input.GetButtonDown("Jump") && Player_jumps == 0 && Player_groundCheck)
        {
            //Debug.Log("Lifted");
            Player_rb2d.velocity = new Vector3(0f, Player_jumpForce, 0f);
            Player_jumps++;
        }
//Cancels first jump if in air
        else if (!(Player_groundCheck) && Player_jumps == 0)
        {
            //Debug.Log("Unlifted");
            Player_jumps++;
        }

        /*  Death  */
//Player falling off stage
        if (Player_xyz.position.y < SuicidePoint)
        {
            //Debug.Log("some death is true");
            ResetScene();
        }

        /*  Invulnerability  */
//Go invulnerable if button press and not on cooldown
        if (Input.GetButtonDown("Invulnerable") && Player_invulnerableReady)
        {
            //Debug.Log("Is invulnerable");
            Player_invulnerable = true;
            Player_invulnerableReady = false;
            Player_invulnerableTimer_stamp = Time.time + Player_invulnerableTimer_Max;
        }
//Un invulnerable after time and not in environment block
        if (Player_invulnerable && Time.time >= Player_invulnerableTimer_stamp && !(Player_inside) && Player_invulnerable)
        {
            //Debug.Log("Invulnerable Over");
            Player_invulnerable = false;
            Player_invulnerableCooldown_stamp = Time.time + Player_invulnerableCooldown_Max;
        }
//Cooldown
        if (!(Player_invulnerable) && !(Player_invulnerableReady) && Time.time >= Player_invulnerableCooldown_stamp)
        {
            //Debug.Log("Invulnerable Ready");
            Player_invulnerableReady = true;
        }

//Make transparent
        if(Player_invulnerable)
        {
            Player_sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        if (!Player_invulnerable)
        {
            Player_sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    /*  Death  */
    //Resets the scene to the beginning
    public void ResetScene()
    {
        Debug.Log("Some Death");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
