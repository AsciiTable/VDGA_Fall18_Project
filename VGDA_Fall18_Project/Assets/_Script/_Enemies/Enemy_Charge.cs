using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Charge : MonoBehaviour
{
    //Input Variables
    [Header("Borders")]
    [Tooltip("Total distance charged to the Left")]
    [SerializeField] private float leftBorder;
    [Tooltip("Total distance charged to the Right")]
    [SerializeField] private float rightBorder;
    [Tooltip("Enemy Slowing to a stop")]
    [SerializeField] private float slowDistance;
    [Space(10)]
    [Header("Movement")]
    [SerializeField] private int chargeStartupTime;
    [SerializeField] private int maxSpeed;

    //Variables
    private float chargeDistance; //left border - right border (static)
    private float chargeSpeed; //Speed calcuations before changing transform
    private int chargeDirection; //Direction (-1 looks left, 1 looks right)
    private bool readyCharge = false;
    private bool charging = false;
    private Vector3 startVector;
    private RaycastHit2D lineSight;
    //Components
    private Transform xyz;
    private SpriteRenderer sprite;
    //Components from Other Objects
    private Player Player_script;
    private LayerMask playerMask;

    private void Awake()
    {
        xyz = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMask = LayerMask.GetMask("Player");

        //Wait for variable readyCharge
        StartCoroutine(charge());

        //Setup static variables
        chargeDistance = rightBorder - leftBorder;
    }

    private void Update()
    {
        //Move when charging is true
        if (charging)
        {
            //Set charge speed & direction
            chargeSpeed = chargeDirection * maxSpeed * Time.deltaTime;
            //Decrease speed if past slow border
            if (chargeDirection == 1 && xyz.position.x > (rightBorder - slowDistance))
            {
                chargeSpeed *= (rightBorder - xyz.position.x) / slowDistance;
            }//Right
            else if (chargeDirection == -1 && xyz.position.x < (leftBorder + slowDistance))
            {
                chargeSpeed *= (xyz.position.x - leftBorder) / slowDistance;
            } //Left
            //Move transform
            xyz.position = xyz.position + new Vector3(chargeSpeed, 0, 0);
        }
        else
        {
            chargeSpeed = 0;
        }


    }

    private void FixedUpdate()
    {

        //Make line to find player & set charge direction
        if (!sprite.flipX) 
        {
            chargeDirection = 1;
            lineSight = Physics2D.Raycast(xyz.position, Vector2.right, chargeDistance,playerMask);
        } //Right
        else if (sprite.flipX)
        {
            chargeDirection = -1;
            lineSight = Physics2D.Raycast(xyz.position, Vector2.left, chargeDistance, playerMask);
        } //Left

        //Check if player is in line of sight
        if(lineSight.collider != null)
        {
            readyCharge = lineSight.collider.tag == "Player" ? true : false;
        }
        else
        {
            readyCharge = false;
        }
        
    }

    IEnumerator charge()
    {
        //Wait until player is in line of sight
        yield return new WaitUntil(() => readyCharge == true);
        //play animation for tell
        Debug.Log("Charge Tell");
        yield return new WaitForSeconds(chargeStartupTime);
        //Enemy Starts charging
        charging = true;
        Debug.Log("CHARGE");
        //Charge until hits border
        if (chargeDirection == 1)
        {
            yield return new WaitUntil(() => xyz.position.x >= rightBorder);
            charging = false;
            Debug.Log("Stop");
            //Stick Enemy to border
            xyz.position = new Vector3(rightBorder ,xyz.position.y, xyz.position.z);
            //Flip Enemy around
            sprite.flipX = true;
        } //Right
        else if (chargeDirection == -1)
        {
            yield return new WaitUntil(() => xyz.position.x <= leftBorder);
            charging = false;
            Debug.Log("Stop");
            //Stick Enemy to border
            xyz.position = new Vector3(leftBorder, xyz.position.y, xyz.position.z);
            //Flip Enemy around
            sprite.flipX = false;
        } //Left
        StartCoroutine(charge());
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Kill player on touch and not invulnerable
        if (col.gameObject.tag == "Player" && !Player_script.Player_invulnerable)
        {
            Debug.Log("Charge Death");
            Player_script.ResetScene();
        }
    }
}
