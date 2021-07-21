using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour                                            //for detecting collision between hitbox and attackbox
{
    public bool knockDownAttack;                                                        //attacks that trigger knocked down state
    public float attackStrength;                                                        //strength of attack

    GameObject otherObject;

    Stats otherStats;

    EnemyState enemyState;
    WarriorControllerScript playerState;

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "PlayerAttackBox" && other.tag == "EnemyHitBox")          //if player attackbox collides with enemy hitbox
            EnemyTakeDamage(other.gameObject);                                          //enemy takes damage
        else if (gameObject.tag == "EnemyAttackBox" && other.tag == "PlayerHitBox")     //if enemy attackbox collides with player hitbox
            PlayerTakeDamage(other.gameObject);                                         //player takes damage
        else return;
    }

    void EnemyTakeDamage(GameObject other)                                              //here other = enemy
    {
        otherObject = other.transform.parent.gameObject;
        enemyState = otherObject.GetComponent<EnemyState>();                            //get enemy state
        otherStats = otherObject.GetComponent<Stats>();                                 //get enemy stats

        otherStats.health = otherStats.health - attackStrength;                         //decrease health by attack strength

        if (knockDownAttack == true)
            enemyState.knockedDown = true;                                             //if attack is knockdown, get knocked down
        else
            enemyState.tookDamage = true;                                              //if attack is not knockdown, just take damage

        Debug.Log("Enemy Takes Damage");                                               //enemy take damage message on console for error checking
    }

    void PlayerTakeDamage(GameObject other)                                             //here other = warrior
    {
        otherObject = other.transform.parent.gameObject;
        playerState = otherObject.GetComponent<WarriorControllerScript>();              //get warrior state
        otherStats = otherObject.GetComponent<Stats>();                                 //get warrior stats

        otherStats.health = otherStats.health - attackStrength;                         //decrease health by attack strength

        if (knockDownAttack == true)
            playerState.knockedDown = true;                                             //if attack is knockdown, get knocked down
        else
            playerState.tookDamage = true;                                              //if attack is not knockdown, just take damage

        Debug.Log("Player Takes Damage");                                               //player take damage message on console for error checking
    }
}
