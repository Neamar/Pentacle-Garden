using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {
	public GameObject nodePrefab;
	public GameObject vertexPrefab;

	// Current level nodes
	private List<Node> nodes;
	private List<Vertex> vertices;

	public Node selectedNode;


	public void SetupLevel(int levelNumber) {
		LevelData level = LevelData.Get(levelNumber);

		nodes = new List<Node> ();
		vertices = new List<Vertex> ();

		foreach(Vector2 vector in level.nodes) {
			GameObject nodeObject = Instantiate (nodePrefab) as GameObject;
			nodeObject.transform.position = new Vector3(vector.x, vector.y, 0);

			Node node = nodeObject.GetComponent<Node> ();
			nodes.Add (node);
		}

		foreach(V vector in level.vertices) {
			GameObject vertexObject = Instantiate (vertexPrefab) as GameObject;

			Vertex vertex = vertexObject.GetComponent<Vertex> ();
			vertex.input = nodes[(int) vector.input];
			vertex.output = nodes[(int) vector.output];

			// Add to our global vertices list
			vertices.Add (vertex);

			// And inform nodes they've been connected
			vertex.input.AddVertex (vertex);
			vertex.output.AddVertex (vertex);
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