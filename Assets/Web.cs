using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {
	LineRenderer lr;
	void Start () {
		lr = gameObject.GetComponent<LineRenderer>();

	}

	
	// Update is called once per frame
	void Update () {

		Node node = GameManager.instance.currentLevel.selectedNode;

		if (node != null) {
			Vector3 start = node.gameObject.transform.position;
			Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mouse.z = 0;
			lr.SetPosition (0, start);
			lr.SetPosition (1, mouse);
		}
	}
}
