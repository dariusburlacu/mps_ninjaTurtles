using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;

    //Target that must be reached
    private Transform target;

    //The index of the current point on the path
    private int wavepointIndex = 0;
    
    // Use this for initialization
    void Start()
    {

        //we start from the enemy base
        target = waypoints.points[0];
        transform.Rotate(Vector3.up, -90, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {

        //direction represents the distance between the object position and
        // the next point position
        Vector3 direction = target.position - transform.position;

        //Here is made a translate tranformation applied to our character
        //the direction is normalized(directionPoint/directionLength in math concepts)
        //So, at each frame the enemy is moved with direction.normalized *speed* deltatime
        //We also relate this transformation to the World place of the game
        if (Vector3.Distance(transform.position, target.position) > 0.2f)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            //When we reach a point we need the next one
            //Because not always , the enemy reach the point , there are some minor 
            //error precision and then i consider to be reached a point when the distance

            //between them is lower than 0.2f
            switch (wavepointIndex)
            {
                //For each reaching point i rotate the enemy to the next point
                case 0:
                    transform.Rotate(Vector3.up, -90, Space.Self);
                    break;
                case 1:
                    transform.Rotate(Vector3.up, 90, Space.Self);
                    break;
                case 2:
                    transform.Rotate(Vector3.up, 90, Space.Self);
                    break;
                case 3:
                    transform.Rotate(Vector3.up, -90, Space.Self);
                    break;
                case 4:
                    transform.Rotate(Vector3.up, -90, Space.Self);
                    break;
                case 5:
                    transform.Rotate(Vector3.up, 90, Space.Self);
                    break;
                case 6:
                    transform.Rotate(Vector3.up, 90, Space.Self);
                    break;
                case 7:
                    transform.Rotate(Vector3.up, -90, Space.Self);
                    break;
            }

            GetNextWayPoint();

        }

    }

    /// <summary>
    /// We update the point target
    /// </summary>
    void GetNextWayPoint()
    {
        //When an enemy arrives at the destination we destroy it
        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            
            Destroy(gameObject);
            WaveSwawner.remainedEnemies--;
            return;
        }

        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }
}
