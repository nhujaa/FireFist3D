using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyArray;

    public static GameManager instance = null;

    public float resetTime = 2.0f;
    public float timeSlowDown = 0.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");        //store all enemies spawed in this array

        if(enemyArray.Length == 0)      //if no enemies spawned, camera starts following warrior
        {
            CameraController.isFollowing = true;
        }
    }

    public void GameOver()      //invokes game reset
    {
        Invoke("Reset", resetTime);
    }

    void Reset()        //reset the game
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
