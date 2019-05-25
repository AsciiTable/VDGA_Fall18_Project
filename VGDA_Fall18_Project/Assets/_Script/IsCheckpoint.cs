using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCheckpoint : MonoBehaviour
{

    public bool checkpoint = false;
    public float pointX;
    public float pointY;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Undying");

        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }
}
