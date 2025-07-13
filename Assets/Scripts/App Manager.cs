using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

public class AppManager : MonoBehaviour
{
    public AppleSpeechSynth appleSpeechSynth;
    public TMP_InputField inputField;
    public int builtInOutputChannel;
    public int avalokiMicrophoneOutputChannel;
    public int currentOutputChannel;
    public bool speechSynthBusy;
    public bool sendButtonPressedBool;
    public int speakingWPM;
    public TMP_Dropdown speedSelectorMenu;
    public TMP_Dropdown voiceSelectorMenu;
    public TMP_Dropdown audioOutputSelectorMenu;
    public string lastSpokenMessage;
    public string voice;
    public CrossSceneStorage css;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        css = FindFirstObjectByType<CrossSceneStorage>();

        avalokiMicrophoneOutputChannel = css.numberAvalokiMicrophoneOutputChannel;
        builtInOutputChannel = css.numberBuiltInOutputChannel;

        SetVoiceThings();

        SetAudioThings();

    }

    public void SetVoiceThings()
    {

        List<string> DropOptions = new List<string> { };

        foreach (string s in css.totalVoicesList)
        {
            DropOptions.Add(s);
        }

        voiceSelectorMenu.ClearOptions();
        voiceSelectorMenu.AddOptions(DropOptions);

        voiceSelectorChanged();

    }

    public void SetAudioThings()
    {

        List<string> DropOptions = new List<string> { };

        foreach (string s in css.totalAudioList)
        {
            DropOptions.Add(s);
        }

        audioOutputSelectorMenu.ClearOptions();
        audioOutputSelectorMenu.AddOptions(DropOptions);

        audioOutputSelectorChanged();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || sendButtonPressedBool)
        {
            lastSpokenMessage = inputField.text;
            HandMessageOff();
            sendButtonPressedBool = false;
        }

    }

    public void sendButtonPressed()
    {
        sendButtonPressedBool = true;
    }

    public void replayMessageButtonPressed()
    {
        if (!appleSpeechSynth.wasSpeaking)
        {
            appleSpeechSynth.Speak(lastSpokenMessage, currentOutputChannel, speakingWPM, voice);
        }
    }

    public void speedSelectorChanged()
    {

        if (speedSelectorMenu.value == 0)
        {
            speakingWPM = 100;
        }

        else if (speedSelectorMenu.value == 1)
        {
            speakingWPM = 175;
        }

        else if (speedSelectorMenu.value == 2)
        {
            speakingWPM = 250;
        }

    }

    public void voiceSelectorChanged()
    {
        int value = voiceSelectorMenu.value;

        print(voiceSelectorMenu.options[value].text);

        string workingString = voiceSelectorMenu.options[value].text;

        int found = workingString.IndexOf(" ");

        workingString = workingString.Substring(0, found);

        voice = workingString;
    }

    public void audioOutputSelectorChanged()
    {
        int value = audioOutputSelectorMenu.value;

        print(audioOutputSelectorMenu.options[value].text);

        string workingString = audioOutputSelectorMenu.options[value].text;

        workingString = System.Text.RegularExpressions.Regex.Match(workingString, @"\d+").Value;

        print(workingString);

        Int32.TryParse(workingString, out currentOutputChannel);
    }
    public void KeepActive()
    {
        inputField.ActivateInputField();
    }

    public void HandMessageOff()
    {
        if (!appleSpeechSynth.wasSpeaking)
        {
            print("Handed Off " + inputField.text);
            appleSpeechSynth.Speak(inputField.text, currentOutputChannel, speakingWPM, voice);
            inputField.text = "";
        }
        KeepActive();
    }

    public void testSettingsMessage()
    {
        if (!appleSpeechSynth.wasSpeaking)
        {
            appleSpeechSynth.Speak("My name is " + voice + ". This is a test message, through Built-in Audio.", builtInOutputChannel, speakingWPM, voice);
        }
    }

    public void setUpButtonPressed()
    {
        appleSpeechSynth.EmergencyStop();
        SceneManager.LoadScene("Tutorial_Scene");
    }
}
