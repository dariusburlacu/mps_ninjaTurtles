using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Transform target;
    public float speed = DefaultConstants.bulletSpeed;
    public float arrowPower = DefaultConstants.arrowPower;
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

        //Destroy enemy when it runs out of life
        Destroy(target.gameObject);

        //Keep the number of the remained enemies for knowing the current level state
        WaveSwawner.remainedEnemies--;
    }
}
