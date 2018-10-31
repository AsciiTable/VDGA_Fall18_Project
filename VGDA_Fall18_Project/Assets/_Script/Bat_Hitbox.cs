﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Bat_Hitbox : MonoBehaviour{
	
	public GameObject bat;
	public Collider2D enemy;
	public bool isSwung = false;
	public float knockBack = 500f;
	public GameObject player;
	
	
	// Use this for initialization
	void Start (){
		bat.GetComponent<Collider2D>().enabled = false;
		bat.GetComponent<SpriteRenderer>().enabled = false;
		if (player.GetComponent<SpriteRenderer>().flipX == true){
			bat.GetComponent<SpriteRenderer>().flipX = true;
		}
	}



	// Update is called once per frame
	void FixedUpdate ()
	{
		float oX = bat.GetComponent<BoxCollider2D>().offset.x;
		float nXLeft = (oX + 1) * -1;
		
		if (Input.GetButtonDown("Bat")){
			bat.GetComponent<Collider2D>().enabled = true;
			bat.GetComponent<SpriteRenderer>().enabled = true;
			if (player.GetComponent<SpriteRenderer>().flipX == true){
				bat.GetComponent<BoxCollider2D>().offset = new Vector2(nXLeft, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = true;
				nXLeft = (bat.GetComponent<BoxCollider2D>().offset.x-1)*1;
				bat.GetComponent<BoxCollider2D>().offset = new Vector2(oX, bat.GetComponent<BoxCollider2D>().offset.y);
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
				bat.GetComponent<BoxCollider2D>().offset = new Vector2((nXLeft-1)*-1, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = true;
				nXLeft = (bat.GetComponent<BoxCollider2D>().offset.x-1)*1;
			}
			else{
				float nX = (bat.GetComponent<BoxCollider2D>().offset.x+1)*1;
				bat.GetComponent<BoxCollider2D>().offset = new Vector2(oX, bat.GetComponent<BoxCollider2D>().offset.y);
				bat.GetComponent<SpriteRenderer>().flipX = false;

			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("collider");
		if (other.CompareTag("Enemy"))
		{
			Debug.Log("Is Enemy");
			if (bat.GetComponent<Collider2D>().IsTouching(other.GetComponent<Collider2D>())){
				other.GetComponent<Rigidbody2D>().AddForce(transform.right * knockBack);
			}
		}
	}

}
