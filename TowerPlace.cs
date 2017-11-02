using UnityEngine;
using UnityEngine.EventSystems;
public class TowerPlace : MonoBehaviour {

    //this is the color that is shown when the mouse cursor is on a tower place
    //this is set in unity where we find the phisical reference of this logical object
    public Color color;
    //this is the tower to be buit on this place 
    private GameObject tower;
    
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start ()
    {
        //Here we save the tower place color
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseDown ()
    {
        if (buildManager.GetTowerToBuild() == null)
            return;

        //if we already have a tower built here
        //we let the user know he is not able to do this
        if(tower != null)
        {
            Debug.Log("Can't build there! - TODO: DISPLAY ON SCREEN.");
            return;
        }
        if (ShopController.activeFlag == true)
        {
            GameObject towerToBuild = buildManager.GetTowerToBuild();
            tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);
        }
    }

    /// <summary>
    /// This method highlights a tower place with set color the tower place when 
    /// the mouse cursor is over it
    /// </summary>
	void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTowerToBuild() == null)
            return;

        if (ShopController.activeFlag == true)
        {
            rend.material.color = color;
        }else if (ShopController.activeFlag == false)
        {
            rend.material.color = startColor;
        }
    }

    /// <summary>
    /// This method sets back the initial color after the mouse cursor
    /// was put outside the tower place
    /// </summary>
    void OnMouseExit ()
    {
        if (ShopController.activeFlag == true)
        {
            rend.material.color = startColor;
        }
        if(ShopController.activeFlag == false)
        {
            rend.material.color = startColor;
        }
    }
}
