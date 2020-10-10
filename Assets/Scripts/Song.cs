using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteKey
{
    LEFTLEFT = 0,
    LEFT = 1,
    RIGHT = 2,
    RIGHTRIGHT = 3,
}


[System.Serializable]
public struct Note
{
    public float timeStamp;
    public NoteKey key;

    public Note(float timeStamp, NoteKey key)
    {
        this.timeStamp = timeStamp;
        this.key = key;
    }
}

[System.Serializable]
public class Song : ScriptableObject
{
    public AudioClip audio;


    public float length;

    [SerializeField]
    public Note[] notes;


}
