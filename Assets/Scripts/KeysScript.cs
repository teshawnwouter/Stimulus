using UnityEngine;

public class KeysScript : MonoBehaviour
{
    [Header("visuals")]
    [SerializeField] private GameObject pressedKey;
    public KeyCode noteKey;

    [SerializeField] private NoteCounter noteCounter;
    [SerializeField] private Score score;

    private void Start()
    {
        noteCounter = FindFirstObjectByType<NoteCounter>();
        score = FindFirstObjectByType<Score>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(noteKey))
        {
            pressedKey.SetActive(true);
            if (noteCounter.notes[0].goodPlay && noteKey == noteCounter.notes[0].notekey)
            {
                score.AddScore(noteCounter.notes[0].pointsGiven);
                noteCounter.notes[0].PlayedNote();
                noteCounter.NotesPlayed(1);
            }
            else if (noteCounter.notes[0].perfectPlay && noteKey == noteCounter.notes[0].notekey)
            {
                score.AddScore(noteCounter.notes[0].pointsGiven * 2);
                noteCounter.notes[0].PlayedNote();
                noteCounter.NotesPlayed(1);
            }else if (!noteCounter.notes[0].perfectPlay && !noteCounter.notes[0].goodPlay && noteKey == noteCounter.notes[0].notekey)
            {
                noteCounter.notes[0].PlayedNote();
                noteCounter.NotesPlayed(1);
                score.bad++;
            }
        }

        if (Input.GetKeyUp(noteKey))
        {
            pressedKey.SetActive(false);
        }
    }
}
