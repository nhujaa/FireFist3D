using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float enemySpeed;
    public float enemyCurrentSpeed;
    
    bool facingRight;

    public GameObject spriteObject;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    EnemySight enemySight;
    EnemyState enemyState;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {        
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();     //get enemy's available path
        enemySight = GetComponent<EnemySight>();
        enemyState = GetComponent<EnemyState>();
        animator = spriteObject.GetComponent<Animator>();

        navMeshAgent.speed = enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState.currentState == EnemyState.currentStateEnum.run)     //if enemy state is run, make the enemy move
            Walk();
        else if (enemyState.currentState == EnemyState.currentStateEnum.idle)       //if enemy state is idle, make enemy not move
            Stop();
    }

    void Walk()
    {
        if (enemySight.playerOnRight == true && !facingRight)           //make enemy face towards player when player in enemy sight
            Flip();
        else if (enemySight.playerOnRight == false && facingRight)
            Flip();

        navMeshAgent.speed = enemySpeed;
        enemyCurrentSpeed = navMeshAgent.velocity.sqrMagnitude;
        navMeshAgent.SetDestination(enemySight.target.transform.position);      //set enemy destination to oject on enemy sight
        navMeshAgent.updateRotation = false;                                    //turn off navmesh rotation
    }
    
    void Stop()
    {
        navMeshAgent.ResetPath();                                              //resets enemy destination to its current coordinates
    }

    void Flip()         //make enemy flip in x direction
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }
}
