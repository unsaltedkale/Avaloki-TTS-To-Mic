using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class TutorialManager : MonoBehaviour
{

    [System.Serializable]
    public struct Event
    {
        string text;
        bool imageBool;
        bool buttonChoicesBool;
        string buttonLabel1;
        string buttonLabel2;
        string buttonLabel3;
        string buttonLabel4;

        public string Text { get => text; set => text = value; }
        public bool ImageBool { get => imageBool; set => imageBool = value; }
        public bool ButtonChoicesBool { get => buttonChoicesBool; set => buttonChoicesBool = value; }
        public string ButtonLabel1 { get => buttonLabel1; set => buttonLabel1 = value; }
        public string ButtonLabel2 { get => buttonLabel2; set => buttonLabel2 = value; }
        public string ButtonLabel3 { get => buttonLabel3; set => buttonLabel3 = value; }
        public string ButtonLabel4 { get => buttonLabel4; set => buttonLabel4 = value; }

        int imagePath;

        public int ImagePath { get => imagePath; set => imagePath = value; }


        public Event(string Text, bool ImageBool, bool ButtonChoicesBool, string ButtonLabel1, string ButtonLabel2, string ButtonLabel3, string ButtonLabel4) : this()
        {
            text = Text;
            imageBool = ImageBool;
            imagePath = default(int);
            buttonChoicesBool = ButtonChoicesBool;
            buttonLabel1 = ButtonLabel1;
            buttonLabel2 = ButtonLabel2;
            buttonLabel3 = ButtonLabel3;
            buttonLabel4 = ButtonLabel4;
        }



        public Event(string Text, bool ImageBool, int ImagePath, bool ButtonChoicesBool, string ButtonLabel1, string ButtonLabel2, string ButtonLabel3, string ButtonLabel4)
        {
            text = Text;
            imageBool = ImageBool;
            imagePath = ImagePath;
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
    public bool choiceButtonPressedBool;
    public TextMeshProUGUI TextBox;
    public Button nextButton;
    public GameObject buttonSelectionGroup;
    public Button buttonSelect1;
    public Button buttonSelect2;
    public Button buttonSelect3;
    public Button buttonSelect4;
    public Image imageDisplay;
    public List<Sprite> imagesForTutorial;
    public TMP_InputField numberInputField;

    public Event welcome1 = new Event("Hello! Welcome to <b>Avaloki</b>.", true, 0, false, "", "", "", "");
    public Event welcome2 = new Event("Before you can use this program, we have to set it up!", false, false, "", "", "", "");
    public Event welcome3 = new Event("This will take approximately <b>20 minutes</b>.", false, false, "", "", "", "");
    public Event welcome4 = new Event("So make sure you can do that before we begin.", false, false, "", "", "", "");


    public Event install1 = new Event("Avaloki currently works with <b>Loopback</b> to deliver the whole experience.", false, false, "", "", "", "");
    public Event install2 = new Event("Loopback allows audio to flow from a speaker to a microphone, so TTS can play in your call!", false, false, "", "", "", "");
    public Event install3 = new Event("First, we will first install Loopback.", false, false, "", "", "", "");
    public Event install4 = new Event("Go to <b>https://rogueamoeba.com/loopback/</b> and download the demo version by pressing <i>Free Download</i>.", false, false, "", "", "", "");
    public Event install5 = new Event("Follow the download instructions and come back here when you are done.", false, false, "", "", "", "");
    public Event install6 = new Event("Welcome back! Now go to Loopback and press the plus button in the bottom left corner that says New Virtual Device.", false, false, "", "", "", "");
    public Event install7 = new Event("Click the name to rename it to <b>Avaloki Microphone</b>", false, false, "", "", "", "");
    public Event install8 = new Event("Do not mess with any of the settings inside of Avaloki Microphone.", false, false, "", "", "", "");


    public Event terminal1 = new Event("Great! Now we will set up Avaloki to work with Loopback.", false, false, "", "", "", "");
    public Event terminal2 = new Event("Open the <b>Terminal</b> on your computer.", false, false, "", "", "", "");
    public Event terminal3 = new Event("Don't panic! This will be very simple commands.", false, false, "", "", "", "");
    public Event terminal4 = new Event("Type\n <b>say -a ?</b> \ninto the terminal and press enter. You should see a list of audio sources like demonstrated above.", false, false, "", "", "", "");
    public Event terminal5 = new Event("Type into the space below the <b>number</b> to the left of Built-in Output and press the confirm button.", false, false, "", "", "", "");
    public Event terminal6 = new Event("Now, type into the space below the <b>number</b> to the left of Avaloki Microphone and press the confirm button", false, false, "", "", "", "");


    public Event integration1 = new Event("Now open back up Loopback Audio.", false, false, "", "", "", "");
    public Event integration2 = new Event("We are going to test that you have the right numbers entered.", false, false, "", "", "", "");
    public Event integration3 = new Event("Can you hear this? Hello? This message should be playing through your default audio output.\n If you cannot hear this, make sure your audio is turned up.", false, false, "", "", "", "");
    public Event integration4 = new Event("Great! When you click next, audio is going to play through Avaloki Microphone. Watch Loopback to see the bars on the dashboard of Avaloki Microphone go up and down.", false, false, "", "", "", "");
    public Event integration5 = new Event("Test test, this audio is now playing through Avaloki Microphone. If you do not see the bars bounce up and down, then, whoopies!", false, false, "", "", "", "");
    public Event integration6 = new Event("Amazing! You have now successfully set up the audio paths for Avaloki.", false, false, "", "", "", "");

    public Event voice1 = new Event("We're almost there! We just need to do one more thing for your quality of life.", false, false, "", "", "", "");
    public Event voice2 = new Event("MacOS TTS has different voices you can use. We're going to select your five favorites!", false, false, "", "", "", "");
    public Event voice3 = new Event("Open back up Terminal.", false, false, "", "", "", "");
    public Event voice4 = new Event("Now type\n<b>say -v ?</b>\n and press enter. You should see a list of names, as shown above.", false, false, "", "", "", "");
    public Event voice5 = new Event("Select five voices from the list and add <b>only the name</b> and put one of each into the spaces on the left. Press confirm on each box when you are finalized.", false, false, "", "", "", "");
    public Event voice6 = new Event("Thank you! Avaloki is now all fired up and ready to go!", false, false, "", "", "", "");



    public Event selectapp1 = new Event("Click on any button to learn how to set up Avaloki on that app. If you don't want any help, just press next.", false, true, "Discord", "Google Meets", "Slack", "Zoom");


    public Event discord1 = new Event("To set up on Discord, press the cog labeled User Settings on the bottom left side of the dashboard.", true, 1, false, "", "", "", "");
    public Event discord2 = new Event("Scroll down on the left until you see a section called APP SETTINGS. Click Video & Video.", true, 2, false, "", "", "", "");
    public Event discord3 = new Event("Under the dropdown menu of Input Device, select Avaloki Microphone.", true, 3, false, "", "", "", "");
    public Event discord4 = new Event("You now have Avaloki set up on Discord! We will now redirect you back to the selection screen.", true, 4, false, "", "", "", "");


    public Event googlemeets1 = new Event("To set up on Google Meets, go to: \n meet.google.com", true, 5, false, "", "", "", "");
    public Event googlemeets2 = new Event("Press the cog labeled Settings in the upper right corner. Allow chrome access to your microphone if you have not already.", true, 6, false, "", "", "", "");
    public Event googlemeets3 = new Event("Under Audio, use the dropdown menu under Microphone to select Avaloki Microphone (Virtual).", true, 7, false, "", "", "", "");
    public Event googlemeets4 = new Event("You now have Avaloki set up on Google Meets! We will now redirect you back to the selection screen.", true, 8, false, "", "", "", "");


    public Event slack1 = new Event("To set up Slack, click your profile picture in the bottom left correct and click Preferences.", true, 9, false, "", "", "", "");
    public Event slack2 = new Event("Click Audio & Video in the side bar.", true, 10, false, "", "", "", "");
    public Event slack3 = new Event("Under Microphone, select Avaloki Microphone (Virtual)\n (Can't find it? Quit Slack from the dock by right clicking it and pressing Quit, and then reopen Slack.)", true, 11, false, "", "", "", "");
    public Event slack4 = new Event("You now have Avaloki set up on Slack! We will now redirect you back to the selection screen.", true, 12, false, "", "", "", "");

    public Event zoom1 = new Event("To set up Zoom, have Zoom selected and click Zoom.us in the top left corner of your screen, and click Preferences.", true, 13, false, "", "", "", "");
    public Event zoom2 = new Event("In the window that appears, select Audio on the left hand side.", true, 14, false, "", "", "", "");
    public Event zoom3 = new Event("Under Microphone, click the drodown menu and select Avaloki Microphone", true, 15, false, "", "", "", "");
    public Event zoom4 = new Event("You now have Avaloki set up on Zoom! We will now redirect you back to the selection screen.", true, 16, false, "", "", "", "");

    public Event end1 = new Event("Thank you for your time! Avaloki will load after you press Next.", false, false, "", "", "", "");




    //public Event welcome1 = new Event("Hello!", false, false, "", "", "", "");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ListEvents.Add(welcome1);
        ListEvents.Add(welcome2);
        ListEvents.Add(welcome3);
        ListEvents.Add(welcome4);


        ListEvents.Add(install1);
        ListEvents.Add(install2);
        ListEvents.Add(install3);
        ListEvents.Add(install4);
        ListEvents.Add(install5);
        ListEvents.Add(install6);
        ListEvents.Add(install7);
        ListEvents.Add(install8);


        ListEvents.Add(terminal1);
        ListEvents.Add(terminal2);
        ListEvents.Add(terminal3);
        ListEvents.Add(terminal4);
        ListEvents.Add(terminal5);
        ListEvents.Add(terminal6);


        ListEvents.Add(integration1);
        ListEvents.Add(integration2);
        ListEvents.Add(integration3);
        ListEvents.Add(integration4);
        ListEvents.Add(integration5);
        ListEvents.Add(integration6);

        ListEvents.Add(selectapp1);

        ListEvents.Add(discord1);
        ListEvents.Add(discord2);
        ListEvents.Add(discord3);
        ListEvents.Add(discord4);

        ListEvents.Add(googlemeets1);
        ListEvents.Add(googlemeets2);
        ListEvents.Add(googlemeets3);
        ListEvents.Add(googlemeets4);

        ListEvents.Add(slack1);
        ListEvents.Add(slack2);
        ListEvents.Add(slack3);
        ListEvents.Add(slack4);

        ListEvents.Add(zoom1);
        ListEvents.Add(zoom2);
        ListEvents.Add(zoom3);
        ListEvents.Add(zoom4);

        ListEvents.Add(end1);

        StartCoroutine(ListEventsystem());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ListEventsystem()
    {
        currentEvent = ListEvents[currentEventIndex];
        bool b = false;
        if (currentEvent.Text == terminal5.Text || currentEvent.Text == terminal6.Text)
        {
            b = true;
        }
        yield return StartCoroutine(Render(b));
        yield return new WaitUntil(() => nextButtonPressedBool == true);
        nextButtonPressedBool = false;

        if (choiceButtonPressedBool == true)
        {
            choiceButtonPressedBool = false;
            yield return null;
            StartCoroutine(ListEventsystem());
        }

        else if (currentEvent.Text != end1.Text)
        {
            var oldcurrentEvent = currentEvent;
            yield return StartCoroutine(CatchCase());
            if (oldcurrentEvent.Text == currentEvent.Text)
            {
                currentEventIndex++;
            }
            yield return null;
            StartCoroutine(ListEventsystem());
        }


        else
        {
            SceneManager.LoadScene("App_Scene");
        }

        yield break;
    }

    public IEnumerator CatchCase()
    {
        if (currentEvent.Text == googlemeets4.Text)
        {
            currentEvent = selectapp1;
        }

        else if (currentEvent.Text == discord4.Text)
        {
            currentEvent = selectapp1;
        }

        else if (currentEvent.Text == slack4.Text)
        {
            currentEvent = selectapp1;
        }

        else if (currentEvent.Text == zoom4.Text)
        {
            currentEvent = selectapp1;
        }

        else if (currentEvent.Text == selectapp1.Text)
        {
            currentEvent = end1;
        }

        for(int i = 0; i < ListEvents.Count; i++)
        {
            Event e = ListEvents[i];
            if (e.Text == currentEvent.Text)
            {
                currentEventIndex = i;
                break;
            }
        }

        yield break;
    }

    public IEnumerator Render(bool b)
    {
        TextBox.text = currentEvent.Text;

        if (b)
        {
            numberInputField.gameObject.SetActive(true);
        }

        else
        {
            numberInputField.gameObject.SetActive(false);
        }

        if (currentEvent.ImageBool)
        {
            imageDisplay.gameObject.SetActive(true);
            float w = imagesForTutorial[currentEvent.ImagePath].texture.width;
            float h = imagesForTutorial[currentEvent.ImagePath].texture.height;
            imageDisplay.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(w * (540 / h), 540);
            imageDisplay.sprite = imagesForTutorial[currentEvent.ImagePath];
        }
        else
        {
            imageDisplay.gameObject.SetActive(false);
        }

        if (currentEvent.ButtonChoicesBool)
        {
            buttonSelectionGroup.SetActive(true);

            if (currentEvent.ButtonLabel1 != "")
            {
                buttonSelect1.gameObject.SetActive(true);
                buttonSelect1.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.ButtonLabel1;
            }

            else
            {
                buttonSelect1.gameObject.SetActive(false);
            }

            if (currentEvent.ButtonLabel2 != "")
            {
                buttonSelect2.gameObject.SetActive(true);
                buttonSelect2.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.ButtonLabel2;
            }

            else
            {
                buttonSelect2.gameObject.SetActive(false);
            }

            if (currentEvent.ButtonLabel3 != "")
            {
                buttonSelect3.gameObject.SetActive(true);
                buttonSelect3.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.ButtonLabel3;
            }

            else
            {
                buttonSelect3.gameObject.SetActive(false);
            }

            if (currentEvent.ButtonLabel4 != "")
            {
                buttonSelect4.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.ButtonLabel4;
            }

            else
            {
                buttonSelect4.gameObject.SetActive(false);
            }

        }

        else
        {
            buttonSelectionGroup.SetActive(false);
        }

        yield break;
    }

    public void nextButtonPressed()
    {
        nextButtonPressedBool = true;
        StartCoroutine(DisableForAMoment());
    }

    public void choiceButtonPressed()
    {
        var o = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        if (o == "Discord")
        {
            currentEvent = discord1;
        }

        else if (o == "Google Meets")
        {
            currentEvent = googlemeets1;
        }

        else if (o == "Slack")
        {
            currentEvent = slack1;
        }

        else if (o == "Zoom")
        {
            currentEvent = zoom1;
        }

        for(int i = 0; i < ListEvents.Count; i++)
        {
            Event e = ListEvents[i];
            if (e.Text == currentEvent.Text)
            {
                currentEventIndex = i;
                print("found");
                break;
            }
        }

        choiceButtonPressedBool = true;
        nextButtonPressedBool = true;
    }

    public IEnumerator DisableForAMoment()
    {
        nextButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        nextButton.interactable = true;
    }
}
