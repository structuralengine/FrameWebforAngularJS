using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageChanged : MonoBehaviour 
{

    public Image image;

    private InputField inputField;
    private Texture2D texture2D;


	void Start () 
    {
        inputField = GetComponent<InputField>();
        var changeEvent = new InputField.OnChangeEvent();
        changeEvent.AddListener(ChangeImage);
        inputField.onValueChanged = changeEvent;
	}

    //Called when the InputField value has changed
    void ChangeImage(string imgPath)
    {
        StartCoroutine(GetImageFromWWW(imgPath));
    }
    
    //Coroutine that receives an image file path, and creates a sprite with the specified image
    public IEnumerator GetImageFromWWW(string imagePath)
    {
        texture2D = new Texture2D(10, 10);

        WWW auxWWW = new WWW("file://" + imagePath);
        yield return auxWWW;

        auxWWW.LoadImageIntoTexture(texture2D);

        byte[] aux = texture2D.EncodeToPNG();
        Texture2D textAux = new Texture2D(texture2D.width, texture2D.height);
        textAux.LoadImage(aux);

        image.sprite = Sprite.Create(textAux, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }
}
