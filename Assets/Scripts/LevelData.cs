﻿using System;
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
			"-3,-3;-3,3;3,3;3,-3;0,-5|0,1;1,2;2,3;3,4;4,0;0,3",
			"5.08,3.75;5.08,6.25;3.86,8.33;3.86,1.67;2.23,2.92;2.23,7.08;-1.42,3.75;-1.42,6.25|6,4;4,5;5,7;7,1;1,0;0,3;3,2;2,1;6,0;6,7",
			"0.00,-8.33;-4.25,-2.83;-2.62,7.42;2.62,7.42;4.25,-2.83;-0.81,0.00;0.81,0.00|0,1;1,2;2,3;3,4;4,0;0,5;0,6;1,6;2,6;3,5;4,5",
			"2.03,-7.21;-2.03,-7.21;-4.06,0.00;-2.03,7.21;2.03,7.21;4.06,0.00;1.02,-3.63;-1.02,-3.63;-2.03,0.00;-1.02,3.63;1.02,3.63;2.03,0.00|0,1;1,2;2,3;3,4;4,5;5,0;6,7;7,8;8,9;9,10;10,11;11,6;0,11;5,6;1,8;2,7;3,10;4,9",
			"1.36,0.92;0.00,2.92;-1.36,0.92;-0.83,-2.38;0.83,-2.38;3.09,2.04;-3.09,2.04;-1.91,-5.38;1.91,-5.38|0,2;2,4;4,1;1,3;3,0;0,5;5,8;8,4;8,7;7,3;7,6;6,2",
			"1.77,2.08;-1.02,3.63;-1.77,-2.08;1.02,-3.63;2.03,7.21;-1.06,8.04;-3.51,4.17;-3.92,-2.17;-2.03,-7.21;1.06,-8.04;3.51,-4.17;3.92,2.17|4,0;0,5;6,1;1,7;8,2;2,9;10,3;3,11;3,1;2,0;7,6;5,4;11,10;9,8;7,8;9,10;11,4",
			"-1.26,-7.92;-4.06,0.00;-1.26,7.92;3.29,4.92;3.29,-4.92;-0.63,-3.96;-2.03,0.00;-0.63,3.96;1.65,2.46;1.65,-2.46;0.00,0.00|0,1;1,2;2,3;3,4;4,0;0,6;0,9;1,5;1,7;2,6;2,8;3,7;3,9;4,5;4,8;5,10;6,10;7,10;8,10;9,10",
			"0.97,0.63;-0.97,0.63;-0.59,-1.67;0.59,-1.67;2.58,5.29;1.32,7.00;-0.18,7.50;-1.67,6.67;-2.84,4.71;-3.53,1.96;-3.62,-1.17;-3.07,-4.08;-1.99,-6.29;-0.57,-7.42;0.95,-7.25;2.30,-5.83;3.25,-3.42;3.66,-0.38;3.41,2.71|0,3;3,2;2,1;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,17;17,18;18,4;4,14;5,10;7,17;8,13;9,1;11,16;12,2;15,3;18,0",
			"3.33,-3.04;2.44,-5.58;1.14,-7.13;-0.39,-7.46;-2.97,-4.42;-3.58,-1.54;-3.58,1.54;-2.97,4.42;-1.83,6.50;-0.39,7.46;1.14,7.13;2.44,5.58;3.33,3.04;3.66,0.00;0.00,0.00|0,1;1,2;2,3;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,0;3,6;5,9;8,12;11,1;13,14;2,14;4,14;7,14;10,14",
			"0.00,-3.33;-1.30,-0.67;1.30,-0.67;0.00,2.00;3.25,-3.33;1.30,-6.67;3.25,-6.67;-1.30,-6.67;-3.25,-6.67;-1.30,5.33;-3.25,2.00;-3.25,5.33;1.30,5.33;3.25,5.33;3.90,-8.00;-3.90,-8.00;-3.90,6.67;3.90,6.67;3.90,2.00;-3.90,-3.33|17,18;18,14;14,15;15,19;19,16;16,17;13,4;4,6;6,5;5,7;7,8;8,10;10,11;11,9;9,12;12,13;9,1;1,7;5,2;2,12;10,3;3,18;19,0;0,4;1,0;3,2;15,8;6,14;13,17;16,11",
			"3.17,-3.75;2.36,-5.75;1.26,-7.04;-1.26,-7.04;-2.36,-5.75;-3.17,-3.75;-3.60,-1.29;-3.60,1.29;-3.17,3.75;-2.36,5.75;-1.26,7.04;0.00,7.50;1.26,7.04;2.36,5.75;3.17,3.75;3.60,1.29;3.60,-1.29;0.00,0.00|0,1;1,2;3,4;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,0;0,17;5,17;2,6;4,9;7,11;8,13;10,14;12,1;3,16",
			"0.97,0.63;0.00,2.08;-0.97,0.63;-0.59,-1.67;0.59,-1.67;2.58,5.29;1.32,7.00;-0.18,7.50;-1.67,6.67;-2.84,4.71;-3.53,1.96;-3.62,-1.17;-3.07,-4.08;-1.99,-6.29;-0.57,-7.42;0.95,-7.25;2.30,-5.83;3.25,-3.42;3.66,-0.38|0,1;1,2;2,3;3,4;4,0;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,17;17,18;5,15;6,11;7,1;8,18;9,14;10,2;12,17;13,3;16,4",
			"0.00,-9.17;-3.88,-4.58;-3.88,4.58;0.00,9.17;3.88,4.58;3.88,-4.58;0.00,-6.25;-2.64,-3.13;-2.64,3.13;0.00,6.25;2.64,3.13;2.64,-3.13;0.00,-3.75;-1.58,-1.88;-1.58,1.88;0.00,3.75;1.58,1.88;1.58,-1.88|0,1;1,2;2,3;3,4;4,5;5,0;0,6;1,7;2,8;3,9;4,10;5,11;12,15;13,16;14,17;6,13;13,8;8,15;15,10;10,17;17,6;11,12;12,7;7,14;14,9;9,16;16,11",
			"3.17,-6.50;0.00,-9.17;-3.17,-6.50;-4.47,0.00;-3.17,6.50;0.00,9.17;3.17,6.50;4.47,0.00;1.91,-5.88;-0.67,-6.96;-2.86,-3.92;-3.39,1.38;-1.91,5.88;0.67,6.96;2.86,3.92;3.39,-1.38;0.93,-4.63;-0.93,-4.63;-2.25,-1.92;-2.25,1.92;-0.93,4.63;0.93,4.63;2.25,1.92;2.25,-1.92|0,1;1,2;2,3;3,4;4,5;5,6;6,7;7,0;0,8;1,9;2,10;3,11;4,12;5,13;6,14;7,15;8,16;9,17;10,18;11,19;12,20;13,21;14,22;15,23;16,19;19,22;22,17;17,20;20,23;23,18;18,21;21,16",
			"-1.93,-8.75;1.93,-8.75;-1.32,-8.13;1.32,-8.13;-2.13,-7.50;2.13,-7.50;-0.51,-6.88;0.51,-6.88;0.00,-7.71;-0.51,-6.04;0.51,-6.04;-2.13,-5.21;2.13,-5.21;-1.22,-4.58;1.22,-4.58;-1.93,-3.96;1.93,-3.96;-1.63,-2.92;1.63,-2.92;-1.93,-1.67;1.93,-1.67;-2.97,8.67;-4.90,1.83;-3.01,7.29;-4.33,2.58;-2.36,8.42;-4.49,0.83;-2.90,5.21;-3.41,3.42;-3.49,4.71;-2.54,4.79;-3.05,3.00;-1.38,7.25;-3.51,-0.29;-1.56,5.33;-2.78,1.00;-0.95,6.29;-2.88,-0.58;-0.67,5.21;-2.30,-0.54;0.02,5.13;4.90,1.83;2.97,8.67;4.33,2.58;3.01,7.29;4.49,0.83;2.36,8.42;3.41,3.42;2.90,5.21;3.49,4.75;3.05,3.00;2.54,4.79;3.51,-0.29;1.38,7.25;2.78,1.00;1.56,5.33;2.88,-0.58;0.95,6.29;2.30,-0.54|0,1;0,2;0,4;1,3;1,5;2,6;2,13;3,7;3,14;4,9;4,11;5,10;5,12;6,8;6,11;7,8;7,12;8,9;8,10;9,13;10,14;11,15;12,16;13,15;14,16;15,17;16,18;17,19;18,20;15,16;17,18;19,20;21,22;21,23;21,25;22,24;22,26;23,27;23,34;24,28;24,35;25,30;25,32;26,31;26,33;27,29;27,32;28,29;28,33;29,30;29,31;30,34;31,35;32,36;33,37;34,36;35,37;36,38;37,39;38,40;39,19;36,37;38,39;19,40;41,42;41,43;41,45;42,44;42,46;43,47;43,54;44,48;44,55;45,50;45,52;46,51;46,53;47,49;47,52;48,49;48,53;49,50;49,51;50,54;51,55;52,56;53,57;54,56;55,57;56,58;56,57;58,20;20,40;21,42;22,0;1,41",
			"4.39,-1.79;4.12,-3.50;3.72,-5.08;3.17,-6.50;2.48,-7.63;1.71,-8.46;0.87,-9.00;0.00,-9.17;-0.87,-9.00;-1.71,-8.46;-2.48,-7.63;-3.17,-6.50;-3.72,-5.08;-4.12,-3.50;-4.39,-1.79;-4.47,0.00;-4.39,1.79;-4.12,3.50;-3.72,5.08;-3.17,6.50;-2.48,7.63;-1.71,8.46;-0.87,9.00;0.00,9.17;0.87,9.00;1.71,8.46;2.48,7.63;3.17,6.50;3.72,5.08;4.12,3.50;4.39,1.79;4.47,0.00|2,0;4,3;5,2;6,1;7,0;8,6;9,5;10,4;11,3;12,2;12,11;13,1;13,10;14,0;14,9;15,8;16,7;17,6;18,5;18,16;19,4;19,15;20,3;20,14;21,2;21,13;22,1;22,12;23,0;23,11;24,10;24,23;25,9;25,22;26,8;26,21;27,7;27,20;28,6;28,19;29,5;29,18;30,4;30,17;31,3;31,16",

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