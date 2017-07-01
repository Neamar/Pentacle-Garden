using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
	LineRenderer lr;
	float lastAngle = 0;

	Node currentNode;
	Vector3 currentNodePosition;
	float squaredDistanceToMouse;
	float mouseAngle;

	// Which nodes are currently part of our web?
	List<Node> nodesInWeb = new List<Node> ();

	// Which direction did we hook the nodes? +1 => trigonometric, -1 => clockwise, 0: can't unhook
	List<int> hookDirections = new List<int> ();

	List<Node> nodesSortedBySquaredDistanceToCurrent = new List<Node> ();

	void Start ()
	{
		lr = gameObject.GetComponent<LineRenderer> ();
	}

	void UpdateState (Vector3 mousePosition)
	{
		currentNode = LastNode ();
		squaredDistanceToMouse = currentNode.distanceTo (mousePosition);
		currentNodePosition = currentNode.transform.position;
		mouseAngle = GetAngle (currentNodePosition, mousePosition);
	}

	int Sorter (Node n1, Node n2)
	{
		float n1Distance = currentNode.distanceTo (n1);
		float n2Distance = currentNode.distanceTo (n2);

		return (int)(n1Distance - n2Distance);
	}

	void SortNodeList ()
	{
		nodesSortedBySquaredDistanceToCurrent.Clear ();
		foreach (Node node in GameManager.instance.currentLevel.nodes) {
			nodesSortedBySquaredDistanceToCurrent.Add (node);
		}

		nodesSortedBySquaredDistanceToCurrent.Sort (Sorter);
	}

	void RestartLevel ()
	{
		GameManager.instance.currentLevel.DeSelectNode ();
		foreach (Node node in GameManager.instance.currentLevel.nodes) {
			node.DeSelectNode ();
		}
		lr.positionCount = 0;
		nodesInWeb.Clear ();
		hookDirections.Clear ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Mouse position
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;

		// Do we need to restart?
		if (Input.GetKey (KeyCode.R)) {
			RestartLevel ();
		}

		if (nodesInWeb.Count == 0) {
			if (GameManager.instance.currentLevel.selectedNode != null) {
				AddHook (GameManager.instance.currentLevel.selectedNode, 0);
				currentNode = LastNode ();
				SortNodeList ();
			} else {
				// Nothing to do, nothing selected
				return;
			}
		}

		UpdateState (mousePosition);

		float minAngle = Mathf.Min (lastAngle, mouseAngle);
		float maxAngle = Mathf.Max (lastAngle, mouseAngle);
		int direction = (int)Mathf.Sign (mouseAngle - lastAngle);

		// Do we need to add another hook?
		if (minAngle < -Mathf.PI + 1 && maxAngle > Mathf.PI - 1) {
			// Mouse turned around the [π, -π] point, we need to switch values
			float swap = minAngle;
			minAngle = maxAngle;
			maxAngle = swap + 2 * Mathf.PI;
			direction = -direction;
		}

		bool hookChanged = false;
		foreach (Node node in nodesSortedBySquaredDistanceToCurrent) {
			if (node == currentNode) {
				continue;
			}


			Vector3 nodePosition = node.transform.position;
			float squaredDistanceToNode = currentNode.distanceTo (node);

			if (squaredDistanceToMouse > squaredDistanceToNode) {
				float nodesAngle = GetAngle (currentNodePosition, nodePosition);

				if (nodesAngle >= minAngle && nodesAngle <= maxAngle) {
					// Hooked!
					AddHook (node, direction);
					UpdateState (mousePosition);
					hookChanged = true;
				}
			}
		}

		// Do we need to unhook?
		while (nodesInWeb.Count > 1) {
			Node previousNode = nodesInWeb [nodesInWeb.Count - 2];
			float lastHookingAngle = GetAngle (previousNode.transform.position, currentNodePosition);
			int lastDirection = hookDirections [hookDirections.Count - 1];

			if (minAngle <= lastHookingAngle && maxAngle >= lastHookingAngle && direction != lastDirection) {
				// Unhook!
				RemoveHook ();
				UpdateState (mousePosition);
				hookChanged = true;
			} else {
				break;
			}
		}
			
		if (hookChanged) {
			SortNodeList ();
		}

		lastAngle = mouseAngle;

		int length = nodesInWeb.Count + 1;
		lr.positionCount = length;
		lr.SetPosition (length - 1, mousePosition);
	}

	void AddHook (Node node, int direction)
	{
		// position: current number of hook + the one we're adding + the mouse (so + 2)
		lr.positionCount = nodesInWeb.Count + 2;
		lr.SetPosition (nodesInWeb.Count, node.transform.position);
		nodesInWeb.Add (node);
		node.SelectNode ();
		hookDirections.Add (direction);
	}

	// Remove the last added hook
	void RemoveHook ()
	{
		Node lastNode = nodesInWeb [nodesInWeb.Count - 1];
		lastNode.DeSelectNode ();

		nodesInWeb.RemoveAt (nodesInWeb.Count - 1);
		hookDirections.RemoveAt (hookDirections.Count - 1);
		
		// position: current number of hook + the mouse (so + 1)
		lr.positionCount = nodesInWeb.Count + 1;
	}

	float GetAngle (Vector3 p1, Vector3 p2)
	{
		float angle = Mathf.Atan2 (p2.y - p1.y, p2.x - p1.x);
		return angle;
	}

	Node LastNode ()
	{
		return nodesInWeb [nodesInWeb.Count - 1];
	}
}
