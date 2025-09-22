using System.Collections.Generic;
using UnityEngine;

public class NoteCounter : MonoBehaviour
{
    public List<NotesToPlay> notes = new List<NotesToPlay>();
    [SerializeField] private Spawner spawner;

    public int playedNotes;

    public void NotesPlayed(int notesPlayed)
    {
        playedNotes += notesPlayed;

        for (int i = 0; i < playedNotes / 5; i++)
        {
            for(int j = 0; j < notes.Count; j++)
            {
                notes[j].speed += 0.1f;
                if (notes[j].speed > 4)
                {
                    notes[j].speed = 4;
                }
            }
            spawner.spawnInterval -= 0.2f;
            if (spawner.spawnInterval <= 1.5f)
            {
                spawner.spawnInterval = 1.5f;
            }
        }
    }
}
