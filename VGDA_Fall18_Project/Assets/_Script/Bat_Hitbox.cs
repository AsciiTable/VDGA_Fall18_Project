using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Bat_Hitbox : MonoBehaviour{
	
	public GameObject bat;
	public Collider2D enemy;
	public bool isSwung = false;
	public float knockBack = 500f;
	public GameObject player;
	public float OriginalXOffset = 4.291245f; //GameObject.GetComponent<BoxCollider2D>().offset.x;

    private Animator Player_animation;

    // Use this for initialization
    void Start (){
        Player_animation = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        bat.GetComponent<Collider2D>().enabled = false;
		bat.GetComponent<SpriteRenderer>().enabled = false;
		if (player.GetComponent<SpriteRenderer>().flipX == true){
			bat.GetComponent<SpriteRenderer>().flipX = true;
		}
		
	}



	// Update is called once per frame
	void FixedUpdate ()
	{
		float nXLeft = (OriginalXOffset + 1) * -1;
		
		if (Input.GetButtonDown("Bat")){
            Player_animation.SetTrigger("PlayerBat");
			Audio.PlaySound("WHOOSH_Short_03_mono");

			bat.GetComponent<Collider2D>().enabled = true;
			if (player.GetComponent<SpriteRenderer>().flipX == true){
				bat.GetComponent<BoxCollider2D>().offset = new Vector2(nXLeft, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = true;
				//nXLeft = (bat.GetComponent<BoxCollider2D>().offset.x-1)*1;
				//bat.GetComponent<BoxCollider2D>().offset = new Vector2(OriginalXOffset, bat.GetComponent<BoxCollider2D>().offset.y);
			}
			else{
				bat.GetComponent<SpriteRenderer>().flipX = false;
			}
			//OnTriggerEnter2D(enemy);
		/*	if (bat.GetComponent<Collider2D>().IsTouching(enemy.GetComponent<Collider2D>())){
				enemy.GetComponent<Rigidbody2D>().AddForce(transform.right * knockBack);
			}*/
			//bat.GetComponent<Collider2D>().enabled = false;
			//bat.GetComponent<SpriteRenderer>().enabled = false;
			//isSwung = false;

		}
		else{
			bat.GetComponent<Collider2D>().enabled = false;
			bat.GetComponent<SpriteRenderer>().enabled = false;
			if (player.GetComponent<SpriteRenderer>().flipX == true)
			{
				//bat.GetComponent<BoxCollider2D>().offset = new Vector2(nXLeft, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = true;
			}
			else{
				//float nX = (bat.GetComponent<BoxCollider2D>().offset.x+1)*1;
				bat.GetComponent<BoxCollider2D>().offset = new Vector2(OriginalXOffset, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = false;

			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("collider");
		if (other.CompareTag("Enemy"))
		{
			Audio.PlaySound("8BIT_RETRO_Hit_Bump_Distorted_Thud_mono");
			Debug.Log("Is Enemy");
			if (bat.GetComponent<Collider2D>().IsTouching(other.GetComponent<Collider2D>()) && player.GetComponent<SpriteRenderer>().flipX == false){
				other.GetComponent<Rigidbody2D>().AddForce(transform.right * knockBack);
			}
			else if (bat.GetComponent<Collider2D>().IsTouching(other.GetComponent<Collider2D>()) &&
			         player.GetComponent<SpriteRenderer>().flipX == true)
			{
				other.GetComponent<Rigidbody2D>().AddForce(transform.right * -knockBack);
			}
		}
	}

}
