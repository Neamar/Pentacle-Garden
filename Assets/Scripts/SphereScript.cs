using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

	void OnMouseDown() {
		GameManager.instance.currentLevel.SelectNode (GetComponentInParent<Node> () as Node);
	}
}
