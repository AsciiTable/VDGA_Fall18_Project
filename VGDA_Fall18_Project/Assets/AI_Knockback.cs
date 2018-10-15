using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Knockback : MonoBehaviour{
	
	public bool isHitOnLeft = false;
	public bool isHitOnRight = false;
	public float knockBack = 500f;
	private Rigidbody2D enemy;
	
	// Use this for initialization
	void Start (){
		enemy = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate(){
		if (isHitOnLeft){
			enemy.AddForce(transform.right*knockBack);
			isHitOnLeft = false;
		}
		else if (isHitOnRight){
			enemy.AddForce(-transform.right*knockBack);
			isHitOnRight = false;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
