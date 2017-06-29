using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {
	LineRenderer lr;
	float lastAngle = 0;
	bool cleared = false;

	Node currentNode;
	Vector3 currentNodePosition;
	float squaredDistanceToMouse;
	float mouseAngle;

	// Which nodes are currently part of our web?
	List<Node> nodesInWeb = new List<Node>();

	// Which direction did we hook the nodes? +1 => trigonometric, -1 => clockwise, 0: can't unhook
	List<int> hookDirections = new List<int>();

	void Start () {
		lr = gameObject.GetComponent<LineRenderer>();
	}

	void UpdateState(Vector3 mousePosition) {
		currentNode = LastNode ();
		currentNodePosition = currentNode.transform.position;
		squaredDistanceToMouse = (currentNodePosition.x - mousePosition.x) * (currentNodePosition.x - mousePosition.x) + (currentNodePosition.y - mousePosition.y) * (currentNodePosition.y - mousePosition.y);
		mouseAngle = GetAngle (currentNodePosition, mousePosition);
	}
	
	// Update is called once per frame
	void Update () {
		// Mouse position
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;

		// Do we need to restart?
		if (Input.GetKey (KeyCode.R)) {
			GameManager.instance.currentLevel.DeSelectNode ();
			lr.positionCount = 0;
			nodesInWeb.Clear ();
			return;
		}

		// Temporary cheat
		if (Input.GetKey (KeyCode.Space) && nodesInWeb.Count > 1) {
			if (!cleared) {
				Debug.Log ("Removing hook");
				RemoveHook ();
				// Update last known angle
				lastAngle = GetAngle (LastNode().transform.position, mousePosition);

				cleared = true;
				return;
			}
		}
		else {
			cleared = false;
		}

		if (nodesInWeb.Count == 0) {
			if (GameManager.instance.currentLevel.selectedNode != null) {
				AddHook (GameManager.instance.currentLevel.selectedNode, 0);
			} else {
				// Nothing to do, nothing selected
				return;
			}
		}


		UpdateState (mousePosition);

		float minAngle = Mathf.Min(lastAngle, mouseAngle);
		float maxAngle = Mathf.Max(lastAngle, mouseAngle);
		int direction = (int) Mathf.Sign(mouseAngle - lastAngle);

		// Do we need to add another hook?
		if (minAngle < -Mathf.PI + 1 && maxAngle > Mathf.PI - 1) {
			// Mouse turned around the [π, -π] point, we need to switch values
			float swap = minAngle;
			minAngle = maxAngle;
			maxAngle = swap + 2 * Mathf.PI;
			direction = -direction;
		}

		foreach (Node node in GameManager.instance.currentLevel.nodes) {
			if (node == currentNode) {
				continue;
			}


			Vector3 nodePosition = node.transform.position;
			float squaredDistanceToNode =  (currentNodePosition.x - nodePosition.x) * (currentNodePosition.x - nodePosition.x) + (currentNodePosition.y - nodePosition.y) * (currentNodePosition.y - nodePosition.y);


			if (squaredDistanceToMouse > squaredDistanceToNode) {
				float nodesAngle = GetAngle (currentNodePosition, nodePosition);

				if (nodesAngle >= minAngle && nodesAngle <= maxAngle) {
					// Hooked!
					AddHook(node, direction);
					UpdateState (mousePosition);
					break;
				}
			}
		}

		// Do we need to unhook?
		while(nodesInWeb.Count > 1) {
			Node previousNode = nodesInWeb [nodesInWeb.Count - 2];
			float lastHookingAngle = GetAngle (previousNode.transform.position, currentNodePosition);
			int lastDirection = hookDirections [hookDirections.Count - 1];

			if (minAngle <= lastHookingAngle && maxAngle >= lastHookingAngle && direction != lastDirection) {
				// Unhook!
				RemoveHook ();

				UpdateState (mousePosition);
			} else {
				break;
			}
		}

		lastAngle = mouseAngle;

		int length = nodesInWeb.Count + 1;
		lr.positionCount = length;
		lr.SetPosition (length - 1, mousePosition);
	}

	void AddHook(Node node, int direction) {
		// position: current number of hook + the one we're adding + the mouse (so + 2)
		lr.positionCount = nodesInWeb.Count + 2;
		lr.SetPosition (nodesInWeb.Count, node.transform.position);
		nodesInWeb.Add (node);
		hookDirections.Add (direction);
	}

	// Remove the last added hook
	void RemoveHook() {
		nodesInWeb.RemoveAt (nodesInWeb.Count - 1);
		hookDirections.RemoveAt (hookDirections.Count - 1);

		// position: current number of hook + the mouse (so + 1)
		lr.positionCount = nodesInWeb.Count + 1;
	}

	float GetAngle(Vector3 p1, Vector3 p2) {
		float angle = Mathf.Atan2 (p2.y - p1.y, p2.x - p1.x);
		return angle;
	}

	Node LastNode() {
		return nodesInWeb [nodesInWeb.Count - 1];
	}
}
