using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour {
    #region variables
    public Transform target;

    [Header("Attributes")]
    public float range = 40f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public Transform CannonTowerUnit;

    //tag for what object type to follow
    public string enemyTag = "Enemy";

    public float rotateSpeed = DefaultConstants.rotateSpeed;

    public GameObject bulletPrefab;
    public Transform firePoint;

    //Author: Denisa Dumitrica
    public int value = DefaultConstants.cannonTowerValue;
    #endregion

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
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
            Vector3 rotation = Quaternion.Lerp(CannonTowerUnit.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            CannonTowerUnit.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        WeaponType1 bullet = bulletGO.GetComponent<WeaponType1>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
