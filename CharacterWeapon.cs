using UnityEngine;

/// <summary>
/// Author: Andreea-Camelia Patru
/// </summary>
public class CharacterWeapon : MonoBehaviour {

    public float speed = DefaultConstants.bulletSpeed;
    public float power = DefaultConstants.characterWeaponPower;
    public Vector3 dir;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.rotation.SetLookRotation(dir, Vector3.right);
        transform.position += transform.right * speed * Time.deltaTime;
        DestroyFunction();
    }

    public void Hit()
    {
        
    }

    public void DestroyFunction()
    {
        Destroy(gameObject, 2.5f);
    }
   
}
