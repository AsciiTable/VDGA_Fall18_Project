using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Despawn : MonoBehaviour
{
	public GameObject enemy;
	public Transform enemyPos;
	public float YDespawnPosition = -15f;
	public float XDespawnPosition = -1500f;

	private float enemyYPosition;
	private float enemyXPosition;
	// Use this for initialization
	void Start ()
	{
		enemyYPosition = enemyPos.position.y;
		enemyXPosition = enemyPos.position.x;

	}
	
	// Update is called once per frame
	void Update () {
		enemyYPosition = enemyPos.position.y;
		enemyXPosition = enemyPos.position.x;

		if (enemyYPosition < YDespawnPosition || enemyXPosition < XDespawnPosition)
		{
			Destroy(enemy);
			Debug.Log("Destroyed");
		}

	}
}
