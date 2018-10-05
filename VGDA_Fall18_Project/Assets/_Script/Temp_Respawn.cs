using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_Respawn : MonoBehaviour {

    public GameObject spawnPosition;
    public float verticalPos = -7.35f;
    public float horizontalPos = -2.93f;
    private bool isDead = false;
    public float SuicidePoint = -15f;
    public Transform playerTrans;
    public GameObject deathSpace;

    // Use this for initialization
    void Start () {
        isDead = false;
    }
	
    // Update is called once per frame
    void Update () {
        if (isDead == true || playerTrans.position.y < SuicidePoint){
            Debug.Log("some death is true");
            ResetScene();
        }
    }
    //tbh is not used but we'll see
    public void Die(){
        Debug.Log("is dead");
        isDead = true;

    }

    //Resets the scene to the beginning
    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}