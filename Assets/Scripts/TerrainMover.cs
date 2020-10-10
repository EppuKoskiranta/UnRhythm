using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMover : MonoBehaviour
{
    MeshGenerator meshgen;

    private void Awake()
    {
        meshgen = GameObject.Find("MeshGenerator").GetComponent<MeshGenerator>();
    }

    private void Update()
    {
        meshgen.AddOffset(new Vector2(0, Time.deltaTime * 0.2f));
    }


}
