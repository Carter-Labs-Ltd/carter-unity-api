using System.Collections.Generic;
using UnityEngine;
using System;


public class Listener : MonoBehaviour
{
    int frameRate = 16000;
    bool isRecording = false;
    AudioSource audioSource;
    List<float> tempRecording = new List<float>();

    void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 1, 16000);
        audioSource.Play();
        Invoke("ResizeRecording", 1);
    }

    public void StartListening()
    {
        if(!isRecording)
        {                     
            isRecording = true;
            audioSource.Stop();
            tempRecording.Clear();
            Microphone.End(null);
            audioSource.clip = Microphone.Start(null, true, 20, frameRate);
            Invoke("ResizeRecording", 1);
        } 
    }

    public string StopListening(){
        if (isRecording)
        {
            isRecording = false;
            int length = Microphone.GetPosition(null);
            Microphone.End(null);
            byte[] bytes = WavUtility.FromAudioClip(audioSource.clip);
            string base64 = Convert.ToBase64String(bytes);
            
            return base64;
        }

        return null;
    }

    void ResizeRecording()
    {
        if (isRecording)
        {
            int length = frameRate;
            float[] clipData = new float[length];
            audioSource.clip.GetData(clipData, 0);
            tempRecording.AddRange(clipData);
            Invoke("ResizeRecording", 1);
        }
    }

   

}