using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR && UNITY_WEBGL
        UnityEngine.WebGLInput.captureAllKeyboardInput = false;
#endif
    }

    // Update is called once per frame
    void Update () {
		
	}

}
