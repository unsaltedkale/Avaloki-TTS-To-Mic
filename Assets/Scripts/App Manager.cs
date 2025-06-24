using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentOutputChannel = avalokiMicrophoneOutputChannel;
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
            voice = "Daniel";
        }

        else if (voiceSelectorMenu.value == 1)
        {
            voice = "Juan";
        }

        else if (voiceSelectorMenu.value == 2)
        {
            voice = "Rishi";
        }

        else if (voiceSelectorMenu.value == 3)
        {
            voice = "Samantha";
        }

        else if (voiceSelectorMenu.value == 4)
        {
            voice = "Victoria";
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
            appleSpeechSynth.Speak("This is a test message, for your ears only.", builtInOutputChannel, speakingWPM, voice);
        }
    }
}
