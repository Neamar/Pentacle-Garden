using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	public Material inactiveMaterial;
	public Material activeMaterial;

	private List<Vertex> vertices = new List<Vertex>();

	public void AddVertex(Vertex vertex) {
		if(vertex.input != this && vertex.output != this) {
			throw new UnityException("Vertex shouldn't be added to this node");
		}

		vertices.Add(vertex);
	}

	void OnMouseDown() {
		Debug.Log ("CLICK");
	}

	public void SelectNode() {
		MeshRenderer meshRenderer = transform.Find ("Sphere").GetComponent<MeshRenderer> () as MeshRenderer;
		meshRenderer.material = activeMaterial;
	}

	public void DeSelectNode() {
		MeshRenderer meshRenderer = transform.Find ("Sphere").GetComponent<MeshRenderer> () as MeshRenderer;
		meshRenderer.material = inactiveMaterial;
	}
}
