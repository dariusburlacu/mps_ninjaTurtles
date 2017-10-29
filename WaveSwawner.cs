using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSwawner : MonoBehaviour {

    #region references to wave spawner
        //this it will be used to choose the enemyType to spawn
        public Transform Undead;
        public Transform Orc;
        public Transform spawnPoint;
    #endregion
    #region neededVariables
        public float timeBetweenWaves = DefaultConstants.timeBetweenWaves;
        private float countdown = DefaultConstants.countdown;

        public Text levelSelector;
        public int level = 1;
        public bool levelState = false;

        private int waveNumber = DefaultConstants.waveNumber;
        public static int remainedEnemies = 0;
    #endregion neededVariables
    void Start()
    {
        level = 1;
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
            remainedEnemies++;
            
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
            Instantiate(Undead, spawnPoint.position, spawnPoint.rotation);
        }
        else if(level == 2)
        {
            Instantiate(Orc, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
    #endregion functions

public static class DefaultConstants
{
    public static float timeBetweenWaves = 20f;
    public static float countdown = 5f;
    public static int waveNumber = 10;
}
