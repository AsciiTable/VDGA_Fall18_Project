using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIMovement : MonoBehaviour {
	public float MoveSpeed = 5f;
	private Rigidbody2D _enemy;
	public float MinPos;
	public float MaxPos;
	public bool SpriteFacingRight;//asks for what side the sprite is facing before 
    private bool goingUp = true;
    private int saveDirection = 1;
    public enum Movement {
        Horizontal = 0,
        Vertical = 1
    }

    public Movement enemyMovement = Movement.Horizontal;
	
	// Use this for initialization
	void Start () {
		_enemy = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
	{
        Move(Moving());
	}

	int Moving(){//returns 1 if AI is moving right
        if (enemyMovement == Movement.Horizontal)
        {
            if (SpriteFacingRight == true)
            {
                if (HasTurned() == false)
                {
                    //Debug.Log("Moving right!1");
                    return 1;
                }
                else
                {
                    //Debug.Log("Moving left!1");
                    return -1;
                }
            }
            if (HasTurned() == false)
            {
                //Debug.Log("Moving left!2");
                return -1;
            }
            else
            {
                //Debug.Log("Moving right!2");
                return 1;
            }

        }
        else
        {
            if (HasTurned() == true && goingUp)
            {
                Debug.Log("!goingUp");
                goingUp = false;
                saveDirection = -1;
                return -1;
            }
            else if (HasTurned() == true && !goingUp)
            {
                Debug.Log("goingUp");
                goingUp = true;
                saveDirection = 1;
                return 1;
            }
            else
            {
                return saveDirection;
            }
        }

	}
	
	void Move(int direction){
        if (enemyMovement == Movement.Horizontal)
        {
            transform.position = transform.position + new Vector3(direction * MoveSpeed * Time.deltaTime, 0, 0);
        }
        else {
            transform.position = transform.position + new Vector3(0, direction * MoveSpeed * Time.deltaTime, 0);
        }
	}
	bool HasTurned(){
        if (enemyMovement == Movement.Horizontal)
        {
            if (!SpriteFacingRight)
            {
                if (_enemy.position.x <= MinPos)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    SpriteFacingRight = true;
                    return true;
                }

            }
            else
            {
                if (_enemy.position.x >= MaxPos)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    SpriteFacingRight = false;
                    return false;
                }

            }
        }
        else {
            if (_enemy.position.y <= MinPos || _enemy.position.y >= MaxPos) {
                return true;
            }
        }

		return false;
	}
}
