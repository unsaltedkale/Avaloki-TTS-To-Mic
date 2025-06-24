using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public struct Event
    {
        string text;
        bool videoBool;
        string videoPath;
        bool buttonChoicesBool;
        string buttonLabel1;
        string buttonLabel2;
        string buttonLabel3;
        string buttonLabel4;

        public string Text { get => text; set => text = value; }
        public bool VideoBool { get => videoBool; set => videoBool = value; }
        public string VideoPath { get => videoPath; set => videoPath = value; }
        public bool ButtonChoicesBool { get => buttonChoicesBool; set => buttonChoicesBool = value; }
        public string ButtonLabel1 { get => buttonLabel1; set => buttonLabel1 = value; }
        public string ButtonLabel2 { get => buttonLabel2; set => buttonLabel2 = value; }
        public string ButtonLabel3 { get => buttonLabel3; set => buttonLabel3 = value; }
        public string ButtonLabel4 { get => buttonLabel4; set => buttonLabel4 = value; }

        public Event(string Text, bool VideoBool, string VideoPath, bool ButtonChoicesBool, string ButtonLabel1, string ButtonLabel2, string ButtonLabel3, string ButtonLabel4)
        {
            text = Text;
            videoBool = VideoBool;
            videoPath = VideoPath;
            buttonChoicesBool = ButtonChoicesBool;
            buttonLabel1 = ButtonLabel1;
            buttonLabel2 = ButtonLabel2;
            buttonLabel3 = ButtonLabel3;
            buttonLabel4 = ButtonLabel4;
        }
    }

    public List<Event> ListEvents;
    public int currentEventIndex;
    public Event currentEvent;
    public bool nextButtonPressedBool;
    public TextMeshProUGUI TextBox;
    public Button nextButton;

    public Event welcome1 = new Event("Hello! Welcome to <b>Avaloki</b>.", false, "", false, "", "", "", "");
    public Event welcome2 = new Event("Before you can use this program, we have to set it up!", false, "", false, "", "", "", "");
    public Event welcome3 = new Event("This will take approximately <b>20 minutes</b> and will require <b>restarting your computer</b>.", false, "", false, "", "", "", "");
    public Event welcome4 = new Event("So make sure you can do that before we begin.", false, "", false, "", "", "", "");


    //public Event welcome1 = new Event("Hello!", false, "", false, "", "", "", "");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ListEvents.Add(welcome1);
        ListEvents.Add(welcome2);
        ListEvents.Add(welcome3);
        ListEvents.Add(welcome4);

        StartCoroutine(ListEventsystem());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ListEventsystem()
    {
        currentEvent = ListEvents[currentEventIndex];
        Render();
        yield return new WaitUntil(() => nextButtonPressedBool == true);
        nextButtonPressedBool = false;
        currentEventIndex++;
        yield return null;
        StartCoroutine(ListEventsystem());
        yield break;
    }

    void Render()
    {
        TextBox.text = currentEvent.Text;

        if (currentEvent.VideoBool)
        {
            //set video
        }
        else
        {
            //disable
        }

        if (currentEvent.ButtonChoicesBool)
        {
            //do text
        }
        else
        {
            //disable
        }
        

    }

    public void nextButtonPressed()
    {
        nextButtonPressedBool = true;
        StartCoroutine(DisableForAMoment());
    }

    public IEnumerator DisableForAMoment()
    {
        nextButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        nextButton.interactable = true;
    }
}
