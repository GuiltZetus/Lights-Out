using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a scriptable object for defining room node types
[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName; // Name of the room node type

    #region Header
    [Header("Only flag the RoomNodeTypes that should be visible in the editor")]
    #endregion Header
    public bool displayInNodeGraphEditor = true; // Flag to determine if the room node type should be visible in the editor
    
    #region Header
    [Header("One Type Should Be A Corridor")]
    #endregion Header
    public bool isCorridor; // Flag indicating if the room node type is a corridor
    
    #region Header
    [Header("One Type Should Be A CorridorNS ")]
    #endregion Header
    public bool isCorridorNS; // Flag indicating if the room node type is a north-south corridor
    
    #region Header
    [Header("One Type Should Be A CorridorEW")]
    #endregion Header
    public bool isCorridorEW; // Flag indicating if the room node type is an east-west corridor
    
    #region Header
    [Header("One Type Should Be An Entrance")]
    #endregion Header
    public bool isEntrance; // Flag indicating if the room node type is an entrance
    
    #region Header
    [Header("One Type Should Be A Boss Room")]
    #endregion Header
    public bool isBossRoom; // Flag indicating if the room node type is a boss room
    
    #region Header
    [Header("One Type Should Be None (Unassigned)")]
    #endregion Header
    public bool isNone; // Flag indicating if the room node type is unassigned
    
    #region Validation
    #if UNITY_EDITOR
    // This method is called when the scriptable object is validated in the editor
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
        // Call a helper method to validate the roomNodeTypeName property and check if it is an empty string
    }
    #endif
    #endregion
}
