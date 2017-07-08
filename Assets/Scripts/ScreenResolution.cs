using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
	private int lastWidth;
	private int lastHeight;

	// Use this for initialization
	void Start ()
	{
		lastWidth = Screen.width;
		lastHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Screen.width != lastWidth || Screen.height != lastHeight) {
			Debug.Log ("Resizing window to " + Screen.width);

			lastWidth = Screen.width;
			lastHeight = Screen.height;
		}
	}
}
