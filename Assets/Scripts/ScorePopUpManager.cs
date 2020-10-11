using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopUpManager : MonoBehaviour
{

    public static ScorePopUpManager instance;

    public GameObject pop_up_parent;
    public GameObject prefab50;
    public GameObject prefab100;
    public GameObject prefab300;
    Transform[] lanes_obj;

    void Awake()
    {
        #region singleton
        if (instance)
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
        }
        #endregion
        lanes_obj = new Transform[4];
        lanes_obj[0] = GameObject.Find("Hitbar1").transform;
        lanes_obj[1] = GameObject.Find("Hitbar2").transform;
        lanes_obj[2] = GameObject.Find("Hitbar3").transform;
        lanes_obj[3] = GameObject.Find("Hitbar4").transform;
    }

    public void Spawn50(int lane)
    {
        Instantiate(prefab50, lanes_obj[lane].position, pop_up_parent.transform.rotation, pop_up_parent.transform);
    }

    public void Spawn100(int lane)
    {
        Instantiate(prefab100, lanes_obj[lane].position, pop_up_parent.transform.rotation, pop_up_parent.transform);
    }

    public void Spawn300(int lane)
    {
        Instantiate(prefab300, lanes_obj[lane].position, pop_up_parent.transform.rotation, pop_up_parent.transform);
    }

}
