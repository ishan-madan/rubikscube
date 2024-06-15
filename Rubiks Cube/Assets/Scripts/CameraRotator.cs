using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    Quaternion defaultAngle;        
    Quaternion rotatedAngle;
    float speed = 500;
    bool back = false;
    Automate automate;

    void Start() {
        automate = FindObjectOfType<Automate>();        /* get automate script */
        defaultAngle.eulerAngles = new Vector3 (
                transform.localEulerAngles.x, 
                transform.localEulerAngles.y,
                transform.localEulerAngles.z);          /* grab coordinates for default angle of sight so we can always return back to this */
        
        rotatedAngle.eulerAngles = new Vector3 (-40, 225, 0);       /* set rotated angle (it can be anything you want, just experiment) */
    }

    void Update()
    {
        float step = speed * Time.deltaTime;                    /* set step speed */

        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown("c")){          /* if button pushed down, sets boolean "back" to true */
            back = true;
        }
        if (Input.GetMouseButtonUp(2) || Input.GetKeyUp("c")){              /* if button pushed down, sets boolean "back" to false */
            back = false;
        }

        if (back) {                         /* if back is true, then rotate towards (smoothly) the rotated angle. otherwise, rotate smoothly back */
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotatedAngle, step);
        }
        else if (!back) {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultAngle, step);
        }

        /* sets shuffling to true of false based on if it is at its original location so that moves cant be made it isn't at the original location 
        (I forget if this was part of the mini-series or if i added it myself, but shuffling prevents moves from being made) */
        if (transform.rotation != defaultAngle){
            automate.shuffling = true;
        }
        else if (transform.rotation != defaultAngle && Automate.moveList.Count <= 0) {
            automate.shuffling = false;
        }
    }
}
