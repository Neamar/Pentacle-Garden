using System;
using UnityEngine;

public class V {
	public int input;
	public int output;

	public V(int i, int o) {
		this.input = i;
		this.output = o;
	}
}
	
public class LevelData
{
	public string name;
	public Vector2[] nodes;
	public V[] vertices;

	public LevelData(string name, Vector2[] n, V[] v) {		
		this.name = name;
		this.nodes = n;
		this.vertices = v;
	}

	public static LevelData Get(int n) {
		switch(n) {
		case 0:
			Vector2[] nodes = {
				new Vector2 (-3, -3),
				new Vector2 (-3, 3),
				new Vector2 (3, 3),
				new Vector2 (3, -3),
				new Vector2 (0, -5),
			};
			V[] vertices = {
				new V (0, 1),
				new V (1, 2),
				new V (2, 3),
				new V (3, 4),
				new V (4, 0),
				new V (0, 3)
			};

			return new LevelData ("0", nodes, vertices);
		default:
			return null;
		}
	}
}