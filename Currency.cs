using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Denisa Dumitrica
/// </summary>
public class Currency : MonoBehaviour {

    public static int money;
    public Text currency;

	// Use this for initialization
	void Start () {
        money = 100;
        currency.text = money.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        currency.text = money.ToString();
    }

    
   
}
