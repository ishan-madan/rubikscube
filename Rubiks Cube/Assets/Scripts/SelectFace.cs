using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{

    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 8;




    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.autoRotating){
            // read current state of the cube
            readCube.ReadState();

            //fire raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask)){
                GameObject face = hit.collider.gameObject;
                
                // make a list of all the sides (lists of face GameObjects)
                List<List<GameObject>> cubeSides = new List<List<GameObject>> {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                // If the face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides){
                    if (cubeSide.Contains(face)){
                        // Pick it up
                        cubeState.PickUp(cubeSide);
                        // start rotation
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }

            }






        }
    }
}
