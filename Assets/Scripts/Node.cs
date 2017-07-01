using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public Material inactiveMaterial;
	public Material activeMaterial;

	private List<Edge> edges = new List<Edge> ();

	public void AddEdge (Edge edge)
	{
		if (edge.input != this && edge.output != this) {
			throw new UnityException ("Edge shouldn't be added to this node");
		}

		edges.Add (edge);
	}

	void OnMouseDown ()
	{
		Debug.Log ("CLICK");
	}

	public void SelectNode ()
	{
		MeshRenderer meshRenderer = transform.Find ("Sphere").GetComponent<MeshRenderer> () as MeshRenderer;
		meshRenderer.material = activeMaterial;
	}

	public void DeSelectNode ()
	{
		MeshRenderer meshRenderer = transform.Find ("Sphere").GetComponent<MeshRenderer> () as MeshRenderer;
		meshRenderer.material = inactiveMaterial;
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
