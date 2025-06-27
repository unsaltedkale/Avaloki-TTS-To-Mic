using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public string voice1name;
    public string voice2name;
    public string voice3name;
    public string voice4name;
    public string voice5name;
    public CrossSceneStorage css;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        css = FindFirstObjectByType<CrossSceneStorage>();

        avalokiMicrophoneOutputChannel = css.numberAvalokiMicrophoneOutputChannel;
        builtInOutputChannel = css.numberBuiltInOutputChannel;

        SetVoiceThings();


        currentOutputChannel = avalokiMicrophoneOutputChannel;
    }

    public void SetVoiceThings()
    {
        voice1name = css.voice1name;
        voice2name = css.voice2name;
        voice3name = css.voice3name;
        voice4name = css.voice4name;
        voice5name = css.voice5name;

        List<string> DropOptions = new List<string> { voice1name, voice2name, voice3name, voice4name, voice5name };


        voice = voice1name;
        voiceSelectorMenu.ClearOptions();
        voiceSelectorMenu.AddOptions(DropOptions);

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
        if (voiceSelectorMenu.value == 0)
        {
            voice = voice1name;
        }

        else if (voiceSelectorMenu.value == 1)
        {
            voice = voice2name;
        }

        else if (voiceSelectorMenu.value == 2)
        {
            voice = voice3name;
        }

        else if (voiceSelectorMenu.value == 3)
        {
            voice = voice4name;
        }

        else if (voiceSelectorMenu.value == 4)
        {
            voice = voice5name;
        }
    }

    public void audioOutputSelectorChanged()
    {
        if (audioOutputSelectorMenu.value == 0)
        {
            currentOutputChannel = avalokiMicrophoneOutputChannel;
        }

        else if (audioOutputSelectorMenu.value == 1)
        {
            currentOutputChannel = builtInOutputChannel;
        }
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
            appleSpeechSynth.Speak("My name is "+ voice + ". This is a test message, through Built-in Audio.", builtInOutputChannel, speakingWPM, voice);
        }
    }

    public void setUpButtonPressed()
    {
        appleSpeechSynth.EmergencyStop();
        SceneManager.LoadScene("Tutorial_Scene");
    }
}
