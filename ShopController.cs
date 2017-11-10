using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public GameObject shop;
    public static bool activeFlag;

    //Author: Denisa Dumitrica
    public Text upgradeActive;

    // Use this for initialization
    void Start () {
        shop.SetActive(false);
        activeFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (LifeController.life > 0)
        {

            //Author:Denisa Dumitrica
            //If shop was  turned off from upgrade state we have to deactivate it from here
            if (activeFlag == false)
            {
                shop.SetActive(false);
            }

            //if the shop menu is not active, we are not in upgrade mood and we are between levels, we open it, otherwise we close it.
            if (activeFlag == false && Input.GetKeyDown(KeyCode.Z) && WaveSwawner.levelState == false)
            {
                //Author: Denisa Dumitrica
                //If upgrade mood is set on we deactivate it when we activate the shop
                if (UpgradeController.upgradeActive == true)
                {
                    UpgradeController.upgradeActive = false;
                    upgradeActive.text = "Upgrade off";
                }

                shop.SetActive(true);
                activeFlag = true;
            }
            else if (activeFlag == true && Input.GetKeyDown(KeyCode.Z) && WaveSwawner.levelState == false)
            {
                shop.SetActive(false);
                activeFlag = false;
            }

            //If a level has started and the shop is opened we have to close it
            if (WaveSwawner.levelState == true && activeFlag == true)
            {
                shop.SetActive(false);
                activeFlag = false;
            }
        }
    }
}
