using System;
using UnityEngine;

public class V
{
	public int input;
	public int output;

	public V (int i, int o)
	{
		this.input = i;
		this.output = o;
	}
}

public class LevelData
{
	public string name;
	public Vector2[] nodes;
	public V[] edges;

	public LevelData (string name, Vector2[] n, V[] v)
	{		
		this.name = name; 
		this.nodes = n;
		this.edges = v;
	}

	public static LevelData Get (int n)
	{
		
		String[] levels = {
			"-3,-3;3,-3;1,-3;-1,-3|0,1;1,2;2,3",
			"-3,-3;-3,3;3,3;3,-3;0,-5|0,1;1,2;2,3;3,4;4,0;0,3",
		};

		string currentLevel = levels [n];
		string[] nodesString = currentLevel.Split ('|') [0].Split (';');
		string[] edgesString = currentLevel.Split ('|') [1].Split (';');
		Vector2[] nodes = new Vector2[nodesString.Length];
		V[] edges = new V[edgesString.Length];

		int i;
		for (i = 0; i < nodes.Length; i++) {
			String[] nodeString = nodesString [i].Split (',');
			nodes [i] = new Vector2 (float.Parse (nodeString [0]), float.Parse (nodeString [1]));
		}
			
		for (i = 0; i < edges.Length; i++) {
			String[] edgeString = edgesString [i].Split (',');
			edges [i] = new V (int.Parse (edgeString [0]), int.Parse (edgeString [1]));
		}

		return new LevelData (n.ToString (), nodes, edges);
	}
}