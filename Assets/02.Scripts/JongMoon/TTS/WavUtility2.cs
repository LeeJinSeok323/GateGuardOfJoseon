using UnityEngine;
using System;

public static class WavUtility2
{
    public static AudioClip ToAudioClip(byte[] data)
    {
        int channels = BitConverter.ToInt16(data, 22);
        int sampleRate = BitConverter.ToInt32(data, 24);
        int subChunk2 = BitConverter.ToInt32(data, 40);

        float[] audioData = new float[subChunk2 / 2];
        for (int i = 0; i < audioData.Length; i++)
        {
            audioData[i] = BitConverter.ToInt16(data, 44 + i * 2) / 32768f;
        }

        AudioClip audioClip = AudioClip.Create("TTS_Audio", audioData.Length, channels, sampleRate, false);
        audioClip.SetData(audioData, 0);

        return audioClip;
    }
}
