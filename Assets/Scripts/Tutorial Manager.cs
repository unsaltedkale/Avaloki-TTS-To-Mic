using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public struct Event
    {
        public string text;
        public bool videoBool;
        public string videoPath;
        public bool buttonChoicesBool;
        public string buttonLabel1;
        public string buttonLabel2;
        public string buttonLabel3;
        public string buttonLabel4;

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
        TextBox.text = currentEvent.text;

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
