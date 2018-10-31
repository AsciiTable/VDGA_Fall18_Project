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
		Move(MovingRight());
	}

	int MovingRight(){//returns 1 if AI is moving right
		if (SpriteFacingRight == true){
			if (HasTurned() == false){
				//Debug.Log("Moving right!1");
				return 1;
			}
			else{
				//Debug.Log("Moving left!1");
				return -1;
			}
		}
		else {
			if (HasTurned() == false){
				//Debug.Log("Moving left!2");
				return -1;
			}
			else{
				//Debug.Log("Moving right!2");
				return 1;
			}
		}
	}
	
	void Move(int direction){
		transform.position = transform.position + new Vector3(direction*MoveSpeed*Time.deltaTime,0,0);
	}
	bool HasTurned(){
		if (!SpriteFacingRight){
			if (_enemy.position.x <= MaxLeftPos){
				GetComponent<SpriteRenderer>().flipX = true;
				SpriteFacingRight = true;
				return true;
			}

		}
		else{
			if (_enemy.position.x >= MaxRightPos){
				GetComponent<SpriteRenderer>().flipX = false;
				SpriteFacingRight = false;
				return false;
			}

		}
		return false;
	}
}
