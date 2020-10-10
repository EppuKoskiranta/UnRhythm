using UnityEditor;
using UnityEngine;

public class CreateSongWizard : ScriptableWizard
{
    public AudioClip audio;

    public Note[] notes;


    [MenuItem("Song Creator/Create a Song")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<CreateSongWizard>("Create a song", "Create");
    }


    private void OnWizardCreate()
    {
        if (audio)
        {
            Song song = ScriptableObject.CreateInstance<Song>();
            song.name = audio.name;
            song.audio = audio;
            song.length = audio.length;
            if (notes.Length > 0)
            {
                Note[] noteArray = new Note[notes.Length];
                for (int i = 0; i < notes.Length; ++i)
                {
                    noteArray[i] = new Note(notes[i].timeStamp, notes[i].key);
                }


                song.notes = noteArray;
            }

            AssetDatabase.CreateAsset(song, "Assets/Resources/Prefabs/Songs/" + song.name + ".asset");
            //    PrefabUtility.SaveAsPrefabAsset(go, "Assets/Prefabs/Songs/" + song.name + ".prefab");
            //    GameObject.DestroyImmediate(go);
        }
    }
}
