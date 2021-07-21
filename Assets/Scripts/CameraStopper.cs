using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour                          //to stop camera at fixed locations
{
    public GameObject otherObject;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraMidPoint")                          
        {
            otherObject = other.transform.parent.gameObject;        //if gameObject with tag CameraMidPoint collides with this gameObject
            CameraController.isFollowing = false;                   //turn camera following warrior off
            gameObject.SetActive(false);                            //sets gameObject off
        }
        else return;
        
    }
}
