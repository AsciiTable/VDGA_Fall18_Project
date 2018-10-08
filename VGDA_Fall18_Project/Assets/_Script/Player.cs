using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float Player_moveForce = 0.4f;
    public float Player_jumpForce = 40f;
    public float Player_doubleJumpForce = 30f;
    public float Player_gravity = 10f;
    public float SuicidePoint = -15f;
    public float Player_invulnerableTimer_Max = 3f;
    public float Player_invulnerableCooldown_Max = 3f;
    public int Player_jumpsMax = 2;
    public bool Player_invulnerable = false;

    private float Player_invulnerableTimer_stamp;
    private float Player_invulnerableCooldown_stamp;
    [SerializeField] private int Player_jumps = 0;
    [SerializeField] private bool Player_flipped = false;
    [SerializeField] private bool Player_groundCheck = false;
    private bool Player_invulnerableReady = true;


    private Rigidbody2D Player_rb2d;
    private Transform Player_xyz;
    private SpriteRenderer Player_sprite;
    private Transform groundCheck1_transform;
    private Transform groundCheck2_transform;

    void Awake()
    {
        Player_rb2d = GetComponent<Rigidbody2D>();
        Player_xyz = GetComponent<Transform>();
        Player_sprite = GetComponent<SpriteRenderer>();

        groundCheck1_transform = transform.Find("groundCheck_1");
        groundCheck2_transform = transform.Find("groundCheck_2");

    }

    void FixedUpdate()
    {
        Player_rb2d.gravityScale = Player_gravity;
        float Player_horizontal = Input.GetAxis("Horizontal");


        //move player left and right
        Player_rb2d.transform.Translate((Player_horizontal * Player_moveForce), 0f, 0f);

        //------------------------------------------------------------------------------------------------------------------------------------------------------------

        //flip player
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
        //Check if on floor
        if (Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Platforms")) || Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Enemy")))
        {
            Player_groundCheck = true;
        }
        else
        {
            Player_groundCheck = false;
        }

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
            if (Player_rb2d.velocity.y < Player_doubleJumpForce)
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

        if (Player_xyz.position.y < SuicidePoint)
        {
            Debug.Log("some death is true");
            ResetScene();
        }

        //Invulnerability stuff
        if (Input.GetButtonDown("Invulnerable") && Player_invulnerableReady)
        {
            Debug.Log("Is invulnerable");
            Player_invulnerable = true;
            Player_invulnerableReady = false;
            Player_invulnerableTimer_stamp = Time.time + Player_invulnerableTimer_Max;

        }
        if (Player_invulnerable && Time.time >= Player_invulnerableTimer_stamp)
        {
            Debug.Log("Invulnerable Over");
            Player_invulnerable = false;
            Player_invulnerableCooldown_stamp = Time.time + Player_invulnerableCooldown_Max;
        }
        if (!(Player_invulnerable) && !(Player_invulnerableReady) && Time.time >= Player_invulnerableCooldown_stamp)
        {
            Debug.Log("Invulnerable Ready");
            Player_invulnerableReady = true;
        }
    }

    //Resets the scene to the beginning
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && !(Player_invulnerable))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
