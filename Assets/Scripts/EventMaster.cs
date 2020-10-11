using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EventMaster : MonoBehaviour
{
    // 1
    public AudioMaster aM;
    // 2
    public GameObject pM;
    ParticleSystem parSys;

    public float time_between_events;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        parSys = pM.GetComponent<ParticleSystem>();
        coroutine = StartEvents(time_between_events);
        StartCoroutine(coroutine);
    }

    IEnumerator StartEvents(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            int randNumber = Random.Range(1, 3);
            Debug.Log(randNumber);
            if (randNumber == 1)
            {
                Debug.Log("MUTE EVENET POPPED");

                aM.ToggleMute();
                yield return new WaitForSeconds(5);
                aM.ToggleMute();
                Debug.Log("MUTE EVENET STOPPED");

            }
            if (randNumber == 2)
            {
                Debug.Log("SMOKE EVENET POPPED");
                parSys.Play();
                yield return new WaitForSeconds(5);
                parSys.Stop();
                Debug.Log("SMOKE EVENET STOPPED");
            }
        }
    }
}
