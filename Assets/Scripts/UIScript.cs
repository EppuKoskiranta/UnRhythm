using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class UIScript: MonoBehaviour
{
    public static UIScript instance;



    //AudioSource audioSource;
    public Text scoreText;
    public Text comboText;
    public Text percentageText;

    public static bool GameHalt = false;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject gameUI;

    public static bool started = false;

    public Button resumeButton, mainmenuButton;

    private void Awake()
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
    }

    private void Start()
    {
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(false);
    }


    private void Update()
    {
        OnPressAnyKeyToStart();
    }



    public void OnPressAnyKeyToStart()
    {
        if (Input.anyKeyDown && !started)
        {
            Play();
        }
    }


    public void Play()
    {
        gameUI.SetActive(true);
        started = true;
        mainMenuUI.SetActive(false);
        SongMaster.instance.Play();
    }

    public void SetScore(long score)
    {
        scoreText.text = score.ToString();
    }


    public void SetCombo(int combo)
    {
        comboText.text = combo.ToString() + "x";
    }


    public void SetPercentage(float value)
    {
        //convert to %
        float percent = value * 100;
        percentageText.text = percent.ToString("F0") + "%";
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
    }

    public void UnPause()
    {
        pauseMenuUI.SetActive(false);
    }
}
