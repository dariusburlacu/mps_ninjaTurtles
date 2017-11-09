using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Denisa Dumitrica
/// </summary>
public class Upgrade2 : MonoBehaviour {

    private Transform target;
    public float speed = DefaultConstants.upgrade2Speed;
    public float bulletPower = DefaultConstants.upgrade2Power;
    public GameObject bulletImpactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //find the target position and set the high for shooting
        Vector3 temp = target.position;
        temp.y = 8f;
        Vector3 dir = temp - transform.position;

        //compute the distance, the bullet is moving this frame
        float distanceThisFrame = speed * Time.deltaTime;

        //if the distance to the enemy is lower than the distance the bullet moves
        //then the target is hitted 
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //instantiate the bullet effect and get reference of it for cleaning up
        GameObject effectInstance = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        //Destroy bullet
        Destroy(gameObject);
        DamageEnemy();
    }

    /// <summary>
    /// Author:Andreea-Camelia Patru
    /// This function applies damage depending on enemy type
    /// </summary>
    public void DamageEnemy()
    {
        //Destroy enemy when it runs out of life
        if (WaveSwawner.spawnIterator == 1)
        {
            Skeleton temp = target.gameObject.GetComponent<Skeleton>();
            temp.skeletonlifePoints -= bulletPower;
            if (temp.skeletonlifePoints <= 0)
            {
                Destroy(target.gameObject);

                //Keep the number of the remained enemies for knowing the current level state
                WaveSwawner.remainedEnemies--;

                //Author:Denisa Dumitrica
                //Add money on dead enemy
                Currency.money += temp.value;
            }
        }
        else if (WaveSwawner.spawnIterator == 2)
        {
            Orc temp = target.gameObject.GetComponent<Orc>();
            temp.orclifePoints -= bulletPower;
            if (temp.orclifePoints <= 0)
            {
                Destroy(target.gameObject);

                //Keep the number of the remained enemies for knowing the current level state
                WaveSwawner.remainedEnemies--;

                //Author:Denisa Dumitrica
                //Add money on dead enemy
                Currency.money += temp.value;
            }
        }
        else if (WaveSwawner.spawnIterator == 3)
        {
            Lich temp = target.gameObject.GetComponent<Lich>();
            temp.lichlifePoints -= bulletPower;
            if (temp.lichlifePoints <= 0)
            {
                Destroy(target.gameObject);

                //Keep the number of the remained enemies for knowing the current level state
                WaveSwawner.remainedEnemies--;

                //Author:Denisa Dumitrica
                //Add money on dead enemy
                Currency.money += temp.value;
            }
        }
    }
}
