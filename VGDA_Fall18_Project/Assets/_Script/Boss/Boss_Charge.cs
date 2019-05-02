using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Charge : MonoBehaviour
{
    //Input Variables
    [Header("Borders")]
    [Tooltip("Total distance charged to the Left")]
    [SerializeField] private float leftBorder;
    [Tooltip("Total distance charged to the Right")]
    [SerializeField] private float rightBorder;
    [Tooltip("Enemy Slowing to a stop")]
    [SerializeField] private float[] slowDistance = new float[3];
    [Space(10)]
    [Header("Movement")]
    [SerializeField] private float[] chargeStartupTime = new float[3];
    [SerializeField] private int[] maxSpeed = new int[3];

    //Variables
    private float chargeDistance; //left border - right border (static)
    private float chargeSpeed; //Speed calcuations before changing transform
    [HideInInspector] public int chargeDirection = -1; //Direction (-1 looks left, 1 looks right)
    [HideInInspector] public bool readyCharge = false;
    [HideInInspector] public bool charging = false; //check if there's any charging
    [HideInInspector] public bool readyRegCharge = false;
    [HideInInspector] public bool tellCharge = false; //Activates tell boss
    [HideInInspector] public bool regCharging = false; //check if there's any charging
    //Components
    private Transform xyz;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    //Components from Other Objects
    private Player Player_script;
    private ShadowParker boss;

    private void Awake()
    {
        xyz = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = FindObjectOfType<ShadowParker>().GetComponent<ShadowParker>();

        //Setup static variables
        chargeDistance = rightBorder - leftBorder;
    }

    private void Update()
    {
        int index = 3 - boss.health;
        Debug.Log("Index: " + index);
        //Move when charging is true
        if (charging && readyCharge)
        {
            //Set charge speed & direction
            chargeSpeed = chargeDirection * maxSpeed[index] * Time.deltaTime;
            //Decrease speed if past slow border
            if (chargeDirection == 1 && xyz.position.x > (rightBorder - slowDistance[index]))
            {
                chargeSpeed *= (rightBorder - xyz.position.x) / slowDistance[index];
            }//Right
            else if (chargeDirection == -1 && xyz.position.x < (leftBorder + slowDistance[index]))
            {
                chargeSpeed *= (xyz.position.x - leftBorder) / slowDistance[index];
            } //Left
            //Move transform
            
        }
        else if (regCharging && readyRegCharge)
        {
            //Set charge speed & direction
            chargeSpeed = chargeDirection * (maxSpeed[0]+(5*index)) * Time.deltaTime;
            //Decrease speed if past slow border
            if (chargeDirection == 1 && xyz.position.x > (rightBorder - slowDistance[0]))
            {
                chargeSpeed *= (rightBorder - xyz.position.x) / slowDistance[0];
            }//Right
            else if (chargeDirection == -1 && xyz.position.x < (leftBorder + slowDistance[0]))
            {
                chargeSpeed *= (xyz.position.x - leftBorder) / slowDistance[0];
            } //Left
            //Move transform
        }
        else
        {
            chargeSpeed = 0;
        }

        rb2d.transform.Translate(chargeSpeed, 0, 0);

    }

    public IEnumerator charge()
    {
        //play animation for tell
        yield return new WaitForSeconds(chargeStartupTime[3 - boss.health]);
        tellCharge = true;
        yield return new WaitForSeconds((boss.health == 2) ? 0.5f : 0.4f);
        tellCharge = false;
        //Enemy Starts charging
        charging = true;
        //Charge until hits border
        if (chargeDirection == 1)
        {
            yield return new WaitUntil(() => xyz.position.x >= rightBorder-0.01f);
            charging = false;
            //Stick Enemy to border
            xyz.position = new Vector3(rightBorder ,xyz.position.y, xyz.position.z);
        } //Right
        else if (chargeDirection == -1)
        {
            yield return new WaitUntil(() => xyz.position.x <= leftBorder +0.01f);
            charging = false;
            //Stick Enemy to border
            xyz.position = new Vector3(leftBorder, xyz.position.y, xyz.position.z);
        } //Left
        chargeDirection = -chargeDirection;
        readyCharge = false;
    }
    public IEnumerator regCharge()
    {
        //play animation for tell
        Debug.Log("Charge Tell");
        yield return new WaitForSeconds(chargeStartupTime[0]);
        tellCharge = true;
        yield return new WaitForSeconds(0.3f);
        tellCharge = false;
        //Enemy Starts charging
        regCharging = true;
        Debug.Log("CHARGE");
        //Charge until hits border
        if (chargeDirection == 1)
        {
            yield return new WaitUntil(() => xyz.position.x >= rightBorder + 0.01f);
            regCharging = false;
            //Stick Enemy to border
            xyz.position = new Vector3(rightBorder, xyz.position.y, xyz.position.z);
        } //Right
        else if (chargeDirection == -1)
        {
            yield return new WaitUntil(() => xyz.position.x <= leftBorder + 0.01f);
            regCharging = false;
            //Stick Enemy to border
            xyz.position = new Vector3(leftBorder, xyz.position.y, xyz.position.z);
        } //Left
        chargeDirection = -chargeDirection;
        readyRegCharge = false;
    }
}
