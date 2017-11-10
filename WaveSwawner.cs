using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSwawner : MonoBehaviour {

    #region references to wave spawner
        //this it will be used to choose the enemyType to spawn
        public Transform Undead;
        public Transform OrcTransform;
        public Transform LichTransform;

        public Transform spawnPoint;
        public Transform secondSpawnPoint;

        public static int pathDirection = 0;
    #endregion

    #region neededVariables
        public float timeBetweenWaves = DefaultConstants.timeBetweenWaves;
        private float countdown = DefaultConstants.countdown;

        public Text levelSelector;
        public static int level = 1;
        public static bool levelState = false;
        public static int spawnIterator = 0;

        private int number = DefaultConstants.waveNumber;
        public static int remainedEnemies = 0;

        // this flag say if the special enemy must be rendered
        public static bool specialEnemyRender;

    #endregion neededVariables

    void Start()
    {
        level = 1;
        pathDirection = 0;
        spawnIterator = 0;
        specialEnemyRender = false;
        number = DefaultConstants.waveNumber;

    }
    #region Functions
    void Update()
    {

        //Author: Denisa Dumitirca
        if (LifeController.life <= 0)

        //Author:Andreea-Camelia Patru
        //Here we reset the enemy spawn type and we upgrade the difficulty
        //after three levels have been played
        if (remainedEnemies == 0 && spawnIterator == 3)
        {
            spawnIterator = 0;
            UpdateDifficulty();
        }

        //if the level is active and the last enemy dies i stop the current level
        //and i set the next one
        if (remainedEnemies == 0 && levelState == true)

        {
            levelSelector.text = "Game Over";
        }
        else
        {

            //Author:Andreea-Camelia Patru
            //Here we reset the enemy spawn type and we upgrade the difficulty
            //after three levels have been played
            if (remainedEnemies == 0 && spawnIterator == 3)
            {
                spawnIterator = 0;
                UpdateDifficulty();
            }

            //i display the level number and o start the current level
            levelSelector.text = "Level " + level.ToString();
            levelState = true;


            //Author :Andreea-Camelia Patru
            //This is used to generate the enemy type on each level
            spawnIterator++;

            /// <summary>
            /// Author: Andreea-Camelia Patru
            /// we mark that the vel started and that the character which is currently use will
            /// become unavailable at the end of the level
            /// </summary>
            if (fpsCharacterController.timeToUse == 1)
            {
                fpsCharacterController.timeToUse = 2;
            }

            //coroutine is a function that has the ability to pause
            //execution and return control to Unity
            //but then to continue where it left off on the following frame
            //(Hint for others: it can be used to apply an animation in a single frame)
            remainedEnemies = 10;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        /// <summary>
        /// Author: Andreea-Camelia Patru
        /// We mark that the character is used and that it will be available only this level
        /// </summary>
        if (levelState == false && fpsCharacterController.used == true && fpsCharacterController.timeToUse == 0)
        {
            fpsCharacterController.timeToUse = 1;
        }


            //if the level is active and the last enemy dies i stop the current level
            //and i set the next one
            if (remainedEnemies == 0 && levelState == true)
            {
                levelState = false;
                level++;
            }

            //when the count down timer expire we reset the flag
            //for the next wave of enemies
            if (countdown <= 0f && levelState == false)
            {
                //i display the level number and o start the current level
                levelSelector.text = "Level " + level.ToString();
                levelState = true;


                //Author :Andreea-Camelia Patru
                //This is used to generate the enemy type on each level
                spawnIterator++;

                /// <summary>
                /// Author: Andreea-Camelia Patru
                /// we mark that the vel started and that the character which is currently use will
                /// become unavailable at the end of the level
                /// </summary>
                if (fpsCharacterController.timeToUse == 1)
                {
                    fpsCharacterController.timeToUse = 2;
                }
                
                //coroutine is a function that has the ability to pause
                //execution and return control to Unity
                //but then to continue where it left off on the following frame
                //(Hint for others: it can be used to apply an animation in a single frame)
                
                remainedEnemies = 12;
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            /// <summary>
            /// Author: Andreea-Camelia Patru
            /// We mark that the character is used and that it will be available only this level
            /// </summary>
            if (levelState == false && fpsCharacterController.used == true && fpsCharacterController.timeToUse == 0)
            {
                fpsCharacterController.timeToUse = 1;
            }

            //Here is the timer that let the player to put towers between levels
            if (levelState == false)
            {
                countdown -= Time.deltaTime;
                levelSelector.text = Mathf.Round(countdown).ToString();
            }
        }
    }

    // every 2 seconds perform the print()
     IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < number; i++)
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
        if (spawnIterator == 1)
        {
            Transform enemytypeObject1 = Instantiate(Undead, spawnPoint.position, spawnPoint.rotation);
            Skeleton  skeletionEnemy = enemytypeObject1.GetComponent<Skeleton>();
            skeletionEnemy.pathDirection = 0;
        }
        else if(spawnIterator == 2)
        {
            pathDirection = 1;
            Transform enemytypeObject2 = Instantiate(OrcTransform, secondSpawnPoint.position, secondSpawnPoint.rotation);
            Orc orcEnemy = enemytypeObject2.GetComponent<Orc>();
            orcEnemy.pathDirection = 1;
        }
        else if(spawnIterator == 3)
        {
            pathDirection = 0;
            Transform enemytypeObject3 = Instantiate(LichTransform, spawnPoint.position, spawnPoint.rotation);
            Lich lichEnemy = enemytypeObject3.GetComponent<Lich>();
            lichEnemy.pathDirection = 0;
        }
    }



    /// <summary>
    /// Author: Andreea-Camelia Patru
    /// </summary>
    public void UpdateDifficulty()
    {

        DefaultConstants.skeletonLife += 60f;
        DefaultConstants.lichLife += 80f;
        DefaultConstants.orcLife += 60f;
        DefaultConstants.enemySpeed += 4f;

        DefaultConstants.skeletonLife += 20f;
        DefaultConstants.lichLife += 30f;
        DefaultConstants.orcLife += 25f;
        DefaultConstants.enemySpeed += 2f;
    }
}
    #endregion functions

