using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {
    
    public void LeaveGameButton()
    {
        Debug.Log("Leave Game");
        Application.Quit();
    }
}
