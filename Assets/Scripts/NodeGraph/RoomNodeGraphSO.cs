using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a scriptable object for defining the room node graph
[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList; // Reference to the list of room node types
    [HideInInspector] public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>(); // List of room nodes in the graph
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>(); // Dictionary to quickly access room nodes by ID

    private void Awake()
    {
        LoadRoomNodeDictionary(); // Call the method to load the room node dictionary
    }

    /// <summary>
    /// Load the room node dictionary from the room node list.
    /// </summary>
    private void LoadRoomNodeDictionary()
    {
        roomNodeDictionary.Clear(); // Clear the existing dictionary

        // Populate dictionary
        foreach (RoomNodeSO node in roomNodeList)
        {
            roomNodeDictionary[node.id] = node; // Add each room node to the dictionary using its ID as the key
        }
    }

    /// <summary>
    /// Get room node by roomNodeType
    /// </summary>
    public RoomNodeSO GetRoomNode(RoomNodeTypeSO roomNodeType)
    {
        foreach (RoomNodeSO node in roomNodeList)
        {
            if (node.roomNodeType == roomNodeType) // Find the first room node with a matching room node type
            {
                return node;
            }
        }
        return null; // Return null if no matching room node is found
    }

    /// <summary>
    /// Get room node by room node ID
    /// </summary>
    public RoomNodeSO GetRoomNode(string roomNodeID)
    {
        if (roomNodeDictionary.TryGetValue(roomNodeID, out RoomNodeSO roomNode))
        {
            return roomNode; // Return the room node with the specified ID if it exists in the dictionary
        }
        return null; // Return null if no room node with the specified ID is found
    }

    /// <summary>
    /// Get child room nodes for supplied parent room node
    /// </summary>
    public IEnumerable<RoomNodeSO> GetChildRoomNodes(RoomNodeSO parentRoomNode)
    {
        foreach (string childNodeID in parentRoomNode.childRoomNodeIDList)
        {
            yield return GetRoomNode(childNodeID); // Return each child room node by looking it up in the dictionary
        }
    }

    #region Editor Code

    // The following code should only run in the Unity Editor
#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null; // The room node from which a connection line should be drawn
    [HideInInspector] public Vector2 linePosition; // The position where the connection line should be drawn

    // Repopulate the node dictionary every time a change is made in the editor
    public void OnValidate()
    {
        LoadRoomNodeDictionary();
    }

    // Set the room node and line position for drawing connection lines in the editor
    public void SetNodeToDrawConnectionLineFrom(RoomNodeSO node, Vector2 position)
    {
        roomNodeToDrawLineFrom = node;
        linePosition = position;
    }

#endif

    #endregion Editor Code
}
