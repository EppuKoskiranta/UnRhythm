using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitialDirection : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody rg;

    void Start()
    {
        int rand = Random.Range(1, 3);
        velocity = new Vector3(Random.Range(15, 20), Random.Range(14, 20), Random.Range(15,19));

        if (rand == 1)
        {
            velocity.x *= -1f;
        }

        rg = gameObject.GetComponent<Rigidbody>();
        rg.AddForce(velocity,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
