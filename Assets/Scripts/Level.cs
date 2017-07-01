using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {
	public GameObject nodePrefab;
	public GameObject edgePrefab;

	// Current level nodes
	public List<Node> nodes;
	private List<Edge> edges;

	public Node selectedNode;


	public void SetupLevel(int levelNumber) {
		LevelData level = LevelData.Get(levelNumber);

		nodes = new List<Node> ();
		edges = new List<Edge> ();

		foreach(Vector2 vector in level.nodes) {
			GameObject nodeObject = Instantiate (nodePrefab) as GameObject;
			nodeObject.transform.position = new Vector3(vector.x, vector.y, 0);

			Node node = nodeObject.GetComponent<Node> ();
			nodes.Add (node);
		}

		foreach(V vector in level.edges) {
			GameObject edgeObject = Instantiate (edgePrefab) as GameObject;

			Edge edge = edgeObject.GetComponent<Edge> ();
			edge.input = nodes[(int) vector.input];
			edge.output = nodes[(int) vector.output];

			// Add to our global vertices list
			edges.Add (edge);

			// And inform nodes they've been connected
			edge.input.AddEdge (edge);
			edge.output.AddEdge (edge);
		}

		SelectNode (nodes [0]);
	}

	public void SelectNode(Node node) {
		if (selectedNode != null) {
			selectedNode.DeSelectNode ();
		}

		selectedNode = node;
		selectedNode.SelectNode ();
	}

	public void DeSelectNode() {
		if (selectedNode != null) {
			selectedNode.DeSelectNode ();
			selectedNode = null;
		}
	}
}
