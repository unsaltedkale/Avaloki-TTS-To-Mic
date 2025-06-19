using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// thank you to JoeStrout: https://discussions.unity.com/t/easy-speech-synthesis-on-a-mac/696566

public class AppleSpeechSynth : MonoBehaviour
{

    public string voice = "Samantha";
    public int outputChannel = 51;

    public UnityEvent onStartedSpeaking;
    public UnityEvent onStoppedSpeaking;

    System.Diagnostics.Process speechProcess;
    bool wasSpeaking;

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

    public void Speak(string text)
    {
        print("Speaking now.");
        string cmdArgs = string.Format("-a {2} -v {0} \"{1}\"", voice, text.Replace("\"", ","), outputChannel);
        speechProcess = System.Diagnostics.Process.Start("/usr/bin/say", cmdArgs);
    }

}