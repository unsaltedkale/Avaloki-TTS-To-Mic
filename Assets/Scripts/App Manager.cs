using UnityEngine;
using TMPro;

public class AppManager : MonoBehaviour
{
    public AppleSpeechSynth appleSpeechSynth;
    public TMP_InputField inputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandMessageOff();
        }

    }

    public void KeepActive()
    {
        inputField.ActivateInputField();
        print("hm");
    }

    public void HandMessageOff()
    {
        print("Handed Off " + inputField.text);
        appleSpeechSynth.Speak(inputField.text);
        inputField.text = "";
        KeepActive();
    }
}
