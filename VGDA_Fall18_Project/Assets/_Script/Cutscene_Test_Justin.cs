using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Test_Justin : MonoBehaviour {

    private Animator cutscene_Anim;
    private int activeCutscene_Anim;

	void Awake () {
        cutscene_Anim = GetComponent<Animator>();
        cutscene_Anim.speed = 0f;

        cutscene_Anim.Play("Cutscene", 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        cutscene_Anim.Play("Cutscene", 0, 0);

    }
}
