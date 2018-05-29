using UnityEngine;
using UnityEngine.UI;

public class AudioChanged : MonoBehaviour {

    public Button playButton;
    
    private InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();
        var changeEvent = new InputField.OnChangeEvent();
        changeEvent.AddListener(ChangeAudio);
        inputField.onValueChanged = changeEvent;
    }

    //Called when the InputField value has changed, and make playButton interactable
    void ChangeAudio(string audioPath)
    {
        if (playButton.interactable == false)
            playButton.interactable = true;
    }
}
