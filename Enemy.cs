using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float skeletonlifePoints;
    public float orclifePoints;
    public float lichlifePoints;
    //Target that must be reached
    private Transform target;

    //The index of the current point on the path
    private int wavepointIndex = 0;

    public int pathDirection;
    // Use this for initialization
    void Start()
    {
        speed = DefaultConstants.enemySpeed;
        orclifePoints = DefaultConstants.orcLife;
        lichlifePoints = DefaultConstants.lichLife;
        skeletonlifePoints = DefaultConstants.skeletonLife;

        if (pathDirection == 0)
        {
            //we start from the first enemy base point
            target = waypoints.points[0];
            pathDirection = 0;
            transform.Rotate(Vector3.up, -90, Space.Self);
        }
        else if(pathDirection == 1)
        {
            //we start from the second enemy base point
            target = SecondWavePoints.points[0];
            pathDirection = 1;
        }
        
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
            if (pathDirection == 0)
                firstPathRotation(wavepointIndex);
            else
                secondPathRotation(wavepointIndex);
            GetNextWayPoint();
        }

    }

    /// <summary>
    /// We update the point target
    /// </summary>
    void GetNextWayPoint()
    {
        //if the enemy walks on the first path
        if(pathDirection  == 0)
        {
            //When an enemy arrives at the destination we destroy it
            if (wavepointIndex >= waypoints.points.Length - 1)
            {

                Destroy(gameObject);
                WaveSwawner.remainedEnemies--;

                //Author: Denisa Dumitrica
                LifeController.life -= DefaultConstants.BaseDamage;
                return;
            }

            wavepointIndex++;
            target = waypoints.points[wavepointIndex];
        }
        //if the enemy walks on the second path
        else if(pathDirection == 1)
        {
            //When an enemy arrives at the destination we destroy it
            if (wavepointIndex >= SecondWavePoints.points.Length - 1)
            {

                Destroy(gameObject);
                WaveSwawner.remainedEnemies--;

                //Author: Denisa Dumitrica
                LifeController.life -= DefaultConstants.BaseDamage;
                return;
            }
            wavepointIndex++;
            target = SecondWavePoints.points[wavepointIndex];
        }
    }

    void firstPathRotation(int wavepointIndex)
    {
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
    }
    void secondPathRotation(int wavepointIndex)
    {
        switch (wavepointIndex)
        {
            //For each reaching point i rotate the enemy to the next point
            case 0:
                transform.Rotate(Vector3.up, -90, Space.Self);
                break;
            case 1:
                transform.Rotate(Vector3.up, -90, Space.Self);
                break;
            case 2:
                transform.Rotate(Vector3.up, 90, Space.Self);
                break;
            case 3:
                transform.Rotate(Vector3.up, -90, Space.Self);
                break;
            case 4:
                transform.Rotate(Vector3.up, 90, Space.Self);
                break;
            case 5:
                transform.Rotate(Vector3.up, 90, Space.Self);
                break;
            case 6:
                transform.Rotate(Vector3.up, -90, Space.Self);
                break;
            case 7:
                transform.Rotate(Vector3.up, -90, Space.Self);
                break;
            case 8:
                transform.Rotate(Vector3.up, 90, Space.Self);
                break;
        }
    }
}
