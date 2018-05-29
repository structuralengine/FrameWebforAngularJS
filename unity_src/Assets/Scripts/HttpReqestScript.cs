using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpReqestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void POSTjson()
    {
		string postURL = "http://localhost:50155/api/values/";
        string jsonString = "sdfgdfg";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("Content-Type", "application/x-www-form-urlencoded");
        post.Add("", jsonString);

        WWW results = POST(postURL, post);
        Debug.Log(results.text);
    }
    
    private WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
