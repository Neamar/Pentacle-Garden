using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexDrawer : MonoBehaviour {
	public GameObject input;
	public GameObject output;

	private LineRenderer lr;
	// Use this for initialization
	void Start () {
		lr = gameObject.GetComponent<LineRenderer>();

		DrawLine (input.transform.position, output.transform.position, Color.blue);
	}

	void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}
}
