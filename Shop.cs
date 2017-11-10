using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseMageTower()
    {
        buildManager.SetTowerToBuild(buildManager.mageTower);
        buildManager.type = 1;
    }

    public void PurchaseWatchTower()
    {
        buildManager.SetTowerToBuild(buildManager.watchTower);
        buildManager.type = 2;
    }

    public void PurchaseCannonTower()
    {
        buildManager.SetTowerToBuild(buildManager.cannonTower);
        buildManager.type = 3;
    }

    public void PurchaseCharacter()
    {
        buildManager.UseCharacter();
        buildManager.type = 4;
    }
}
