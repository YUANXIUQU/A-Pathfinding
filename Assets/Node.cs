using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node
{
    public float costSoFar; // cost from the starting point (G)
    public float fScore; // cost from start to end
    public bool isObstacle; 
    public Node parent;
    public Vector3 position;

    public Node(Vector3 pos)
    {
        fScore = 0.0f;
        costSoFar = 0.0f;
        isObstacle = false;
        parent = null;
        position = pos;
    }

    public void MarkAsObstacle()
    {
        isObstacle = true;
    }

    public override bool Equals(object obj)
    {
        return obj is Node node && position.Equals(node.position);
                 // turn obj to Node type
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(position);
    }
}
