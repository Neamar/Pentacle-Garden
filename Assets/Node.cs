using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	private List<Vertex> vertices = new List<Vertex>();

	public void AddVertex(Vertex vertex) {
		if(vertex.input != this && vertex.output != this) {
			throw new UnityException("Vertex shouldn't be added to this node");
		}

		vertices.Add(vertex);
	}
}
