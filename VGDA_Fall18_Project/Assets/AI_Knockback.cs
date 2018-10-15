using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Knockback : MonoBehaviour{
	
	public bool isHit = false;
	public float knockBack = 500f;
	private Rigidbody2D enemy;
	
	// Use this for initialization
	void Start (){
		enemy = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate(){
		if (isHit){
			enemy.AddForce(transform.right*knockBack);
			isHit = false;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
