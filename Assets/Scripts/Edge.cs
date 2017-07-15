using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Edge : MonoBehaviour
{
	public Node input;
	public Node output;

	private LineRenderer lr;

    public void FromEditor()
    {
        Start();
    }

	// Use this for initialization
	void Start ()
	{
		lr = gameObject.GetComponent<LineRenderer> ();

		DrawLine (input.gameObject.transform.position, output.gameObject.transform.position);
	}

	void DrawLine (Vector3 start, Vector3 end)
	{
		lr.SetPosition (0, start - Vector3.back);
		lr.SetPosition (1, end - Vector3.back);
	}

    private void Update()
    {
        DrawLine(input.gameObject.transform.position, output.gameObject.transform.position);
    }
}
