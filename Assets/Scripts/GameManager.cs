using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public int currentLevelNumber = 0;
	public Level currentLevel;

	//Awake is always called before any Start functions
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			// Enforce Singleton pattern
			Destroy (gameObject);	
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		currentLevel = GetComponent<Level> () as Level;

		//Call the InitGame function to initialize the first level 
		InitLevel ();
	}

	void InitLevel ()
	{
		currentLevel.SetupLevel (currentLevelNumber);
	}

	void LevelWon ()
	{
		
	}
}
