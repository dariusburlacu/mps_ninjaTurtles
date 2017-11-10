using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Author: Denisa Dumitrica
/// </summary>
public class LifeController : MonoBehaviour {

    public Text lifeScreen;
    public static int life;

	// Use this for initialization
	void Start () {
        life = 500;
        lifeScreen.text = "Life:" + life.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        lifeScreen.text = "Life:" + life.ToString();
    }
}
