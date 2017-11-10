using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Andreea-Camelia Patru
/// </summary>
public class fpsCharacter : MonoBehaviour {

    float moveFB;
    float moveLR;
    float rotX;
    float rotY;

    CharacterController player;
    public GameObject eyes;
    public GameObject characterWeapon;
    public Transform bulletSpawn;
    public float fireRate = 0.3f;
  
    // Use this for initialization
    void Start () {
        SetPosition();
        player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        //These are used to move the character forward and backward
        moveFB = Input.GetAxis("Horizontal") * DefaultConstants.characterSpeed;

        //These are used to move to left or right
        moveLR = Input.GetAxis("Vertical") * DefaultConstants.characterSpeed;

        //Here we set the rotation around y axe, to left and right
        //and to rotate around x axe, up direction and down direction
        rotX = Input.GetAxis("Mouse X") * DefaultConstants.characterSensitivity;
        rotY = Input.GetAxis("Mouse Y") * DefaultConstants.characterSensitivity;

        //Here we set the movement vector to be updated at each frame
        Vector3 movement = new Vector3(moveLR, 0, moveFB);

        transform.Rotate(0, rotX, 0);
        eyes.transform.Rotate(-rotY, 0, 0);

        //update the character movement at each frame depending on the vertical and horizontal axes
        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime * DefaultConstants.characterMovementMultiplier);

        //Pressed the left click
        if (Input.GetButtonDown("Fire1") && fireRate <= 0)
        {
            Shoot();
            fireRate = 0.6f;
        }
        if(fireRate > 0)
            fireRate-=Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bull = Instantiate(characterWeapon, bulletSpawn.position, bulletSpawn.rotation);
        CharacterWeapon weapon = bull.GetComponent<CharacterWeapon>();
        var dir = Input.mousePosition;
        
        weapon.dir = dir;
    }
    void SetPosition()
    {
        transform.position.Set(235f, 4.5f, 365f);
    }
}
