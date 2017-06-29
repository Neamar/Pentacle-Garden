using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {
	public Node input;
	public Node output;

	private LineRenderer lr;
	// Use this for initialization
	void Start () {
		lr = gameObject.GetComponent<LineRenderer>();

		DrawLine (input.gameObject.transform.position, output.gameObject.transform.position, Color.blue);
	}

	void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}
}
