using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Andreea-Camelia Patru
/// </summary>
public class fpsCharacterController : MonoBehaviour {

    public Camera thirdPersonCamera;
    public GameObject fps;
    public static int timeToUse = 0;
    public static bool used = false;
    
    // Use this for initialization
    void Start () {
        thirdPersonCamera.gameObject.SetActive(true);
        fps.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //if the third person camera is set on true we activate it and
        //deactivate the fps camera
        if(used == false)
        {
            thirdPersonCamera.gameObject.SetActive(true);
            fps.SetActive(false);
        }
        //if the fps is activated and third person camera is deactivated
        //we activate fps camera and turn off third person camera
        else if(used == true && WaveSwawner.levelState == true && timeToUse == 2)
        {
            thirdPersonCamera.gameObject.SetActive(false);
            fps.SetActive(true);
        }
        //if i am using the character during the current level and i pass the level i am
        //automatically switched to third person camera
        if(WaveSwawner.levelState == false && used == true && timeToUse == 2)
        {
            used = false;
            timeToUse = 0;
        }
    }
}
