using UnityEngine;

public class CrossSceneStorage : MonoBehaviour
{
    private static CrossSceneStorage css;
    public int numberBuiltInOutputChannel;
    public int numberAvalokiMicrophoneOutputChannel;
    public string[] totalVoicesList;
    public string[] totalAudioList;

    // > to make something have crossscenestorage
    // > before the start function
    // public CrossSceneStorage css;
    // > in the start function
    // css = FindFirstObjectByType<CrossSceneStorage>();
    // :]


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (css != null && css != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            css = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
