using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorControllerScript : MonoBehaviour
{

    public float walkMovementSpeed;             //player movement speed
    public float attackMovementSpeed;           //player movement speed when attacking
    public float xMin, xMax, zMin, zMax;        //max and min axis player can go
    public bool isBlocking;                     //check if player is blocking
    public float KnockedDownTime;               //time to stay knocked down
    public bool canMove;                        //to set player movement on and off
    public float stunTime;                      //time to stun when hit
    public bool tookDamage;                     //check if damage
    public bool knockedDown;                    //check if knocked down

    private float movementSpeed;
    bool facingRight;

    private Rigidbody rigidBody;
    
    Animator animator;

    AnimatorStateInfo currentStateInfo;

    public GameObject attack1Box, attack2Box;
    public Sprite attack1SpriteHitFrame, attack2SpriteHitFrame;

    SpriteRenderer currentSprite;

    static int currentState;
    static int idleState = Animator.StringToHash("Base Layer.Idle");                    //Get animations in the animator window
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int blockState = Animator.StringToHash("Base Layer.Block");
    static int hitState = Animator.StringToHash("Base Layer.Hit");
    static int attack1State = Animator.StringToHash("Base Layer.Attack1");
    static int attack2State = Animator.StringToHash("Base Layer.Attack2");
    static int deathState = Animator.StringToHash("Base Layer.Death");


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();      
        animator = GetComponent<Animator>();
        movementSpeed = walkMovementSpeed;
        facingRight = false;
        currentSprite = GetComponent<SpriteRenderer>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;

        if (currentState == idleState)              //check warrior current state
            Debug.Log("Idle State");               //used for bug fixing
        if (currentState == runState)
            Debug.Log("Run State");
        if (currentState == blockState)
            Debug.Log("Block State");
        if (currentState == hitState)
            Debug.Log("Hit State");
        if (currentState == attack1State)
            Debug.Log("Attack1 State");
        if (currentState == attack2State)
            Debug.Log("Attack2 State");
        if (currentState == deathState)
            Debug.Log("Death State");

        //Control Speed Based On Commands---------------------------------------------------------
        if (currentState == idleState || currentState == runState)
            movementSpeed = walkMovementSpeed;
        else
            movementSpeed = attackMovementSpeed;
        //Control Speed Based On Commands---------------------------------------------------------
    }

    void FixedUpdate()
    {
        //Movement--------------------------------------------------------------------------------
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidBody.velocity = movement * movementSpeed;
        rigidBody.position = new Vector3
                                        (
                                            Mathf.Clamp(rigidBody.position.x, xMin, xMax),
                                            transform.position.y,
                                            Mathf.Clamp(rigidBody.position.z, zMin, zMax)
                                        );
        
        if (moveHorizontal > 0 && facingRight && canMove == true)
            Flip();
        else if (moveHorizontal < 0 && !facingRight && canMove == true)
            Flip();

        animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);
        //Movement--------------------------------------------------------------------------------

        //Attack----------------------------------------------------------------------------------
        if (Input.GetMouseButton(0))
            animator.SetBool("Attack", true);
        else
            animator.SetBool("Attack", false);

        if (attack1SpriteHitFrame == currentSprite.sprite)
        {
            attack1Box.gameObject.SetActive(true);
        }
        else if (attack2SpriteHitFrame == currentSprite.sprite)
        {
            attack2Box.gameObject.SetActive(true);
        }
        else
        {
            attack1Box.gameObject.SetActive(false);
            attack2Box.gameObject.SetActive(false);
        }
        //Attack----------------------------------------------------------------------------------

        //Block-----------------------------------------------------------------------------------
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            animator.SetBool("Block", false);
            isBlocking = false;
        }
        //Block-----------------------------------------------------------------------------------

        //Hit-------------------------------------------------------------------------------------
        if (tookDamage == true && knockedDown == false)
            StartCoroutine(TookDamage());
        //Hit-------------------------------------------------------------------------------------

        //Knocked Down----------------------------------------------------------------------------
        if (knockedDown == true)
        {
            StartCoroutine(KnockedDown());
        }
        //Knocked Down----------------------------------------------------------------------------
    }

    void Flip()     //flips scale of object in the x direction
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }

    IEnumerator TookDamage()        //give a stun effect when being hit
    {
        animator.Play("Hit");
        canMove = false;

        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Is Hit", false);
        canMove = true;
        tookDamage = false;
    }

    IEnumerator KnockedDown()       //stay on ground for a few seconds after being knockled down
    {
        animator.Play("Death");
        animator.SetBool("Knocked Down", true);
        canMove = false;

        yield return new WaitForSeconds(KnockedDownTime);

        animator.SetBool("Knocked Down", false);
        canMove = true;
        knockedDown = false;
    }
}
