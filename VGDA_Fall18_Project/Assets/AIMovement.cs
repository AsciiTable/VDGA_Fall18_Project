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
	void Update()
	{
		if (HasTurned()){
		}

		Move(MovingRight());
	}

	int MovingRight(){//returns 1 if AI is moving right
		if (SpriteFacingRight == true && GetComponent<SpriteRenderer>().flipX == false){
			Debug.Log("Moving right!");
			return 1;
		}
		Debug.Log("Moving left!");
		return -1;
	}
	
	void Move(int direction){
		transform.position = transform.position + new Vector3(direction*MoveSpeed*Time.deltaTime,0,0);
	}

	bool HasTurned(){
		if (!SpriteFacingRight){
			if (_enemy.position.x < MaxLeftPos){
				GetComponent<SpriteRenderer>().flipX = true;
				return true;
			}
		}
	}
}
