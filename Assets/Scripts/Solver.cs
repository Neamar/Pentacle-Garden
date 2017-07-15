using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver
{
	Level level;

	public Solver (Level level)
	{
		this.level = level;

        Node start = level.nodes[0];
        HashSet<Node> visitedNodes = new HashSet<Node>();
        visitedNodes.Add(start);
        List<Node> solution = FindPath(start, start, visitedNodes);

        foreach(Node n in solution)
        {
            Debug.Log(n.gameObject.name);
        }
    }

	public List<Node> FindPath (Node startingNode, Node currentNode, HashSet<Node> visitedNodes)
	{
		foreach (Node n in currentNode.adjacent) {
            // For each node non visited yet
			if(!visitedNodes.Contains(n))
            {
                visitedNodes.Add(n);
                List<Node> solution = FindPath(startingNode, n, visitedNodes);

                if(solution != null)
                {
                    solution.Add(currentNode);
                    return solution;
                }

                visitedNodes.Remove(n);
            }
            else if(n == startingNode && visitedNodes.Count == level.nodes.Count)
            {
                // We have a solution!
                List<Node> solution = new List<Node>();
                solution.Add(startingNode);
                solution.Add(currentNode);
                return solution;
            }
		}

        return null;
	}
}
