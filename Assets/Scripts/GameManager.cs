using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	private LevelManager levelScript;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			// Enforce Singleton pattern
			Debug.Log("Non singleton GameManager created");
			Destroy (gameObject);	
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		levelScript = GetComponent<LevelManager>() as LevelManager;

		//Call the InitGame function to initialize the first level 
		InitLevel();
	}

	void InitLevel() {
		levelScript.SetupLevel (0);
	}
}
