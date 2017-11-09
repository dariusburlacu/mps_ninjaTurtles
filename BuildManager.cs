using UnityEngine;

public class BuildManager : MonoBehaviour {

    //Singleton implementation for the build manager
    //We do this because we don't want to have a reference for each tower place
    //So we only use a single instance to reference a build manager
    public static BuildManager instance;

    private GameObject towerToBuild;

    //towers to be built
    public GameObject mageTower;
    public GameObject watchTower;
    public GameObject cannonTower;
    public GameObject character;

    /// <summary>
    /// this method retrieves the build manager instance to be used
    /// </summary>
    void Awake ()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public void SetTowerToBuild(GameObject towerType)
    {
        towerToBuild = towerType;
    }

    /// <summary>
    /// Author: Andreea-Camelia Patru
    /// </summary>
    public void UseCharacter()
    {
        if (WaveSwawner.levelState == false)
        {
            fpsCharacterController.used = true;
        }
    }

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }
}
