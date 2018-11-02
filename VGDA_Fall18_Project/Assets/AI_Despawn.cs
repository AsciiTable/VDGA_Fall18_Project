using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Despawn : MonoBehaviour{

	private GameObject enemy;
	public float YDespawnPosition = -15f;
	public float XDespawnPosition = -1500f;

	private float enemyYPosition;
	private float enemyXPosition;
	// Use this for initialization
	void Start ()
	{
		enemyYPosition = enemy.GetComponent<SpriteRenderer>().transform.position.y;
		enemyXPosition = enemy.GetComponent<SpriteRenderer>().transform.position.x;

	}
	
	// Update is called once per frame
	void Update () {
		enemyYPosition = enemy.GetComponent<SpriteRenderer>().transform.position.y;
		enemyXPosition = enemy.GetComponent<SpriteRenderer>().transform.position.x;

		if (enemyYPosition < YDespawnPosition || enemyYPosition < XDespawnPosition)
		{
			Destroy(enemy);
		}

	}
}
