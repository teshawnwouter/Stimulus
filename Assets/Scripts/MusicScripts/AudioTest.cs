using System.Collections;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    int bpm;
    private void Start()
    {
        bpm = FirstBPMAttempt.AnalizeBPM(audioClip);
        if (bpm < 0)
        {
            return;
        }

        InvokeRepeating(nameof(Beatcheck),0,60f/(float)bpm);
    }

    private void Beatcheck() 
    {
        Debug.Log("beat");
    }

}
