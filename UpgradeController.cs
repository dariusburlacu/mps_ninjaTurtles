using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Denisa Dumitrica
/// </summary>
public class UpgradeController : MonoBehaviour {

    public static bool upgradeActive = false;
    public Text upgradeActiveScreen;

    // Use this for initialization
    void Start () {
        upgradeActive = false;
    }
	
	// Update is called once per frame
	void Update () {
      
            //if U key is pressed and upgrade is active and we are no playing a level, we deactivate the upgrade mood
            if (Input.GetKeyDown(KeyCode.U) && upgradeActive == true && WaveSwawner.levelState == false)
            {
                upgradeActive = false;
                upgradeActiveScreen.text = "Upgrade off";
            }
            //if U is pressed and upgrade is deactivated and we are not playing a level, we set it on
            else if (Input.GetKeyDown(KeyCode.U) && upgradeActive == false && WaveSwawner.levelState == false)
            {
                //If the shop is active and open we have to close it and set it off
                if (ShopController.activeFlag == true)
                {
                    ShopController.activeFlag = false;
                }
                upgradeActive = true;
                upgradeActiveScreen.text = "Upgrade on";
            }

            //If a level has started and the upgrade is on, we have to turned it off
            if (WaveSwawner.levelState == true && upgradeActive == true)
            {
                upgradeActive = false;
                upgradeActiveScreen.text = "Upgrade off";
            }
       
    }

}
