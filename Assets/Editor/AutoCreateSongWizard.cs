using UnityEditor;
using UnityEngine;

public class AutoCreateSongWizard : ScriptableWizard
{
    public SongDifficulty difficulty = SongDifficulty.NORMAL;
    public AudioClip audioClip;


    [MenuItem("Song Creator/Generate a song")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<AutoCreateSongWizard>("Generate a song", "Create");
    }

    private void OnWizardCreate()
    {
        if (audioClip)
        {
            //Create a generator
            SongGenerator generator = new SongGenerator(difficulty, audioClip);

            generator.GenerateNotes();
        }
    }
}
