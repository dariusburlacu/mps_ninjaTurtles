using UnityEngine;
/// <summary>
/// Author: Andreea-Camelia Patru
/// </summary>
public class CharacterWeapon : MonoBehaviour {

    public GameObject bulletImpactEffect;
    public float speed = DefaultConstants.bulletSpeed;
    public float power = DefaultConstants.characterWeaponPower;
    public Vector3 dir;
    public string enemyTag = "Enemy";

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.rotation.SetLookRotation(dir, Vector3.right);
        transform.position += transform.right * speed * Time.deltaTime;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < 15f)
            {
                Destroy(gameObject);

                //instantiate the bullet effect and get reference of it for cleaning up
                GameObject effectInstance = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
                Destroy(effectInstance, 2f);

                Hit(enemy);
            }
        }
        DestroyFunction();
    }

    public void Hit(GameObject target)
    {
        //Destroy enemy when it runs out of life
        if (WaveSwawner.spawnIterator == 1)
        {
            Skeleton temp = target.gameObject.GetComponent<Skeleton>();
            temp.skeletonlifePoints -= power;
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
            temp.orclifePoints -= power;
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
            temp.lichlifePoints -= power;
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

    public void DestroyFunction()
    {
        Destroy(gameObject, 2.5f);
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
