﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMaster : MonoBehaviour
{
    public static AudioMaster instance;

    //References
    public AudioSource audioSource;

    //Song values
    [Range(0, 1)]
    public float masterVolume = 1f;
    [Range(0, 1)]
    public float songVolume = 1f;

    public float playbackSpeed = 1.0f;


    private void Awake()
    {
        #region singleton
        //singleton
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

        audioSource = GetComponent<AudioSource>();


    }

    public void SetSong(Song song)
    {
        audioSource.clip = song.audio;
    }

    public void PauseSong()
    {
        if (audioSource)
        {
            audioSource.Pause();
        }
    }

    public void UnPauseSong()
    {
        if (audioSource)
        {
            audioSource.UnPause();
        }
    }


    public void StartSong()
    {
        if (!audioSource)
        {
            Debug.LogError("No audio data!");
            return;
        }
        audioSource.Play();
    }

    private void SendVolumeChangeToAudioSource()
    {
        audioSource.volume = masterVolume * songVolume;
    }




}
