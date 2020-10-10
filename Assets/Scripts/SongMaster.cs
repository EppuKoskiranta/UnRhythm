﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongMaster : MonoBehaviour
{
    public static SongMaster instance;


    public struct NoteSpawn
    {
        public float spawnTime;
        public bool instantiated;

        public NoteSpawn(float spawnTime, bool instantiated = false)
        {
            this.spawnTime = spawnTime;
            this.instantiated = instantiated;
        }
    }


    [Header("Visual Elements")]
    public GameObject board;
    public GameObject noteLeftLeftPrefab;
    public GameObject noteLeftPrefab;
    public GameObject noteRightPrefab;
    public GameObject noteRightRightPrefab;
    public GameObject hitbar;
    public Transform notesParent;
    private List<GameObject> activeNotes;
    private float distanceToHitbar;

    [Space(20)]

    public Song song;


    public NoteSpawn[] notesSpawns;

    public long score;
    private float scoreMultiplier;
    public int combo;
    public int comboIncrement = 50;
    public float comboMultiplier = 1.5f;

    public Note nextNote;
    public int noteCounter;

    public int perfectScore = 300;
    public int normalScore = 100;
    public int badScore = 50;
    public float perfectHit = 0.05f;
    public float normalHit = 0.1f;
    public float badHit = 0.2f;
    
    public float activeNoteThreshold = 0.2f; //active area for note when reading input
    public float progress;
    public float percentProgress;
    public bool paused = true;
    public float playbackSpeed = 1.0f;
    public float noteApproachTime = 5f;


    public void Awake()
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

        activeNotes = new List<GameObject>();


        distanceToHitbar = Mathf.Abs(hitbar.transform.localPosition.y - 600);

        //disable canvas at start
        board.SetActive(false);
    }


    private void Start()
    {
        LoadSongWithName("Mariya Takeuchi Plastic Love");
    }

    private void Update()
    {
        if (song)
        {
            if (!paused)
            {
                if (progress < song.length)
                {
                    SpawnNotes();

                    MoveNotes();

                    progress += playbackSpeed * Time.deltaTime;
                    percentProgress = progress / song.length;

                    UIScript.instance.SetPercentage(percentProgress);
                    if (progress > nextNote.timeStamp + activeNoteThreshold && noteCounter < song.notes.Length)
                    {
                        Debug.Log("Missed note");
                        combo = 0;
                        UIScript.instance.SetCombo(0);
                        NextNote();
                    }
                }
            }
        }
        
    }


    public void Play()
    {
        if (song)
        {
            if (progress != 0 || !paused)
            {
                Debug.LogError("Song already started!");
            }
            else
            {
                board.SetActive(true);
                UnPause();
                nextNote = song.notes[0];
                noteCounter = 0;
                score = 0;
                combo = 0;
                UIScript.instance.SetCombo(0);
                UIScript.instance.SetScore(0);
                UIScript.instance.SetPercentage(0);
                AudioMaster.instance.StartSong();
            }
        }
        else
        {
            Debug.LogError("No song selected");
        }
    }


    public void PauseToggle()
    {
        if (paused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!paused)
        {
            UIScript.instance.Pause();
            paused = true;
            AudioMaster.instance.PauseSong();
        }
        else
        {
            Debug.Log("Already paused");
        }
    }

    private void UnPause()
    {
        if (paused)
        {
            UIScript.instance.UnPause();
            paused = false;
            AudioMaster.instance.UnPauseSong();
        }
        else
        {
            Debug.Log("Already unpaused");
        }
    }


    public void Restart()
    {
        AudioMaster.instance.PauseSong();
        AudioMaster.instance.audioSource.time = 0;
        progress = 0;
        percentProgress = 0;
        combo = 0;
        score = 0;
        AudioMaster.instance.UnPauseSong();
    }


    public void LoadSongWithName(string songName)
    {
        song = Resources.Load<Song>("Prefabs/Songs/" + songName);
        if (song)
        {
            progress = 0;
            percentProgress = 0;
            AudioMaster.instance.SetSong(song);
            SetupNotes();
        }
        else
        {
            Debug.LogError("SongMaster::Failed to load song");
        }
    }



    public void HitKey(NoteKey key)
    {
        if (nextNote.key == key)
        {
            if (progress >= nextNote.timeStamp - activeNoteThreshold && progress <= nextNote.timeStamp + activeNoteThreshold) //Check if hit was during nextnotes activethreshold
            {
                combo++;

                if (combo >= 50)
                {
                    scoreMultiplier = Mathf.FloorToInt(combo / comboIncrement) * comboMultiplier;
                }
                else
                {
                    scoreMultiplier = 1;
                }


                float difference = Mathf.Abs(progress - nextNote.timeStamp);
                NextNote();
                if (difference <= perfectHit)
                {
                    score += Mathf.FloorToInt(scoreMultiplier * perfectScore);
                    Debug.Log("Perfect hit");
                }
                else if (difference <= normalHit)
                {
                    score += Mathf.FloorToInt(scoreMultiplier * normalScore);
                    Debug.Log("Normal hit");
                }
                else if (difference <= badHit)
                {
                    score += Mathf.FloorToInt(scoreMultiplier * badScore);
                    Debug.Log("Bad hit");
                }


                UIScript.instance.SetCombo(combo);
                UIScript.instance.SetScore(score);
            }
        }
    }


    private void MoveNotes()
    {
        float speed = distanceToHitbar / noteApproachTime;
        if (activeNotes.Count > 0)
        {
            for (int i = 0; i < activeNotes.Count; ++i)
            {
                activeNotes[i].transform.localPosition -= new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
    }

    private void NextNote()
    {

        noteCounter++;
        if (noteCounter < song.notes.Length)
            nextNote = song.notes[noteCounter];
    }
    

    private void SetupNotes()
    {
        notesSpawns = new NoteSpawn[song.notes.Length];
        for (int i = 0; i < song.notes.Length; ++i)
        {
            notesSpawns[i] = new NoteSpawn(song.notes[i].timeStamp - noteApproachTime);
        }
    }


    private void SpawnNotes()
    {
        for (int i = 0; i < 10; ++i)
        {
            if (i < song.notes.Length)
            {
                if (!notesSpawns[i].instantiated && progress >= notesSpawns[i].spawnTime)
                {
                    SpawnNote(song.notes[i].key, i);
                }
            }
        }
    }

    private void SpawnNote(NoteKey key, int index)
    {
        GameObject note;
        switch (key)
        {
            case NoteKey.LEFTLEFT:
                note = Instantiate(noteLeftLeftPrefab, notesParent);
                activeNotes.Add(note);
                notesSpawns[index].instantiated = true;
                break;
            case NoteKey.LEFT:
                note = Instantiate(noteLeftPrefab, notesParent);
                activeNotes.Add(note);
                notesSpawns[index].instantiated = true;
                break;
            case NoteKey.RIGHT:
                note = Instantiate(noteRightPrefab, notesParent);
                activeNotes.Add(note);
                notesSpawns[index].instantiated = true;
                break;
            case NoteKey.RIGHTRIGHT:
                note = Instantiate(noteRightRightPrefab, notesParent);
                activeNotes.Add(note);
                notesSpawns[index].instantiated = true;
                break;
        }
    }
}