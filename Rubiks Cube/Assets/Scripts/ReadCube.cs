﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{


    // variables
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();

    private int layerMask = 1 << 8;     //this layerMask is for the faces of the cube only

    CubeState cubeState;
    CubeMap cubeMap;

    public GameObject emptyGO;







    // methods

    // start method
    void Start() {
        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        ReadState();
        CubeState.started = true;

    }

    // Update is called once per frame
    void Update() {

    }

    // reading all faces method
    public void ReadState() {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        // set a state of each position in the list f sides so that we know what color is in what position
        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);

        // update the ap with the found positions
        cubeMap.Set();
    }






    // setting ray transform method
    void SetRayTransforms() {
        // Populate the ray lists with raycasts eminating from the transform, angled towards the cube
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));
    }

    // building rays method
    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction){
        // the ray count is used to name the race so we can be sure they're in the right order
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        // This creates nine rays in the shape of a cube with the ray zero on the top left and ray eight on the bottom right
        //  |0|1|2|
        //  |3|4|5|
        //  |6|7|8|

        for (int y = 1; y > -2; y -= 1){
            for (int x = -1; x < 2; x += 1){
                Vector3 startPos = new Vector3 (rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount += 1;

            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    // reading one face method
    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform){
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts){
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            // Does the ray intersect any objects in the layer mask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask)){
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                // Debug.Log(hit.collider.gameObject.name);
            }
            else {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }


        return facesHit;
    }



}