public static class DefaultConstants
{
    public static float timeBetweenWaves = 20f;
    public static float countdown = 10f;

    public static int waveNumber = 12;

    public static int waveNumber = 10;
  
    public static float range = 30f;

    public static float rotateSpeed = 10f;
    public static float bulletSpeed = 50f;

    public static float upgrade2Rotate = 12f;
    public static float upgrade2Speed = 65f;
    public static int upgrade2Value = 50;

    public static float upgrade3Rotate = 12f;
    public static float upgrade3Speed = 85f;
    public static int upgrade3Value = 70;

    public static float mageTowerLife = 100f;
    public static float watchTowerLife = 150f;
    public static float cannonTowerLife = 200f;

    public static int mageTowerValue = 100;
    public static int watchTowerValue = 150;
    public static int cannonTowerValue = 250;

    public static int skeletonValue = 15;
    public static int orcValue = 20;
    public static int lichValue = 25;

    public static float arrowPower = 60f;
    public static float missilePower = 45f;
    public static float upgrade2Power = 60f;
    public static float upgrade3Power = 75f;

    public static float characterMovementMultiplier = 7;
    public static float characterSpeed = 5f;
    public static float characterSensitivity = 4f;
    public static float characterWeaponPower = 100f;
    public static int characterValue = 300;

    public static float skeletonLife = 70f;
    public static float orcLife = 100f;
    public static float lichLife = 170f;


    public static float enemySpeed = 10f;
    public static int BaseDamage = 50;

    public static float specialEnemyLife = 200f;
    public static int specialEnemyValue = 50;
    public static float specialEnemyPower = 50f;
    public static float specialEnemySpeed = 15f;


    public static float mageTowerLife = 100f;
    public static float watchTowerLife = 150f;
    public static float cannonTowerLife = 200f;

    public static int mageTowerValue = 100;
    public static int watchTowerValue = 150;
    public static int cannonTowerValue = 200;

    public static float arrowPower = 25f;
    public static float missilePower = 50f;
    public static float weaponType3Power = 100f;

    public static float characterMovementMultiplier = 7;
    public static float characterSpeed = 5f;
    public static float characterSensitivity = 4f;
    public static float characterWeaponPower = 200f;

    public static float skeletonLife = 75f;
    public static float orcLife = 125f;
    public static float lichLife = 200f;

    public static float enemySpeed = 10f;

}
