using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public GameObject target;
    float speed = 200f;
    Vector3 previousMousePosition;
    Vector3 mouseDelta;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    void Drag() {
        if (Input.GetMouseButton(1) && !CubeState.autoRotating){
            //while the mouse is held down the cube can be moved around its central axis to provide visual feedback
            mouseDelta = Input.mousePosition - previousMousePosition;

            mouseDelta *= 0.1f;   // reduction of rotation speed (0.1f = one tenth as fast)

            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;       // sets new rotation
        }
        else {
            // if mouse button not pushed down, then move to target position

            // move to target position
            if (transform.rotation != target.transform.rotation){
                float step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }
        previousMousePosition = Input.mousePosition;
    }

    void Swipe() {
        if (Input.GetMouseButtonDown(1) && !CubeState.autoRotating){
            // get the 2D position of the first mouse click
            firstPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1) && !CubeState.autoRotating){
            // get the 2D position of thesecond mouse click
            secondPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

            //create vector 2 from the two mouse presses
            currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2D vector
            currentSwipe.Normalize();

            // rotates in correct direction depending on the swipe direction
            if (LeftSwipe(currentSwipe)){
                target.transform.Rotate(0, 90, 0, Space.World);
            } 
            else if (RightSwipe(currentSwipe)){
                target.transform.Rotate(0, -90, 0, Space.World);
            }  
            else if (UpLeftSwipe(currentSwipe)){
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(currentSwipe)){
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(currentSwipe)){
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(currentSwipe)){
                target.transform.Rotate(-90, 0, 0, Space.World);
            }

        }
    }



    // checks if swipe is to the left
    bool LeftSwipe(Vector2 swipe){
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    // checks if swipe is to the right
    bool RightSwipe(Vector2 swipe){
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool UpLeftSwipe(Vector2 swipe){
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe){
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe){
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe){
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }
}
