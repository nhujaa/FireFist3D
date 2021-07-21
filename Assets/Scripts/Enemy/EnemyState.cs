using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    EnemySight enemySight;
    EnemyAttack enemyAttack;
    Stats stats;

    public enum currentStateEnum { idle = 0, run = 1, attack = 2, hit = 3, knockedDown = 4};

    public currentStateEnum currentState;
    
    public GameObject spriteObject;

    public GameObject healthUI;

    public bool tookDamage;
    public bool knockedDown;

    public float stunTime;
    public float knockedDownTime;

    Animator animator;
    AnimatorStateInfo currentStateInfo;    

    static int currentAnimState;
    static int idleState = Animator.StringToHash("Base Layer.Idle");                //reference animations with state names
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int blockState = Animator.StringToHash("Base Layer.Block");
    static int hitState = Animator.StringToHash("Base Layer.Hit");
    static int attackState = Animator.StringToHash("Base Layer.Attack");
    static int deathState = Animator.StringToHash("Base Layer.Death");

    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = spriteObject.GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (knockedDown == true && tookDamage == false)                                             //if enemy is knocked down set animator boolean knocked down to true and start knocked down coroutine
        {
            stats.displayUI = true;
            healthUI = GameObject.FindGameObjectWithTag("EnemyHealthUI");
            animator.SetBool("Knocked Down", true);
            StartCoroutine(KnockedDown());
        }
        else if(tookDamage == true)                                                                 //if enemy is taking damage set is hit to true and start took damage coroutine
        {
            stats.displayUI = true;
            healthUI = GameObject.FindGameObjectWithTag("EnemyHealthUI");
            animator.SetBool("Is Hit", true);
            StartCoroutine(TookDamage());
        } 
        else if (tookDamage == false &&                                                                 //if enemy is in attack distance, stop enemy movement and start attack
                enemySight.playerInSight == true &&
                enemySight.player.GetComponent<WarriorControllerScript>().knockedDown == false &&
                enemySight.targetDistance < enemyAttack.attackRange &&
                navMeshAgent.velocity.sqrMagnitude < enemyAttack.attackStartDelay)
        {
            stats.displayUI = false;
            healthUI = null;
            animator.SetBool("Attack", true);
            animator.SetBool("Run", false);
        }
        else if (knockedDown == false &&                                                                 //if player is in sight of enemy, move enemy towards player
                tookDamage == false &&
                enemySight.playerInSight == true)
        {
            stats.displayUI = false;
            healthUI = null; 
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
        }
        else if (tookDamage == false && enemySight.playerInSight == false)                              //if player is not inside enemy's field of view, enemy stays idle
        {
            stats.displayUI = false;
            healthUI = null;
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
        }

        if(currentAnimState == idleState)                                                               //set enum to enemy state
        {
            currentState = currentStateEnum.idle;
        }

        else if (currentAnimState == runState)
        {
            currentState = currentStateEnum.run;
        }

        else if (currentAnimState == attackState)
        {
            currentState = currentStateEnum.attack;
        }

        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);                                     //determine which animation is currently executing
        currentAnimState = currentStateInfo.fullPathHash;
    }

    IEnumerator TookDamage()
    {
        animator.Play("Hit");

        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Is Hit", false);
        tookDamage = false;
    }

    IEnumerator KnockedDown()
    {
        animator.Play("Death");

        yield return new WaitForSeconds(knockedDownTime);

        animator.SetBool("Knocked Down", false);
        knockedDown = false;
    }
}
