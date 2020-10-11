using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class InputMaster : MonoBehaviour
{
    public static InputMaster instance;



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

    }




    private void Update()
    {
        ReadInputs();
    }




    void ReadInputs()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SongMaster.instance.HitKey(NoteKey.LEFTLEFT);
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    SongMaster.instance.HitKey(NoteKey.LEFT);
        //}
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    SongMaster.instance.HitKey(NoteKey.RIGHT);
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SongMaster.instance.HitKey(NoteKey.RIGHTRIGHT);
        //}


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SongMaster.instance.PauseToggle();
        //}




        if (Input.GetKeyDown(KeyCode.D))
        {
            SongMaster3D.instance.HitKey(NoteKey.LEFTLEFT);
            AudioMaster.instance.PlayHitSound();
            GameObject.FindGameObjectWithTag("Hitbar1").GetComponent<Animator>().SetTrigger("KeyDown");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SongMaster3D.instance.HitKey(NoteKey.LEFT);
            AudioMaster.instance.PlayHitSound();
            GameObject.FindGameObjectWithTag("Hitbar2").GetComponent<Animator>().SetTrigger("KeyDown");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SongMaster3D.instance.HitKey(NoteKey.RIGHT);
            AudioMaster.instance.PlayHitSound();
            GameObject.FindGameObjectWithTag("Hitbar3").GetComponent<Animator>().SetTrigger("KeyDown");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SongMaster3D.instance.HitKey(NoteKey.RIGHTRIGHT);
            AudioMaster.instance.PlayHitSound();
            GameObject.FindGameObjectWithTag("Hitbar4").GetComponent<Animator>().SetTrigger("KeyDown");
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SongMaster3D.instance.PauseToggle();
        }

    }
}
