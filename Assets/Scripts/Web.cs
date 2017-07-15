using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A custom implementation of the Gift wrapping algorithm
 * https://en.wikipedia.org/wiki/Gift_wrapping_algorithm
 * Follows the mouse cursor, and wraps a virtual rope around the level's vertices
 * Lets you "hook" a node (add a node to the current web) and "unhook" the last one
 * provided your cursor is going in the opposite direction that was used to "hook" something
 * 
 * To ensure the algorithm behaves correctly, a couple thingsd are required:
 * * angle are all given mod 2π, but when your mouse wraps around (at -π), special code is required to ensure all the angles are given mod 2π
 * * you can hook multiple nodes at once. This complexifies the algorihm, but is required
 * as having multiple nodes in a flat line is fairly common. For the same reason, you can unhook multiple nodes at once. Multiple hooking
 * requires ordering by distance, as you need to add the nodes in order to the web lest you miss some.
 * * Hooking / unhooking an angle requires to recompute the current values (angles between new last node and mouse), as keeping the old value
 * will lead to unexpected behavior.
 */
public class Web : MonoBehaviour
{
	
	LineRenderer lr;

	bool hasWon = false;

	// Last known angle, will be used to compare with the new angle
	float lastAngle = 0;

	// those values are updated by UpdateState() every time a node is added or removed,
	// and on every frame.
	// currentNode is the last node that was added, the one currently linked to the cursor
	// squaredDistanceToMouse should be self explanatory -- in this code, all the distances are squared to avoid ocmputing a costly square root
	// (we never need a absolute distance, just a relative comparison, and since distances are positive a > b means a² > b²)
	Node currentNode;
	Vector3 currentNodePosition;
	float squaredDistanceToMouse;
	float mouseAngle;

	// Which nodes are currently part of our web? ([0] being the first node, [Count -1] the currentNode
	List<Node> nodesInWeb = new List<Node> ();

	// Which direction did we hook the nodes? +1 => trigonometric, -1 => clockwise, 0: can't unhook (first node)
	// Indices are linked with the indices in nodesInWeb
	List<int> hookDirections = new List<int> ();

	// Nodes in a straight line should all be hooked simultaneously, and not one by one
	// To achieve this, you need to cycle through nodes starting with the closest one,
	// otherwise you're likely to "hook" the furthest one which will disqualify everything else.
	List<Node> nodesSortedBySquaredDistanceToCurrent = new List<Node> ();

	void Start ()
	{
		lr = gameObject.GetComponent<LineRenderer> ();
	}

	void ToNextLevel ()
	{
		GameManager.instance.LevelWon ();
	}

	// Ensure state is up to date.
	// This function will be called every frame, but also every time we add or remove a hook.
	void UpdateState (Vector3 mousePosition)
	{
		currentNode = LastNode ();
		squaredDistanceToMouse = currentNode.distanceTo (mousePosition);
		currentNodePosition = currentNode.transform.position;
		mouseAngle = GetAngle (currentNodePosition, mousePosition);
	}

	// Comparison<T> Delegate to sort the nodes by distance to the current one.
	int Sorter (Node n1, Node n2)
	{
		float n1Distance = currentNode.distanceTo (n1);
		float n2Distance = currentNode.distanceTo (n2);

		return (int)(n1Distance - n2Distance);
	}

	// Reorder nodes based on their distance to the current node
	void SortNodeList ()
	{
		nodesSortedBySquaredDistanceToCurrent.Sort (Sorter);
	}

	// Completely restarts the web,
	// Deselects everything, empty all arrays
	// and undraw everything.
	void RestartWeb ()
	{
		GameManager.instance.currentLevel.DeSelectNode ();
		foreach (Node node in GameManager.instance.currentLevel.nodes) {
			node.DeSelectNode ();
		}
		ResetWeb ();
	}

