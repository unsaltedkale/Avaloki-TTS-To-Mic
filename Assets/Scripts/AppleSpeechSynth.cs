using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// thank you to JoeStrout: https://discussions.unity.com/t/easy-speech-synthesis-on-a-mac/696566

public class AppleSpeechSynth : MonoBehaviour
{
    public UnityEvent onStartedSpeaking;
    public UnityEvent onStoppedSpeaking;

    System.Diagnostics.Process speechProcess;
    public bool wasSpeaking;

    void Update()
    {
        bool isSpeaking = (speechProcess != null && !speechProcess.HasExited);
        if (isSpeaking != wasSpeaking)
        {
            if (isSpeaking) onStartedSpeaking.Invoke();
            else onStoppedSpeaking.Invoke();
            wasSpeaking = isSpeaking;
        }
    }

    public void Speak(string text, int sentOutputChannel, int speakingWPM, string voice)
    {
        print("Speaking now.");
        string cmdArgs = string.Format("-a {2} -v {0} -r {3} \"{1}\"", voice, text.Replace("\"", ","), sentOutputChannel, speakingWPM);
        speechProcess = System.Diagnostics.Process.Start("/usr/bin/say", cmdArgs);
    }

    public void EmergencyStop()
    {
        if (speechProcess != null && !speechProcess.HasExited)
        {
        speechProcess.Kill();
        }
    }

}