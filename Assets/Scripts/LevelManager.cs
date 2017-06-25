using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public List<Level> levelList = new List<Level>();

	public GameObject node;
	public GameObject vertex;

	public void SetupLevel(int levelNumber) {
		Level level = levelList [levelNumber];

		List<GameObject> nodes = new List<GameObject> ();
		List<GameObject> vertices = new List<GameObject> ();

		foreach(Vector2 vector in level.nodes) {
			GameObject nodeClone = Instantiate (node) as GameObject;
			nodeClone.transform.position = new Vector3(vector.x, vector.y, 0);
			nodes.Add (nodeClone);
		}

		foreach(Vector2 vector in level.vertices) {
			GameObject vertexClone = Instantiate (vertex) as GameObject;
			VertexDrawer drawer = vertexClone.GetComponent<VertexDrawer> ();
			drawer.input = nodes[(int) vector.x];
			drawer.output = nodes[(int) vector.y];

			vertices.Add (vertexClone);
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