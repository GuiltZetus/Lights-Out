using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle _roomNodeStyle;
    private const float NodeWidth = 160f;
    private const float NodeHeight = 75f;
    private const int NodePadding = 25;
    private const int NodeBorder = 12;
    
    
    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room node graph editor")]
    private static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room node graph editor");
    }

    private void OnEnable()
    {
        // Define node layout style
        _roomNodeStyle = new GUIStyle();
        _roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        _roomNodeStyle.normal.textColor = Color.white;
        _roomNodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
        _roomNodeStyle.border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder);
        
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(new Vector2(100f, 100f), new Vector2(NodeWidth, NodeHeight)), _roomNodeStyle);
        EditorGUILayout.LabelField("Node1");
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(new Vector2(500f, 100f), new Vector2(NodeWidth, NodeHeight)), _roomNodeStyle);
        EditorGUILayout.LabelField("Node2");
        GUILayout.EndArea();
    }
}
