using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGrid : MonoBehaviour
{
    InventoryController inventoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
        inventoryController.SelectedItemGrid = itemGrid;    
    }
}
