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

			Debug.Log ("New viewport: " + GetCurrentViewport ());

			GameManager.instance.LoadCurrentLevelAsScene ();
			lastWidth = Screen.width;
			lastHeight = Screen.height;
		}
	}

	public static Rect GetCurrentViewport ()
	{
		Vector3 origin = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		return new Rect (origin.x, origin.y, bounds.x, bounds.y);
	}
}
