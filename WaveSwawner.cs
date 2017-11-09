using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSwawner : MonoBehaviour {

    #region references to wave spawner
        //this it will be used to choose the enemyType to spawn
        public Transform Undead;
        public Transform Orc;
        public Transform Lich;
        public Transform spawnPoint;
        public Transform secondSpawnPoint;
        public static int pathDirection = 0;
    #endregion

    #region neededVariables
        public float timeBetweenWaves = DefaultConstants.timeBetweenWaves;
        private float countdown = DefaultConstants.countdown;

        public Text levelSelector;
        public static int level = 1;
        public bool levelState = false;

        private int waveNumber = DefaultConstants.waveNumber;
        public static int remainedEnemies = 0;
    #endregion neededVariables

    void Start()
    {
        level = 1;
        pathDirection = 0;
    }
    #region Functions
    void Update()
    {
        //if the level is active and the last enemy dies i stop the current level
        //and i set the next one
        if (remainedEnemies == 0 && levelState == true)
        {
            levelState = false;
            level++;
        }

        //when the count down timer expire we reset the flag
        //for the next wave of enemies
        if(countdown <= 0f && levelState == false)
        {
            //i display the level number and o start the current level
            levelSelector.text = "Level " + level.ToString();
            levelState = true;

            //coroutine is a function that has the ability to pause
            //execution and return control to Unity
            //but then to continue where it left off on the following frame
            //(Hint for others: it can be used to apply an animation in a single frame)
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            remainedEnemies = 10;
        }

        //Here is the timer that let the player to put towers between levels
        if (levelState == false)
        {
            countdown -= Time.deltaTime;
            levelSelector.text = Mathf.Round(countdown).ToString();
        }
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            
            //after an enemy is generated we want to wait
            // for a couple of seconds in order to 
            //generate the next one
            yield return new WaitForSeconds(2.0f);
        }
    }

    void SpawnEnemy()
    {
        if (level == 1)
        {
            Transform enemytypeObject1 = Instantiate(Undead, spawnPoint.position, spawnPoint.rotation);
            Skeleton  skeletionEnemy = enemytypeObject1.GetComponent<Skeleton>();
            skeletionEnemy.pathDirection = 0;
        }
        else if(level == 2)
        {
            pathDirection = 1;
            Transform enemytypeObject2 = Instantiate(Orc, secondSpawnPoint.position, secondSpawnPoint.rotation);
            Orc orcEnemy = enemytypeObject2.GetComponent<Orc>();
            orcEnemy.pathDirection = 1;
        }
        else if(level == 3)
        {
            pathDirection = 0;
            Transform enemytypeObject3 = Instantiate(Lich, spawnPoint.position, spawnPoint.rotation);
            Lich lichEnemy = enemytypeObject3.GetComponent<Lich>();
            lichEnemy.pathDirection = 0;
        }
    }
}
    #endregion functions

public static class DefaultConstants
{
    public static float timeBetweenWaves = 20f;
    public static float countdown = 5f;
    public static int waveNumber = 10;
    public static float range = 30f;
    public static float rotateSpeed = 10f;
    public static float bulletSpeed = 70f;

    public static float mageTowerLife = 700f;
    public static float watchTowerLife = 1000f;
    public static float cannonTowerLife = 1500f;

    public static int mageTowerValue = 150;
    public static int watchTowerValue = 250;
    public static int cannonTowerValue = 400;

    public static float arrowPower = 25f;
    public static float missilePower = 50f;
    public static float weaponType3Power = 100f;
}
