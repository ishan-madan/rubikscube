using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{


    CubeState cubeState;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set() {
        cubeState = FindObjectOfType<CubeState>();
        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
    }


    void UpdateMap(List<GameObject> face, Transform side){
        int i = 0;
        foreach (Transform map in side){
            if (face[i].name[0] == 'F'){
                map.GetComponent<Image>().color = new Color(1, 0, 0, 1); // red
            }
            if (face[i].name[0] == 'B'){
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1); // orange
            }
            if (face[i].name[0] == 'L'){
                map.GetComponent<Image>().color = new Color(0, 1, 0, 1); // green
            }
            if (face[i].name[0] == 'R'){
                map.GetComponent<Image>().color = new Color(0, 0, 1, 1); // blue
            }
            if (face[i].name[0] == 'U'){
                map.GetComponent<Image>().color = new Color(1, 1, 1, 1); // white
            }
            if (face[i].name[0] == 'D'){
                map.GetComponent<Image>().color = new Color(1, 1, 0, 1); // yellow
            }

            i += 1;
        }
    }












}
