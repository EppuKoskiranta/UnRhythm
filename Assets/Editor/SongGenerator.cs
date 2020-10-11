using UnityEngine;
using UnityEditor;
public enum SongDifficulty
{
    EASY = 0,
    NORMAL,
    HARD,
    INSANE,
}




public class SongGenerator
{

    SongDifficulty difficulty;
    AudioClip audioClip;

    public SongGenerator(SongDifficulty dif, AudioClip clip)
    {
        this.difficulty = dif;
        this.audioClip = clip;
    }




    public void GenerateNotes()
    {
        float[] timeSteps = { 12.5f, 13, 14, 14, 15, 16, 17, 18, 19, 19, 19.6f, 20, 21, 21, 21.5f, 22.4f, 24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,4,42.6f,42.6f,43.5f,43.5f, 45,45,45,45,50,50.5f,50.7f,50.9f,60.2f,60.5f,61,61.7f,63,64,65,68,69,70,71,72,73,74,74,75,75,76,76,78,78};
        NoteKey[] keys = { NoteKey.LEFTLEFT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.LEFTLEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.LEFTLEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFTLEFT, NoteKey.LEFTLEFT, NoteKey.LEFTLEFT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.RIGHTRIGHT, NoteKey.RIGHT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.RIGHT, NoteKey.LEFTLEFT, NoteKey.LEFT, NoteKey.LEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.LEFTLEFT, NoteKey.LEFTLEFT, NoteKey.LEFTLEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.LEFTLEFT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.RIGHT, NoteKey.RIGHT, NoteKey.RIGHT, NoteKey.LEFT, NoteKey.LEFT, NoteKey.LEFT, NoteKey.LEFTLEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.LEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.RIGHT, NoteKey.LEFTLEFT, NoteKey.RIGHTRIGHT, NoteKey.RIGHT, NoteKey.LEFT, NoteKey.LEFTLEFT, NoteKey.RIGHTRIGHT, NoteKey.LEFT, NoteKey.LEFTLEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.LEFTLEFT, NoteKey.RIGHT, NoteKey.RIGHTRIGHT, NoteKey.LEFT };

        Song song = ScriptableObject.CreateInstance<Song>();

        song.name = audioClip.name;
        song.audio = audioClip;
        song.length = audioClip.length;

        Note[] notes = new Note[timeSteps.Length];

        for (int i = 0; i < notes.Length; ++i)
        {
            notes[i] = new Note(timeSteps[i], keys[i]);
        }

        song.notes = notes;

        AssetDatabase.CreateAsset(song, "Assets/Resources/Prefabs/Songs/" + song.name + ".asset");
    }
}
