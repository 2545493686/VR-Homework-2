using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackgroud : MonoBehaviour {
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.clearFlags = CameraClearFlags.Color;
        Camera.main.backgroundColor = Color.gray;
        Destroy(this);
    }
}
