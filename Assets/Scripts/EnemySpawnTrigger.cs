using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab;

    public bool lastEnemy = false;

    void Awake()
    {
        enemyPrefab.SetActive (false);      //turns off all enemy prefabs on enemy spawner
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemySpawnTrigger")        //if enemy spawn trigger collides with enemy spawner, turn on enemy prefab
        {
            enemyPrefab.SetActive(true);
            if (lastEnemy == true)              //if last enemy(boss), turn on enemy to win
            {
                Stats enemyStats = enemyPrefab.GetComponent<Stats>();
                enemyStats.enemyToWin = true;
            }
        }
    }
}
