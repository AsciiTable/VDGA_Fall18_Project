using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class LoadCutscene : MonoBehaviour {

    private Image Cutscene_image;

    [Header("Buttons")]
    [SerializeField] private Image NextButton_image;
    [SerializeField] private Image PrevButton_image;
    [SerializeField] private Image StartGame_image;
    private Text NextButton_text;
    private Text PrevButton_text;
    private Text StartGame_text;

    [Space(10)][Header("Scenes")]
    [SerializeField] private Sprite[] Cutscene_Sprite;
    private int Cutscene_Num = 0;
    private int Cutscene_Num_Max;
    private bool finCutscene = false;

    [Space(10)][Header("Transitioning")]
    [SerializeField] private Image Transition_image;
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private bool playingScene = false;

    private int counter = 0;

    void Awake () {
        Cutscene_image = GetComponent<Image>();
        NextButton_text = NextButton_image.gameObject.GetComponentInChildren<Text>();
        PrevButton_text = PrevButton_image.gameObject.GetComponentInChildren<Text>();
        StartGame_text = StartGame_image.gameObject.GetComponentInChildren<Text>();

        Cutscene_Num_Max = Cutscene_Sprite.Length-1;
        Transition_image.color = new Color(1f, 1f, 1f, 0f);
    }

    //Go to Next Cutscene
    public void NextButton()
    {
        if (Cutscene_Num < Cutscene_Num_Max && !playingScene)
        {
            playingScene = true;
            StartCoroutine(changeScene(1));
        }
        switch (counter)
        {
            case 0:
                Audio.PlaySound("chip_eating");
                break;
            case 2:
                Audio.PlaySound("leaves_rustle_step");
                break;
            case 4:
                break;
            case 6:
                Audio.PlaySound("tire_squeal");
                break;
            case 7:
                Audio.PlaySound("blood_splat");
                break;
            case 8:
                Audio.PlaySound("ambulance");
                break;
            default:
                break;
        }
        counter++;
    }

    //Go to Previous Cutscene
    public void PrevButton()
    {
        if (Cutscene_Num > 0 && !playingScene)
        {
            playingScene = true;
            StartCoroutine(changeScene(-1));
        }
    }

    IEnumerator changeScene(int changeNum)
    {
        yield return new WaitUntil(()=> playingScene);
        Transition_image.sprite = Cutscene_image.sprite;
        Cutscene_Num += changeNum;
        Transition_image.color = new Color(1f,1f,1f,1f);
        for(float i = 0; i < transitionSpeed; i+= 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Debug.Log(Transition_image.color.a);
            Transition_image.color = new Color(1f, 1f, 1f, Transition_image.color.a - (1 / (transitionSpeed * 100)));
        }
        playingScene = false;
    }


    void Update () {

        if (Input.GetButtonDown("NextCutscene"))
        {
            NextButton();
        }
        /*if (Input.GetButtonDown("PrevCutscene"))
        {
            PrevButton();
        }*/

        Cutscene_image.sprite = Cutscene_Sprite[Cutscene_Num];

        //Change button if they reach end of slideshows
        if (Cutscene_Num == Cutscene_Num_Max)
        {
            NextButton_image.enabled = false;
            NextButton_text.enabled = false;

            if (!finCutscene)
            {
                //Audio.PlaySound("bgm_cutscene_soundtelling");
                finCutscene = true;
            }

            StartGame_image.enabled = true;
            StartGame_text.enabled = true;
        }
        else
        {
            NextButton_image.enabled = true;
            NextButton_text.enabled = true;

            StartGame_image.enabled = false;
            StartGame_text.enabled = false;
        }
        if (Cutscene_Num == 0)
        {
            PrevButton_image.enabled = false;
            PrevButton_text.enabled = false;
        }
        else
        {
            PrevButton_image.enabled = true;
            PrevButton_text.enabled = true;
        }

    }
}
