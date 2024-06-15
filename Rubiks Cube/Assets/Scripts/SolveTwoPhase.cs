using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    
    public ReadCube readCube;
    public CubeState cubeState;
    bool DoOnce = true;
    public PivotRotation pr;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started && DoOnce){
            DoOnce = false;
            Solver();
        }
    }


    public void Solver() {
        readCube.ReadState();

        // get the state of the cube as a string
        string moveString = cubeState.GetStateString();
        // Debug.Log(moveString);

        // solve the cube

        string info = "";


        // First time, build the tables
        // string solution = SearchRunTime.solution(moveString, out info, buildTables: true);


        // Every other time, read the pregenerated tables
        string solution = Search.solution(moveString, out info); 

        // convert the colved moves from a string to a list
        List<string> solutionList = StringToList(solution);

        pr.automaticMovement = true;

        // Automate the list
        Automate.moveList = solutionList;
    }


    List<string> StringToList(string solution){
        List<string> solutionList = new List<string>(solution.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }














}
