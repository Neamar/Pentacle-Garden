using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public int currentLevelNumber = 0;
	public Level currentLevel;
	public Web web;

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
	}

	void InitLevel ()
	{
		Debug.Log ("InitLevel " + currentLevelNumber);
		currentLevel.SetupLevel (currentLevelNumber);
	}

	public void LevelWon ()
	{
		currentLevelNumber += 1;
		Debug.Log ("Setting up level " + currentLevelNumber);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex, LoadSceneMode.Single);
	}

	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
	{
		web.ResetWeb ();
		//Call InitGame to initialize our level.
		InitLevel ();
	}

	void OnEnable ()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable ()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
		//Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
}
