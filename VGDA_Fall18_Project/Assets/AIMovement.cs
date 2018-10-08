using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
	public float MoveSpeed = 5f;
	private Rigidbody2D _enemy;
	// Use this for initialization
	void Start () {
		_enemy = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + new Vector3((MoveSpeed * Time.deltaTime), 0, 0);
		if(transform.position.x < 0 && GetComponent<SpriteRenderer>().flipX){
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else if (transform.position.x> 0 && !GetComponent<SpriteRenderer>().flipX){
			GetComponent<SpriteRenderer>().flipX = true;
		}
		
	}
}
