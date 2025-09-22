using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FirstBPMAttempt : MonoBehaviour
{
    private const int minBPM = 60;
    private const int maxBPM = 400;

    private const int baseFrequency = 44100;
    private const int baseChannels = 2;

    private const int sampleRate = 2205;

    private struct BPMMAtchData
    {
        public int BPM;
        public float match;
    }

    private static BPMMAtchData[] matchData = new BPMMAtchData[maxBPM - minBPM + 1];

    public static int AnalizeBPM(AudioClip clip)
    {
        for (int i = 0; i < matchData.Length; i++)
        {
            matchData[i].match = 0;
        }
        if (clip == null)
            return -1;

        //Debug.Log("AnalyzeBpm audioClipName : " + clip.name);

        int beetFrequency = clip.frequency;
        //Debug.Log("Frequency : " + beetFrequency);
        int beetChannales = clip.channels;
        //Debug.Log("Channles : " + beetChannales);

        int splitFrameSize = Mathf.FloorToInt(((float)beetFrequency / (float)baseFrequency) * ((float)beetChannales / (float)baseChannels) * (float)sampleRate);

        var allSamples = new float[clip.samples * beetChannales];
        clip.GetData(allSamples, 0);

        var volumeArr = CreateVolumeArray(allSamples, beetFrequency, beetChannales, splitFrameSize);

        BPMMAtchData match = SearchBpm(volumeArr, beetFrequency, splitFrameSize);
        //Debug.Log("Found match bpm : " + match.BPM + " with match power " + match.match);

        var strBuilder = new StringBuilder("BPM Match DataList\n");
        for (int i = 0; i < matchData.Length; i++)
        {
            strBuilder.Append("bpm : " + matchData[i].BPM + ", match : " + (Mathf.FloorToInt(matchData[i].match * 10000f)) + "\n");
        }
        //Debug.Log(strBuilder.ToString());
        return match.BPM;
    }


    private static float[] CreateVolumeArray(float[] allSamples, int frequency, int channels, int splitSampleRate)
    {
        var volumeArray = new float[Mathf.CeilToInt((float)allSamples.Length / (float)splitSampleRate)];
        int powerIndex = 0;

        for (int sampleIndex = 0; sampleIndex < allSamples.Length; sampleIndex += splitSampleRate)
        {
            float sum = 0;
            for (int FrameIndex = sampleIndex; FrameIndex < sampleIndex + splitSampleRate; FrameIndex++)
            {
                if (allSamples.Length <= FrameIndex)
                {
                    break;
                }

                float absValue = Mathf.Abs(allSamples[FrameIndex]);

                if (absValue > 1f)
                {
                    continue;
                }

                sum += (absValue * absValue);

            }
            volumeArray[powerIndex] = Mathf.Sqrt(sum / splitSampleRate);
            powerIndex++;
        }

        float maxVolume = volumeArray.Max();

        for (int i = 0; i < volumeArray.Length; i++)
        {
            volumeArray[i] /= maxVolume;
        }

        return volumeArray;
    }

    private static BPMMAtchData SearchBpm(float[] volumearr, int frequency, int splitSampleRate)
    {
        List<float> difflist = new List<float>();
        for (int i = 1; i < volumearr.Length; i++)
        {
            difflist.Add(Mathf.Max(volumearr[i] - volumearr[i - 1], 0));
        }

        int index = 0;
        float splitFrequency = (float)frequency / (float)splitSampleRate;
        for (int bpm = minBPM; bpm <= maxBPM; bpm++)
        {
            float sinMath = 0;
            float cosMath = 0;
            float bps = (float)bpm / 60;

            if (difflist.Count > 0)
            {
                for (int i = 0; i < difflist.Count; i++)
                {
                    sinMath += (difflist[i] * Mathf.Cos(i * 2f * Mathf.PI * bps / splitFrequency));
                    cosMath += (difflist[i] * Mathf.Sin(i * 2f * Mathf.PI * bps / splitFrequency));
                }

                sinMath *= (1f / (float)difflist.Count);
                cosMath *= (1f / (float)difflist.Count);
            }
            float match = Mathf.Sqrt((sinMath * sinMath) + (cosMath * cosMath));
            matchData[index].BPM = bpm;
            matchData[index].match = match;
            index++;
        }

        int matchIndex = Array.FindIndex(matchData, x => x.match == matchData.Max(y => y.match));
        return matchData[matchIndex];
    }
}
