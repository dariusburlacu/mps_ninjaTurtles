using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : MonoBehaviour {

    public Transform target;

    [Header("Attributes")]
    public float range = 30f;
    

    [Header("Unity Setup Fields")]
    public Transform MageTowerUnit;

    public string enemyTag = "Enemy";
    public float rotateSpeed = 10f;

    
	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
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
        //if we don't have a target we do nothing
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;

        //this is used for rotation
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(MageTowerUnit.rotation, lookRotation, Time.deltaTime * rotateSpeed ).eulerAngles;
        MageTowerUnit.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
