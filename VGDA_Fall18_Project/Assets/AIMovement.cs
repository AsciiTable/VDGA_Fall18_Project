using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIMovement : MonoBehaviour {
	public float MoveSpeed = 5f;
	private Rigidbody2D _enemy;
	public float MaxLeftPos;
	public float MaxRightPos;
	public bool SpriteFacingRight;//asks for what side the sprite is facing before 
	
	// Use this for initialization
	void Start () {
		_enemy = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update(){
		Move(MovingRight());
	}

	public int MovingRight(){//returns 1 if AI is moving right
		if (SpriteFacingRight == true){
			return 1;
		}
		return -1;
	}
	
	void Move(int right){
		transform.position = transform.position + new Vector3(right*MoveSpeed*Time.deltaTime,0,0);
	}
}
