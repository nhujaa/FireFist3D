using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTime : MonoBehaviour                    //for showing controls momentarily when game begins
{

    public float time = 1; //Seconds to read the text

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);                                        //destroy gameObject in this case the controls in UI
    }
}
