using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public List<Level> levelList = new List<Level>();

	public GameObject nodePrefab;
	public GameObject vertexPrefab;

	// Current level nodes
	private List<Node> nodes;
	private List<Vertex> vertices;


	public void SetupLevel(int levelNumber) {
		Level level = levelList [levelNumber];

		nodes = new List<Node> ();
		vertices = new List<Vertex> ();

		foreach(Vector2 vector in level.nodes) {
			GameObject nodeObject = Instantiate (nodePrefab) as GameObject;
			nodeObject.transform.position = new Vector3(vector.x, vector.y, 0);

			Node node = nodeObject.GetComponent<Node> ();
			nodes.Add (node);
		}

		foreach(Vector2 vector in level.vertices) {
			GameObject vertexObject = Instantiate (vertexPrefab) as GameObject;

			Vertex vertex = vertexObject.GetComponent<Vertex> ();
			vertex.input = nodes[(int) vector.x];
			vertex.output = nodes[(int) vector.y];

			// Add to our global vertices list
			vertices.Add (vertex);

			// And inform nodes they've been connected
			vertex.input.AddVertex (vertex);
			vertex.output.AddVertex (vertex);
		}
	}
}


[System.Serializable]
public class Level
{
	public string name;
	public Vector2[] nodes;
	public Vector2[] vertices;
}