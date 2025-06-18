using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    private Transform StartPos,endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }

    public List<Node> pathArray;
    GameObject objStartCube, objEndCube;
    private float elaspedTime = 0.0f;
    //Interval TIme between pathfinding
    public float intervalTime = 1.0f;

    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");
        pathArray = new List<Node>();
        FindPath();
    }

    void Update()
    {
        elaspedTime += Time.deltaTime;
        if(elaspedTime >= intervalTime)
        {
            elaspedTime = 0.0f;
            FindPath();
        }
    }
    void FindPath()
    {
        StartPos = objStartCube.transform;
        endPos = objEndCube.transform;

        var (startColumn, startRow) = GridManager.instance.GetGridCoordinates(StartPos.position);
        var (goalColumn, goalRow) = GridManager.instance.GetGridCoordinates(endPos.position);
        startNode = new Node(GridManager.instance.GetGridCellCenter(startColumn, startRow));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(goalColumn, goalRow));
        pathArray = new AStar().FindPath(startNode, goalNode);
    }

    private void OnDrawGizmos()
    {
        if(pathArray == null)
        {
            return;
        }
        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            };
        }
    }
}
