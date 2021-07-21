using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    public bool playerInSight;
    public bool playerOnRight;
    public GameObject target;
    public float targetDistance;


    public GameObject player;

    Vector3 playerRelativePosition;

    GameObject frontTarget;
    GameObject backTarget;

    float frontTargetDistance;
    float backTargetDistance;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        frontTarget = GameObject.Find("EnemyFrontTarget");
        backTarget = GameObject.Find("EnemyBackTarget");
    }

    // Update is called once per frame
    void Update()
    {
        playerRelativePosition = player.transform.position - gameObject.transform.position;         //checked if player is on right or left
        if (playerRelativePosition.x < 0)
            playerOnRight = false;
        else if (playerRelativePosition.x > 0)
            playerOnRight = true;

        frontTargetDistance = Vector3.Distance(frontTarget.transform.position, gameObject.transform.position);      //distance from front target area of player
        backTargetDistance = Vector3.Distance(backTarget.transform.position, gameObject.transform.position);        //distance from back target area of player

        if (frontTargetDistance < backTargetDistance)                                                         //set target to front if it is near
            target = frontTarget;
        else if (frontTargetDistance > backTargetDistance)                                                    //set target to back if it is near
            target = backTarget;

        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);            //distance from target
    }

    void OnTriggerStay(Collider other)                                      //if player is inside collider, player in sight else not in sight
    {
        if (other.gameObject == player)
            playerInSight = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }
}
