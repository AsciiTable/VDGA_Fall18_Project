using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jessica : MonoBehaviour {
    public int MAXHP = 10;
    public int CURRENTHP;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    bool canJump = true;
    bool onGround = true;
    [SerializeField] private bool m_AirControl = false;// Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;// A mask determining what is ground to the character

    private Rigidbody2D player;
    private Animator playerAnim;//reference to player's animator component

    //These are all initialized during the loading screen
    private void Awake(){
        player = GetComponent<Rigidbody2D>();//applies physics to player
        playerAnim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start (){

    }

    /*Update for physics stuff: will run 0, once, or multiple times depending on what is needed
     * Includes:
     * Jump
     */
    private void FixedUpdate(){
        if(onGround && canJump && playerAnim.GetBool("onGround")){
            onGround = false;
            playerAnim.SetBool("onGround", false);
            player.AddForce(new Vector2(0f, jumpForce));
        }
    }

    // Update is called once per frame
    /*Includes:
     * Special Ghose Power CD
     * Recieving input to: jump, move left, move right, swing bat, apologize, run
     */
    void Update () {
        //Horizontal movement w/ flip
        float inputHorizontal = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3((inputHorizontal * moveSpeed * Time.deltaTime), 0, 0);
        //Flip
        if(inputHorizontal > 0 && GetComponent<SpriteRenderer>().flipX){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputHorizontal < 0 && !GetComponent<SpriteRenderer>().flipX){
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //Vertical movement 

    }
}
