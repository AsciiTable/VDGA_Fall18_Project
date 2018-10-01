using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jessica : MonoBehaviour {
    public int MAXHP = 10;
    public int CURRENTHP;
    public float moveSpeed = 5f;
    // Use this for initialization
    void Start () {
		
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
        if(inputHorizontal > 0 && GetComponent<SpriteRenderer>().flipX){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputHorizontal < 0 && !GetComponent<SpriteRenderer>().flipX){
            GetComponent<SpriteRenderer>().flipX = true;
        }

    }
}
