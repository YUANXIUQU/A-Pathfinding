using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshGenerator : MonoBehaviour
{
    abstract public void GenerateMesh(int[,] map, float squareSize);
}
