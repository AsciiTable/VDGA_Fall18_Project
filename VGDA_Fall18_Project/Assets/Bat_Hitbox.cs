using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Hitbox : MonoBehaviour{
	
	public GameObject bat;
	public bool isSwung = false;
	
	// Use this for initialization
	void Start (){
		bat.GetComponent<Collider2D>().enabled = false;
		bat.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSwung){
			bat.GetComponent<Collider2D>().enabled = true;
			bat.GetComponent<SpriteRenderer>().enabled = true;
			isSwung = false;
		}
		else{
			bat.GetComponent<Collider2D>().enabled = false;
			bat.GetComponent<SpriteRenderer>().enabled = false;

		}
	}
}
