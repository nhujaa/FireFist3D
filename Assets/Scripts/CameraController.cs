using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour                               //to move camera
{
    public static bool isFollowing;
    public float cameraSpeed;

    Vector3 playerXPos;

    GameObject playerTarget;

    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");          //coordinates of player object
    }

    // Update is called once per frame
    void Update()
    {

        playerXPos = new Vector3(playerTarget.transform.position.x-10.6f, 0.0f, 0.0f);      //get just x coordinates of player object
                                                                                            // -10.6 for the offset of camera and player from origin

        if (isFollowing == true)
        {
            transform.position = Vector3.Lerp(transform.position, playerXPos, cameraSpeed);     //smooths out camera movement from current position to player position in x direction

        
        }
    }
}
