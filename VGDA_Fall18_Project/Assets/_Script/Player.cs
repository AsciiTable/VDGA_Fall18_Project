using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float Player_invTimer_Max = 3f;
    [Tooltip("How many seconds before invulnerability can be used again")]
    public float Player_invCooldown_Max = 3f;
    public bool Player_invulnerable = false;
    public int Player_inside;
        private float Player_invTimer_stamp;
        private float Player_invCooldown_stamp;
        private bool Player_invReady = true;

    [Space(10)]

    [Header("Misc.")]
    [Tooltip("Rigidbody2D Gravity Scale")]
    public float Player_gravity = 10f;
    [Tooltip("The Height Player Dies")]
    public float SuicidePoint = -15f;
    public string Player_winningScene;
    public bool Player_invulnerableEnabled = true;
    private Rigidbody2D Player_rb2d;
    private Transform Player_xyz;
    private SpriteRenderer Player_sprite;
    private Animator Player_animation;

    //private IsCheckpoint isCheck;

    private Transform groundCheck1_transform;
    private Transform groundCheck2_transform;

    private Transform Environment_transform;
    private Image InvulnerableCooldown_Sprite;

    //Boss Fight
    [HideInInspector]
    public bool restrained = false;

    private void Start()
    {
        /**if (isCheck.checkpoint == true)
        {
            Player_xyz.position = new Vector3(isCheck.pointX,isCheck.pointY, 0f);
        }**/
    }

    void Awake()
    {
        Player_rb2d = GetComponent<Rigidbody2D>();
        Player_xyz = GetComponent<Transform>();
        Player_sprite = GetComponent<SpriteRenderer>();
        Player_animation = GetComponent<Animator>();

        if(Player_invulnerableEnabled)
        {
            InvulnerableCooldown_Sprite = GameObject.FindGameObjectWithTag("InvulnerableSprite").GetComponent<Image>();
            //isCheck = GameObject.Find("UndyingScriptHolder").GetComponent<IsCheckpoint>();
        }

        groundCheck1_transform = transform.Find("groundCheck_1");
        groundCheck2_transform = transform.Find("groundCheck_2");
    }

    void FixedUpdate()
    {
        float Player_horizontal = 0;
        /*  Moving  */
        if (!restrained)
        {
            Player_horizontal = Input.GetAxis("Horizontal");
        }
        
        if(Player_horizontal != 0)
        {
            Player_animation.SetBool("PlayerMove", true);
        }
        else {
            Player_animation.SetBool("PlayerMove", false);
        }

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
        /*  Jumping  */
//Check if on floor (enemies, platforms, and enemies)
        if (Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Platforms")) ||
            Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Environment")) ||
            Physics2D.Linecast(groundCheck1_transform.position, groundCheck2_transform.position, 1 << LayerMask.NameToLayer("Enemy"))  )
        {
            Player_animation.SetBool("PlayerJump", false);
            Player_groundCheck = true;
            Player_jumps = 0;
            //Debug.Log("Grounded");
        }
        else
        {
            Player_animation.SetBool("PlayerJump", true);
            Player_groundCheck = false;
        }
//Drop to floor
        if (Input.GetButtonDown("Down") && Player_groundCheck == false)
        {
            Player_rb2d.velocity = new Vector3(0f, -2f*Player_jumpForce, 0f);
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
            Audio.PlaySound("8BIT_RETRO_Jump_Glide_Up_Muffled_mono");
            Player_rb2d.velocity = new Vector3(0f, Player_jumpForce, 0f);
            Player_jumps++;
        }
//Cancels first jump if in air
        else if (!(Player_groundCheck) && Player_jumps == 0)
        {
            //Debug.Log("Unlifted");
            Player_jumps++;
        }

        /* Jump Velocity (Gravity) */
        if(Player_rb2d.velocity.y < 0 && !(Player_groundCheck))
        {
            Player_rb2d.gravityScale = Player_gravity * 3;
        }
        else
        {
            Player_rb2d.gravityScale = Player_gravity;
        }

        /*  Death  */
//Player falling off stage
        if (Player_xyz.position.y < SuicidePoint)
        {
            //Debug.Log("some death is true");
            ResetScene();
        }

        /*  Invulnerability  */
        if(Player_invulnerableEnabled)
        {
            //Go invulnerable if button press and not on cooldown
            if (Input.GetButtonDown("Invulnerable") && Player_invReady)
            {
                //Debug.Log("Is invulnerable");
                Player_invulnerable = true;
                Player_invReady = false;
                Player_invTimer_stamp = Time.time + Player_invTimer_Max;
            }
            //Turn off Invulerable Sooner
            else if (Input.GetButtonDown("Invulnerable") && Player_invulnerable)
            {
                Player_invTimer_stamp = Time.time;
            }
            //Un invulnerable after time and not in environment block
            if (Player_invulnerable && Time.time >= Player_invTimer_stamp && Player_inside == 0)
            {
                //Debug.Log("Invulnerable Over");
                Player_invulnerable = false;
                Player_invCooldown_stamp = Time.time + Player_invCooldown_Max;
            }
            //Cooldown
            if (!(Player_invulnerable) && !(Player_invReady) && Time.time >= Player_invCooldown_stamp)
            {
                
                //Debug.Log("Invulnerable Ready");
                Player_invReady = true;
            }
            if(Player_invReady)
            {
                InvulnerableCooldown_Sprite.color = new Color(0f, (80f / 255f), 1f, 1f);
            }
            else
            {
                InvulnerableCooldown_Sprite.color = new Color(0f, (80f / 255f), 1f, 0.3f);
            }

            //Make transparent
            if (Player_invulnerable)
            {
                Player_sprite.color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (!Player_invulnerable)
            {
                Player_sprite.color = new Color(1f, 1f, 1f, 1f);
            }
        }

    }

    /*  Death  */
    //Resets the scene to the beginning
    public void ResetScene()
    {
        Debug.Log("Some Death");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Win")
        {
            SceneManager.LoadScene(Player_winningScene);
        }
    }
}
