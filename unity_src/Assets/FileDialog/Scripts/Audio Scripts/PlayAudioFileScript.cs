using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudioFileScript : MonoBehaviour
{
    public InputField inputField;
    public AudioSource audioSource;
    
    void Start () 
    {
        //Adding an "On Click" listener so when you click/press it, it will call GetAudioFromWWWAndPlaySound(string audioPath) method
        GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(GetAudioFromWWWAndPlaySound(inputField.text)); });
	}

    //Coroutine that receives an audio file path, attributes the audio to the AudioSource clip and then plays it
    public IEnumerator GetAudioFromWWWAndPlaySound(string audioPath)
    {
        WWW auxWWW = new WWW("file://" + audioPath);
        yield return auxWWW;
        
        //audioSource.clip = auxWWW.audioClip;
        audioSource.Play();
    }
}
