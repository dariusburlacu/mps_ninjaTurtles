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
    }

    public void PurchaseWatchTower()
    {
        buildManager.SetTowerToBuild(buildManager.watchTower);
    }

    public void PurchaseCannonTower()
    {
        buildManager.SetTowerToBuild(buildManager.cannonTower);
    }
}
