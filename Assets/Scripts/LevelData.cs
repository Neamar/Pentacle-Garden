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

	public static LevelData Get (int n, Rect currentViewport)
	{

		Rect defaultViewport = new Rect (-10, -10f, 20, 20);

		Debug.Log (currentViewport);
		String[] levels = {
			// House
			"-4.5,-3;-4.5,5;4.5,5;4.5,-3;0,-6|0,1;1,2;2,3;3,4;4,0;0,3",
			// David star
			"6.00,0.00;1.85,5.71;-4.85,3.53;-4.85,-3.53;1.85,-5.71|0,2;0,3;1,3;1,4;2,4",
			// Arrow / crossbow
			"-6,2;-6,-2;-3,5;-3,-5;3,5;3,-5;6,2;6,-2|0,1;0,2;0,6;1,3;1,7;2,3;4,5;4,6;5,7;6,7",
			// African art
			"0.00,-6.25;-6.53,-2.13;-4.03,5.56;4.03,5.56;6.53,-2.13;-2.5,1.00;2.5,1.00|0,1;1,2;2,3;3,4;4,0;0,5;0,6;1,6;2,6;3,5;4,5",
			// "Google Chrome"
			"3.13,-5.41;-3.13,-5.41;-6.25,0.00;-3.13,5.41;3.13,5.41;6.25,0.00;1.56,-2.72;-1.56,-2.72;-3.13,0.00;-1.56,2.72;1.56,2.72;3.13,0.00|0,1;1,2;2,3;3,4;4,5;5,0;6,7;7,8;8,9;9,10;10,11;11,6;0,11;5,6;1,8;2,7;3,10;4,9",
			// Embedded David Star
			"3.09,1.69;0.00,4.19;-3.09,1.69;-2.28,-2.78;2.28,-2.78;5.75,2.53;-5.75,2.53;-3.94,-5.03;3.94,-5.03|0,2;2,4;4,1;1,3;3,0;0,5;5,8;8,4;8,7;7,3;7,6;6,2",
			// Jerusalem cross
			"2.72,1.56;-1.56,2.72;-2.72,-1.56;1.56,-2.72;3.13,5.41;-1.63,6.03;-5.41,3.13;-6.03,-1.63;-3.13,-5.41;1.63,-6.03;5.41,-3.13;6.03,1.63|4,0;0,5;6,1;1,7;8,2;2,9;10,3;3,11;3,1;2,0;7,6;5,4;11,10;9,8;7,8;9,10;11,4;5,6",
			// Jerusalem cross tweaked
			"2.72,1.56;-1.56,2.72;-2.72,-1.56;1.56,-2.72;3.13,5.41;-1.63,6.03;-5.41,3.13;-6.03,-1.63;-3.13,-5.41;1.63,-6.03;5.41,-3.13;6.03,1.63|4,0;0,5;6,1;1,7;8,2;2,9;10,3;3,11;3,1;2,0;7,6;5,4;11,10;9,8;7,8;9,10;11,4",
			// Helix in pentacle
			"-1.94,-5.94;-6.25,0.00;-1.94,5.94;5.06,3.69;5.06,-3.69;-0.97,-2.97;-3.13,0.00;-0.97,2.97;2.53,1.84;2.53,-1.84;0.00,0.00|0,1;1,2;2,3;3,4;4,0;0,6;0,9;1,5;1,7;2,6;2,8;3,7;3,9;4,5;4,8;5,10;6,10;7,10;8,10;9,10",
			// "La grande ourse" constellation
			"1.50,0.47;-1.50,0.47;-0.91,-1.25;0.91,-1.25;3.97,3.97;2.03,5.25;-0.28,5.63;-2.56,5.00;-4.38,3.53;-5.44,1.47;-5.56,-0.88;-4.72,-3.06;-3.06,-4.72;-0.88,-5.56;1.47,-5.44;3.53,-4.38;5.00,-2.56;5.63,-0.28;5.25,2.03|0,3;3,2;2,1;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,17;17,18;18,4;4,14;5,10;7,17;8,13;9,1;11,16;12,2;15,3;18,0",
			// Driving wheel?
			"5.13,-2.28;3.75,-4.19;1.75,-5.34;-0.59,-5.59;-4.56,-3.31;-5.50,-1.16;-5.50,1.16;-4.56,3.31;-2.81,4.88;-0.59,5.59;1.75,5.34;3.75,4.19;5.13,2.28;5.63,0.00;0.00,0.00|0,1;1,2;2,3;3,4;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,0;3,6;5,9;8,12;11,1;13,14;2,14;4,14;7,14;10,14",
			"0.00,-2.50;-2.00,-0.50;2.00,-0.50;0.00,1.50;5.00,-2.50;2.00,-5.00;5.00,-5.00;-2.00,-5.00;-5.00,-5.00;-2.00,4.00;-5.00,1.50;-5.00,4.00;2.00,4.00;5.00,4.00;6.00,-6.00;-6.00,-6.00;-6.00,5.00;6.00,5.00;6.00,1.50;-6.00,-2.50|17,18;18,14;14,15;15,19;19,16;16,17;13,4;4,6;6,5;5,7;7,8;8,10;10,11;11,9;9,12;12,13;9,1;1,7;5,2;2,12;10,3;3,18;19,0;0,4;1,0;3,2;15,8;6,14;13,17;16,11",
			"4.88,-2.81;3.63,-4.31;1.94,-5.28;-1.94,-5.28;-3.63,-4.31;-4.88,-2.81;-5.53,-0.97;-5.53,0.97;-4.88,2.81;-3.63,4.31;-1.94,5.28;0.00,5.63;1.94,5.28;3.63,4.31;4.88,2.81;5.53,0.97;5.53,-0.97;0.00,0.00|0,1;1,2;3,4;4,5;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,0;0,17;5,17;2,6;4,9;7,11;8,13;10,14;12,1;3,16",
			"1.50,0.47;0.00,1.56;-1.50,0.47;-0.91,-1.25;0.91,-1.25;3.97,3.97;2.03,5.25;-0.28,5.63;-2.56,5.00;-4.38,3.53;-5.44,1.47;-5.56,-0.88;-4.72,-3.06;-3.06,-4.72;-0.88,-5.56;1.47,-5.44;3.53,-4.38;5.00,-2.56;5.63,-0.28|0,1;1,2;2,3;3,4;4,0;5,6;6,7;7,8;8,9;9,10;10,11;11,12;12,13;13,14;14,15;15,16;16,17;17,18;5,15;6,11;7,1;8,18;9,14;10,2;12,17;13,3;16,4",
			"0.00,-6.88;-5.97,-3.44;-5.97,3.44;0.00,6.88;5.97,3.44;5.97,-3.44;0.00,-4.69;-4.06,-2.34;-4.06,2.34;0.00,4.69;4.06,2.34;4.06,-2.34;0.00,-2.81;-2.44,-1.41;-2.44,1.41;0.00,2.81;2.44,1.41;2.44,-1.41|0,1;1,2;2,3;3,4;4,5;5,0;0,6;1,7;2,8;3,9;4,10;5,11;12,15;13,16;14,17;6,13;13,8;8,15;15,10;10,17;17,6;11,12;12,7;7,14;14,9;9,16;16,11",
			"4.88,-4.88;0.00,-6.88;-4.88,-4.88;-6.88,0.00;-4.88,4.88;0.00,6.88;4.88,4.88;6.88,0.00;2.94,-4.41;-1.03,-5.22;-4.41,-2.94;-5.22,1.03;-2.94,4.41;1.03,5.22;4.41,2.94;5.22,-1.03;1.44,-3.47;-1.44,-3.47;-3.47,-1.44;-3.47,1.44;-1.44,3.47;1.44,3.47;3.47,1.44;3.47,-1.44|0,1;1,2;2,3;3,4;4,5;5,6;6,7;7,0;0,8;1,9;2,10;3,11;4,12;5,13;6,14;7,15;8,16;9,17;10,18;11,19;12,20;13,21;14,22;15,23;16,19;19,22;22,17;17,20;20,23;23,18;18,21;21,16",
			"-2.97,-6.56;2.97,-6.56;-2.03,-6.09;2.03,-6.09;-3.28,-5.63;3.28,-5.63;-0.78,-5.16;0.78,-5.16;0.00,-5.78;-0.78,-4.53;0.78,-4.53;-3.28,-3.91;3.28,-3.91;-1.88,-3.44;1.88,-3.44;-2.97,-2.97;2.97,-2.97;-2.50,-2.19;2.50,-2.19;-2.97,-1.25;2.97,-1.25;-4.56,6.50;-7.53,1.38;-4.63,5.47;-6.66,1.94;-3.63,6.31;-6.91,0.63;-4.47,3.91;-5.25,2.56;-5.38,3.53;-3.91,3.59;-4.69,2.25;-2.13,5.44;-5.41,-0.22;-2.41,4.00;-4.28,0.75;-1.47,4.72;-4.44,-0.44;-1.03,3.91;-3.53,-0.41;0.03,3.84;7.53,1.38;4.56,6.50;6.66,1.94;4.63,5.47;6.91,0.63;3.63,6.31;5.25,2.56;4.47,3.91;5.38,3.56;4.69,2.25;3.91,3.59;5.41,-0.22;2.13,5.44;4.28,0.75;2.41,4.00;4.44,-0.44;1.47,4.72;3.53,-0.41|0,1;0,2;0,4;1,3;1,5;2,6;2,13;3,7;3,14;4,9;4,11;5,10;5,12;6,8;6,11;7,8;7,12;8,9;8,10;9,13;10,14;11,15;12,16;13,15;14,16;15,17;16,18;17,19;18,20;15,16;17,18;19,20;21,22;21,23;21,25;22,24;22,26;23,27;23,34;24,28;24,35;25,30;25,32;26,31;26,33;27,29;27,32;28,29;28,33;29,30;29,31;30,34;31,35;32,36;33,37;34,36;35,37;36,38;37,39;38,40;39,19;36,37;38,39;19,40;41,42;41,43;41,45;42,44;42,46;43,47;43,54;44,48;44,55;45,50;45,52;46,51;46,53;47,49;47,52;48,49;48,53;49,50;49,51;50,54;51,55;52,56;53,57;54,56;55,57;56,58;56,57;58,20;20,40;21,42;22,0;1,41",
			"6.75,-1.34;6.34,-2.63;5.72,-3.81;4.88,-4.88;3.81,-5.72;2.63,-6.34;1.34,-6.75;0.00,-6.88;-1.34,-6.75;-2.63,-6.34;-3.81,-5.72;-4.88,-4.88;-5.72,-3.81;-6.34,-2.63;-6.75,-1.34;-6.88,0.00;-6.75,1.34;-6.34,2.63;-5.72,3.81;-4.88,4.88;-3.81,5.72;-2.63,6.34;-1.34,6.75;0.00,6.88;1.34,6.75;2.63,6.34;3.81,5.72;4.88,4.88;5.72,3.81;6.34,2.63;6.75,1.34;6.88,0.00|2,0;4,3;5,2;6,1;7,0;8,6;9,5;10,4;11,3;12,2;12,11;13,1;13,10;14,0;14,9;15,8;16,7;17,6;18,5;18,16;19,4;19,15;20,3;20,14;21,2;21,13;22,1;22,12;23,0;23,11;24,10;24,23;25,9;25,22;26,8;26,21;27,7;27,20;28,6;28,19;29,5;29,18;30,4;30,17;31,3;31,16",
		};
		 
		string currentLevel = levels [n];
		string[] nodesString = currentLevel.Split ('|') [0].Split (';');
		string[] edgesString = currentLevel.Split ('|') [1].Split (';');
		Vector2[] nodes = new Vector2[nodesString.Length];
		V[] edges = new V[edgesString.Length];

		int i;
		for (i = 0; i < nodes.Length; i++) {
			String[] nodeString = nodesString [i].Split (',');
			Vector2 originalCoordinates = new Vector2 (float.Parse (nodeString [0]), float.Parse (nodeString [1]));
			nodes [i] = translateCoordinates (defaultViewport, currentViewport, originalCoordinates);
		}
			
		for (i = 0; i < edges.Length; i++) {
			String[] edgeString = edgesString [i].Split (',');
			edges [i] = new V (int.Parse (edgeString [0]), int.Parse (edgeString [1]));
		}

		return new LevelData (n.ToString (), nodes, edges);
	}

	static Vector2 translateCoordinates (Rect original, Rect target, Vector2 point)
	{
		Vector2 translated = new Vector2 ();

		translated.x = (point.x - original.center.x) / original.width * target.width;
		translated.y = (point.y - original.center.y) / original.height * target.height;

		return translated;
	}
}