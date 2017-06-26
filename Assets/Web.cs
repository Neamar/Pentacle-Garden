using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {
	LineRenderer lr;

	List<Node> nodesInWeb = new List<Node>();
	void Start () {
		lr = gameObject.GetComponent<LineRenderer>();
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			GameManager.instance.currentLevel.DeSelectNode ();
			lr.positionCount = 0;
			nodesInWeb.Clear ();
			return;
		}

		if (nodesInWeb.Count == 0 && GameManager.instance.currentLevel.selectedNode != null) {
			AddHook (GameManager.instance.currentLevel.selectedNode);
		}

		if(nodesInWeb.Count > 0) {
			int length = nodesInWeb.Count + 1;

			// Mouse position
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition.z = 0;

			lr.positionCount = length;
			lr.SetPosition (length - 1, mousePosition);
		}
	}

	void AddHook(Node node) {
		// position: current number of hook +9 the one we're adding + the mouse (so + 2)
		lr.positionCount = nodesInWeb.Count + 2;
		lr.SetPosition (nodesInWeb.Count, node.transform.position);
		nodesInWeb.Add (node);
	}
}
