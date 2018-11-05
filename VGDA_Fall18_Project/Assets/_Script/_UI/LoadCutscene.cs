using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCutscene : MonoBehaviour {

    public Image Cutscene_image;
        
    public Sprite[] Cutscene_Sprite;

    public int Cutscene_Num = 0;
    public int Cutscene_Num_Max = 5;

    // Use this for initialization
    void Awake () {

        Cutscene_Num = 0;
        Cutscene_Num_Max = Cutscene_Sprite.Length-1;
	}
	
    public void NextCutscene()
    {
        if (Cutscene_Num < Cutscene_Num_Max)
        {
            Cutscene_Num++;
        }
    }
    public void PrevCutscene()
    {
        if (Cutscene_Num > 0)
        {
            Cutscene_Num--;
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("NextCutscene"))
        {
            NextCutscene();
        }
        if (Input.GetButtonDown("PrevCutscene"))
        {
            PrevCutscene();
        }

        Cutscene_image.sprite = Cutscene_Sprite[Cutscene_Num];

    }
}
