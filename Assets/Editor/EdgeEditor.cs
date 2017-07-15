using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Edge))]
public class EdgeEditor : Editor {
    public override void OnInspectorGUI()
    {
        Transform transform = (target as Edge).gameObject.transform;

        Vector3 position = transform.position;
        transform.position = position;

    }

    void OnSceneGUI()
    {
        Edge edge = target as Edge;
        edge.FromEditor();
    }
}
