using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject shop;
    public static bool activeFlag;

    // Use this for initialization
	void Start () {
        shop.SetActive(false);
        activeFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        //if the shop menu is not active we open it, otherwise we close it.
        if (activeFlag == false && Input.GetKeyDown(KeyCode.S))
        {
            shop.SetActive(true);
            activeFlag = true;
        }
        else if (activeFlag == true && Input.GetKeyDown(KeyCode.S))
        {
            shop.SetActive(false);
            activeFlag = false;
        }
    }
}
