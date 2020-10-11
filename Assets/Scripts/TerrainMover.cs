using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TerrainMover : MonoBehaviour
{
    MeshGenerator meshgen;
    GameObject meshmountain;

    public AnimationCurve anim_curve;

    float time_elapsed = 0;

    private void Awake()
    {
        meshgen = GameObject.Find("MeshGenerator").GetComponent<MeshGenerator>();
        meshmountain = GameObject.Find("MeshMountains");
    }

    private void Update()
    {
        meshgen.AddOffset(new Vector2(0, Time.deltaTime * 0.2f));
        time_elapsed += Time.deltaTime;
        if (time_elapsed >= 50.0f)
        {
            time_elapsed -= 50.0f;
        }
        meshmountain.GetComponent<Renderer>().sharedMaterial.SetFloat("_WireframeVal", anim_curve.Evaluate(time_elapsed / 50));

    }




}