	// Resets the web to be used for another level (or the same one reloaded)
	public void ResetWeb ()
	{
		nodesInWeb.Clear ();
		hookDirections.Clear ();
		hasWon = false;

		if (lr != null) {
			lr.positionCount = 0;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (hasWon) {
			return;
		}

		// Mouse position
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;

		// Do we need to restart?
		if (Input.GetKey (KeyCode.R)) {
			RestartWeb ();
		}

		// Are we cheating?
		if (Input.GetKey (KeyCode.A)) {
			ToNextLevel ();
			return;
		}
		// First time initialization (or restart)
		if (nodesInWeb.Count == 0) {
			if (GameManager.instance.currentLevel.selectedNode == null) {
				// Level not started yet. Do nothing.
				return;
			}

			// Add our first hook. Direction 0 means it can't be removed.
			AddHook (GameManager.instance.currentLevel.selectedNode, 0);
			currentNode = LastNode ();

			// Add all nodes, not ordered for now.
			nodesSortedBySquaredDistanceToCurrent.Clear ();
			foreach (Node node in GameManager.instance.currentLevel.nodes) {
				nodesSortedBySquaredDistanceToCurrent.Add (node);
			}
			SortNodeList ();
		}

		// Update angles and distance based on current mouse position
		UpdateState (mousePosition);

		float minAngle = Mathf.Min (lastAngle, mouseAngle);
		float maxAngle = Mathf.Max (lastAngle, mouseAngle);
		int direction = (int)Mathf.Sign (mouseAngle - lastAngle);

		// Tiny bit of magic, used when your mouse wraps around the -π / π point.
		// In this case, we invert min and max and add 2-π.
		// Note this isn't perfect, as it means nodes with a small and negative Y won't "hook".
		// However, it works well enough for straight lines scenarios, or cases where you get the mouse position every few milliseconds.
		if (minAngle < -Mathf.PI + 1 && maxAngle > Mathf.PI - 1) {
			// Mouse turned around the [π, -π] point, we need to switch values
			float swap = minAngle;
			minAngle = maxAngle;
			maxAngle = swap + 2 * Mathf.PI;
			direction = -direction;
		}

		// Flag, set to true if we add or remove a hook (which means we'll have to recompute the nodesSortedBySquaredDistanceToCurrent list)
		bool hookChanged = false;

		// For each node, sorted by distance to the current one, check if it needs to be added.
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

			// Check for victory
			if (nodesInWeb.Count == GameManager.instance.currentLevel.nodes.Count + 1 && nodesInWeb [0] == nodesInWeb [nodesInWeb.Count - 1]) {
				// Simple heuristic passed, ensure we have each node once
				hasWon = true;
				HashSet<Node> hashSet = new HashSet<Node> ();
				for (int i = 0; i < nodesInWeb.Count - 1; i++) {
					Node node = nodesInWeb [i];
					Node nextNode = nodesInWeb [i + 1];

					if (!node.isAdjacentTo (nextNode)) {
						hasWon = false;
						break;
					}
					hashSet.Add (node);
				}

				if (hashSet.Count != GameManager.instance.currentLevel.nodes.Count) {
					hasWon = false;
				}

				if (hasWon) {
					// +1, because it's a cycle and the first node appears twice in our drawing
					lr.positionCount = hashSet.Count + 1;
					Invoke ("ToNextLevel", 1);

					return;
				}
			}
		}

		// Store currentAngle as lastAngle, which means next frame we'll start from there.
		lastAngle = mouseAngle;

		// Also draw a line to the mouse pointer
		int length = nodesInWeb.Count + 1;
		lr.positionCount = length;
		lr.SetPosition (length - 1, mousePosition);
	}

	void AddHook (Node node, int direction)
	{
		// position: current number of hook + the one we're adding + the mouse (so + 2)
		lr.positionCount = nodesInWeb.Count + 2;
		lr.SetPosition (nodesInWeb.Count, node.transform.position);

        if(nodesInWeb.Contains(node))
        {
            node.ErroredNode();
        }
        else
        {
            node.SelectNode();
        }
		nodesInWeb.Add (node);

		hookDirections.Add (direction);
	}

	// Remove the last added hook
	void RemoveHook ()
	{
		Node lastNode = nodesInWeb [nodesInWeb.Count - 1];

		nodesInWeb.RemoveAt (nodesInWeb.Count - 1);
		hookDirections.RemoveAt (hookDirections.Count - 1);

        if (!nodesInWeb.Contains(lastNode))
        {
            // Deselect node only if it's not in our list more than once
            lastNode.DeSelectNode();
        }
        else if (nodesInWeb.IndexOf(lastNode) == nodesInWeb.LastIndexOf(lastNode))
        {
            lastNode.SelectNode();
        }
        else
        {
            Debug.Log("IO:" + nodesInWeb.IndexOf(lastNode) + " LIO" + nodesInWeb.LastIndexOf(lastNode));
            lastNode.ErroredNode();
        }

        // position: current number of hook + the mouse (so + 1)
        lr.positionCount = nodesInWeb.Count + 1;
	}

	// Return the angle between two specified vectors, value between ]-π,π]
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
