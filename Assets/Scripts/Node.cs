using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public Material inactiveMaterial;
	public Material activeMaterial;

	public HashSet<Node> adjacent = new HashSet<Node> ();

	public void AddEdge (Edge edge)
	{
		if (edge.input != this && edge.output != this) {
			throw new UnityException ("Edge shouldn't be added to this node");
		}

		adjacent.Add (edge.input == this ? edge.output : edge.input);
	}

	public bool isAdjacentTo (Node node)
	{
		return adjacent.Contains (node);
	}

	void OnMouseDown ()
	{
		Debug.Log ("CLICK");
	}

	public void SelectNode ()
	{
        SpriteRenderer spriteRenderer = transform.Find ("node").GetComponent<SpriteRenderer> () as SpriteRenderer;
        spriteRenderer.color = Color.red;
	}

	public void DeSelectNode ()
	{
        SpriteRenderer spriteRenderer = transform.Find("node").GetComponent<SpriteRenderer>() as SpriteRenderer;
        spriteRenderer.color = Color.white;
	}

	public float distanceTo (Node node)
	{
		return distanceTo (node.transform.position);
	}

	public float distanceTo (Vector3 point)
	{
		return (this.transform.position.x - point.x) * (this.transform.position.x - point.x) + (this.transform.position.y - point.y) * (this.transform.position.y - point.y);
	}
}
