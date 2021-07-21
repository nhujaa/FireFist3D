using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange;
    public float attackStartDelay;

    public GameObject spriteObject;

    public GameObject attackBox;
    public Sprite attackSpriteHitFrame;
    public Sprite currentSprite;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    EnemySight enemySight;
    EnemyWalk enemyWalk;
    EnemyState enemyState;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();
        enemyWalk = GetComponent<EnemyWalk>();
        enemyState = GetComponent<EnemyState>();
        animator = spriteObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSprite = spriteObject.GetComponent<SpriteRenderer>().sprite;

        if (enemyState.currentState == EnemyState.currentStateEnum.attack)      //if enemy is in attack state initiate attack
            Attack();
    }

    void Attack()
    {
        navMeshAgent.ResetPath();                               //stop movement while attacking

        if (attackSpriteHitFrame == currentSprite)              //turn attack box on only when enemy attack animation is in set frame
            attackBox.gameObject.SetActive(true);
        else
            attackBox.gameObject.SetActive(false);
    }
}
