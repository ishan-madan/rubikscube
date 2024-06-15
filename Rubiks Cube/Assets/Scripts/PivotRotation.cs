using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    
    List<GameObject> activeSide;
    Vector3 localForward;
    Vector3 mouseRef;
    bool dragging = false;
    ReadCube readCube;
    CubeState cubeState;
    float sensitivity = 0.4f;
    Vector3 rotation;
    public float speed = 300f;
    bool autoRotating = false;
    Quaternion targetQuaternion;
    public bool automaticMovement = false;



    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    
    // Late update is called once per frame at the end
    void LateUpdate()
    {
        if (dragging && !autoRotating) {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0)){
                dragging = false;
                RotateToRightAngle();
            }
        }

        if (autoRotating){
            AutoRotate();
        }
    }


    void SpinSide(List<GameObject> side) {
        
        Vector2 PivotScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, activeSide[4].transform.position);
        Vector2 PivotOffset =new Vector2(Input.mousePosition.x - PivotScreenPoint.x, Input.mousePosition.y - PivotScreenPoint.y);
        PivotOffset.Normalize();
        float Xdir = Vector2.Dot(Vector2.right, PivotOffset);
        float Ydir = -Vector2.Dot(Vector2.up, PivotOffset);
        
        
        // reset rotation
        rotation = Vector3.zero;
        
        // current mouse position minus the last mouse position
        Vector3 mouseOffset = Input.mousePosition - mouseRef;

        if (side == cubeState.front){
            rotation.x = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir) * -sensitivity;
        }
        if (side == cubeState.back){
            rotation.x = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir ) * sensitivity;
        }
        if (side == cubeState.up){
            rotation.y = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir ) * -sensitivity;
        }
        if (side == cubeState.down){
            rotation.y = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir ) * sensitivity;
        }
        if (side == cubeState.left){
            rotation.z = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir ) * -sensitivity;
        }
        if (side == cubeState.right){
            rotation.z = (mouseOffset.x * Xdir  + mouseOffset.y * Ydir ) * -sensitivity;
        }
        
        // rotate
        transform.Rotate(rotation, Space.Self);

        // store mouse position
        mouseRef = Input.mousePosition;



    }


    public void Rotate(List<GameObject> side){
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;

        // create vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;


    }



    







    public void RotateToRightAngle() {
        Vector3 vec = transform.localEulerAngles;
        
        // round vec to the nearnest 90 degrees
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }

    public void StartAutoRotate(List<GameObject> side, float angle){
        cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotating = true;
        
    }





    void AutoRotate() {
        float step = speed * Time.deltaTime;
        if (automaticMovement){
            step *= 2.2f;
        }
        dragging = false;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // if within 1 degree, set angle to target angle and end rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) < 1){
            transform.localRotation = targetQuaternion;
            
            // unparent the little cubes
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            CubeState.autoRotating = false;
            autoRotating = false;
            dragging = false;
            automaticMovement = false;
        }
    }

}
