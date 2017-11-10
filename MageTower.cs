using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : MonoBehaviour {
    #region variables
    public Transform target;

    [Header("Attributes")]
    public float range = DefaultConstants.range;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public Transform MageTowerUnit;

    //tag for what object type to follow
    public string enemyTag = "Enemy";

    public float rotateSpeed = DefaultConstants.rotateSpeed;

    public GameObject bulletPrefab1;

    //Author: Denisa Dumitrica
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    public Transform firePoint;

    //Author: Denisa Dumitrica
    public int value = DefaultConstants.mageTowerValue;

    //Author:Denisa Dumitrica
    public int upgradeState = 1; 
    #endregion

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    /// <summary>
    /// This method updates the current target to attack
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        //Find the nearest enemy computing the distance to each one
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //the nearest enemy becomes the current target to attack
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update () {
       
        //Author:Denisa Dumitrica
        //If  upgrade is turned on, and this mage tower is cliked we try to upgrade it
        if (Input.GetButtonDown("Fire1") && UpgradeController.upgradeActive == true)
        {
            //If we are in the first upgrade level we try to upgrade to level 2,if we have money
            if(upgradeState == 1 && Currency.money - DefaultConstants.upgrade2Value >= 0)
            {
                upgradeState = 2;
                Currency.money -= DefaultConstants.upgrade2Value;
            }
            //If we are in upgrade level 2 we try to upgrade to level 3 if we have money
            else if(upgradeState == 2 && Currency.money - DefaultConstants.upgrade3Power >= 0)
            {
                upgradeState = 3;
                Currency.money -= DefaultConstants.upgrade3Value;
            }
        }
        if (LifeController.life <= 0)
        {
            //do nothing
        }
        else
        {
            //if we don't have a target we do nothing
            if (target == null)
                return;

            Vector3 dir = target.position - transform.position;

            //this is used for rotation, making the tower to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(MageTowerUnit.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            MageTowerUnit.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

        /// <summary>
        /// This method instantiate the current weapon type and use it to attack the enemy
        /// </summary>
        void Shoot()
        {
            //Auhor: Denisa Dumitrica
            if (upgradeState == 1)
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab1, firePoint.position, firePoint.rotation);
                WeaponType1 bullet = bulletGO.GetComponent<WeaponType1>();

               if (bullet != null)
                {
                    bullet.Seek(target);
                }
            }
            else if (upgradeState == 2)
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
                Upgrade2 bullet = bulletGO.GetComponent<Upgrade2>();

                if (bullet != null)
                {
                    bullet.Seek(target);
                }
        }
            else if(upgradeState == 3)
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab3, firePoint.position, firePoint.rotation);
                Upgrade3 bullet = bulletGO.GetComponent<Upgrade3>();

                if (bullet != null)
                {
                    bullet.Seek(target);
                }
        }
        }

    /// <summary>
    /// Draw range
    /// </summary>
void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

