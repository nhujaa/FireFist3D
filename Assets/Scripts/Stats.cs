using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float startingHealth;        //health of warrior or bandit when game starts
    public float health;                //health which gets updated as the object receives damage

    public bool displayUI;

    public Slider healthSlider;

    public GameObject healthUI;

    public bool enemyToWin;         //final boss

    void Awake()
    {
        health = startingHealth;
    }

    void Update()
    {
        if(gameObject.tag == "Player")
        {
            healthUI = GameObject.FindGameObjectWithTag("PlayerHealthUI");
            healthSlider = healthUI.gameObject.transform.GetChild(0).GetComponent<Slider>();
            if (healthSlider.maxValue == 0)
                healthSlider.maxValue = startingHealth;            //set slider's max value to starting health
            healthSlider.value = health;            //change value of slider to set health
        }

        if (gameObject.tag == "Enemy" && displayUI == true)     //health of enemy current hit is being connected on
        {
            healthUI = GameObject.FindGameObjectWithTag("EnemyHealthUI");
            healthSlider = healthUI.gameObject.transform.GetChild(0).GetComponent<Slider>();
            if (healthSlider.maxValue == 0)
                healthSlider.maxValue = startingHealth;
            healthSlider.value = health;
        }
        else if (gameObject.tag == "enemy")     //if there is no enemy there is no health bar
        {
            healthUI = null;
            healthSlider = null;
        }

        if (health <= 0)            //destroy enemy gameObject after their health gets zero
        {
            Destroy(gameObject.transform.parent.gameObject);
            if (enemyToWin == true)             //if final boss dies, finish the game
            {
                Time.timeScale = 0.5f;              //after killing boss, slow time down for cool effect
                GameManager.instance.GameOver();
            }
        }            

    }

}
